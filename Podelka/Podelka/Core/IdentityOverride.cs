using Microsoft.AspNet.Identity.EntityFramework;
using Podelka.Core.DataBase;
using Podelka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Core
{
    //добавим обязательным классам int первичный ключ
    public class UserRoleIntPk : IdentityUserRole<long>
    {
    }

    public class UserClaimIntPk : IdentityUserClaim<long>
    {
    }

    public class UserLoginIntPk : IdentityUserLogin<long>
    {
    }

    public class RoleIntPk : IdentityRole<long, UserRoleIntPk>
    {
        public RoleIntPk() { }
        public RoleIntPk(string name)
        {
            Name = name;
        }
    }

    public class UserStoreIntPk : UserStore<ApplicationUser, RoleIntPk, long, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public UserStoreIntPk(Context context)
            : base(context)
        {
        }
    }

    public class RoleStoreIntPk : RoleStore<RoleIntPk, long, UserRoleIntPk>
    {
        public RoleStoreIntPk(Context context)
            : base(context)
        {
        }
    }
}