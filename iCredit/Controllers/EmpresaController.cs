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
    public class EmpresaController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();

        // GET: Empresa
        public ActionResult Index(string campos1, string filtro1, string currentCampo, string currentFilter,string sortOrder, int? page)
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
              //            listaFiltro.Add(new SelectListItem { Text = "EmpresaId", Value = "EmpresaId" });
              //ViewBag.SortEmpresaId = sortOrder == "EmpresaId" ? "EmpresaId_Desc" : "EmpresaId";
						  listaFiltro.Add(new SelectListItem { Text = "Nit", Value = "Nit" });
			  ViewBag.SortNit = sortOrder == "Nit" ? "Nit_Desc" : "Nit";
						  listaFiltro.Add(new SelectListItem { Text = "Nombre", Value = "Nombre" });
			  ViewBag.SortNombre = sortOrder == "Nombre" ? "Nombre_Desc" : "Nombre";
             
            listaFiltro.Add(new SelectListItem { Text = "Email", Value = "EmpEmail" });
            ViewBag.SortEmail = sortOrder == "Email" ? "Email_Desc" : "Email";
						  listaFiltro.Add(new SelectListItem { Text = "Direccion", Value = "Direccion" });
			  ViewBag.SortDireccion = sortOrder == "Direccion" ? "Direccion_Desc" : "Direccion";
						  listaFiltro.Add(new SelectListItem { Text = "Telefono", Value = "Telefono" });
			  ViewBag.SortTelefono = sortOrder == "Telefono" ? "Telefono_Desc" : "Telefono";
						  listaFiltro.Add(new SelectListItem { Text = "Estado", Value = "Estado" });
			  ViewBag.SortEstado = sortOrder == "Estado" ? "Estado_Desc" : "Estado";
			            ViewBag.campos1 = listaFiltro;
            var q = "select * from empresa where empresaId='"+empresaId.ToString()+"'";
            List<empresa> lista;
            if (!String.IsNullOrEmpty(campos1) && !String.IsNullOrEmpty(filtro1))
            {
                 if (!campos1.ToUpper().Equals("ESTADO"))
                    q = q + " and upper(" + campos1 + ") like '%" + filtro1.Trim().ToUpper() + "%'";
                else
                    q = q + " and (CASE WHEN estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END)= '" + filtro1.Trim().ToUpper() + "'";

                lista = db.Database.SqlQuery< empresa >(q).ToList();
            }
            else
			{
                lista=db.empresa.Where(e=>e.EmpresaId==empresaId).ToList();
								
				switch (sortOrder)
                {

								  case "EmpresaId":
					lista = lista.OrderBy(s => s.EmpresaId).ToList();
					break;
				   
				   case "EmpresaId_Desc":
					lista = lista.OrderByDescending(s => s.EmpresaId).ToList();
					break;
				 case "Nit":
					lista = lista.OrderBy(s => s.Nit).ToList();
					break;
				   
				   case "Nit_Desc":
					lista = lista.OrderByDescending(s => s.Nit).ToList();
					break;
				
                    case "Nombre":
					lista = lista.OrderBy(s => s.Nombre).ToList();
					break;
				   
				   case "Nombre_Desc":
					lista = lista.OrderByDescending(s => s.Nombre).ToList();
					break;

                   case "Email":
                    lista = lista.OrderBy(s => s.EmpEmail).ToList();
                    break;

                   case "Email_Desc":
                    lista = lista.OrderByDescending(s => s.EmpEmail).ToList();
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

        // GET: Empresa/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string idDecrypted = MiUtil.desEncriptar(id);
            int intId = Convert.ToInt32(idDecrypted);

            empresa empresa = db.empresa.Find(intId);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: Empresa/Create
        public ActionResult Create()
        {
            int mesActual = DateTime.Now.Month, anioActual = DateTime.Now.Year;
            ViewBag.EmpMesActual = MiUtil.getMeses(mesActual);
            empresa e = new empresa();
            e.EmpAnioActual = anioActual;
            e.EmpMesActual = mesActual;
            return View(e);
        }

        // POST: Empresa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpresaId,Nit,Nombre,Direccion,Telefono,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion,LogoUrl,EmpEmail")] empresa empresa,
             HttpPostedFileBase LogoUrl,string controlador)
        {
            if (ModelState.IsValid)
            {
                if (this.validarImagen(LogoUrl, "AlcLogo"))
                { 
                    db.empresa.Add(empresa);
                    db.SaveChanges();
                    this.guardarImagen(empresa, "LogoUrl", LogoUrl);
                    if (controlador.Equals("Home"))
                    { 
                        ViewBag.tipo = "bg-primary";
                        ViewBag.mensaje = "La Empresa " + empresa.Nombre + " fue creada exisotamente. Recibira al correo electronico las credenciales de autenticación para ingresar a nuestra Plataforma";
                        return View("../Shared/Mensaje");
                    }
                    

                    return RedirectToAction("Index");
                }
            }
            if (controlador.Equals("Home"))
                return RedirectToAction("Index", "Home");
            return View(empresa);
        }


        public bool validarImagen(HttpPostedFileBase imagen, string campo)
        {
            if (imagen != null)
            {
                var supportedTypes = new[] { "jpg", "jpeg", "png" };
                var fileExt = System.IO.Path.GetExtension(imagen.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    ModelState.AddModelError(campo, "Tipo de archivo invalido. Solo extensiones (jpg, jpeg, png) son soportadas.");
                    return false;

                }
            }
            return true;
        }

        public void guardarImagen(empresa empresa, string campo, HttpPostedFileBase imagen)
        {
            string path = @"~\Uploads\Logos\";
            switch (campo)
            {
                case "LogoUrl":
                    // alcaldia.AlcLogo = "";
                    if (imagen != null)
                    {
                        var fileExt = System.IO.Path.GetExtension(imagen.FileName).Substring(1);
                        imagen.SaveAs(Server.MapPath(path + "LogoUrl" + empresa.EmpresaId.ToString() + "." + fileExt));
                        empresa.LogoUrl = "LogoUrl" + empresa.EmpresaId.ToString() + "." + fileExt;
                    }
                    break;
                //case "AlcPieDePagina":
                //    //  alcaldia.AlcPieDePagina = "";
                //    if (imagen != null)
                //    {
                //        var fileExt = System.IO.Path.GetExtension(imagen.FileName).Substring(1);
                //        imagen.SaveAs(Server.MapPath(path + "AlcPieDePagina" + alcaldia.AlcaldiaId.ToString() + "." + fileExt));
                //        alcaldia.AlcPieDePagina = "AlcPieDePagina" + alcaldia.AlcaldiaId.ToString() + "." + fileExt;
                //    }
                //    break;
            }

            db.Entry(empresa).State = EntityState.Modified;
            db.SaveChanges();
        }

        // GET: Empresa/Edit/5
        public ActionResult Edit(string id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string idDecrypted = MiUtil.desEncriptar(id);
            int intId = Convert.ToInt32(idDecrypted);

            empresa empresa = db.empresa.Find(intId);
            
           // int mesActual = DateTime.Now.Month, anioActual = DateTime.Now.Year;
            ViewBag.EmpMesActual = MiUtil.getMeses(empresa.EmpMesActual);

            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpresaId,Nit,Nombre,Direccion,Telefono,EmpEmail,LogoUrl,EmpAnioActual,EmpMesActual,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] empresa empresa,
            HttpPostedFileBase LogoUrl)
        {
            if (ModelState.IsValid)
            {
                this.guardarImagen(empresa, "LogoUrl", LogoUrl);
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            int mesActual = DateTime.Now.Month;
            ViewBag.EmpMesActual = MiUtil.getMeses(mesActual);
            return View(empresa);
        }

        // GET: Empresa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresa empresa = db.empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            empresa empresa = db.empresa.Find(id);
            db.empresa.Remove(empresa);
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
