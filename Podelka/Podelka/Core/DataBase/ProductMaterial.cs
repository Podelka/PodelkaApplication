using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class ProductMaterial
    {
        [Key, Column(Order = 1)]
        public long ProductId { get; set; }
        [Key, Column(Order = 2)]
        public short MaterialId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Material Material { get; set; }
    }
}