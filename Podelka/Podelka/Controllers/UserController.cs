using System;
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
        public ActionResult Profile(long? id)
        {
            if (id != null)
            {
                var user = UserManager.FindById((long)id);

                if (user != null)
                {
                    var model = new UserProfileModel(user.Id, user.FirstName, user.SecondName, user.Email, user.City, user.Skype, user.SocialNetwork, user.PersonalWebsite, user.Phone, user.DateRegistration);
                        
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
                    return View("_Error"); //Не найден пользователь с данным идентификатором (id)
                }
            }
            else
            {
                return View("_Error"); //В ссылке отсутвует идентификатор пользователя (id)
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

        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult Workrooms(long? id)
        {
            if (id != null)
            {
                var user = UserManager.FindById((long)id);

                if (user != null)
                {
                    var workroomCollection = new Collection<WorkroomPreviewModel>();
                    if (user.Workrooms != null)
                    {
                        foreach (var item in user.Workrooms)
                        {
                            var workroom = new WorkroomPreviewModel(item.WorkroomId, item.UserId, item.User.Email, item.Name, item.Description, item.CountGood, item.CountMedium, item.CountBad);
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
        //        ModelState.AddModelError("", "Failed to verify phone");
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
        //        : "";
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
        private const string XsrfKey = "XsrfId";

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
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
            var user = UserManager.FindById(userId);
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var userId = Convert.ToInt64(HttpContext.User.Identity.GetUserId());
            var user = UserManager.FindById(userId);
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
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