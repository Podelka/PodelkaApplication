using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class Bookmark
    {
        [Key, Column(Order = 1)]
        public long UserId { get; set; }
        [Key, Column(Order = 2)]
        public long ProductId { get; set; }
        public DateTime DateAdd { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Product Product { get; set; }
    }
}