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
using Microsoft.AspNet.Identity.Owin;


namespace CrediAdmin.Controllers
{
    [SessionExpire]
    [Authorize]
    public class UsuarioController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();
        ApplicationDbContext context = new ApplicationDbContext();

       // private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: Usuario
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
              //            listaFiltro.Add(new SelectListItem { Text = "UsuarioId", Value = "UsuarioId" });
              //ViewBag.SortUsuarioId = sortOrder == "UsuarioId" ? "UsuarioId_Desc" : "UsuarioId";
						  listaFiltro.Add(new SelectListItem { Text = "Documento", Value = "UsuDocumento" });
			  ViewBag.SortUsuDocumento = sortOrder == "UsuDocumento" ? "UsuDocumento_Desc" : "UsuDocumento";
						  listaFiltro.Add(new SelectListItem { Text = "Nombre", Value = "UsuNombre" });
			  ViewBag.SortUsuNombre = sortOrder == "UsuNombre" ? "UsuNombre_Desc" : "UsuNombre";
						  listaFiltro.Add(new SelectListItem { Text = "Telefono", Value = "UsuTelefono" });
			  ViewBag.SortUsuTelefono = sortOrder == "UsuTelefono" ? "UsuTelefono_Desc" : "UsuTelefono";
              
            listaFiltro.Add(new SelectListItem { Text = "Email", Value = "UsuEmail" });
            ViewBag.SortaspnetusersId = sortOrder == "UsuEmail" ? "UsuEmail_Desc" : "UsuEmail";
    				  listaFiltro.Add(new SelectListItem { Text = "Estado", Value = "Estado" });
			  ViewBag.SortEstado = sortOrder == "Estado" ? "Estado_Desc" : "Estado";
			            ViewBag.campos1 = listaFiltro;
            var q = "select * from usuario where empresaId='"+empresaId.ToString()+"'";
            List<usuario> lista;
            if (!String.IsNullOrEmpty(campos1) && !String.IsNullOrEmpty(filtro1))
            {
                 if (!campos1.ToUpper().Equals("ESTADO"))
                    q = q + " and upper(" + campos1 + ") like '%" + filtro1.Trim().ToUpper() + "%'";
                else
                    q = q + " and (CASE WHEN estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END)= '" + filtro1.Trim().ToUpper() + "'";

                lista = db.Database.SqlQuery< usuario >(q).ToList();
            }
            else
			{
                							var usuario = db.usuario.Include(u => u.empresa).Where(u=>u.EmpresaId==empresaId);
											lista=usuario.ToList();
								
				switch (sortOrder)
                {

								  case "UsuarioId":
					lista = lista.OrderBy(s => s.UsuarioId).ToList();
					break;
				   
				   case "UsuarioId_Desc":
					lista = lista.OrderByDescending(s => s.UsuarioId).ToList();
					break;
								  case "UsuDocumento":
					lista = lista.OrderBy(s => s.UsuDocumento).ToList();
					break;
				   
				   case "UsuDocumento_Desc":
					lista = lista.OrderByDescending(s => s.UsuDocumento).ToList();
					break;
								  case "UsuNombre":
					lista = lista.OrderBy(s => s.UsuNombre).ToList();
					break;
				   
				   case "UsuNombre_Desc":
					lista = lista.OrderByDescending(s => s.UsuNombre).ToList();
					break;
								  case "UsuTelefono":
					lista = lista.OrderBy(s => s.UsuTelefono).ToList();
					break;
				   
				   case "UsuTelefono_Desc":
					lista = lista.OrderByDescending(s => s.UsuTelefono).ToList();
					break;
                   case "UsuEmail":
                    lista = lista.OrderBy(s => s.UsuEmail).ToList();
					break;

                                  case "UsuEmail_Desc":
                    lista = lista.OrderByDescending(s => s.UsuEmail).ToList();
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

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            if(empresaId==1)
              ViewBag.RoleId = new SelectList(context.Roles.OrderBy(r => r.Name).ToList(), "Name", "Name");
            else
              ViewBag.RoleId = new SelectList(context.Roles.Where(r => r.Name != "Master").OrderBy(r => r.Name).ToList(), "Name", "Name");

            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit");
            usuario u = new usuario();
            u.EmpresaId = empresaId;
            return View(u);
        }

        // POST: Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioId,UsuDocumento,UsuNombre,UsuEmail,UsuTelefono,aspnetusersId,EmpresaId,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] usuario usuario,
             string RoleId)
        {
            if(RoleId.Equals(""))
                ModelState.AddModelError("RoleId", String.Format("Debe seleccionar un Roll"));
            if (db.aspnetusers.Any(c => c.UserName.Trim().ToUpper() == usuario.UsuEmail))
                ModelState.AddModelError("", String.Format("Este correo ya se encuentra registrado"));

            if (ModelState.IsValid)
            {
                        HomeController home = new HomeController();
                        string[] arr1 = home.crearUsuario(RoleId, usuario.UsuEmail, UserManager).Split(',');
                        string AspUserId = arr1[0];
                        string AspUserPasswd = arr1[1];
                        if (!AspUserId.Equals(""))
                        {
                            string mensaje = "<p <span style=\"font-size: 10pt;\">Apreciado&nbsp;" + usuario.UsuNombre + "</span></p>";
                            mensaje = mensaje + "<p <span style=\"font-size: 10pt;\">Ingresando a&nbsp;<a style=\"font-family: Calibri, sans-serif; font-size: 11pt;\" href=\"http://google.com\">CrediAdmin.com</a><span style=\"color: #1f497d; font-family: Calibri, sans-serif;\">,&nbsp;</span><span style=\"color: #1f497d; font-family: Calibri, sans-serif;\">&nbsp;</span>podr&aacute;s utilizar nuestra plataforma.</span></p>";
                            mensaje = mensaje + "<p <span style=\"font-size: 10pt;\">Nombre de Usuario: " + usuario.UsuEmail + "</span></p>";
                            mensaje = mensaje + "<p <span style=\"font-size: 10pt;\">Clave: " + AspUserPasswd + "</span></p>";
                            mensaje = mensaje + "<p><span style=\"font-size: 10pt;\">&nbsp;</span></p>";

                            //Nombre + "" + AspUserPasswd;
                            if (EnviarCorreoController.enviarCorreo(1, usuario.UsuEmail, "Bienvenido a CrediAdmin", mensaje))
                            {
                            usuario.aspnetusersId = AspUserId;
                            db.usuario.Add(usuario);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                             }else{
                                    home.eliminarUsuarioAspNet(AspUserId);
                                    ModelState.AddModelError("UsuEmail", String.Format("No fue posible enviar correo electronico con credenciales"));
                                   }
                        }else
                            ModelState.AddModelError("RoleId", String.Format("No fue posible crear usuario, vuelva a intentarlo"));
                
            }
            if (usuario.EmpresaId == 1)
                ViewBag.RoleId = new SelectList(context.Roles.OrderBy(r => r.Name).ToList(), "Name", "Name");
            else
                ViewBag.RoleId = new SelectList(context.Roles.Where(r => r.Name != "Master").OrderBy(r => r.Name).ToList(), "Name", "Name");

            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", usuario.EmpresaId);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(id));
            int intId = Convert.ToInt32(idDecrypted);
            usuario usuario = db.usuario.Find(intId);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            var user = db.aspnetusers.Find(usuario.aspnetusersId);
            string rolActual = user.aspnetroles.FirstOrDefault().Name;

            if (usuario.EmpresaId == 1)
                ViewBag.RoleId = new SelectList(context.Roles.OrderBy(r => r.Name).ToList(), "Name", "Name", rolActual);
            else
                ViewBag.RoleId = new SelectList(context.Roles.Where(r => r.Name != "Master").OrderBy(r => r.Name).ToList(), "Name", "Name", rolActual);

            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", usuario.EmpresaId);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioId,UsuDocumento,UsuNombre,UsuEmail,UsuTelefono,aspnetusersId,EmpresaId,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] usuario usuario,
            string RoleId)
        {

            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                
                var user = UserManager.Users.FirstOrDefault(u=>u.Id.Equals(usuario.aspnetusersId));
                if (user != null)
                {
                    user.UserName = usuario.UsuEmail;
                    user.Email = usuario.UsuEmail;
                    UserManager.UpdateAsync(user);
                }
                //.FindByIdAsync(usuario.aspnetusersId);
                //UserManager.
                //user.

                return RedirectToAction("Index");
            }
            if (usuario.EmpresaId == 1)
                ViewBag.RoleId = new SelectList(context.Roles.OrderBy(r => r.Name).ToList(), "Name", "Name", RoleId);
            else
                ViewBag.RoleId = new SelectList(context.Roles.Where(r => r.Name != "Master").OrderBy(r => r.Name).ToList(), "Name", "Name", RoleId);

            //ViewBag.RoleId = new SelectList(context.Roles.OrderBy(r => r.Name).ToList(), "Name", "Name", RoleId);
            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", usuario.EmpresaId);
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuario usuario = db.usuario.Find(id);
            db.usuario.Remove(usuario);
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
