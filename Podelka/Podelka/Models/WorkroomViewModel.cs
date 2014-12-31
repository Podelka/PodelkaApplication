using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Models
{
    public class WorkroomProfileModel
    {
        public WorkroomProfileModel()
        {

        }

        public WorkroomProfileModel(long workroomId, long userId, string name, string description, short countGood, short countMedium, short countBad, UserProfileModel user)
        {
            WorkroomId = workroomId;
            UserId = userId;
            Name = name;
            Description = description;
            CountGood = countGood;
            CountMedium = countMedium;
            CountBad = countBad;
            User = user;
        }
        
        public long WorkroomId { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short CountGood { get; set; }
        public short CountMedium { get; set; }
        public short CountBad { get; set; }
        public UserProfileModel User { get; set; }
    }

    public class WorkroomPreviewModel
    {
        public WorkroomPreviewModel()
        {

        }

        public WorkroomPreviewModel(long workroomId, string name, string description, short countGood, short countMedium, short countBad)
        {
            WorkroomId = workroomId;
            Name = name;
            Description = description;
            CountGood = countGood;
            CountMedium = countMedium;
            CountBad = countBad;
        }

        public long WorkroomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short CountGood { get; set; }
        public short CountMedium { get; set; }
        public short CountBad { get; set; }
    }

    public class WorkroomProfileCreate
    {
        public WorkroomProfileCreate()
        {

        }

        public WorkroomProfileCreate(string name, string description, ICollection<RegisterTypeModel> registerTypes, ICollection<PayMethodModel> payMethods, ICollection<DeliveryMethodModel> deliveryMethods)
        {
            Name = name;
            Description = description;
            RegisterTypes = registerTypes;
            PayMethods = payMethods;
            DeliveryMethods = deliveryMethods;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RegisterTypeModel> RegisterTypes { get; set; }
        public ICollection<PayMethodModel> PayMethods { get; set; }
        public ICollection<DeliveryMethodModel> DeliveryMethods { get; set; }
    }

    public class RegisterTypeModel
    {
        public RegisterTypeModel(byte registerTypeId, string name)
        {
            RegisterTypeId = registerTypeId;
            Name = name;
            IsSelected = true;
        }

        public byte RegisterTypeId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }

    public class PayMethodModel
    {
        public PayMethodModel(byte payMethodId, string name)
        {
            PayMethodId = payMethodId;
            Name = name;
            IsSelected = true;
        }

        public byte PayMethodId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }

    public class DeliveryMethodModel
    {
        public DeliveryMethodModel(byte deliveryMethodId, string name)
        {
            DeliveryMethodId = deliveryMethodId;
            Name = name;
        }

        public byte DeliveryMethodId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}