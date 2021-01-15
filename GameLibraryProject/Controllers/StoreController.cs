using GameLibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using GameLibraryProject.Models.MyDatabaseInitializer;

namespace GameLibraryProject.Controllers
{
    [AllowAnonymous]
    public class StoreController : Controller
    {
        private DbCtx db = new DbCtx();
        public ActionResult Index()
        {
            ViewBag.Stores = db.Stores.ToList();
            return View();
        }
        public ActionResult New()
        {
           
            StoreManagerViewModel store = new StoreManagerViewModel();
            return View(store);
        }
        [HttpPost]
        public ActionResult New(StoreManagerViewModel storeRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Manager manager = new Manager
                    {   
                        NameM = storeRequest.NameM,
                        Surname = storeRequest.Surname,
                        PhoneNumber = storeRequest.PhoneNumber
                    };
                    db.Managers.Add(manager);
                    Store store = new Store
                    {
                        Name = storeRequest.Name,
                        Manager = manager
                    };
                    db.Stores.Add(store);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(storeRequest);
            }
            catch (Exception e)
            {
                return View(storeRequest);
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {   
                
                Store store = db.Stores.Find(id);
                
                
                    if (store == null)
                {
                    return HttpNotFound("Couldn't find the store with id " + id.ToString() + "!");
                }

                StoreManagerViewModel storeM = new StoreManagerViewModel
                {

                    Name = store.Name,
                    NameM = store.Manager.NameM,
                    Surname = store.Manager.Surname,
                    PhoneNumber = store.Manager.PhoneNumber,
                    
                };
                System.Diagnostics.Debug.Write("am ajuns");
                return View(storeM);
            }
            return HttpNotFound("Couldn't find the store with id " + id.ToString() + "!");
        }
        [HttpPut]
        public ActionResult Edit(int id, StoreManagerViewModel storeRequestor)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Store store = db.Stores.Find(id);
                    if (TryUpdateModel(storeRequestor))
                    {


                        Manager manager = db.Managers.Find(store.Manager.ManagerId);
                        manager.NameM = storeRequestor.NameM;
                        manager.Surname = storeRequestor.Surname;
                        manager.PhoneNumber = storeRequestor.PhoneNumber;
                        //db.Managers.Add(manager);
                        store.Name = storeRequestor.Name;
                        store.Manager = manager;
                        System.Diagnostics.Debug.WriteLine("eroare save changes");
                        //db.Stores.Add(store);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }

                return View(storeRequestor);
            }
            catch (Exception e)
            {
                return View(storeRequestor);
            }
        }
        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Store store = db.Stores.Find(id);
                Manager manager = db.Managers.Find(store.Manager.ManagerId);
                if (store != null)
                {   
                    db.Managers.Remove(manager);
                    db.Stores.Remove(store);
                    
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound("Couldn't find the store with id " + id.ToString() + "!");
            }
            return HttpNotFound("Store id parameter is missing!");
        }
    }
}
