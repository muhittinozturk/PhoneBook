using EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace EventBus.Event
{
    public abstract class BaseEventBus : IEventBus
    {
        public readonly IServiceProvider _serviceProvider;
        public readonly ISubscriptionsManager _subscriptionsManager;
        public Configuration _configuration;
        public BaseEventBus(IServiceProvider serviceProvider, Configuration configuration)
        {

            _subscriptionsManager = new SubscriptionsManager();
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }


        public async Task<bool> ProcessEvent(string eventName, string message)
        {
            if (_subscriptionsManager.HasSubscriptionsForEvent(eventName))
            {
                await using var scope = _serviceProvider.CreateAsyncScope();
                var subscriptions = _subscriptionsManager.GetHandlersForEvent(eventName);

                foreach (var subscription in subscriptions)
                {
                    var handler = scope.ServiceProvider.GetService(subscription.HandlerType);
                    if (handler == null) continue;
                    var eventType = _subscriptionsManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonSerializer.Deserialize(message, eventType);

                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });

                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual string GetSubName(string eventName)
        {
            return $"{_configuration.SubClientAppName}.{eventName}";
        }

        public abstract void Publish(IntegrationEvent @event);
        public abstract void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        public abstract void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
    }
}
