using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class Material
    {

        public short MaterialId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
    }
}