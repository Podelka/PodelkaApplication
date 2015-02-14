using Podelka.AttributeValidation;
using Podelka.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Podelka.Models
{
    public class UploadImageModel
    {

        [Display(Name = "Local file")]
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

    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [EmailAddress(ErrorMessage = "Вы ввели недопустимый Email адрес")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Zа-яА-Я]).*$", ErrorMessage = "{0} должен содержать хотя бы одну букву и цифру")]
        [MinLength(6, ErrorMessage = "{0} должен содержать не менее {1} символов")]
        [MaxLength(100, ErrorMessage = "Максимальная длина пароля {1} символов")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Password")]
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
        [EmailAddress(ErrorMessage = "Вы ввели недопустимый Email адрес")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Zа-яА-Я]).*$", ErrorMessage = "{0} должен содержать хотя бы одну букву и цифру")]
        [MinLength(6, ErrorMessage = "{0} должен содержать не менее {1} символов")]
        [MaxLength(100, ErrorMessage = "Максимальная длина пароля {1} символов")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "{1} и {0} не совпадают")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "City")]
        public string City { get; set; }

        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Skype")]
        public string Skype { get; set; }

        [Url(ErrorMessage = "Пожалуйста, введите адрес по образцу: https://vk.com/podelkaby")] //главное http(s)://site.domen, а остальное добавить разрешается все что угодно
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "SocialNetwork")]
        public string SocialNetwork { get; set; }

        [Url(ErrorMessage = "Пожалуйста, введите адрес по образцу: http://podelka.by")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "PersonalWebsite")]
        public string PersonalWebsite { get; set; }

        [RegularExpression(@"^(\+375\s\(((\d{2}\)\s\d{3}\-\d{2}\-\d{2})|(\d{3}\)\s\d{2}\-\d{2}\-\d{2})|(\d{4}\)\s\d{1}\-\d{2}\-\d{2})))$", ErrorMessage = "Пожалуйста, введите номер по подходящему образцу: +375 (**) ***-**-**, +375 (***) **-**-**, +375 (****) *-**-**")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Phone")]
        public string Phone { get; set; }

        [MustBeTrue(ErrorMessage = "Вы обязаны согласиться с правилами, чтобы зарегестрироваться")]
        [Display(Name = "Я согласен с этими правилами")]
        public bool AgreeRules { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [EmailAddress(ErrorMessage = "Вы ввели недопустимый Email адрес")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Email")]
        public string Email { get; set; }
    }

    public class ResetPasswordModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [EmailAddress(ErrorMessage = "Вы ввели недопустимый Email адрес")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} должен содержать не менее {2} символов", MinimumLength = 6)]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessagesValidation), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля не совпадают")]
        [Display(ResourceType = typeof(DisplayNamesValidation), Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
