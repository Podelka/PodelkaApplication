using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class StatusReadyProduct
    {
        [Key]
        public byte ProductStatusReadyId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}