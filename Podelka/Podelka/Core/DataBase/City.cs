using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class City
    {
        public short CityId { get; set; }
        public byte DistrictId { get; set; }
        public string Name { get; set; }

        public virtual District District { get; set; }
    }
}