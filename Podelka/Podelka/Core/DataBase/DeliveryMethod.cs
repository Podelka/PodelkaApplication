using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class DeliveryMethod
    {
        public byte DeliveryMethodId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<WorkroomDeliveryMethod> WorkroomDeliveryMethods { get; set; }
    }
}