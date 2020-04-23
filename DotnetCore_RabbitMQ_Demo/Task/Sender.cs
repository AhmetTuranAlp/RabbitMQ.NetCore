using DotnetCore_RabbitMQ_Demo.Model;
using DotnetCore_RabbitMQ_Demo.Model.Work;
using DotnetCore_RabbitMQ_Demo.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace DotnetCore_RabbitMQ_Demo.Task
{
    public class Sender : IWork
    {
        private static string _queueName = "SenderMail";
        private static RabbitMQSender _sender;

        private Timer timer1 = new Timer();
        public void Work()
        {
            timer1.Interval = 60000;
            timer1.Elapsed += new ElapsedEventHandler(timer1_Elapsed);
            timer1.Enabled = true;
            timer1.Start();
        }
        private void timer1_Elapsed(object sender, EventArgs e)
        {
            UserBirthdateMail userBirthdateMail = new UserBirthdateMail();
            List<UserBirthdateMail> userBirthdateMails = userBirthdateMail.UserBirthdateMailList();
            if (userBirthdateMails != null && userBirthdateMails.Count > 0)
            {
                userBirthdateMails.ForEach(x =>
                {
                    string dateNow = DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();
                    if (x.Birthdate == dateNow)
                    {
                        _sender = new RabbitMQSender(_queueName, x.Id);
                    }
                });
            }

        }
    }
}
