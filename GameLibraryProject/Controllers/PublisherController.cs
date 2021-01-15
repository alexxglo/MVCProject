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
    public class PublisherController : Controller
    {
        private DbCtx db = new DbCtx();
        public ActionResult Index()
        {
            ViewBag.Publishers = db.Publishers.ToList();
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Publisher publisher = db.Publishers.Find(id);
                if (publisher != null)
                {
                    ViewBag.Store = db.Stores.Find(publisher.Store.StoreId).Name;
                    return View(publisher);
                }
                return HttpNotFound("Couldn't find the publisher with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing publisher id parameter!");
        }
        public ActionResult New()
        {
            Publisher publisher = new Publisher();
            publisher.StoreNamesList = GetAllStores();
            return View(publisher);
        }
        [HttpPost]
        public ActionResult New(Publisher publisherRequest)
        {
           // publisherRequest.StoreNamesList = GetAllStores();
            try
            {
                if (ModelState.IsValid)
                {
                    Publisher publisher = new Publisher
                    {
                        Name = publisherRequest.Name,
                        Store = publisherRequest.Store
                    };
                    System.Diagnostics.Debug.Print("am ajuns aici");
                    db.Publishers.Add(publisherRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(publisherRequest);
            }
            catch (Exception e)
            {
                return View(publisherRequest);
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Publisher publisher = db.Publishers.Find(id);
                publisher.StoreNamesList = GetAllStores();
                if (publisher == null)
                {
                    return HttpNotFound("Couldn't find the publisher with id " + id.ToString() + "!");
                }
                return View(publisher);
            }
            return HttpNotFound("Couldn't find the publisher with id " + id.ToString() + "!");
        }
        [HttpPut]
        public ActionResult Edit(int id, Publisher publisherRequestor)
        {
            publisherRequestor.StoreNamesList = GetAllStores();
            //Publisher publisher = db.Publishers.Include("Store").SingleOrDefault(b => b.StoreId.Equals(id)); //recent change
            Publisher publisher = db.Publishers.Find(id);
            try
            {
                
                
                if (ModelState.IsValid)
                {
                    //Publisher publisher = db.Publishers.Find(id);
                    if (TryUpdateModel(publisher))
                    {
                        //publisher.Name = publisherRequestor.Name;
                        publisher.Store = publisherRequestor.Store;
                       // publisher.StoreId = publisherRequestor.StoreId;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(publisherRequestor);
            }
            catch (Exception e)
            {
                return View(publisherRequestor);
            }
        }
        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Publisher publisher = db.Publishers.Find(id);
                if (publisher != null)
                {
                    db.Publishers.Remove(publisher);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound("Couldn't find the publisher with id " + id.ToString() + "!");
            }
            return HttpNotFound("Publisher id parameter is missing!");
        }
        [NonAction]
        public IEnumerable<SelectListItem> GetAllStores()
        {

            var selectList = new List<SelectListItem>();
            foreach (var format in db.Stores.ToList())
            {

                selectList.Add(new SelectListItem
                {
                    Value = format.StoreId.ToString(),
                    Text = format.Name
                });
            }
            // returnam lista pentru dropdown
            return selectList;
        }
    }
}