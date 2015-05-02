using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Podelka.AttributeValidation;
using Podelka.Resources;

namespace Podelka.Models
{
    public class LoginModel
    {
        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "Email")]
        [Required(ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "RequiredTemplate")]
        [RegularExpression(@"^[_A-Za-z0-9-\+]([_A-Za-z0-9-\+]|(\.[_A-Za-z0-9-\+]))+@([_A-Za-z0-9-\+]){2,}(\.([A-Za-z]){2,})$", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "EmailRegular")]
        [MaxLength(100, ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "EmailMaxLength")]
        public string Email { get; set; }

        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "Password")]
        [Required(ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Zа-яА-Я]).*$", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "PasswordRegular")]
        [MinLength(6, ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "PasswordMinLength")]
        [MaxLength(100, ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "PasswordMaxLength")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "FirstName")]
        [Required(ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "RequiredTemplate")]
        [RegularExpression(@"^[А-ЯЁ](([а-яё]([\s]|[-]|[\s][-][\s])[А-ЯЁ])*[а-яё])*$", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "FirstNameRegular")]
        [MinLength(2, ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "FirstNameMinLength")]
        [MaxLength(30, ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "FirstNameMaxLength")]
        public string FirstName { get; set; }
        
        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "SecondName")]
        [Required(ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "SecondNameRequired")]
        [RegularExpression(@"^[А-ЯЁ](([а-яё]([\s]|[-]|[\s][-][\s])[А-ЯЁ])*[а-яё])*$", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "SecondNameRegular")]
        [MinLength(2, ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "SecondNameMinLength")]
        [MaxLength(30, ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "SecondNameMaxLength")]
        public string SecondName { get; set; }

        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "Email")]
        [Required(ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "RequiredTemplate")]
        [RegularExpression(@"^[_A-Za-z0-9-\+]([_A-Za-z0-9-\+]|(\.[_A-Za-z0-9-\+]))+@([_A-Za-z0-9-\+]){2,}(\.([A-Za-z]){2,})$", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "EmailRegular")]
        [MaxLength(100, ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "EmailMaxLength")]
        [Remote("CheckUserEmail", "Account", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "EmailEqual")]
        public string Email { get; set; }

        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "City")]
        [Required(ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "CityRequired")]
        public string City { get; set; }

        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "Password")]
        [Required(ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Zа-яА-Я]).*$", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "PasswordRegular")]
        [MinLength(6, ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "PasswordMinLength")]
        [MaxLength(100, ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "PasswordMaxLength")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "ConfirmPassword")]
        [Required(ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "ConfirmPasswordCompare")]
        public string ConfirmPassword { get; set; }

        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "PersonalWebsite")]
        [RegularExpression(@"^https?://([_A-Za-zА-Яа-яЁё0-9-]){2,}(\.([_A-Za-zА-Яа-яЁё0-9-]){2,})*(\.([A-Za-zА-Яа-яЁё]){2,}){1}(/[\S]*)*?$", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "PersonalWebsiteRegular")]
        public string PersonalWebsite { get; set; }

        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "SocialNetwork")]
        [RegularExpression(@"^https?://([_A-Za-zА-Яа-яЁё0-9-]){2,}(\.([_A-Za-zА-Яа-яЁё0-9-]){2,})*(\.([A-Za-zА-Яа-яЁё]){2,}){1}(/[\S]*)*?$", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "SocialNetworkRegular")]
        public string SocialNetwork { get; set; }

        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "Skype")]
        public string Skype { get; set; }

        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "Phone")]
        [RegularExpression(@"^\+375\s\(((\d{2}\)\s\d{3}\-\d{2}\-\d{2})|(\d{3}\)\s\d{2}\-\d{2}\-\d{2})|(\d{4}\)\s\d{1}\-\d{2}\-\d{2}))$", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "PhoneRegular")]
        public string Phone { get; set; }

        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "AgreeRules")]
        [MustBeTrue(ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "AgreeRulesMustBeTrue")]
        public bool AgreeRules { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "Email")]
        [Required(ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "RequiredTemplate")]
        [RegularExpression(@"^[_A-Za-z0-9-\+]([_A-Za-z0-9-\+]|(\.[_A-Za-z0-9-\+]))+@([_A-Za-z0-9-\+]){2,}(\.([A-Za-z]){2,})$", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "EmailRegular")]
        [MaxLength(100, ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "EmailMaxLength")]
        public string Email { get; set; }
    }

    public class ResetPasswordModel
    {
        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "Email")]
        [Required(ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "RequiredTemplate")]
        [RegularExpression(@"^[_A-Za-z0-9-\+]([_A-Za-z0-9-\+]|(\.[_A-Za-z0-9-\+]))+@([_A-Za-z0-9-\+]){2,}(\.([A-Za-z]){2,})$", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "EmailRegular")]
        [MaxLength(100, ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "EmailMaxLength")]
        public string Email { get; set; }

        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "Password")]
        [Required(ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Zа-яА-Я]).*$", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "PasswordRegular")]
        [MinLength(6, ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "PasswordMinLength")]
        [MaxLength(100, ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "PasswordMaxLength")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(AccountConDisplayNamesVal), Name = "ConfirmPassword")]
        [Required(ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "RequiredTemplate")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(AccountConErrorMessagesVal), ErrorMessageResourceName = "ConfirmPasswordCompare")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}