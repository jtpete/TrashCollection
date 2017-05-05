namespace TrashGuy.Migrations
{
    using TrashGuy.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration<TrashGuy.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TrashGuy.Models.ApplicationDbContext";
        }

        protected override void Seed(TrashGuy.Models.ApplicationDbContext context)
        {

            const string name = "admin@admin.com";
            const string password = "Admin@12345";
            const string roleName = "Admin";

           
            if (!context.Roles.Any(r => r.Name == roleName))
            {
                ApplicationRole role = new ApplicationRole(roleName);
                var roleStore = new RoleStore<ApplicationRole>(context);
                var roleManager = new RoleManager<ApplicationRole>(roleStore);
                var roleResult = roleManager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == name))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                ApplicationUser user = new ApplicationUser
                {
                    UserName = name,
                    Email = name,
                    Address = "100 Company Ln.",
                    Name = "God",
                    City = "Franklin",
                    State = "WI",
                    ZipCode = "53132",
                    StartDate = DateTime.Now,
                };
                var result = userManager.Create(user, password);
                result = userManager.AddToRole(user.Id, roleName);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }
        }
    }
}
