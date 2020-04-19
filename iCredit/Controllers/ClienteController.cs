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
using CrediAdmin.Util;

namespace CrediAdmin.Controllers
{
    [SessionExpire]
    [Authorize]
    public class ClienteController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();

        // GET: Cliente
        public ActionResult Index(string campos1, string filtro1, string currentCampo, string currentFilter, string sortOrder, int? page)
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);

            ViewBag.CurrentSort = sortOrder;
            if (filtro1 != null)
                page = 1;
            else
            {
                campos1 = currentCampo;
                filtro1 = currentFilter;
            }
            campos1 = String.IsNullOrEmpty(campos1) ? "" : campos1;
            ViewBag.CurrentCampo = campos1;
            ViewBag.CurrentFilter = filtro1;

            List<SelectListItem> listaFiltro = new List<SelectListItem>();
            //            listaFiltro.Add(new SelectListItem { Text = "ClienteId", Value = "ClienteId" });
            //ViewBag.SortClienteId = sortOrder == "ClienteId" ? "ClienteId_Desc" : "ClienteId";
            listaFiltro.Add(new SelectListItem { Text = "Nit", Value = "Nit" });
            ViewBag.SortNit = sortOrder == "Nit" ? "Nit_Desc" : "Nit";
            listaFiltro.Add(new SelectListItem { Text = "Nombre", Value = "Nombre" });
            ViewBag.SortNombre = sortOrder == "Nombre" ? "Nombre_Desc" : "Nombre";
            listaFiltro.Add(new SelectListItem { Text = "Ciudad", Value = "ciudad.Nombre" });
            ViewBag.SortCiudad = sortOrder == "Ciudad" ? "Ciudad_Desc" : "Ciudad";
            listaFiltro.Add(new SelectListItem { Text = "Direccion", Value = "Direccion" });
            ViewBag.SortDireccion = sortOrder == "Direccion" ? "Direccion_Desc" : "Direccion";
            listaFiltro.Add(new SelectListItem { Text = "Telefono", Value = "Telefono" });
            ViewBag.SortTelefono = sortOrder == "Telefono" ? "Telefono_Desc" : "Telefono";
            listaFiltro.Add(new SelectListItem { Text = "Email", Value = "Email" });
            ViewBag.SortEmail = sortOrder == "Email" ? "Email_Desc" : "Email";
            //            listaFiltro.Add(new SelectListItem { Text = "EmpresaId", Value = "EmpresaId" });
            //ViewBag.SortEmpresaId = sortOrder == "EmpresaId" ? "EmpresaId_Desc" : "EmpresaId";
            listaFiltro.Add(new SelectListItem { Text = "Estado", Value = "Estado" });
            ViewBag.SortEstado = sortOrder == "Estado" ? "Estado_Desc" : "Estado";
            ViewBag.campos1 = listaFiltro;
            var q = "select * from cliente inner join ciudad on cliete.ciudadId=ciudad.CiudadId where empresaId='" + empresaId.ToString() + "'";
            List<cliente> lista;
            if (!String.IsNullOrEmpty(campos1) && !String.IsNullOrEmpty(filtro1))
            {
                if (!campos1.ToUpper().Equals("ESTADO"))
                    q = q + " and upper(" + campos1 + ") like '%" + filtro1.Trim().ToUpper() + "%'";
                else
                    q = q + " and (CASE WHEN estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END)= '" + filtro1.Trim().ToUpper() + "'";

                lista = db.Database.SqlQuery<cliente>(q).ToList();
                foreach (cliente c in lista)
                    c.ciudad = db.ciudad.Find(c.CiudadId);
            }
            else
            {
                var cliente = db.cliente.Include(c => c.empresa).Where(c=>c.EmpresaId==empresaId);
                lista = cliente.ToList();

                switch (sortOrder)
                {

                    case "ClienteId":
                        lista = lista.OrderBy(s => s.ClienteId).ToList();
                        break;

                    case "ClienteId_Desc":
                        lista = lista.OrderByDescending(s => s.ClienteId).ToList();
                        break;
                    case "Nit":
                        lista = lista.OrderBy(s => s.Nit).ToList();
                        break;

                    case "Nit_Desc":
                        lista = lista.OrderByDescending(s => s.Nit).ToList();
                        break;

                    case "Ciudad":
                        lista = lista.OrderBy(s => s.ciudad.Nombre).ToList();
                        break;

                    case "Ciudad_Desc":
                        lista = lista.OrderByDescending(s => s.ciudad.Nombre).ToList();
                        break;

                    case "Nombre":
                        lista = lista.OrderBy(s => s.Nombre).ToList();
                        break;

                    case "Nombre_Desc":
                        lista = lista.OrderByDescending(s => s.Nombre).ToList();
                        break;
                    case "Direccion":
                        lista = lista.OrderBy(s => s.Direccion).ToList();
                        break;

                    case "Direccion_Desc":
                        lista = lista.OrderByDescending(s => s.Direccion).ToList();
                        break;
                    case "Telefono":
                        lista = lista.OrderBy(s => s.Telefono).ToList();
                        break;

                    case "Telefono_Desc":
                        lista = lista.OrderByDescending(s => s.Telefono).ToList();
                        break;
                    case "Email":
                        lista = lista.OrderBy(s => s.Email).ToList();
                        break;

                    case "Email_Desc":
                        lista = lista.OrderByDescending(s => s.Email).ToList();
                        break;
                    case "EmpresaId":
                        lista = lista.OrderBy(s => s.EmpresaId).ToList();
                        break;

                    case "EmpresaId_Desc":
                        lista = lista.OrderByDescending(s => s.EmpresaId).ToList();
                        break;
                    case "Estado":
                        lista = lista.OrderBy(s => s.Estado).ToList();
                        break;

                    case "Estado_Desc":
                        lista = lista.OrderByDescending(s => s.Estado).ToList();
                        break;
                }
            }

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["RegistrosPorPagina"].ToString());
            int pageNumber = (page ?? 1);
            return View(lista.ToPagedList(pageNumber, pageSize));

            //return  View(lista);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string idDecrypted = MiUtil.desEncriptar(id);
            int intId = Convert.ToInt32(idDecrypted);

            cliente cliente = db.cliente.Find(intId);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c => c.Estado == true).OrderBy(e => e.Nit), "EmpresaId", "Nit");
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            cliente cl = new cliente();
            cl.EmpresaId = empresaId;
            ViewBag.CiudadId = new SelectList(db.ciudad.Where(c => c.Estado == true && c.EmpresaId==empresaId).OrderBy(e => e.Nombre), "CiudadId", "Nombre");
            return View(cl);
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClienteId,Nit,Nombre,Direccion,Telefono,Email,EmpresaId,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion,CiudadId")] cliente cliente)
        {
            //int empresaId = 0;
            //if (Session["EmpresaId"] != null)
            //    Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            //cliente.EmpresaId = empresaId;

            if (ModelState.IsValid)
            {
                db.cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c => c.Estado == true && c.EmpresaId == cliente.EmpresaId).OrderBy(e => e.Nit), "EmpresaId", "Nit", cliente.EmpresaId);
            ViewBag.CiudadId = new SelectList(db.ciudad.Where(c => c.Estado == true && c.EmpresaId == cliente.EmpresaId).OrderBy(e => e.Nombre), "CiudadId", "Nombre", cliente.EmpresaId);
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(id));
            int intId = Convert.ToInt32(idDecrypted);
            cliente cliente = db.cliente.Find(intId);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            //ViewBag.EmpresaId = new SelectList(db.empresa.Where(c => c.Estado == true).OrderBy(e => e.Nit), "EmpresaId", "Nit", cliente.EmpresaId);
            ViewBag.CiudadId = new SelectList(db.ciudad.Where(c => c.Estado == true && c.EmpresaId==cliente.EmpresaId).OrderBy(e => e.Nombre), "CiudadId", "Nombre", cliente.CiudadId);
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClienteId,Nit,Nombre,Direccion,Telefono,Email,EmpresaId,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion,CiudadId")] cliente cliente)
        {

            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c => c.Estado == true).OrderBy(e => e.Nit), "EmpresaId", "Nit", cliente.EmpresaId);
            ViewBag.CiudadId = new SelectList(db.ciudad.Where(c => c.Estado == true && c.EmpresaId == cliente.EmpresaId).OrderBy(e => e.Nombre), "CiudadId", "Nombre", cliente.EmpresaId);
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = db.cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cliente cliente = db.cliente.Find(id);
            db.cliente.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
