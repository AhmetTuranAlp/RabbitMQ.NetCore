using DotnetCore_RabbitMQ_Demo.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCore_RabbitMQ_Demo.RabbitMQ
{
    public class RabbitMQSender
    {
        private readonly RabbitMQService _rabbitMQService;
        public RabbitMQSender(string queueName, UserBirthdateMail user)
        {
            _rabbitMQService = new RabbitMQService();
            //RabbitMQ H
            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                //CreateModel methodu ile RabbitMQ üzerinde yeni bir channel yaratılır. İşte bu açılan channel yani session ile yeni bir queue oluşturulup istenen mesaj bu channel üzerinden gönderilmektedir.
                using (var channel = connection.CreateModel())
                {
                    #region QueueDeclare
                    //QueueDeclare methodu ile oluşturulacak olan queue‘nin ismi tanımlanır.
                    //Durable ile in-memory mi yoksa fiziksel olarak mı saklanacağı belirlenir. Genel de RabbitMQ’da hız amcı ile ilgili queuelerin memory’de saklanması tercih edilse de sunucunun restart olması durumunda ilgili mesajların kaybolmasından dolayı da, hızdan ödün verilerek fiziksel olarak bir hard diskte saklanması tercih edilebilir.
                    //Exclusive parametresi ile diğer connectionlar ile kullanılması izni belirlenir.
                    //Eğer queue deleted olarak işaretlenmiş ise ve tüm consumerlar bunu kullanmayı bitirmiş ise ya da son consumer iptal edilmiş veya channel kapanmış ise silinmez. İşte bu gibi durumlarda "autoDelete" ile  delete method’u normal olarak çalıştırılır. Ve ilgili queueler silinir.  
                    #endregion
                    channel.QueueDeclare(queueName, false, false, false, null);
                    #region BasicPublish
                    //BasicPublish methodu "exchange" aslında mesajın alınıp bir veya daha fazla queue’ya konmasını sağlar. Bu route algoritması exchange tipine ve bindinglere göre farklılık gösterir. "Direct, Fanout ,Topic ve Headers" tiplerinde exchangeler mevcuttur.
                    //Direct exchange: Yapılacak işlere göre bir routing key belirlenir ve buna göre ilgili direct exchange ile amaca en uygun queue gidilir.
                    //Fanout exchange: Burada routing key’in bir önemi yoktur. Daha çok broadcast yayınlar için uygundur. Özellikle (MMO) oyunlarda top10 güncellemeleri ve global duyurular için kullanılır. Yine real-time spor haberleri gibi yayınlarda fanout exchange kullanılır.
                    //Topic Exchange: Bir route mesajın bir veya daha çok queue’ye gitmesi amacı ile kullanılır. Publish/Subscribe pattern’in bir varyasyonudur. Eğer ilgili sorun birkaç consumer’i alakadar ediyor ise, hangi çeşit mesajı almak istediklerini belirlemek için Topic Exchange kullanılmalıdır.
                    //Headers Exchange: Yine bu exchange de routing key’i kullanmaz ve message headers’daki birkaç özellik ve tanımlama ile doğru queue’ye iletim yapar. Header üzerindeki attributeler ile  queue üzerindeki attributelerin, tamamının değerlerinin birbirini tutması gerekmektedir. 
                    //BasicPublish methoddaki routingKey : Girilen key'e göre ilgili queue'ye gidilmesi sağlanır. "body:" Queue’ye gönderilecek mesaj byte[] tipinde gönderilir. Mesaj denince aklınıza sadece Text gelmesin. Her türlü object'i gönderebiliriz.
                    #endregion
                    string message = JsonConvert.SerializeObject(user);
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "",routingKey: queueName,basicProperties: null,body: body);
                    Console.WriteLine("{0} queue'si üzerine, ilgili istek yazıldı.", queueName);
                }
            }
        }
    }
}
