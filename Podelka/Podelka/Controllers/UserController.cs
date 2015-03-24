﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.ObjectModel;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Podelka.Models;
using Podelka.Core.DataBase;
using System.Collections.Generic;

namespace Podelka.Controllers
{
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Profile(long? id, string menu)
        {
            if (id != null)
            {
                ViewBag.Menu = RouteData.Values["menu"];
                var user = UserManager.FindById((long)id);

                if (user != null)
                {
                    string mapPath = System.Web.HttpContext.Current.Server.MapPath("/Files");
                    bool isUserImage = System.IO.File.Exists(@"" + mapPath + "\\Users\\" + user.Id + "-avatar.jpg");

                    string profileImageSrc = String.Empty;
                    if (isUserImage)
                    {
                        profileImageSrc = String.Format("~/Files/Users/{0}-avatar.jpg", user.Id);
                    }
                    else
                    {
                        profileImageSrc = "~/Content/img/user-avatar.jpg";
                    }

                    var model = new UserProfileModel(user.Id, user.FirstName, user.SecondName, profileImageSrc, user.Email, user.City, user.Skype, user.SocialNetwork, user.PersonalWebsite, user.Phone, user.DateRegistration);

                    var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
                    if (userId != 0 && id == userId)
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
                    return View("_Error");//Не найден пользователь с данным идентификатором (id)
                }
            }
            else
            {
                return View("_Error");//В ссылке отсутвует идентификатор пользователя (id)
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult ChangeProfile()
        {
            var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
            var user = UserManager.FindById(userId);

            var model = new UserProfileChangeModel(user.FirstName, user.SecondName, user.Email, user.Phone, user.Skype, user.SocialNetwork, user.PersonalWebsite, user.City);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeProfile(UserProfileChangeModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
                var userInfo = UserManager.FindById(userId);
                var user = UserManager.Find(userInfo.Email, model.ConfirmOldPassword);

                if (user != null)
                {
                    user.UserName = model.Email;
                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.SecondName = model.SecondName;
                    user.Phone = model.Phone;
                    user.Skype = model.Skype;
                    user.SocialNetwork = model.SocialNetwork;
                    user.PersonalWebsite = model.PersonalWebsite;
                    user.City = model.City;

                    using (var db = new Context())
                    {
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Profile", "User");
                }
                else
                {
                    //Старый Пароль введен неправильно
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        //[ChildActionOnly]
        [AllowAnonymous]
        public ActionResult Workrooms(long? id)
        {
            if (id != null)
            {
                var user = UserManager.FindById((long)id);

                if (user != null)
                {
                    var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
                    byte viewType;
                    if(id == userId)
                    {
                        viewType = 4;
                    }
                    else
                    {
                        viewType = 3;
                    }

                    var workroomCollection = new Collection<WorkroomPreviewModel>();
                    if (user.Workrooms != null)
                    {
                        foreach (var item in user.Workrooms)
                        {
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
                            workroomCollection.Add(workroom);
                        }
                    }
                    return PartialView("_WorkroomPreview", workroomCollection);
                }
                else
                {
                    return View("_Error"); //Не найден пользователь с данным идентификатором (id)
                }
            }
            else
            {
                return View("_Error"); //В ссылке отсутвует идентификатор пользователя (id)
            }
        }

        //[ChildActionOnly]
        [Authorize]
        public ActionResult Bookmarks()
        {
            var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
            var user = UserManager.FindById(userId);

            if (user != null)
            {
                var productsCollection = new Collection<ProductPreviewModel>();

                if (user.Bookmarks != null)
                {
                    var products = user.Bookmarks.OrderByDescending(p => p.DateAdd).ToList();
                    foreach (var item in products)
                    {
                        var product = new ProductPreviewModel(item.Product.ProductId, item.Product.Name, item.Product.Price, item.Product.PriceDiscount);
                        productsCollection.Add(product);
                    }
                }

                //return PartialView("_ProductPreview", productsCollection);
                return PartialView("_ProductPreviewRemove", productsCollection);
            }
            else
            {
                return View("_Error"); //Не найдена пользователь с данным идентификатором (id)
            }
        }

        //[ChildActionOnly]
        [AllowAnonymous]
        public ActionResult Adverts()
        {
            //var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
            //var user = UserManager.FindById(userId);

            //if (user != null)
            //{
            //    var advertsCollection = new Collection<AdvertPreviewModel>();

            //    if (user.Adverts != null)
            //    {
            //        var adverts = user.Adverts.OrderByDescending(p => p.DateAdd).ToList();
            //        foreach (var item in adverts)
            //        {
            //            var advert = new AdvertPreviewModel();
            //            advertsCollection.Add(advert);
            //        }
            //    }
            return PartialView("_UserAdverts");
            //}
            //else
            //{
            //    return View("_Error"); //Не найдена пользователь с данным идентификатором (id)
            //}
        }




        //[HttpGet]
        //[Authorize]
        //public ActionResult AddPhoneNumber()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Generate the token and send it
        //        var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
        //        var code = await UserManager.GenerateChangePhoneNumberTokenAsync(userId, model.Number);
        //        if (UserManager.SmsService != null)
        //        {
        //            var message = new IdentityMessage
        //            {
        //                Destination = model.Number,
        //                Body = "Your security code is: " + code
        //            };
        //            await UserManager.SmsService.SendAsync(message);
        //        }
        //        return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        //    }
        //    else
        //    {
        //        return View(model);
        //    }
        //}

        //[HttpGet]
        //[Authorize]
        //public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        //{
        //    var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
        //    var code = await UserManager.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber);
        //    // Send an SMS through the SMS provider to verify the phone number
        //    return phoneNumber == null ? View("_Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        //}

        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
        //        var result = await UserManager.ChangePhoneNumberAsync(userId, model.PhoneNumber, model.Code);
        //        if (result.Succeeded)
        //        {
        //            var user = await UserManager.FindByIdAsync(userId);
        //            if (user != null)
        //            {
        //                await SignInAsync(user, isPersistent: false);
        //            }
        //            return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
        //        }
        //        // If we got this far, something failed, redisplay form
        //        ModelState.AddModelError(String.Empty, "Failed to verify phone");
        //        return View(model);
        //    }
        //    else
        //    {
        //        return View(model);
        //    }
        //}

        //[HttpGet]
        //[Authorize]
        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
        //        var result = await UserManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
        //        if (result.Succeeded)
        //        {
        //            var user = await UserManager.FindByIdAsync(userId);
        //            if (user != null)
        //            {
        //                await SignInAsync(user, isPersistent: false);
        //            }
        //            return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
        //        }
        //        AddErrors(result);
        //        return View(model);
        //    }
        //    else
        //    {
        //        return View(model);
        //    }
        //}

        //[HttpGet]
        //[Authorize]
        //public ActionResult SetPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
        //        var result = await UserManager.AddPasswordAsync(userId, model.NewPassword);
        //        if (result.Succeeded)
        //        {
        //            var user = await UserManager.FindByIdAsync(userId);
        //            if (user != null)
        //            {
        //                await SignInAsync(user, isPersistent: false);
        //            }
        //            return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
        //        }
        //        AddErrors(result);
        //        // If we got this far, something failed, redisplay form
        //        return View(model);
        //    }
        //    else
        //    {
        //        return View(model);
        //    }
        //}

        //[HttpGet]
        //[Authorize]
        //public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        //{
        //    ViewBag.StatusMessage =
        //        message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
        //        : message == ManageMessageId.Error ? "An error has occurred."
        //        : String.Empty;
        //    var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
        //    var user = await UserManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return View("_Error");
        //    }
        //    var userLogins = await UserManager.GetLoginsAsync(userId);
        //    var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
        //    ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
        //    return View(new ManageLoginsViewModel
        //    {
        //        CurrentLogins = userLogins,
        //        OtherLogins = otherLogins
        //    });
        //}

#region Helpers
        // Used for XSRF protection when adding external logins

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(String.Empty, error);
            }
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}