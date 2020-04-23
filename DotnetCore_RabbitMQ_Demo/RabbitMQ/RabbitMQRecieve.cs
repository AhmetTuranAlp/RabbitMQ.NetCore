using DotnetCore_RabbitMQ_Demo.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace DotnetCore_RabbitMQ_Demo.RabbitMQ
{
    public class RabbitMQRecieve
    {
        private readonly RabbitMQService _rabbitMQService;

        public RabbitMQRecieve(string queueName)
        {
            _rabbitMQService = new RabbitMQService();
            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    // Received event'i sürekli listen modunda olacaktır.
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        UserBirthdateMail user = JsonConvert.DeserializeObject<UserBirthdateMail>(message);
                        GetReciver(queueName, user);
                    };
                    channel.BasicConsume(queueName, true, consumer);
                    Console.ReadLine();
                }
            }
        }

        public void GetReciver(string queueName, UserBirthdateMail user)
        {
            Console.WriteLine("{0} isimli queue üzerinden gelen user bilgisi: \"{1}\"", queueName, user.Name + " " + user.Surname);
        }

    }
}
