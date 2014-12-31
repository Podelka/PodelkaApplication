using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Podelka.Core;
using Podelka.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Podelka.Models
{

    //проинициализируем базу данных начальными значениями для ролей и пользователей.
    //Так как у нас по умолчанию уже определен в проекте контекст данных для управления пользователями и ролями Context, 
    //то мы его указываем при создании объектов UserManager и RoleManager.
    //Метод roleManager.Create позволяет добавить роль в бд в таблицу AspNetRoles, а метод userManager.AddToRole устанавливает определенную 
    //роль для пользователя с переданным в метод id.
    //класс AppDbInitializer и его вызов при старте сайта нужен только один раз, чтобы добавить админа, а далее уже админ посредством метода 
    //AddUserToRoleAsync дает зареганым юзерам ту или иную роль. То есть я создаю класс AppDbInitializer, добавляю вызов в глобал асакс, 
    //запускаю сайт, автоматом создаются все необходимые для системы авторизации поля (их еще нет), далее я могу этот класс с глобала убирать. 

    public class AppDbInitializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context)
        {
            var userManager = new UserManager<ApplicationUser, long>(new UserStoreIntPk(context)); //public UserManager(IUserStore<TUser, TKey> store);
 
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
 
            // создаем четыре роли
            var role1 = new IdentityRole { Name = "User" };
            var role2 = new IdentityRole { Name = "MakerYesRegistration" };
            var role3 = new IdentityRole { Name = "MakerNoRegistration" };
            var role4 = new IdentityRole { Name = "Moderator" };
            var role5 = new IdentityRole { Name = "Admin" };
 
            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);
            roleManager.Create(role4);
            roleManager.Create(role5);

            // создаем пользователей
            var admin = new ApplicationUser { Email = "admin@mail.ru", UserName = "admin@mail.ru" };
            string password1 = "qwe123";
            var result1 = userManager.Create(admin, password1);

            var moderator = new ApplicationUser { Email = "moderator@mail.ru", UserName = "moderator@mail.ru" };
            string password2 = "qwe123";
            var result2 = userManager.Create(moderator, password2);

            var user = new ApplicationUser { Email = "user@mail.ru", UserName = "user@mail.ru" };
            string password3 = "qwe123";
            var result3 = userManager.Create(user, password3);

            var maker_yes = new ApplicationUser { Email = "maker_yes@mail.ru", UserName = "maker_yes@mail.ru" };
            string password4 = "qwe123";
            var result4 = userManager.Create(maker_yes, password4);

            var maker_no = new ApplicationUser { Email = "maker_no@mail.ru", UserName = "maker_no@mail.ru" };
            string password5 = "qwe123";
            var result5 = userManager.Create(maker_no, password5);

            // если создание пользователя прошло успешно
            if (result1.Succeeded && result2.Succeeded && result3.Succeeded && result4.Succeeded && result5.Succeeded)            
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role5.Name);
                userManager.AddToRole(moderator.Id, role4.Name);
                userManager.AddToRole(user.Id, role1.Name);
                userManager.AddToRole(maker_yes.Id, role2.Name);
                userManager.AddToRole(maker_no.Id, role3.Name);
            }
 
            base.Seed(context);
        }
    }

}