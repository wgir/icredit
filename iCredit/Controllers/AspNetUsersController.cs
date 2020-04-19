using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrediAdmin.Models;
using PagedList;
using System.Configuration;

namespace ORGDocentes.Controllers
{
    [Authorize]
    public class AspNetUsersController : Controller
    {
        //private BaseDatosContext db = new BaseDatosContext();
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: AspNetUsers
        public ActionResult Index()
        {
            var users = context.Users.OrderBy(u=>u.UserName).ToList();  
            return View(users);

    		//return  View(lista);
        }

        // GET: AspNetUsers/Details/5
        //public ActionResult Details(string id)
        //{
        //    //if (id == null)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}
        //    //AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
        //    //if (aspNetUsers == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}
        //    //return View(aspNetUsers);
           
        //}

        // GET: AspNetUsers/Create
        public ActionResult Create()
        {
            return RedirectToAction("Register", "Account");
        }

        //// POST: AspNetUsers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Email,UserName")] AspNetUsers aspNetUsers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.AspNetUsers.Add(aspNetUsers);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(aspNetUsers);
        //}

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            return RedirectToAction("ChangePassword", "Manage");
        }

        //// POST: AspNetUsers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Email,UserName")] AspNetUsers aspNetUsers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(aspNetUsers).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(aspNetUsers);
        //}

        //// GET: AspNetUsers/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
        //    if (aspNetUsers == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aspNetUsers);
        //}

        //// POST: AspNetUsers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
        //    db.AspNetUsers.Remove(aspNetUsers);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
