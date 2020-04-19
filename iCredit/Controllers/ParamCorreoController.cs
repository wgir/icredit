using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrediAdmin.Models;
using CrediAdmin.Util;

namespace CrediAdmin.Controllers
{
    [SessionExpire]
    [Authorize]
    public class ParamCorreoController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();

        //
        // GET: /ParamCorreo/

        public ViewResult Index()
        {
           // var e = db.empresa.Where(u => u.Estado == true);
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);

            
            var paramcorreos = db.paramcorreo.Include(p => p.empresa).Where(em => em.EmpresaId==empresaId);
            ViewBag.CantParamCorreos = paramcorreos.Count();

            return View(paramcorreos.ToList());
        }

        //
        // GET: /ParamCorreo/Details/5

        public ViewResult Details(string id)
        {
            string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(id));
            int intId = Convert.ToInt32(idDecrypted);
            paramcorreo paramcorreo = db.paramcorreo.Find(intId);
            return View(paramcorreo);
        }

        //
        // GET: /ParamCorreo/Create

        public ActionResult Create()
        {
            var e = db.empresa.Where(u => u.Estado == true);
            int empresaId = Convert.ToInt32(Session["EmpresaId"]);
            ViewBag.EmpresaId = new SelectList(e.Where(u => u.EmpresaId == empresaId), "EmpresaId", "Nombre", empresaId);

            //ViewBag.EmpresaId = new SelectList(db.Empresas, "EmpresaId", "Nombre");
            return View(new paramcorreo());
        } 

        //
        // POST: /ParamCorreo/Create

        [HttpPost]
        public ActionResult Create(paramcorreo paramcorreo)
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            paramcorreo.EmpresaId = empresaId;
            if (ModelState.IsValid)
            {
                db.paramcorreo.Add(paramcorreo);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            var e = db.empresa.Where(u => u.Estado == true);
           // int empresaId = Convert.ToInt32(Session["EmpresaId"]);
            ViewBag.EmpresaId = new SelectList(e.Where(u => u.EmpresaId == empresaId), "EmpresaId", "Nombre", empresaId);

            //ViewBag.EmpresaId = new SelectList(db.Empresas, "EmpresaId", "Nombre", paramcorreo.EmpresaId);
            return View(paramcorreo);
        }
        
        //
        // GET: /ParamCorreo/Edit/5
 
        public ActionResult Edit(string id)
        {
            //int empresaId = 0;
            //if (Session["EmpresaId"] != null)
            //    Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            
            string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(id));
            int intId = Convert.ToInt32(idDecrypted);

            paramcorreo paramcorreo = db.paramcorreo.Find(intId);
            var e = db.empresa.Where(u => u.Estado == true);
           // int empresaId = Convert.ToInt32(Session["EmpresaId"]);
          //  ViewBag.EmpresaId = new SelectList(e.Where(u => u.EmpresaId == empresaId), "EmpresaId", "Nombre", empresaId);

            //ViewBag.EmpresaId = new SelectList(db.Empresas, "EmpresaId", "Nombre", paramcorreo.EmpresaId);
            return View(paramcorreo);
        }

        //
        // POST: /ParamCorreo/Edit/5

        [HttpPost]
        public ActionResult Edit(paramcorreo paramcorreo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paramcorreo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var e = db.empresa.Where(u => u.Estado == true);
            int empresaId = Convert.ToInt32(Session["EmpresaId"]);
            ViewBag.EmpresaId = new SelectList(e.Where(u => u.EmpresaId == empresaId), "EmpresaId", "Nombre", empresaId);

            //ViewBag.EmpresaId = new SelectList(db.Empresas, "EmpresaId", "Nombre", paramcorreo.EmpresaId);
            return View(paramcorreo);
        }

        //
        // GET: /ParamCorreo/Delete/5
 
        public ActionResult Delete(int id)
        {
            paramcorreo paramcorreo = db.paramcorreo.Find(id);
            return View(paramcorreo);
        }

        //
        // POST: /ParamCorreo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            paramcorreo paramcorreo = db.paramcorreo.Find(id);
            db.paramcorreo.Remove(paramcorreo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}