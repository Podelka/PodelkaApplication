using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Podelka.Core;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Podelka.Core.DataBase
{
    public class Context : IdentityDbContext<ApplicationUser, RoleIntPk, long, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public Context() : base("PodelkaConnection")
        {

        }

        public static Context Create()
        {
            return new Context();
        }

        public DbSet<WorkroomRegisterType> WorkroomRegisterTypes { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<WorkroomDeliveryMethod> WorkroomDeliveryMethods { get; set; }
        public DbSet<PayMethod> PayMethods { get; set; }
        public DbSet<WorkroomPayMethod> WorkroomPayMethods { get; set; }
        public DbSet<Workroom> Workrooms { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}