﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Xml.Linq;

namespace Account_Menagment_System.Server.models.database.Account
{
    public class Account : IIdentification
    {
        public Guid ID { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        [NotMapped]
        public bool IsActive => Active && ExpirationDate > DateTime.UtcNow;
       
        public bool Active { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ExpirationDate { get; set; }

        public static implicit operator AccountDTO(Account model)
        {
            return new AccountDTO()
            {
                ID = model.ID,
                Login = model.Login,
                IsActive = model.IsActive
            };
        }
    }
}
