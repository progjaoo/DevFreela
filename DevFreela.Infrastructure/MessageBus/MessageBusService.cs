using DevFreela.Coree.Interfaces;
using RabbitMQ.Client;

namespace DevFreela.Infrastructure.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _factory;
        public MessageBusService()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        }
        public void Publish(string queue, byte[] message)
        {
            using (var connection = _factory.CreateConnection()) //conexao estabelecida com RabbitMQ
            {
                using (var channel = connection.CreateModel()) // aqui começa a comunicação através de filas
                {
                    //Garatir que a fila esteja criada
                    channel.QueueDeclare(
                        queue: queue,
                        durable: false, //fila duravel 
                        exclusive: false, //permite so 1 conexao dps deleta fila
                        autoDelete: false, //permite varias, dps de todas terminar deleta fila
                        arguments: null);
                    //Publicar a mensagem
                    channel.BasicPublish( 
                        exchange: "", //roteia as mensagens
                        routingKey: queue, //aqui passa pro exchange qual fila redirecionar
                        basicProperties: null,
                        body: message); //recebe o array de bytes do parametro
                }
            }
        }
    }
}
