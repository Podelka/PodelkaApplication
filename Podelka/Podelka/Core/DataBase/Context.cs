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

        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<TypePhone> TypePhones { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Workroom> Workrooms { get; set; }
        public DbSet<RegisterTypeWorkroom> WorkroomRegisterTypes { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<WorkroomDeliveryMethod> WorkroomDeliveryMethods { get; set; }
        public DbSet<PayMethod> PayMethods { get; set; }
        public DbSet<WorkroomPayMethod> WorkroomPayMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GenderTypeProduct> GenderTypeProducts { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<StatusReadyProduct> StatusReadyProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Workroom>()
            //.HasRequired(f => f.Section)
            //.WithRequiredDependent()
            //.WillCascadeOnDelete(false);

            //modelBuilder.Entity<ApplicationUser>()
            //.HasRequired(f => f.Workrooms)
            //.WithRequiredDependent()
            //.WillCascadeOnDelete(false);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}