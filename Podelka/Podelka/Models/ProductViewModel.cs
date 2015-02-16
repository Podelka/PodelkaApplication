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

        public ProductPreviewModel(long productId, string name, double? price, double? priceDiscount)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            PriceDiscount = priceDiscount;
        }

        public long ProductId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public double? PriceDiscount { get; set; }
    }

    public class ProductSmallPreviewModel
    {
        public ProductSmallPreviewModel()
        {
        }

        public ProductSmallPreviewModel(long productId, string name)
        {
            ProductId = productId;
            Name = name;
        }

        public long ProductId { get; set; }
        public string Name { get; set; }
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

        public string RegisterTypeWorkroom { get; set; }
        public string SectionName { get; set; }
        public bool SectionGender { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите {0}")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите {0}")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите {0}")]
        [Display(Name = "Ключевые слова")]
        public string KeyWords { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите Цену")]
        [Display(Name = "Цена")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите Высоту")]
        [Display(Name = "Высота")]
        public string Size1 { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите Ширину")]
        [Display(Name = "Ширина")]
        public string Size2 { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите Глубину")]
        [Display(Name = "Глубина")]
        public string Size3 { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите Массу")]
        [Display(Name = "Масса")]
        public string Weight { get; set; }

        [Required(ErrorMessage = "Пожалуйста, выберите Категорию")]
        [Display(Name = "Категория")]
        public short SelectedCategory { get; set; }
        public ICollection<CategoriesDbModel> Categories { get; set; }

        [Required(ErrorMessage = "Пожалуйста, выберите {0}")]
        [Display(Name = "Состояние готовности")]
        public byte SelectedStatusReady { get; set; }
        public ICollection<StatusReadyProductDbModel> StatusReady { get; set; }

        //[Required(ErrorMessage = "Пожалуйста, выберите {0}")]  Т.к. половой признак добавляется не к каждому товару, то Required нельзя применять к SelectedGenderType
        //[Display(Name = "Половой признак")]
        public byte? SelectedGenderType { get; set; }
        public ICollection<GenderTypeDbModel> GenderTypes { get; set; }

        [Required(ErrorMessage = "Пожалуйста, выберите {0}")]
        [Display(Name = "Материалы")]
        public int[] SelectedMaterials { get; set; }
        public ICollection<MaterialsDbModel> Materials { get; set; }

        [MustBeTrue(ErrorMessage = "Вы обязаны согласиться с правилами, чтобы добавить изделие")]
        [Display(Name = "Я согласен с правилами сайта")]
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