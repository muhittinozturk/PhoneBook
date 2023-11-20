using RabbitMQ.Client;
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
        public RabbitMQConnection(IConnectionFactory connectionFactory)
        {
            
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !Disposed;

        public bool TryConnect()
        {
            try
            {
                _connection = _connectionFactory.CreateConnection();
            }
            catch (Exception ex)
            {
                return false;
            }

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
