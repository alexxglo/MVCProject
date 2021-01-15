using GameLibraryProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using GameLibraryProject.Models.MyDatabaseInitializer;
[assembly: OwinStartupAttribute(typeof(GameLibraryProject.Startup))]
namespace GameLibraryProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateAdminAndUserRoles();
        }
        private void CreateAdminAndUserRoles()
        {
            var ctx = new DbCtx();
            var roleManager = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(ctx));
            var userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(ctx));
            // adaugam rolurile pe care le poate avea un utilizator
            // din cadrul aplicatiei
            if (!roleManager.RoleExists("Admin"))
            {
                // adaugam rolul de administrator
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
                // se adauga utilizatorul administrator
                var user = new ApplicationUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";
                var adminCreated = userManager.Create(user, "Admin2020!");
                if (adminCreated.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
            
            if (!roleManager.RoleExists("Client"))
            {
            // adaugati rolul specific aplicatiei voastre
            var role = new IdentityRole();
            role.Name = "Client";
            roleManager.Create(role);
            // se adauga utilizatorul
            var user = new ApplicationUser();
            user.UserName = "Client";
            user.Email = "Client@client.com";
            }
        }

    }
}
