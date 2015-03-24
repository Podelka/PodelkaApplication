using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Podelka.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Podelka.Core;
using Podelka.Core.DataBase;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Web.Helpers;
using Podelka.Core.Service;

namespace Podelka.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

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

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        [ChildActionOnly]
        [Authorize]
        public ActionResult GetUserId()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            return Content(userId);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);
                if (user != null)
                {
                    if (user.EmailConfirmed != true)
                    {
                        ModelState.AddModelError(String.Empty, "Ваш Email не подтвержден");
                        return View(model);
                    }
                    else
                    {
                        var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                        switch (result)
                        {
                            case SignInStatus.Success:
                                return RedirectToLocal(returnUrl);
                            case SignInStatus.Failure:
                            default:
                                ModelState.AddModelError(String.Empty, "Неверный Email или Пароль");
                                return View(model);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Неверный Email или Пароль");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    Phone = model.Phone,
                    Skype = model.Skype,
                    SocialNetwork = model.SocialNetwork,
                    PersonalWebsite = model.PersonalWebsite,
                    City = model.City,
                    DateRegistration = DateTime.Today
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await AddUserToRoleAsync(user, "User");
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("EmailConfirmation", "Account", new { userId = user.Id, code = code },
                        protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Подтверждение электронной почты",
                        "<h2 style=font-family:Georgia,serif;font-size:40px;font-weight:bold;font-style:italic>Всё готово</h2>"+
                        "Для завершения регистрации перейдите по ссылке: <a href=\"" + callbackUrl + "\">завершить регистрацию</a>");
                    //return View("RegisterConfirmation");
                    return PartialView("_RegisterConfirmation");
                }
                else
                {
                    AddErrors(result);
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> EmailConfirmation(long? userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("_Error");//В адресной строке отсутвуют необходимые параметры (идентификатор пользователя(id) и/или секретный код)
            }
            
            var result = await UserManager.ConfirmEmailAsync((long)userId, code);
            
            if (result.Succeeded)
            {
                return View("EmailConfirmation");
            }
            else
            {
                return View("_Error"); //отдельную вьюшку можно сделать, чтобы показывало, что произошла ошибка подтверждения email - а
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    return View("ForgotPasswordConfirmation");
                }
                else
                {
                    string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Сброс пароля",
                        "Для сброса пароля, нажмите по ссылке <a href=\"" + callbackUrl + "\">здесь</a>");
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
            }
            else
            { 
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("_Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                AddErrors(result);
                return View();
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

///<summary>
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///</summary>
///<returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Photo()
        {
            return View();
        }

        //[HttpPost]
        //[Authorize]
        //public ActionResult Photo(UploadImageModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userId = Convert.ToUInt64(HttpContext.User.Identity.GetUserId());
        //        var name = userId.ToString() + "avatar";

        //        Bitmap original = Bitmap.FromStream(model.File.InputStream) as Bitmap;

        //        if (original != null)
        //        {
        //            var imgService = new ImageApplication();
        //            var img = imgService.CreateImage(original, model.X, model.Y, model.Width, model.Height);
        //            var fn = Server.MapPath("~/Content/img/" + name + ".jpg");

        //            img.Save(fn, System.Drawing.Imaging.ImageFormat.Jpeg);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(String.Empty, "Your upload did not seem valid. Please try again using only correct images!");
        //            return View(model);
        //        }
        //    }
        //    else
        //    {
        //        return View(model);
        //    }
        //}

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
        public ActionResult Save(string t, string l, string h, string w, string fileName)
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
                string newFileName = "/Files/Users/" + userId.ToString() + "-avatar.jpg";
                string newFileLocation = HttpContext.Server.MapPath(newFileName);
                if (Directory.Exists(Path.GetDirectoryName(newFileLocation)) == false)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(newFileLocation));
                }

                img.Save(newFileLocation);

                return Json(new { success = true, avatarFileLocation = newFileName });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "Unable to upload file.\nERRORINFO: " + ex.Message });
            }
        }


//////////////////////////////////////////////////////////////////////////////////////////////

        #region Helpers

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(String.Empty, error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else 
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        private async Task AddUserToRoleAsync(ApplicationUser user, string role)
        {
            using (var db = new Context())
            {
                var userManager = new UserManager<ApplicationUser, long>(new UserStoreIntPk(db));
                var result = await userManager.AddToRoleAsync(user.Id, role);
            }
        }

        public JsonResult CheckUserEmail(string email)
        {
            var user = UserManager.FindByEmail(email);
            bool result = true;
            if (user != null)
            {
                result = false;       
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}