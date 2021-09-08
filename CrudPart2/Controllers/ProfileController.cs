using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudPart2.Context;

namespace CrudPart2.Controllers
{
    public class ProfileController : Controller
    {
        dbCobaEntities dbCoba = new dbCobaEntities();

        // GET: Profile
        public ActionResult Profile(profile obj)
        {
                return View(obj);

        }
        [HttpPost]
        public ActionResult InsertProfile(profile model)
        {
            profile prof = new profile();
            if (ModelState.IsValid) {
                prof.ID = model.ID;
                prof.Name = model.Name;
                prof.Email = model.Email;
                prof.Address = model.Address;
                if (model.ID == 0)
                {
                    dbCoba.profiles.Add(prof);
                    dbCoba.SaveChanges();
                }
                else {
                    dbCoba.Entry(prof).State = EntityState.Modified;
                    dbCoba.SaveChanges();
                }
                ModelState.Clear();
            }
            return RedirectToAction("ProfileList");
        }

        public ActionResult ProfileList() 
        {

            var res = dbCoba.profiles.ToList();
            return View(res);
        }
        public ActionResult DeleteProfile(int id) {
            var res = dbCoba.profiles.Where(x => x.ID == id).First();
            dbCoba.profiles.Remove(res);
            dbCoba.SaveChanges();

            return RedirectToAction("ProfileList");
            
        }
    }
}