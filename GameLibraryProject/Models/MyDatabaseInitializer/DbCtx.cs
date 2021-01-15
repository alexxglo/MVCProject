using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GameLibraryProject.Models.MyDatabaseInitializer
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

  
    public class DbCtx : IdentityDbContext<ApplicationUser>
    {
        public DbCtx() : base("DbConnectionString", throwIfV1Schema: false)
        {

            // set the initializer here
            Database.SetInitializer<DbCtx>(new Initp());
            //Database.SetInitializer<DbCtx>(new CreateDatabaseIfNotExists<DbCtx>());
            //Database.SetInitializer<DbCtx>(new DropCreateDatabaseIfModelChanges<DbCtx>());
            //Database.SetInitializer<DbCtx>(new DropCreateDatabaseAlways<DbCtx>());
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public static DbCtx Create()
        {
            return new DbCtx();
        }
    }
        

    public class Initp : DropCreateDatabaseAlways<DbCtx>
    { // custom initializer
        protected override void Seed(DbCtx ctx)
        {
            //try {
            GameType format1 = new GameType { GameTypeId = 60, Name = "Hard Case" };
            GameType format2 = new GameType { GameTypeId = 61, Name = "Code" };
            ctx.GameTypes.Add(format1);
            ctx.GameTypes.Add(format2);

            Manager manager1 = new Manager { NameM = "Smith", Surname = "John", PhoneNumber = "0712345222" };
            Manager manager2 = new Manager { NameM = "Cash", Surname = "Papa", PhoneNumber = "0733345222" };
            Manager manager3 = new Manager { NameM = "Radu", Surname = "Stephan", PhoneNumber = "0722113456" };
            ctx.Managers.Add(manager1);
            ctx.Managers.Add(manager2);
            ctx.Managers.Add(manager3);

            Store store1 = new Store { Name = "Steam", Manager = manager1};
            Store store2 = new Store { Name = "Origin", Manager = manager2};
            Store store3 = new Store { Name = "UPlay", Manager = manager3};
            ctx.Stores.Add(store1);
            ctx.Stores.Add(store2);
            ctx.Stores.Add(store3);

            Genre genre1 = new Genre { Name = "Sports" };
            Genre genre2 = new Genre { Name = "Shooter" };
            Genre genre3 = new Genre { Name = "Adventure" };
            Genre genre4 = new Genre { Name = "Open World" };
            Genre genre5 = new Genre { Name = "Singleplayer" };
            Genre genre6 = new Genre { Name = "Multiplayer" };
            ctx.Genres.Add(genre1);
            ctx.Genres.Add(genre2);
            ctx.Genres.Add(genre3);
            ctx.Genres.Add(genre4);
            ctx.Genres.Add(genre5);
            ctx.Genres.Add(genre6);

            Publisher publisher1 = new Publisher { Name = "EA", Store = store2 };
            Publisher publisher2 = new Publisher { Name = "2K", Store = store1 };
            Publisher publisher3 = new Publisher { Name = "Valve", Store = store1 };
            Publisher publisher4 = new Publisher { Name = "Ubisoft", Store = store3 };
            Publisher publisher5 = new Publisher { Name = "CD Projekt Red", Store = store1 };
            Publisher publisher6 = new Publisher { Name = "Bethesda Softworks", Store = store1 };
            ctx.Publishers.Add(publisher1);
            ctx.Publishers.Add(publisher2);
            ctx.Publishers.Add(publisher3);
            ctx.Publishers.Add(publisher4);
            ctx.Publishers.Add(publisher5);
            ctx.Publishers.Add(publisher6);



            /*
            ctx.Games.Add(new Game
            {
                Title = "NBA 2K21 - Basketball Game",
                Producer = "Visual Concepts",
                Genres = new List<Genre> {
                new Genre {Name = "Sports"},
                new Genre {Name = "Multiplayer"} },
                Publisher = publisher2,
                Summary = "NBA 2K21 is the latest title in the world-renowned, best-selling NBA 2K series, delivering an industry-leading sports video game experience.",
                GameType = format2
            });
            ctx.Games.Add(new Game
            {
                Title = "Counter-Strike: Global Offensive",
                Producer = "Hidden Path Entertainment",
                Genres = new List<Genre> {
                new Genre {Name = "Shooter"},
                new Genre {Name = "Multiplayer"} },
                Publisher = publisher3,
                Summary= "Counter-Strike: Global Offensive (CS: GO) expands upon the team-based action gameplay that it pioneered when it was launched 19 years ago. CS: GO features new maps, characters, weapons, and game modes, and delivers updated versions of the classic CS content (de_dust2, etc.).",
                GameType = format1
            });
            ctx.Games.Add(new Game
            {
                Title = "Half-Life 2",
                Producer = "Hidden Path Entertainment",
                Genres = new List<Genre> {
                new Genre {Name = "Open world"},
                new Genre {Name = "Singleplayer"} },
                Publisher = publisher3,
                Summary= "1998. HALF-LIFE sends a shock through the game industry with its combination of pounding action and continuous, immersive storytelling. Valve's debut title wins more than 50 game-of-the-year awards on its way to being named <Best PC Game Ever> by PC Gamer, and launches a franchise with more than eight million retail units sold worldwide.",
                GameType = format1
            });
            ctx.Games.Add(new Game
            {
                Title = "Fifa 21",
                Producer = "EA Sports",
                Genres = new List<Genre> {
                new Genre {Name = "Sports"},
                new Genre {Name = "Multiplayer"} },
                Publisher = publisher1,
                Summary= "Play FIFA 21, Get David Beckham. Receive an untradeable David Beckham item in FIFA ULTIMATE TEAM (FUT), and get him as a VOLTA Groundbreaker, starting December 15 when you Play FIFA 21 by January 15, 2021*.",
                GameType = format2
            });
            ctx.Games.Add(new Game
            {
                Title = "Far Cry 4",
                Producer = "Ubisoft",
                Genres = new List<Genre> {
                new Genre {Name = "Adventure"},
                new Genre {Name = "Singleplayer"},
                new Genre {Name = "Open World" } },
                Publisher = publisher1,
                Summary= "Hidden in the towering Himalayas lies Kyrat, a country steeped in tradition and violence. You are Ajay Ghale. Traveling to Kyrat to fulfill your mother’s dying wish, you find yourself caught up in a civil war to overthrow the oppressive regime of dictator Pagan Min.",
                GameType = format2
            });
            ctx.Games.Add(new Game
            {
                Title = "The Witcher",
                Producer = "CD Projekt Red",
                Genres = new List<Genre> {
                new Genre {Name = "Open World"},
                new Genre {Name = "Singleplayer"} },
                Publisher = publisher5,
                Summary= "As war rages on throughout the Northern Realms, you take on the greatest contract of your life — tracking down the Child of Prophecy, a living weapon that can alter the shape of the world.",
                GameType = format2
            });
            ctx.Games.Add(new Game
            {
                Title = "The Elder Scrolls V: Skyrim",
                Producer = "Bethesda Game Studios",
                Genres = new List<Genre> {
                new Genre {Name = "Open World"},
                new Genre {Name = "Singleplayer"} },
                Publisher = publisher6,
                Summary = "The next chapter in the highly anticipated Elder Scrolls saga arrives from the makers of the 2006 and 2008 Games of the Year, Bethesda Game Studios. Skyrim reimagines and revolutionizes the open-world fantasy epic, bringing to life a complete virtual world open for you to explore any way you choose.",
                GameType = format2
            });
            */


            ctx.SaveChanges();
            base.Seed(ctx);


        }
        //catch (DbEntityValidationException e)
        //{
        //    foreach (var eve in e.EntityValidationErrors)
        //    {
        //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //        foreach (var ve in eve.ValidationErrors)
        //        {
        //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                ve.PropertyName, ve.ErrorMessage);
        //        }
        //    }
        //    throw;
        //}
    }
}