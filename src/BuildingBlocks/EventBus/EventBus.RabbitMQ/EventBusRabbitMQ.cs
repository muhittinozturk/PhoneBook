using EventBus.Abstractions;
using EventBus.Event;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : BaseEventBus
    {
        private readonly IRabbitMQConnection _persistentConnection;
        private readonly IConnectionFactory _connectionFactory;
        private readonly IModel consumerChannel;
        public EventBusRabbitMQ(IServiceProvider serviceProvider, Configuration configuration) : base(serviceProvider, configuration)
        {
            _connectionFactory = (ConnectionFactory)configuration.Connection;
            _persistentConnection = new RabbitMQConnection(_connectionFactory);
            consumerChannel = CreateConsumerChannel();

            _subscriptionsManager.OnEventRemoved += _subscriptionsManager_OnEventRemoved;
        }

        private void _subscriptionsManager_OnEventRemoved(object? sender, string eventName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            consumerChannel.QueueUnbind(queue: eventName,
                exchange: _configuration.TopicName,
                routingKey: eventName);

            if (_subscriptionsManager.IsEmpty)
            {
                consumerChannel.Close();
            }
        }

        public override void Publish(IntegrationEvent @event)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            var eventName = @event.GetType().Name;


            consumerChannel.ExchangeDeclare(exchange: _configuration.TopicName, type: "direct");

            var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType());

            var properties = consumerChannel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            consumerChannel.BasicPublish(
                    exchange: _configuration.TopicName,
                    routingKey: eventName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
        }

        public override void Subscribe<T, TH>()
        {
            var eventName = _subscriptionsManager.GetEventKey<T>();

            if (!_subscriptionsManager.HasSubscriptionsForEvent(eventName))
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                consumerChannel.QueueDeclare(GetSubName(eventName), true, false, false, null);
                consumerChannel.QueueBind(GetSubName(eventName), _configuration.TopicName, eventName);
            }
            _subscriptionsManager.AddSubscription<T, TH>();
            StartBasicConsume(eventName);
        }

        public override void Unsubscribe<T, TH>()
        {
            _subscriptionsManager.RemoveSubscription<T, TH>();
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: _configuration.TopicName,
                                    type: "direct");

            return channel;
        }

        private void StartBasicConsume(string eventName)
        {
            if (consumerChannel != null)
            {
                var consumer = new EventingBasicConsumer(consumerChannel);

                consumer.Received += Consumer_Received;

                consumerChannel.BasicConsume(GetSubName(eventName), false, consumer);

            }
        }

        private async void Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
            }

            consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }
    }
}
