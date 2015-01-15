using Podelka.AttributeValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Podelka.Models
{
    public class ProductProfileModel
    {
        public ProductProfileModel()
        {
        }

        public ProductProfileModel(long productId, long workroomId, byte sectionId, string sectionName, short categoryId, string categoryName, string name, string description, string keyWords, double? price, double? priceDiscount, string statusReady, string gender, string materials, string size, string weight, DateTime dateCreate, WorkroomProfileModel workroom)
        {
            ProductId = productId;
            WorkroomId = workroomId;        
            SectionId = sectionId;
            SectionName = sectionName;
            CategoryId = categoryId;
            CategoryName = categoryName;
            Name = name;
            Description = description;
            KeyWords = keyWords;
            Price = price;
            PriceDiscount = priceDiscount;
            StatusReady = statusReady;
            Gender = gender;
            Materials = materials;
            Size = size;
            Weight = weight;
            DateCreate = dateCreate;
            Workroom = workroom;
        }

        public long ProductId { get; set; }
        public long WorkroomId { get; set; }
        public byte SectionId { get; set; }
        public short CategoryId { get; set; }
        public string SectionName { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeyWords { get; set; }
        public double? Price { get; set; }
        public double? PriceDiscount { get; set; }
        public string StatusReady { get; set; }
        public string Gender { get; set; }
        public string Materials { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public DateTime DateCreate { get; set; }
        public WorkroomProfileModel Workroom { get; set; }
    }

    public class ProductPreviewModel
    {
        public ProductPreviewModel()
        {
        }

        public ProductPreviewModel(long productId, string name, string description, double? price, double? priceDiscount)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Price = price;
            PriceDiscount = priceDiscount;
        }

        public long ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public double? PriceDiscount { get; set; }
    }

    public class ProductCreateModel
    {
        public ProductCreateModel()
        {
        }
        
        public ProductCreateModel(string registerTypeWorkroom, string sectionName, bool sectionGender, ICollection<CategoriesDbModel> categories, ICollection<StatusReadyProductDbModel> statusReady, ICollection<GenderTypeDbModel> genderTypes, ICollection<MaterialsDbModel> materials)
        {
            RegisterTypeWorkroom = registerTypeWorkroom;
            SectionName = sectionName;
            SectionGender = sectionGender;
            Categories = categories;
            StatusReady = statusReady;
            GenderTypes = genderTypes;
            Materials = materials;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string KeyWords { get; set; }
        public double? Price { get; set; }
        public string Size1 { get; set; }
        public string Size2 { get; set; }
        public string Size3 { get; set; }
        public string Weight { get; set; }
        public string RegisterTypeWorkroom { get; set; }
        public string SectionName { get; set; }
        public bool SectionGender { get; set; }
        public short SelectedCategory { get; set; }
        public ICollection<CategoriesDbModel> Categories { get; set; }
        public byte SelectedStatusReady { get; set; }
        public ICollection<StatusReadyProductDbModel> StatusReady { get; set; }
        public byte? SelectedGenderType { get; set; }
        public ICollection<GenderTypeDbModel> GenderTypes { get; set; }
        public int[] SelectedMaterials { get; set; }
        public ICollection<MaterialsDbModel> Materials { get; set; }

        [MustBeTrue(ErrorMessage = "Вы обязаны согласиться с правилами, чтобы добавить товар")]
        [Display(Name = "Я согласен с этими правилами")]
        public bool AgreeRules { get; set; }
    }

    public class ProductMaterialsModel
    {
        public ProductMaterialsModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}