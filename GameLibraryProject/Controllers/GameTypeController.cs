using GameLibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameLibraryProject.Models.MyDatabaseInitializer;

namespace GameLibraryProject.Controllers
{
    [AllowAnonymous]
    public class GameTypeController : Controller
    {
        private DbCtx db = new DbCtx();
        public ActionResult Index()
        {
            ViewBag.GameTypes = db.GameTypes.ToList();
            return View();
        }
        public ActionResult New()
        {
            GameType gameType = new GameType();
            return View(gameType);
        }
        [HttpPost]
        public ActionResult New(GameType gameTypeRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.GameTypes.Add(gameTypeRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(gameTypeRequest);
            }
            catch (Exception e)
            {
                return View(gameTypeRequest);
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                GameType gameType = db.GameTypes.Find(id);
                if (gameType == null)
                {
                    return HttpNotFound("Couldn't find the game type with id " + id.ToString() + "!");
                }
                return View(gameType);
            }
            return HttpNotFound("Couldn't find the game type with id " + id.ToString() + "!");
        }
        [HttpPut]
        public ActionResult Edit(int id, GameType gameTypeRequestor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GameType gameType = db.GameTypes.Find(id);
                    if (TryUpdateModel(gameType))
                    {
                        gameType.Name = gameTypeRequestor.Name;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(gameTypeRequestor);
            }
            catch (Exception e)
            {
                return View(gameTypeRequestor);
            }
        }
        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                GameType gameType = db.GameTypes.Find(id);
                if (gameType != null)
                {
                    db.GameTypes.Remove(gameType);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound("Couldn't find the game type with id " + id.ToString() + "!");
            }
            return HttpNotFound("Game type id parameter is missing!");
        }
    }
}
