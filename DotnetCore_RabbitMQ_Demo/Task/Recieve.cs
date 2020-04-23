using DotnetCore_RabbitMQ_Demo.Model.Work;
using DotnetCore_RabbitMQ_Demo.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCore_RabbitMQ_Demo.Task
{
    public class Recieve : IWork
    {
        private static string _queueName = "SenderMail";
        private static RabbitMQRecieve _reciver;
        public void Work()
        {
            _reciver = new RabbitMQRecieve(_queueName);
        }
    }
}
