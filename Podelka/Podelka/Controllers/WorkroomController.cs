using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Podelka.Core.DataBase;
using Podelka.Models;
using System.Collections.ObjectModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Podelka.Controllers
{
    public class WorkroomController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var workrooms = new List<Workroom>();
            using (var db = new Context())
            {
                workrooms = db.Workrooms.ToList();
            }

            if (workrooms != null)
            {
                var workroomsCollectoin = new Collection<WorkroomPreviewModel>();
                foreach (var item in workrooms)
                {
                    var workroom = new WorkroomPreviewModel(item.WorkroomId, item.Name, item.Description, item.CountGood, item.CountMedium, item.CountBad);
                    workroomsCollectoin.Add(workroom);
                }
                return View(workroomsCollectoin);
            }
            else
            {
                return View("_Error");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Profile(long? id)
        {
            if (id != null)
            {
                var workroom = new Workroom();

                using (var db = new Context())
                {
                    workroom = db.Workrooms.Find(id);
                    if (workroom != null)
                    {
                        db.Entry(workroom).Reference(w => w.User).Load();
                    }
                }

                if (workroom != null)
                {
                    var user = new UserProfileModel(workroom.UserId, workroom.User.FirstName, workroom.User.SecondName, workroom.User.Email, workroom.User.City, workroom.User.Skype, workroom.User.SocialNetwork, workroom.User.PersonalWebsite, workroom.User.Phone);

                    var model = new WorkroomProfileModel(workroom.WorkroomId, workroom.UserId, workroom.Name, workroom.Description, workroom.CountGood, workroom.CountMedium, workroom.CountBad, user);
                        
                    var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
                    if (userId != 0 && workroom.User.Id == userId)
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
                    return View("_Error"); //Не найдена мастерская с данным идентификатором (id)
                }
            }
            else
            {
                return View("_Error"); //В ссылке отсутвует идентификатор мастерской (id)
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var workroomRegisterTypesDb = new List<WorkroomRegisterType>();
            var payMethodsDb = new List<PayMethod>();
            var deliveryMethodsDb = new List<DeliveryMethod>();

            using (var db = new Context())
            {
                workroomRegisterTypesDb = db.WorkroomRegisterTypes.ToList();
                payMethodsDb = db.PayMethods.ToList();
                deliveryMethodsDb = db.DeliveryMethods.ToList();
            }
                
            var registerTypeModel = new Collection<RegisterTypeModel>();
            foreach (var item in workroomRegisterTypesDb)
            {
                var regType = new RegisterTypeModel(item.WorkroomRegisterTypeId, item.Name);
                registerTypeModel.Add(regType);
            }
                
            var payMethodsModel = new Collection<PayMethodModel>();
            foreach (var item in payMethodsDb)
            {
                var payMet = new PayMethodModel(item.PayMethodId, item.Name);
                payMethodsModel.Add(payMet);
            }

            var deliveryMethodsModel = new Collection<DeliveryMethodModel>();
            foreach (var item in deliveryMethodsDb)
            {
                var deliveryMet = new DeliveryMethodModel(item.DeliveryMethodId, item.Name);
                deliveryMethodsModel.Add(deliveryMet);
            }

            var model = new WorkroomProfileCreate(null, null, registerTypeModel, payMethodsModel, deliveryMethodsModel);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkroomProfileCreate model)
        {
            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
                var workroom = new Workroom
                {
                    UserId = userId,
                    RegisterTypeId = model.RegisterTypeId,
                    Name = model.Name,
                    Description = model.Description,
                    CountGood = 0,
                    CountMedium = 0,
                    CountBad = 0,
                    DateRegistration = DateTime.Now
                };

                using (var db = new Context())
                {
                    db.Workrooms.Add(workroom);
                    db.SaveChanges();
                }

                return RedirectToAction("Profile", "Workroom", new { id = workroom.WorkroomId });
            }
            else
            {
                return View(model);
            }
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult Products(long? id)
        {
            if (id != null)
            {
                var workroom = new Workroom();

                using (var db = new Context())
                {
                    workroom = db.Workrooms.Find(id);
                    if (workroom != null)
                    {
                        db.Entry(workroom).Collection(w => w.Products).Load();
                    }
                }

                if (workroom != null)
                {
                    var productsCollection = new Collection<ProductPreviewModel>();

                    if (workroom.Products != null)
                    {
                        foreach (var item in workroom.Products)
                        {
                            var product = new ProductPreviewModel(item.ProductId, item.Name, item.Description, item.Price);
                            productsCollection.Add(product);
                        }
                    }

                    return PartialView("_ProductPreview", productsCollection);
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