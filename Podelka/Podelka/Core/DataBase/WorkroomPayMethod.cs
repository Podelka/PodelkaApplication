using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class WorkroomPayMethod
    {
        [Key, Column(Order = 1)]
        public long WorkroomId { get; set; }
        [Key, Column(Order = 2)]
        public byte PayMethodId { get; set; }
        public string Description { get; set; }

        public virtual Workroom Workroom { get; set; }
        public virtual PayMethod PayMethod { get; set; }
    }
}