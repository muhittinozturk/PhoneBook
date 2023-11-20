using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        public bool Disposed;
        private readonly ILogger<RabbitMQConnection> _logger;
        private readonly int _retryCount = 15;
        public RabbitMQConnection(IConnectionFactory connectionFactory, ILogger<RabbitMQConnection> logger)
        {

            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _logger = logger;
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !Disposed;

        public bool TryConnect()
        {
            var policy = RetryPolicy.Handle<Exception>().Or<BrokerUnreachableException>()
               .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
               {
                   _logger.LogWarning($"RabbitMQ Client could not connect after {time.TotalSeconds:n1}s (Retry {_retryCount})", ex);

               }
           );

            policy.Execute(() =>
            {
                try
                {
                    _connection = _connectionFactory.CreateConnection();
                }
                catch (Exception ex)
                {
                    _logger.LogError("RabbitMQ Client connection attempt failed", ex);
                }
            });

            return IsConnected;
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("RabbitMQ bağlantısı yok!");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (Disposed) return;

            Disposed = true;

            _connection.Dispose();

        }
    }
}
