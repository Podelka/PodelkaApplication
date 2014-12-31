using Podelka.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Podelka.Models
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Email")]
        [EmailAddress(ErrorMessage = "Вы ввели некорректный e-mail адрес")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Password")]
        [StringLength(100, ErrorMessage = " {0} должен быть по крайне мере {2} символов в длину", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplateSpecialForSecondName")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "SecondName")]
        public string SecondName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [EmailAddress]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Password")]
        [StringLength(100, ErrorMessage = " {0} должен быть по крайне мере {2} символов в длину", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля не совпадают")]
        public string ConfirmPassword { get; set; }

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

    public class ForgotPasswordModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [EmailAddress]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Email")]
        public string Email { get; set; }
    }

    public class ResetPasswordModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [EmailAddress]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [StringLength(100, ErrorMessage = " {0} должен быть по крайне мере {2} символов в длину", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля не совпадают")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
