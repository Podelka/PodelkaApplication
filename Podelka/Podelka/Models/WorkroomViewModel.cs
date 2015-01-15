using Podelka.AttributeValidation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Podelka.Models
{
    public class WorkroomProfileModel
    {
        public WorkroomProfileModel()
        {
        }

        public WorkroomProfileModel(long workroomId, long userId, string name, string description, int countGood, int countMedium, int countBad, DateTime dateCreate, UserProfileModel user)
        {
            WorkroomId = workroomId;
            UserId = userId;
            Name = name;
            Description = description;
            CountGood = countGood;
            CountMedium = countMedium;
            CountBad = countBad;
            DateCreate = dateCreate;
            User = user;
        }
        
        public long WorkroomId { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountGood { get; set; }
        public int CountMedium { get; set; }
        public int CountBad { get; set; }
        public DateTime DateCreate { get; set; }
        public UserProfileModel User { get; set; }
    }

    public class WorkroomPreviewModel
    {
        public WorkroomPreviewModel()
        {
        }

        public WorkroomPreviewModel(long workroomId, long userId, string email, string name, string description, int countGood, int countMedium, int countBad)
        {
            WorkroomId = workroomId;
            UserId = userId;
            Email = email;
            Name = name;
            Description = description;
            CountGood = countGood;
            CountMedium = countMedium;
            CountBad = countBad;
        }

        public long WorkroomId { get; set; }
        public long UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountGood { get; set; }
        public int CountMedium { get; set; }
        public int CountBad { get; set; }
    }

    public class WorkroomCreateModel
    {
        public WorkroomCreateModel()
        {
            RegisterTypes = new Collection<RegisterTypeDbModel>();
            PayMethods = new Collection<PayMethodDbModel>();
            DeliveryMethods = new Collection<DeliveryMethodDbModel>();
        }

        public WorkroomCreateModel(ICollection<RegisterTypeDbModel> registerTypes, ICollection<SectionDbModel> sections, ICollection<PayMethodDbModel> payMethods, ICollection<DeliveryMethodDbModel> deliveryMethods)
        {
            RegisterTypes = registerTypes;
            Sections = sections;
            PayMethods = payMethods;
            DeliveryMethods = deliveryMethods;
        }

        [Required(ErrorMessage = "Введите название мастерской")]
        [Display(Name = "Название мастерской")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите описание мастерской")]
        [Display(Name = "Описание мастерской")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Выберите тип регистрации")]
        [Display(Name = "Тип регистрации")]
        public byte SelectedRegisterType { get; set; }
        public ICollection<RegisterTypeDbModel> RegisterTypes { get; set; }

        [Required(ErrorMessage = "Выберите раздел")]
        [Display(Name = "Раздел")]
        public byte SelectedSection { get; set; }
        public ICollection<SectionDbModel> Sections { get; set; }

        [Required(ErrorMessage = "Выберите способы оплаты")]
        [Display(Name = "Способы оплаты")]
        public int[] SelectedPayGroups { get; set; }
        public ICollection<PayMethodDbModel> PayMethods { get; set; }

        [Required(ErrorMessage = "Выберите способы доставки")]
        [Display(Name = "Способы доставки")]
        public int[] SelectedDeliveryGroups { get; set; }     
        public ICollection<DeliveryMethodDbModel> DeliveryMethods { get; set; }

        [MustBeTrue(ErrorMessage = "Вы обязаны согласиться с правилами, чтобы открыть мастерскую")]
        [Display(Name = "Я согласен с этими правилами")]
        public bool AgreeRules { get; set; }
    }

    public class PayMethodModel
    {
        public PayMethodModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    public class DeliveryMethodModel
    {
        public DeliveryMethodModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    public class WorkroomMethodsModel
    {
        public WorkroomMethodsModel(ICollection<PayMethodModel> payMethods, ICollection<DeliveryMethodModel> deliveryMethods)
        {
            PayMethods = payMethods;
            DeliveryMethods = deliveryMethods;
        }

        public ICollection<PayMethodModel> PayMethods { get; set; }
        public ICollection<DeliveryMethodModel> DeliveryMethods { get; set; }
    }
}