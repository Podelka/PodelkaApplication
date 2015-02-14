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
                if (workrooms != null)
                {
                    foreach(var item in workrooms)
                    {
                        db.Entry(item).Reference(i => i.User).Load();
                        db.Entry(item).Collection(i => i.Products).Load();
                    }
                }
            }

            if (workrooms != null)
            {
                var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
                byte viewType;
                var workroomsCollectoin = new Collection<WorkroomPreviewModel>();

                foreach (var item in workrooms)
                {
                    if (item.UserId == userId)
                    {
                        viewType = 2;
                    }
                    else
                    {
                        viewType = 1;
                    }

                    var lastProductsCollection = new Collection<ProductSmallPreviewModel>();
                    var products = item.Products.OrderByDescending(p => p.ProductId).Take(4);
                    if (products != null)
                    {
                        foreach (var product in products)
                        {
                            var lastProduct = new ProductSmallPreviewModel(product.ProductId, product.Name);
                            lastProductsCollection.Add(lastProduct);
                        }
                    }

                    var workroom = new WorkroomPreviewModel(item.WorkroomId, item.UserId, item.User.Email, item.Name, item.Description, item.CountGood, item.CountMedium, item.CountBad, viewType, lastProductsCollection);
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
                ViewBag.Menu = RouteData.Values["menu"];
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
                    var user = new UserProfileModel(workroom.UserId, workroom.User.FirstName, workroom.User.SecondName, workroom.User.Email, workroom.User.City, workroom.User.Skype, workroom.User.SocialNetwork, workroom.User.PersonalWebsite, workroom.User.Phone, workroom.User.DateRegistration);

                    var model = new WorkroomProfileModel(workroom.WorkroomId, workroom.UserId, workroom.Name, workroom.Description, workroom.CountGood, workroom.CountMedium, workroom.CountBad, workroom.DateCreate, user);

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
            var workroomRegisterTypesDb = new List<RegisterTypeWorkroom>();
            var payMethodsDb = new List<PayMethod>();
            var deliveryMethodsDb = new List<DeliveryMethod>();
            var sectionsDb = new List<Section>();

            using (var db = new Context())
            {
                workroomRegisterTypesDb = db.WorkroomRegisterTypes.ToList();
                payMethodsDb = db.PayMethods.ToList();
                deliveryMethodsDb = db.DeliveryMethods.ToList();
                sectionsDb = db.Sections.ToList();
            }

            var registerTypeDbModel = new List<RegisterTypeDbModel>();
            foreach (var item in workroomRegisterTypesDb)
            {
                var regType = new RegisterTypeDbModel(item.WorkroomRegisterTypeId, item.Name);
                registerTypeDbModel.Add(regType);
            }

            var sectionDbModel = new List<SectionDbModel>();
            var defaultSection = new SectionDbModel(0, "Выберите раздел мастерской");
            sectionDbModel.Add(defaultSection);
            foreach (var item in sectionsDb)
            {
                var section = new SectionDbModel(item.SectionId, item.Name);
                sectionDbModel.Add(section);
            }

            var payMethodsModel = new Collection<PayMethodDbModel>();
            foreach (var item in payMethodsDb)
            {
                var payMet = new PayMethodDbModel(item.PayMethodId, item.Name);
                payMethodsModel.Add(payMet);
            }

            var deliveryMethodsModel = new Collection<DeliveryMethodDbModel>();
            foreach (var item in deliveryMethodsDb)
            {
                var deliveryMet = new DeliveryMethodDbModel(item.DeliveryMethodId, item.Name);
                deliveryMethodsModel.Add(deliveryMet);
            }

            var model = new WorkroomCreateModel(registerTypeDbModel, sectionDbModel, payMethodsModel, deliveryMethodsModel);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkroomCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
                var workroom = new Workroom
                {
                    UserId = userId,
                    WorkroomRegisterTypeId = model.SelectedRegisterType,
                    SectionId = model.SelectedSection,
                    Name = model.Name,
                    Description = model.Description,
                    ResultRating = 0,
                    CountGood = 0,
                    CountMedium = 0,
                    CountBad = 0,
                    DateCreate = DateTime.Now
                };

                using (var db = new Context())
                {
                    db.Workrooms.Add(workroom);

                    foreach (var item in model.SelectedPayGroups)
                    {
                        var workroomPayMethod = new WorkroomPayMethod
                        {
                            WorkroomId = workroom.WorkroomId,
                            PayMethodId = (byte)item
                        };
                        db.WorkroomPayMethods.Add(workroomPayMethod);
                    }

                    foreach (var item in model.SelectedDeliveryGroups)
                    {
                        var workroomDeliveryMethod = new WorkroomDeliveryMethod
                        {
                            WorkroomId = workroom.WorkroomId,
                            DeliveryMethodId = (byte)item
                        };
                        db.WorkroomDeliveryMethods.Add(workroomDeliveryMethod);
                    }

                    db.SaveChanges();
                }

                return RedirectToAction("Profile", "Workroom", new { id = workroom.WorkroomId });
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Methods(long? id)
        {
            if (id != null)
            {
                var workroom = new Workroom();

                using (var db = new Context())
                {
                    workroom = db.Workrooms.Find(id);
                    if (workroom != null)
                    {
                        db.Entry(workroom).Collection(w => w.WorkroomPayMethods).Load();
                        if (workroom.WorkroomPayMethods != null)
                        {
                            foreach (var item in workroom.WorkroomPayMethods)
                            {
                                db.Entry(item).Reference(w=>w.PayMethod).Load();
                            }
                        }

                        db.Entry(workroom).Collection(w => w.WorkroomDeliveryMethods).Load();
                        if (workroom.WorkroomDeliveryMethods != null)
                        {
                            foreach (var item in workroom.WorkroomDeliveryMethods)
                            {
                                db.Entry(item).Reference(w => w.DeliveryMethod).Load();
                            }
                        }
                    }
                }

                if (workroom != null)
                {
                    var payMethodsModel = new Collection<PayMethodModel>();

                    if (workroom.WorkroomPayMethods != null)
                    {
                        foreach (var item in workroom.WorkroomPayMethods)
                        {
                            var payMet = new PayMethodModel(item.PayMethod.Name);
                            payMethodsModel.Add(payMet);
                        }
                    }

                    var deliveryMethodsModel = new Collection<DeliveryMethodModel>();

                    if (workroom.WorkroomDeliveryMethods != null)
                    {
                        foreach (var item in workroom.WorkroomDeliveryMethods)
                        {
                            var deliveryMet = new DeliveryMethodModel(item.DeliveryMethod.Name);
                            deliveryMethodsModel.Add(deliveryMet);
                        }
                    }

                    var model = new WorkroomMethodsModel(payMethodsModel, deliveryMethodsModel);

                    return View(model);
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

        //[ChildActionOnly]
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
                            var product = new ProductPreviewModel(item.ProductId, item.Name, item.Price, item.PriceDiscount);
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

        //[ChildActionOnly]
        [AllowAnonymous]
        public ActionResult Reviews(long? id)
        {
            if (id != null)
            {
                return PartialView("_WorkroomReviews");
            }
            else
            {
                return View("_Error"); //В ссылке отсутвует идентификатор мастерской (id)
            }
        }
    }
}