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
//    [Authorize]
    public class ContactoController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();

        // GET: Contacto
        public ActionResult Index(string campos1, string filtro1, string currentCampo, string currentFilter,string sortOrder, int? page)
        {
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
						  listaFiltro.Add(new SelectListItem { Text = "ContactoId", Value = "ContactoId" });
			  ViewBag.SortContactoId = sortOrder == "ContactoId" ? "ContactoId_Desc" : "ContactoId";
						  listaFiltro.Add(new SelectListItem { Text = "ConNombre", Value = "ConNombre" });
			  ViewBag.SortConNombre = sortOrder == "ConNombre" ? "ConNombre_Desc" : "ConNombre";
						  listaFiltro.Add(new SelectListItem { Text = "ConTelefono", Value = "ConTelefono" });
			  ViewBag.SortConTelefono = sortOrder == "ConTelefono" ? "ConTelefono_Desc" : "ConTelefono";
						  listaFiltro.Add(new SelectListItem { Text = "ConEmail", Value = "ConEmail" });
			  ViewBag.SortConEmail = sortOrder == "ConEmail" ? "ConEmail_Desc" : "ConEmail";
						  listaFiltro.Add(new SelectListItem { Text = "ConObserva", Value = "ConObserva" });
			  ViewBag.SortConObserva = sortOrder == "ConObserva" ? "ConObserva_Desc" : "ConObserva";
						  listaFiltro.Add(new SelectListItem { Text = "Estado", Value = "Estado" });
			  ViewBag.SortEstado = sortOrder == "Estado" ? "Estado_Desc" : "Estado";
			            ViewBag.campos1 = listaFiltro;
            var q = "select * from contacto";
            List<contacto> lista;
            if (!String.IsNullOrEmpty(campos1) && !String.IsNullOrEmpty(filtro1))
            {
                 if (!campos1.ToUpper().Equals("ESTADO"))
                    q = q + " where upper(" + campos1 + ") like '%" + filtro1.Trim().ToUpper() + "%'";
                else
                    q = q + " where (CASE WHEN estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END)= '" + filtro1.Trim().ToUpper() + "'";

                lista = db.Database.SqlQuery< contacto >(q).ToList();
            }
            else
			{
                											lista=db.contacto.ToList();
								
				switch (sortOrder)
                {

								  case "ContactoId":
					lista = lista.OrderBy(s => s.ContactoId).ToList();
					break;
				   
				   case "ContactoId_Desc":
					lista = lista.OrderByDescending(s => s.ContactoId).ToList();
					break;
								  case "ConNombre":
					lista = lista.OrderBy(s => s.ConNombre).ToList();
					break;
				   
				   case "ConNombre_Desc":
					lista = lista.OrderByDescending(s => s.ConNombre).ToList();
					break;
								  case "ConTelefono":
					lista = lista.OrderBy(s => s.ConTelefono).ToList();
					break;
				   
				   case "ConTelefono_Desc":
					lista = lista.OrderByDescending(s => s.ConTelefono).ToList();
					break;
								  case "ConEmail":
					lista = lista.OrderBy(s => s.ConEmail).ToList();
					break;
				   
				   case "ConEmail_Desc":
					lista = lista.OrderByDescending(s => s.ConEmail).ToList();
					break;
								  case "ConObserva":
					lista = lista.OrderBy(s => s.ConObserva).ToList();
					break;
				   
				   case "ConObserva_Desc":
					lista = lista.OrderByDescending(s => s.ConObserva).ToList();
					break;
								  case "Estado":
					lista = lista.OrderBy(s => s.Estado).ToList();
					break;
				   
				   case "Estado_Desc":
					lista = lista.OrderByDescending(s => s.Estado).ToList();
					break;
                   default :
                    lista = lista.OrderByDescending(s => s.ContactoId).ToList();
                    break;

				                }
			}

			int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["RegistrosPorPagina"].ToString()); 
            int pageNumber = (page ?? 1);
            return View(lista.ToPagedList(pageNumber, pageSize));

    		//return  View(lista);
        }

        // GET: Contacto/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(id));
            int intId = Convert.ToInt32(idDecrypted);

            contacto contacto = db.contacto.Find(intId);

            if (contacto == null)
            {
                return HttpNotFound();
            }
            return View(contacto);
        }

        // GET: Contacto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactoId,ConNombre,ConTelefono,ConEmail,ConObserva,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion,Estado")] contacto contacto)
        {
            if (ModelState.IsValid)
            {
                db.contacto.Add(contacto);
                db.SaveChanges();
                //ViewBag.mensajeRegistroContacto = "Se registro su observacion o sugerencia exitosamente.";
                return RedirectToAction("Contact", "Home", new { mensaje = "Se registro su observacion o sugerencia exitosamente." });
            }
            //return RedirectToAction("Contact", "Home");
            return View(contacto);
        }

        // GET: Contacto/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(id));
            int intId = Convert.ToInt32(idDecrypted);
            contacto contacto = db.contacto.Find(intId);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            return View(contacto);
        }

        // POST: Contacto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactoId,ConNombre,ConTelefono,ConEmail,ConObserva,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion,Estado")] contacto contacto)
        {

            if (ModelState.IsValid)
            {
                db.Entry(contacto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contacto);
        }

        // GET: Contacto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contacto contacto = db.contacto.Find(id);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            return View(contacto);
        }

        // POST: Contacto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contacto contacto = db.contacto.Find(id);
            db.contacto.Remove(contacto);
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
