using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Podelka.Core.DataBase
{
    public class ApplicationUser : IdentityUser<long, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {   
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string SocialNetwork { get; set; }
        public string PersonalWebsite { get; set; }

        public virtual ICollection<Workroom> Workrooms { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}