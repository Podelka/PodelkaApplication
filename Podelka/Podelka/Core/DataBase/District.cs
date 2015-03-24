using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class District
    {
        public byte DistrictId { get; set; }
        public byte RegionId { get; set; }
        public string Name { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}