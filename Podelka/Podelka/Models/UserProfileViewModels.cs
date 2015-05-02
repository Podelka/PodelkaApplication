using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Podelka.Resources;
using System;
using System.Web;

namespace Podelka.Models
{
    public class UploadImageModel
    {
        public long UserId { get; set; }

        [Display(Name = "Фотография")]
        public HttpPostedFileBase File { get; set; }

        [Range(0, int.MaxValue)]
        public int X { get; set; }

        [Range(0, int.MaxValue)]
        public int Y { get; set; }

        [Range(1, int.MaxValue)]
        public int Width { get; set; }

        [Range(1, int.MaxValue)]
        public int Height { get; set; }
    }

    public class UserProfileModel
    {
        public UserProfileModel()
        {
        }

        public UserProfileModel(long userId, string firstName, string secondName, string profileImage, string email, string city, string skype, string socialNetwork, string personalWebsite, string phone, DateTime dateRegistration)
        {
            UserId = userId;
            FirstName = firstName;
            SecondName = secondName;
            ProfileImage = profileImage;
            Email = email;
            City = city;
            Skype = skype;
            SocialNetwork = socialNetwork;
            PersonalWebsite = personalWebsite;
            Phone = phone;
            DateRegistration = dateRegistration;
        }

        public UserProfileModel(long userId, string firstName, string secondName, string email, string city, string skype, string socialNetwork, string personalWebsite, string phone, DateTime dateRegistration)
        {
            UserId = userId;
            FirstName = firstName;
            SecondName = secondName;
            Email = email;
            City = city;
            Skype = skype;
            SocialNetwork = socialNetwork;
            PersonalWebsite = personalWebsite;
            Phone = phone;
            DateRegistration = dateRegistration;
        }

        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ProfileImage { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Skype { get; set; }
        public string SocialNetwork { get; set; }
        public string PersonalWebsite { get; set; }
        public string Phone { get; set; }
        public DateTime DateRegistration { get; set; }
    }
    
    public class UserProfileChangeModel
    {
        public UserProfileChangeModel()
        {
        }

        public UserProfileChangeModel(string firstName, string secondName, string email, string phone, string skype, string socialNetwork, string personalWebsite, string city)
        {
            FirstName = firstName;
            SecondName = secondName;
            Email = email;
            Phone = phone;
            Skype = skype;
            SocialNetwork = socialNetwork;
            PersonalWebsite = personalWebsite;
            City = city;
        }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplateSpecialForSecondName")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "SecondName")]
        public string SecondName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [EmailAddress(ErrorMessage = "Вы ввели недопустимый Email адрес")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "NewPassword")]
        [StringLength(100, ErrorMessage = " {0} должен быть по крайне мере {2} символов в длину", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "ConfirmOldPassword")]
        public string ConfirmOldPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "City")]
        public string City { get; set; }

        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Skype")]
        public string Skype { get; set; }

        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "SocialNetwork")]
        public string SocialNetwork { get; set; }

        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "PersonalWebsite")]
        public string PersonalWebsite { get; set; }

        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Phone")]
        public string Phone { get; set; }

    }






    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}