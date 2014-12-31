using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class Workroom
    {
        public long WorkroomId { get; set; }
        public long UserId { get; set; }
        public byte RegisterTypeWorkroomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short CountGood { get; set; }
        public short CountMedium { get; set; }
        public short CountBad { get; set; }
        public DateTime DateRegistration { get; set; }
       
        public virtual ApplicationUser User { get; set; }
        public virtual RegisterTypeWorkroom RegisterTypeWorkroom { get; set; }
        public virtual ICollection<WorkroomDeliveryMethod> WorkroomDeliveryMethods { get; set; }
        public virtual ICollection<WorkroomPayMethod> WorkroomPayMethods { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}