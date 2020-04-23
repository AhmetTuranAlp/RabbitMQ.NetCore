using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCore_RabbitMQ_Demo.RabbitMQ
{
    public class RabbitMQSender
    {
        private readonly RabbitMQService _rabbitMQService;
        public RabbitMQSender(string queueName, int id)
        {
            _rabbitMQService = new RabbitMQService();
            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, false, false, false, null);
                    channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(id.ToString()));
                    Console.WriteLine("{0} queue'si üzerine, \"{1}\" kayıt id bilgisi yazıldı.", queueName, id.ToString());
                }
            }
        }
    }
}
