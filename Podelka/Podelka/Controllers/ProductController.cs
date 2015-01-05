using Podelka.Core.DataBase;
using Podelka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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
                
                using (var db = new Context())
                {
                    product = db.Products.Find(id);
                    if (product != null)
                    {
                        db.Entry(product).Reference(w => w.Workroom).Load();
                        db.Entry(product.Workroom).Reference(w => w.User).Load();
                    }
                }

                if (product != null)
                {
                    var user = new UserProfileModel(product.Workroom.UserId, product.Workroom.User.FirstName, product.Workroom.User.SecondName, product.Workroom.User.Email, product.Workroom.User.City, product.Workroom.User.Skype, product.Workroom.User.SocialNetwork, product.Workroom.User.PersonalWebsite, product.Workroom.User.Phone);
                    var workroom = new WorkroomProfileModel(product.Workroom.WorkroomId, product.Workroom.UserId, product.Workroom.Name, product.Workroom.Description, product.Workroom.CountGood, product.Workroom.CountMedium, product.Workroom.CountBad, user);

                    var model = new ProductProfileModel(product.ProductId, product.WorkroomId, product.Name, product.Description, product.Price, product.StatusReady, product.Material, product.Size, product.Weight, workroom);

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
        [AllowAnonymous]
        public ActionResult Category(int? categoryId)
        {
            if (categoryId != null)
            {
                var categoryName = "";
                if (categoryId == 1)
                {
                    categoryName = "Одежда";
                }
                if (categoryId == 2)
                {
                    categoryName = "Украшения";
                }
                if (categoryId == 3)
                {
                    categoryName = "Аксессуары";
                }
                if (categoryId == 4)
                {
                    categoryName = "На подарок";
                }
                if (categoryId == 5)
                {
                    categoryName = "Для детей";
                }
                if (categoryId == 6)
                {
                    categoryName = "Для дома";
                }
                if (categoryId == 7)
                {
                    categoryName = "Для животных";
                }
                if (categoryId == 8)
                {
                    categoryName = "Канцелярские товары";
                }
                if (categoryId == 9)
                {
                    categoryName = "Средства по уходу";
                }
                var model = new CategoryModel(categoryName);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}