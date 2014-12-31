using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class WorkroomDeliveryMethod
    {
        [Key, Column(Order = 1)]
        public long WorkroomId { get; set; }
        [Key, Column(Order = 2)]
        public byte DeliveryMethodId { get; set; }
        public string Description { get; set; }

        public virtual DeliveryMethod DeliveryMethod { get; set; }
        public virtual Workroom Workroom { get; set; }
    }
}