using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class PayMethod
    {
        public byte PayMethodId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<WorkroomPayMethod> WorkroomPayMethods { get; set; }
    }
}