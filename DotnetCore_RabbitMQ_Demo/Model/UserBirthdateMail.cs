using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCore_RabbitMQ_Demo.Model
{
    public class UserBirthdateMail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthdate { get; set; }
        public string Email { get; set; }
        public List<UserBirthdateMail> UserBirthdateMailList()
        {
            List<UserBirthdateMail> list = new List<UserBirthdateMail>();

            list.Add(new UserBirthdateMail() { Id = 1, Name = "name1", Surname = "surname1", Birthdate = "17.3.1991", Email = "ahmet.alp@dreamsandbytes.com" });
            list.Add(new UserBirthdateMail() { Id = 2, Name = "name2", Surname = "surname2", Birthdate = "23.4.2020", Email = "ahmet.alp@dreamsandbytes.com" });
            list.Add(new UserBirthdateMail() { Id = 3, Name = "name3", Surname = "surname3", Birthdate = "17.3.1991", Email = "ahmet.alp@dreamsandbytes.com" });
            list.Add(new UserBirthdateMail() { Id = 4, Name = "name4", Surname = "surname4", Birthdate = "17.3.2001", Email = "ahmet.alp@dreamsandbytes.com" });
            return list;
        }
    }
}
