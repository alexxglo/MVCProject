using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameLibraryProject.Models;
using GameLibraryProject.Models.MyDatabaseInitializer;
namespace GameLibraryProject.Controllers
{
    [AllowAnonymous]
    public class GenreController : Controller
    {
        private DbCtx db = new DbCtx();
        public ActionResult Index()
        {
            ViewBag.Genres = db.Genres.ToList();
            return View();
        }
        public ActionResult New()
        {
            Genre genre = new Genre();
            return View(genre);
        }
        [HttpPost]
        public ActionResult New(Genre genreRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Genres.Add(genreRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(genreRequest);
            }
            catch (Exception e)
            {
                return View(genreRequest);
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Genre genre = db.Genres.Find(id);
                if (genre == null)
                {
                    return HttpNotFound("Couldn't find the genre with id " + id.ToString() + "!");
                }
                return View(genre);
            }
            return HttpNotFound("Couldn't find the genre with id " + id.ToString() + "!");
        }
        [HttpPut]
        public ActionResult Edit(int id, Genre genreRequestor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Genre genre = db.Genres.Find(id);
                    if (TryUpdateModel(genre))
                    {
                        genre.Name = genreRequestor.Name;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(genreRequestor);
            }
            catch (Exception e)
            {
                return View(genreRequestor);
            }
        }
        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Genre genre = db.Genres.Find(id);
                if (genre != null)
                {
                    db.Genres.Remove(genre);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound("Couldn't find the genre with id " + id.ToString() + "!");
            }
            return HttpNotFound("Genre id parameter is missing!");
        }
    }
}