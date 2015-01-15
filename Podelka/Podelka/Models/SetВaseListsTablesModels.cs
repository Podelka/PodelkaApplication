using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Models
{
    public class RegisterTypeDbModel
    {
        public RegisterTypeDbModel(byte registerTypeId, string name)
        {
            RegisterTypeId = registerTypeId;
            Name = name;
        }

        public byte RegisterTypeId { get; set; }
        public string Name { get; set; }
    }

    public class PayMethodDbModel
    {
        public PayMethodDbModel(byte payMethodId, string name)
        {
            PayMethodId = payMethodId;
            Name = name;
        }

        public byte PayMethodId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }

    public class DeliveryMethodDbModel
    {
        public DeliveryMethodDbModel(byte deliveryMethodId, string name)
        {
            DeliveryMethodId = deliveryMethodId;
            Name = name;
        }

        public byte DeliveryMethodId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }

    public class SectionDbModel
    {
        public SectionDbModel(byte sectionId, string name)
        {
            SectionId = sectionId;
            Name = name;
        }

        public byte SectionId { get; set; }
        public string Name { get; set; }
    }

    public class CategoriesDbModel
    {
        public CategoriesDbModel(short categoryId, string name, bool gender)
        {
            CategoryId = categoryId;
            Name = name;
            Gender = gender;
        }

        public short CategoryId { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
    }

    public class StatusReadyProductDbModel
    {
        public StatusReadyProductDbModel(byte statusReadyId, string name)
        {
            StatusReadyId = statusReadyId;
            Name = name;
        }

        public byte StatusReadyId { get; set; }
        public string Name { get; set; }
    }

    public class GenderTypeDbModel
    {
        public GenderTypeDbModel(byte genderTypeId, string name)
        {
            GenderTypeId = genderTypeId;
            Name = name;
        }

        public byte GenderTypeId { get; set; }
        public string Name { get; set; }
    }

    public class MaterialsDbModel
    {
        public MaterialsDbModel(short materialId, string name)
        {
            MaterialId = materialId;
            Name = name;
        }

        public short MaterialId { get; set; }
        public string Name { get; set; }
    }
}