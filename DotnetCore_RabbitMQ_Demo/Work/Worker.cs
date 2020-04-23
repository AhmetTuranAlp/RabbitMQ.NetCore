using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCore_RabbitMQ_Demo.Model.Work
{
    public class Worker
    {
        public IWork task;

        public Worker(IWork task)
        {
            this.task = task;
        }

        public void Work()
        {
            task.Work();
        }
    }
}
