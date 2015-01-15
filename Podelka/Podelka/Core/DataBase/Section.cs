using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class Section
    {
        [Key]
        public byte SectionId { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}