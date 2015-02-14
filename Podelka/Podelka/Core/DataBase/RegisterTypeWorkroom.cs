using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class RegisterTypeWorkroom
    {
        [Key]
        public byte WorkroomRegisterTypeId { get; set; }
        public string Name { get; set; }
    }
}