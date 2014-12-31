using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {

        }

        public CategoryModel(string categoryName)
        {
            CategoryName = categoryName;
        }

        public string CategoryName { get; set; }
    }

    public class ProductProfileModel
    {
        public ProductProfileModel()
        {

        }

        public ProductProfileModel(long productId, long workroomId, string name, string description, float price, string statusReady, string material, string size, string weight, WorkroomProfileModel workroom)
        {
            ProductId = productId;
            WorkroomId = workroomId;
            Name = name;
            Description = description;
            Price = price;
            StatusReady = statusReady;
            Material = material;
            Size = size;
            Weight = weight;
            Workroom = workroom;
        }

        public long ProductId { get; set; }
        public long WorkroomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string StatusReady { get; set; }
        public string Material { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public WorkroomProfileModel Workroom { get; set; }
    }

    public class ProductPreviewModel
    {
        public ProductPreviewModel()
        {

        }

        public ProductPreviewModel(long productId, string name, string description, float price)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Price = price;
        }

        public long ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}