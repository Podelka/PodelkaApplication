using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class Region
    {
        public byte RegionId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<District> Disctricts { get; set; }
    }
}