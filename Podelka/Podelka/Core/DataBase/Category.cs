using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class Category
    {
        public short CategoryId { get; set; }
        public byte SectionId { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }

        public virtual Section Section { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}