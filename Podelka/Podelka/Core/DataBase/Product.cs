using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class Product
    {
        public long ProductId { get; set; }
        public long WorkroomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string StatusReady { get; set; }
        public string Material {get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }

        public virtual Workroom Workroom { get; set; }
    }
}