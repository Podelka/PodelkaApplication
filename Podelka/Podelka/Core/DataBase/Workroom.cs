using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class Workroom
    {
        public long WorkroomId { get; set; }
        public long UserId { get; set; }
        public byte WorkroomRegisterTypeId { get; set; }
        public byte SectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountGood { get; set; }
        public int CountMedium { get; set; }
        public int CountBad { get; set; }
        public double ResultRating { get; set; }
        public bool Background { get; set; }
        public DateTime DateCreate { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual RegisterTypeWorkroom RegisterTypeWorkroom { get; set; }
        public virtual Section Section { get; set; }//Introducing FOREIGN KEY constraint 'FK_dbo.Workrooms_dbo.Sections_SectionId' on table 'Workrooms' may cause cycles or multiple cascade paths.
        public virtual ICollection<WorkroomDeliveryMethod> WorkroomDeliveryMethods { get; set; }
        public virtual ICollection<WorkroomPayMethod> WorkroomPayMethods { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}