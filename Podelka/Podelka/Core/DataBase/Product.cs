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
        public short CategoryId { get; set; }
        public byte? ProductGenderTypeId { get; set; }
        public byte ProductStatusReadyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeyWords { get; set; }
        public double? Price { get; set; }
        public double? PriceDiscount { get; set; }
        public int CountGood { get; set; }
        public int CountMedium { get; set; }
        public int CountBad { get; set; }
        public double ResultRating { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public DateTime DateCreate { get; set; }

        public virtual Workroom Workroom { get; set; }
        public virtual Category Category { get; set; }
        public virtual GenderTypeProduct GenderTypeProduct { get; set; }
        public virtual StatusReadyProduct ProductStatusReady { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
    }
}