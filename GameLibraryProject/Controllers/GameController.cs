using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameLibraryProject.Models;
using GameLibraryProject.Models.MyDatabaseInitializer;

namespace GameLibraryProject.Controllers
{
    [Authorize (Roles = "Admin, User")]
    public class GameController : Controller
    {
        private DbCtx db = new DbCtx();

        [HttpGet]
        public ActionResult Index()
        {
            //List<Game> games = db.Games.Include("Publisher").ToList();
            List<Game> games = db.Games.ToList();
            ViewBag.Games = games;
            return View();
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Game game = db.Games.Find(id);
                if (game != null)
                {
                    return View(game);
                }
                return HttpNotFound("Couldn't find the game with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing game id parameter!");
        }
        
        [HttpGet]
        public ActionResult New()
        {
            Game game = new Game();

            game.GameTypesList = GetAllGameTypes();
            game.PublishersList = GetAllPublishers();
            
            game.GenresList = GetAllGenres();
            game.Genres = new List<Genre>();
           
            return View(game);
        }
        
        [HttpPost]
        public ActionResult New(Game gameRequest)
        {

            gameRequest.GameTypesList = GetAllGameTypes();
            gameRequest.PublishersList = GetAllPublishers();
            gameRequest.GenresList = GetAllGenres(); // added recently
            var selectedGenres = gameRequest.GenresList.Where(b => b.Checked).ToList();
            
            try
            {

                if (ModelState.IsValid)
                {
                    gameRequest.Genres = new List<Genre>();
                    gameRequest.Publisher = db.Publishers.FirstOrDefault(p => p.PublisherId.Equals(1));
                    for (int i = 0; i < selectedGenres.Count(); i++)
                    {

                        Genre genre = db.Genres.Find(selectedGenres[i].Id);
                        gameRequest.Genres.Add(genre);
                    }
                    db.Games.Add(gameRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(gameRequest);
            }
            
            catch (Exception e)
            {
                return View(gameRequest);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Game game = db.Games.Find(id);
                game.GenresList = GetAllGenres();
                game.PublishersList = GetAllPublishers();

                foreach (Genre checkedGenre in game.Genres)
                {   
                    game.GenresList.FirstOrDefault(g => g.Id == checkedGenre.GenreId).Checked = true;
                }
                if (game == null)
                {
                    return HttpNotFound("Couldn't find the game with id " + id.ToString());
                }
                game.GameTypesList = GetAllGameTypes();
                game.PublishersList = GetAllPublishers();
                return View(game);
            }
            return HttpNotFound("Missing game id parameter!");
        }
        [HttpPut]
        public ActionResult Edit(int id, Game gameRequest)
        {       
            gameRequest.GameTypesList = GetAllGameTypes();
            gameRequest.PublishersList = GetAllPublishers();
            Game game = db.Games.Include("Publisher").SingleOrDefault(b => b.GameId.Equals(id));
            var selectedGenres = gameRequest.GenresList.Where(b => b.Checked).ToList();
            try
            {

                if (ModelState.IsValid)
                {
                    
                    if (TryUpdateModel(game))
                    {
                        game.Title = gameRequest.Title;
                        game.Price = gameRequest.Price;
                        game.Producer = gameRequest.Producer;
                        game.Summary = gameRequest.Summary;
                        game.PublisherId = gameRequest.PublisherId;
                        game.GameTypeId = gameRequest.GameTypeId;
                        game.Genres.Clear();

                        game.Genres = new List<Genre>();
                        for (int i = 0; i < selectedGenres.Count(); i++)
                        {
                            // joc pe care vrem sa o editam ii asignam genurile selectate 
                            Genre genre = db.Genres.Find(selectedGenres[i].Id);
                            game.Genres.Add(genre);
                        }
                            db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(gameRequest);
            }
            catch (Exception e)
            {
                return View(gameRequest);
            }
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Game game = db.Games.Find(id);
            
            if (game != null)
            {
                db.Games.Remove(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the game with id " + id.ToString());
        }
        [NonAction] // specificam faptul ca nu este o actiune
        public IEnumerable<SelectListItem> GetAllGameTypes()
        {
            // generam o lista goala
            var selectList = new List<SelectListItem>();
            foreach (var format in db.GameTypes.ToList())
            {
                // adaugam in lista elementele necesare pt dropdown
                selectList.Add(new SelectListItem
                {
                    Value = format.GameTypeId.ToString(),
                    Text = format.Name
                });
            }
            // returnam lista pentru dropdown
            return selectList;
        }
        [NonAction] 
        public IEnumerable<SelectListItem> GetAllPublishers()
        {
            
            var selectList = new List<SelectListItem>();
            foreach (var format in db.Publishers.ToList())
            {
                
                selectList.Add(new SelectListItem
                {
                    Value = format.PublisherId.ToString(),
                    Text = format.Name
                });
            }
            // returnam lista pentru dropdown
            return selectList;
        }
        [NonAction]
        public List<CheckBoxViewModel> GetAllGenres()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach (var genre in db.Genres.ToList())
            {
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = genre.GenreId,
                    Name = genre.Name,
                    Checked = false
                });
            }
            return checkboxList;
        }
        
    }
}