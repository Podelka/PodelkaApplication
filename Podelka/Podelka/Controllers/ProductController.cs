using Podelka.Core.DataBase;
using Podelka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Podelka.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Profile(long? id)
        {
            if (id != null)
            {
                var product = new Product();
                var genderTypeProduct = new GenderTypeProduct();

                using (var db = new Context())
                {
                    product = db.Products.Find(id);
                    if (product != null)
                    {
                        db.Entry(product).Reference(p => p.Category).Load();
                        db.Entry(product.Category).Reference(p => p.Section).Load();
                        db.Entry(product).Reference(w => w.ProductStatusReady).Load();
                        if (product.ProductGenderTypeId != null)
                        {
                            genderTypeProduct = product.GenderTypeProduct;
                        }
                        db.Entry(product).Collection(p => p.ProductMaterials).Load();
                        foreach (var item in product.ProductMaterials)
                        {
                            db.Entry(item).Reference(p => p.Material).Load();
                        }
                        db.Entry(product).Reference(w => w.Workroom).Load();
                        db.Entry(product.Workroom).Reference(w => w.User).Load();
                    }
                }

                if (product != null)
                {
                    var user = new UserProfileModel(product.Workroom.UserId, product.Workroom.User.FirstName, product.Workroom.User.SecondName, product.Workroom.User.Email, product.Workroom.User.City, product.Workroom.User.Skype, product.Workroom.User.SocialNetwork, product.Workroom.User.PersonalWebsite, product.Workroom.User.Phone, product.Workroom.User.DateRegistration);
                    var workroom = new WorkroomProfileModel(product.Workroom.WorkroomId, product.Workroom.UserId, product.Workroom.Name, product.Workroom.Description, product.Workroom.CountGood, product.Workroom.CountMedium, product.Workroom.CountBad, product.Workroom.DateCreate, user);

                    var materialsSB = new StringBuilder();
                    if (product.ProductMaterials != null && product.ProductMaterials.Any())
                    {
                        foreach (var item in product.ProductMaterials)
                        {
                            materialsSB.Append(item.Material.Name).Append(", ");
                        }
                        materialsSB.Remove(materialsSB.Length - 2, 2);
                    }

                    var model = new ProductProfileModel(product.ProductId, product.WorkroomId, product.Category.SectionId, product.Category.Section.Name, product.CategoryId, product.Category.Name, product.Name, product.Description, product.KeyWords, product.Price, product.PriceDiscount, product.ProductStatusReady.Name, genderTypeProduct.Name, materialsSB.ToString(), product.Size, product.Weight, product.DateCreate, workroom);

                    var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
                    if (userId != 0 && product.Workroom.User.Id == userId)
                    {
                        return View("PersonalProfile", model);
                    }
                    else
                    {
                        return View("Profile", model);
                    }  
                }
                else
                {
                    return View("_Error");
                }
            }
            else
            {
                return View("_Error");
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(long? id)
        {
            if (id != null)
            {
                var workroom = new Workroom();
                var categoriesDb = new List<Category>();
                var statusReadyDb = new List<StatusReadyProduct>();
                var genderTypeDb = new List<GenderTypeProduct>();
                var materialsDb = new List<Material>();
                var RegisterTypeWorkroom = "";
                var Section = "";
                var SectionGender = false;

                using (var db = new Context())
                {
                    workroom = db.Workrooms.Find(id);
                    if (workroom != null && workroom.UserId == Convert.ToInt64(HttpContext.User.Identity.GetUserId()))
                    {
                        RegisterTypeWorkroom = workroom.RegisterTypeWorkroom.Name;
                        Section = workroom.Section.Name;
                        SectionGender = workroom.Section.Gender;
                        categoriesDb = db.Categories.Where(c => c.SectionId == workroom.SectionId).ToList();
                        statusReadyDb = db.StatusReadyProducts.ToList();
                        if (workroom.Section.Gender)
                        {
                            genderTypeDb = db.GenderTypeProducts.ToList();
                        }
                        materialsDb = db.Materials.ToList();
                    }
                }

                if (workroom != null && workroom.UserId == Convert.ToInt64(HttpContext.User.Identity.GetUserId()))
                {
                    var categoriesDbModel = new Collection<CategoriesDbModel>();
                    var categoryDefault = new CategoriesDbModel(0, "Категория", false);
                    categoriesDbModel.Add(categoryDefault);
                    foreach (var item in categoriesDb)
                    {
                        var category = new CategoriesDbModel(item.CategoryId, item.Name, item.Gender);
                        categoriesDbModel.Add(category);
                    }

                    var statusReadyDbModel = new Collection<StatusReadyProductDbModel>();
                    var statusDefault = new StatusReadyProductDbModel(0, "Наличие");
                    statusReadyDbModel.Add(statusDefault);
                    foreach (var item in statusReadyDb)
                    {
                        var status = new StatusReadyProductDbModel(item.ProductStatusReadyId, item.Name);
                        statusReadyDbModel.Add(status);
                    }

                    var genderTypeDbModel = new Collection<GenderTypeDbModel>();
                    var genderTypeDefault = new GenderTypeDbModel(0, "Половой признак");
                    genderTypeDbModel.Add(genderTypeDefault);
                    if (genderTypeDb != null && genderTypeDb.Any())
                    {
                        foreach (var item in genderTypeDb)
                        {
                            var genderType = new GenderTypeDbModel(item.ProductGenderTypeId, item.Name);
                            genderTypeDbModel.Add(genderType);
                        }
                    }

                    var materialsDbModel = new Collection<MaterialsDbModel>();
                    var materialDefault = new MaterialsDbModel(0, "Материалы");
                    materialsDbModel.Add(materialDefault);
                    foreach (var item in materialsDb)
                    {
                        var material = new MaterialsDbModel(item.MaterialId, item.Name);
                        materialsDbModel.Add(material);
                    }

                    var model = new ProductCreateModel(RegisterTypeWorkroom, Section, SectionGender, categoriesDbModel, statusReadyDbModel, genderTypeDbModel, materialsDbModel);
                    return View(model);
                }
                else
                {
                    return View("_Error"); //вам не пренадлежит эта мастерская проверьте id
                }
            }
            else
            {
                return View("_Error");
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreateModel model, long? id)
        {
            if (ModelState.IsValid)
            {
                var size = new StringBuilder();
                size.Append(model.Size1).Append(" x ").Append(model.Size2).Append(" x ").Append(model.Size3);
                var product = new Product();
                if (model.SelectedGenderType != null)
                {
                    product = new Product
                    {
                        WorkroomId = (long)id,
                        CategoryId = model.SelectedCategory,
                        ProductGenderTypeId = (byte)model.SelectedGenderType,
                        ProductStatusReadyId = model.SelectedStatusReady,
                        Name = model.Name,
                        Description = model.Description,
                        KeyWords = model.KeyWords,
                        Price = model.Price,
                        CountGood = 0,
                        CountMedium = 0,
                        CountBad = 0,
                        ResultRating = 0,
                        Size = size.ToString(),
                        Weight = model.Weight,
                        DateCreate = DateTime.Now
                    };
                }
                else
                {
                    product = new Product
                    {
                        WorkroomId = (long)id,
                        CategoryId = model.SelectedCategory,
                        ProductStatusReadyId = model.SelectedStatusReady,
                        Name = model.Name,
                        Description = model.Description,
                        KeyWords = model.KeyWords,
                        Price = model.Price,
                        CountGood = 0,
                        CountMedium = 0,
                        CountBad = 0,
                        ResultRating = 0,
                        Size = size.ToString(),
                        Weight = model.Weight,
                        DateCreate = DateTime.Now
                    };
                }

                using (var db = new Context())
                {
                    db.Products.Add(product);
                    
                    foreach(var item in model.SelectedMaterials)
                    {
                        var material = new ProductMaterial
                        {
                            MaterialId = (short)item
                        };
                        db.ProductMaterials.Add(material);
                    }

                    db.SaveChanges();
                }

                return RedirectToAction("Profile", "Product", new { id = product.ProductId });
            }
            else
            {
                return View(model);
            }
        }

        //[Authorize]
        //[HttpPost]
        //public ActionResult Favorite(long? id)
        //{
        //    if (id != null)
        //    {
        //        using (var db = new Context())
        //        {
        //            var product = db.Products.Find(id);
        //            if (product != null)
        //            {
        //                var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
        //                var favorite = db.BookMark.Where(p => p.ProductId == id && p.UserId == userId).FirstOrDefault();
        //                if(favorite == null)
        //                {
        //                    var fovoriteNew = new BookMark
        //                    {
        //                        ProductId = id,
        //                        UserId = userId
        //                    };
        //                    db.BookMarks.Add(fovoriteNew);
        //                    db.SaveChanges();
        //                }
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                //удалили изделие
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return View("_Error"); //В ссылке отсутвует идентификатор изделия (id)
        //    }
        //}

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Section(int? id)
        {
            if (id != null)
            {
                var products = new List<Product>();

                using (var db = new Context())
                {
                    products = db.Products.Where(p=>p.Category.SectionId == id).ToList();
                }

                if (products != null)
                {
                    var productsCollection = new Collection<ProductPreviewModel>();

                    foreach (var item in products)
                    {
                        var product = new ProductPreviewModel(item.ProductId, item.Name, item.Price, item.PriceDiscount);
                        productsCollection.Add(product);
                    }

                    return View(productsCollection);
                }
                else
                {
                    return View("_Error"); //Не найдена мастерская с данным идентификатором (id)
                }
            }
            else
            {
                return View("_Error"); //В ссылке отсутвует идентификатор мастерской (id)
            }
        }
    }
}