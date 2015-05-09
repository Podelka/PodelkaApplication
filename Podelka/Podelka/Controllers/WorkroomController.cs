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
using System.Threading;
using Podelka.Core.Service;
using System.IO;
using System.Web.Helpers;

namespace Podelka.Controllers
{
    public class WorkroomController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int id = 1)
        {
            var workrooms = new List<Workroom>();
            int page = id;
            int pageSize = 4; // количество объектов на страницу
            int count;

            using (var db = new Context())
            {
                workrooms = db.Workrooms.OrderBy(workroom => workroom.DateCreate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                count = db.Workrooms.ToList().Count;
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

                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = count };
                WorkroomsPaginationModel model = new WorkroomsPaginationModel { PageInfo = pageInfo, Workrooms = workroomsCollectoin };
                return View(model);
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
            var defaultSection = new SectionDbModel(0, "--Выберите--");
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

                var sectionDbModel = new List<SectionDbModel>();
                var defaultSection = new SectionDbModel(0, "--Выберите--");
                sectionDbModel.Add(defaultSection);
                foreach (var item in sectionsDb)
                {
                    var section = new SectionDbModel(item.SectionId, item.Name);
                    sectionDbModel.Add(section);
                }

                var registerTypeDbModel = new List<RegisterTypeDbModel>();
                foreach (var item in workroomRegisterTypesDb)
                {
                    var regType = new RegisterTypeDbModel(item.WorkroomRegisterTypeId, item.Name);
                    registerTypeDbModel.Add(regType);
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
                model.DeliveryMethods = deliveryMethodsModel;
                model.RegisterTypes = registerTypeDbModel;
                model.PayMethods = payMethodsModel;
                model.Sections = sectionDbModel;
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
        public ActionResult Products(long? id, int page = 1)
        {
            //Thread.Sleep(5000);
            if (id != null)
            {
                var workroom = new Workroom();
                var products = new List<Product>();
                int pageSize = 12; // количество объектов на страницу
                int count = 0;

                using (var db = new Context())
                {
                    workroom = db.Workrooms.Find(id);
                    if (workroom != null)
                    {
                        products = workroom.Products.OrderBy(p => p.ProductId).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                        count = workroom.Products.Count;

                        //db.Entry(workroom).Collection(w => w.Products).Load();
                    }
                }

                if (workroom != null)
                {
                    var productsCollection = new Collection<ProductPreviewModel>();

                    if (products != null)
                    {
                        foreach (var item in products)
                        {
                            var product = new ProductPreviewModel(item.ProductId, item.Name, item.Price, item.PriceDiscount);
                            productsCollection.Add(product);
                        }
                    }

                    PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = workroom.Products.Count };
                    ProductsPaginationModelWorkroomProfile model = new ProductsPaginationModelWorkroomProfile { PageInfo = pageInfo, Products = productsCollection, WorkroomId = workroom.WorkroomId };
                    return PartialView("_ProductPreviewWorkroomProfile", model);
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
            //Thread.Sleep(5000);
            if (id != null)
            {
                return PartialView("_WorkroomReviews");
            }
            else
            {
                return View("_Error"); //В ссылке отсутвует идентификатор мастерской (id)
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Photo(long id)
        {
            var model = new UploadWorkroomImageModel { WorkroomId = id };
            return View(model);
        }

        private int _avatarWidth = 300;//Изменить размер (ширину) хранимого изображения
        private int _avatarHeight = 300;//Изменить размер (высоту) хранимого изображения

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult _Upload(IEnumerable<HttpPostedFileBase> File)
        {
            ImageService imageService = new ImageService();
            string errorMessage = String.Empty;

            if (File != null && File.Count() > 0)
            {
                //Получить только один файл
                var file = File.FirstOrDefault();
                // Проверка, является ли файл изображением с нужным нам расширением
                if (file != null && imageService.IsImage(file))
                {
                    //Убедитесь, что пользователь выбрал файл
                    if (file != null && file.ContentLength > 0)
                    {
                        var webPath = imageService.SaveTemporaryFile(file, System.Web.HttpContext.Current);
                        return Json(new { success = true, fileName = webPath.Replace("\\", "/") });//успех
                    }
                    errorMessage = "File cannot be zero length.";//ошибка
                }
                errorMessage = "File is of wrong format.";//ошибка
            }
            errorMessage = "No file uploaded.";//ошибка

            return Json(new { success = false, errorMessage = errorMessage });
        }

        [HttpPost]
        public ActionResult Save(string t, string l, string h, string w, string fileName, long workroomId)
        {
            try
            {
                //Получение файла из временной папки
                var fn = Path.Combine(Server.MapPath("~/Temp"), Path.GetFileName(fileName));

                //Рассчет размеров
                int top = Convert.ToInt32(t.Replace("-", String.Empty).Replace("px", String.Empty));
                int left = Convert.ToInt32(l.Replace("-", String.Empty).Replace("px", String.Empty));
                int height = Convert.ToInt32(h.Replace("-", String.Empty).Replace("px", String.Empty));
                int width = Convert.ToInt32(w.Replace("-", String.Empty).Replace("px", String.Empty));

                //Получение изображения и изменение его размеров
                var img = new WebImage(fn);
                img.Resize(width, height);
                //Обрезать часть изображения, выбранной пользователем
                img.Crop(top, left, img.Height - top - _avatarHeight, img.Width - left - _avatarWidth);
                //Удалить временные файлы
                System.IO.File.Delete(fn);
                //Сохранить новое изображение
                var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
                //проверить пренадлежит ли workroomId текущему пользователю
                string newFileName = "/Files/Workrooms/" + workroomId + "-workroom.jpg";
                string newFileLocation = HttpContext.Server.MapPath(newFileName);
                if (Directory.Exists(Path.GetDirectoryName(newFileLocation)) == false)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(newFileLocation));
                }

                img.Save(newFileLocation);

                //Сделать миниатюрное изображение для Preview
                img.Resize(90, 90);
                string newFileNameSmall = "/Files/Workrooms/" + workroomId + "-workroom.small.jpg";
                string newFileLocationSmall = HttpContext.Server.MapPath(newFileNameSmall);
                img.Save(newFileLocationSmall);

                return Json(new { success = true, avatarFileLocation = newFileName });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "Unable to upload file.\nERRORINFO: " + ex.Message });
            }
        }
    }
}