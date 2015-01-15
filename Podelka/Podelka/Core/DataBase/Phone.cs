using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class Phone
    {
        public long PhoneId { get; set; }
        public long UserId { get; set; }
        public byte PhoneTypeId { get; set; }
        public string Number { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual TypePhone TypePhone { get; set; }
    }
}