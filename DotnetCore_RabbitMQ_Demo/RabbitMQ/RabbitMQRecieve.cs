using DotnetCore_RabbitMQ_Demo.Model;
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

        public RabbitMQRecieve(string queueName) // RabbitMQ'dan id alır.
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
                        int Id = Convert.ToInt32(Encoding.UTF8.GetString(body));
                       // Console.WriteLine("{0} isimli queue üzerinden gelen kayıt Id bilgisi: \"{1}\"", queueName, Id);
                        GetReciver(Id);
                    };
                    channel.BasicConsume(queueName, true, consumer);
                    Console.ReadLine();
                }
            }
        }

        public void GetReciver(int Id)
        {
            UserBirthdateMail userBirthdateMail = new UserBirthdateMail();
            List<UserBirthdateMail> userBirthdateMails = userBirthdateMail.UserBirthdateMailList();
            if (userBirthdateMails != null && userBirthdateMails.Count > 0)
            {
                userBirthdateMails.ForEach(x =>
                {
                    if (x.Id == Id)
                    {
                        Console.WriteLine("Doğum günün kutlu olsun " + x.Name + " " + x.Surname);
                    }
                });
            }

        }

    }
}
