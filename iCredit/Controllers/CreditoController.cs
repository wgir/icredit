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
    public class CreditoController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();

        // GET: Credito
        public ActionResult Index(string campos1, string filtro1, string currentCampo, string currentFilter, string sortOrder, int? page,bool? todos=false)
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            ViewBag.todos = todos;
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
            listaFiltro.Add(new SelectListItem { Text = "CreditoId", Value = "CreditoId" });
            ViewBag.SortCreditoId = sortOrder == "CreditoId" ? "CreditoId_Desc" : "CreditoId";
            listaFiltro.Add(new SelectListItem { Text = "Fecha", Value = "Fecha" });
            ViewBag.SortFecha = sortOrder == "Fecha" ? "Fecha_Desc" : "Fecha";
            listaFiltro.Add(new SelectListItem { Text = "Cliente", Value = "Nombre" });
            ViewBag.SortClienteId = sortOrder == "ClienteId" ? "ClienteId_Desc" : "ClienteId";
            listaFiltro.Add(new SelectListItem { Text = "Valor", Value = "Valor" });
            ViewBag.SortValor = sortOrder == "Valor" ? "Valor_Desc" : "Valor";
            listaFiltro.Add(new SelectListItem { Text = "Interes", Value = "Interes" });
            ViewBag.SortInteres = sortOrder == "Interes" ? "Interes_Desc" : "Interes";
            listaFiltro.Add(new SelectListItem { Text = "Meses", Value = "Meses" });
            ViewBag.SortMeses = sortOrder == "Meses" ? "Meses_Desc" : "Meses";
            listaFiltro.Add(new SelectListItem { Text = "TipoCuotaId", Value = "TipoCuotaId" });
            ViewBag.SortTipoCuotaId = sortOrder == "TipoCuotaId" ? "TipoCuotaId_Desc" : "TipoCuotaId";
            listaFiltro.Add(new SelectListItem { Text = "PrimCuota", Value = "PrimCuota" });
            ViewBag.SortPrimCuota = sortOrder == "PrimCuota" ? "PrimCuota_Desc" : "PrimCuota";
            listaFiltro.Add(new SelectListItem { Text = "DivisionCreditoId", Value = "DivisionCreditoId" });
            ViewBag.SortDivisionCreditoId = sortOrder == "DivisionCreditoId" ? "DivisionCreditoId_Desc" : "DivisionCreditoId";
            listaFiltro.Add(new SelectListItem { Text = "InteresPrimCuota", Value = "InteresPrimCuota" });
            ViewBag.SortInteresPrimCuota = sortOrder == "InteresPrimCuota" ? "InteresPrimCuota_Desc" : "InteresPrimCuota";
            listaFiltro.Add(new SelectListItem { Text = "CapitalFinalCredito", Value = "CapitalFinalCredito" });
            ViewBag.SortCapitalFinalCredito = sortOrder == "CapitalFinalCredito" ? "CapitalFinalCredito_Desc" : "CapitalFinalCredito";
            listaFiltro.Add(new SelectListItem { Text = "Observacion", Value = "Observacion" });
            ViewBag.SortObservacion = sortOrder == "Observacion" ? "Observacion_Desc" : "Observacion";
            listaFiltro.Add(new SelectListItem { Text = "Estado", Value = "Estado" });
            ViewBag.SortEstado = sortOrder == "Estado" ? "Estado_Desc" : "Estado";
            ViewBag.campos1 = listaFiltro;
            var q = "select * from credito,cliente  where credito.clienteid=cliente.clienteid and credito.empresaId='" + empresaId.ToString() + "'";
            List<credito> lista;
            if (!String.IsNullOrEmpty(campos1) && !String.IsNullOrEmpty(filtro1))
            {
                if (!campos1.ToUpper().Equals("ESTADO"))
                    q = q + " and upper(" + campos1 + ") like '%" + filtro1.Trim().ToUpper() + "%'";
                else
                    q = q + " and (CASE WHEN estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END)= '" + filtro1.Trim().ToUpper() + "'";

                lista = db.Database.SqlQuery<credito>(q).ToList();
            
            }
            else
            {
                var credito = db.credito.Include(c => c.cliente).Include(c => c.tipocuota).Include(c => c.divisioncredito).Where(c=>c.EmpresaId==empresaId);
                lista = credito.ToList();

                switch (sortOrder)
                {

                    case "CreditoId":
                        lista = lista.OrderBy(s => s.CreditoId).ToList();
                        break;

                    case "CreditoId_Desc":
                        lista = lista.OrderByDescending(s => s.CreditoId).ToList();
                        break;
                    case "Fecha":
                        lista = lista.OrderBy(s => s.Fecha).ToList();
                        break;

                    case "Fecha_Desc":
                        lista = lista.OrderByDescending(s => s.Fecha).ToList();
                        break;
                    case "ClienteId":
                        lista = lista.OrderBy(s => s.ClienteId).ToList();
                        break;

                    case "ClienteId_Desc":
                        lista = lista.OrderByDescending(s => s.ClienteId).ToList();
                        break;
                    case "Valor":
                        lista = lista.OrderBy(s => s.Valor).ToList();
                        break;

                    case "Valor_Desc":
                        lista = lista.OrderByDescending(s => s.Valor).ToList();
                        break;
                    case "Interes":
                        lista = lista.OrderBy(s => s.Interes).ToList();
                        break;

                    case "Interes_Desc":
                        lista = lista.OrderByDescending(s => s.Interes).ToList();
                        break;
                    case "Meses":
                        lista = lista.OrderBy(s => s.Meses).ToList();
                        break;

                    case "Meses_Desc":
                        lista = lista.OrderByDescending(s => s.Meses).ToList();
                        break;
                    case "TipoCuotaId":
                        lista = lista.OrderBy(s => s.TipoCuotaId).ToList();
                        break;

                    case "TipoCuotaId_Desc":
                        lista = lista.OrderByDescending(s => s.TipoCuotaId).ToList();
                        break;
                    case "PrimCuota":
                        lista = lista.OrderBy(s => s.PrimCuota).ToList();
                        break;

                    case "PrimCuota_Desc":
                        lista = lista.OrderByDescending(s => s.PrimCuota).ToList();
                        break;
                    case "DivisionCreditoId":
                        lista = lista.OrderBy(s => s.DivisionCreditoId).ToList();
                        break;

                    case "DivisionCreditoId_Desc":
                        lista = lista.OrderByDescending(s => s.DivisionCreditoId).ToList();
                        break;
                    case "InteresPrimCuota":
                        lista = lista.OrderBy(s => s.InteresPrimCuota).ToList();
                        break;

                    case "InteresPrimCuota_Desc":
                        lista = lista.OrderByDescending(s => s.InteresPrimCuota).ToList();
                        break;
                    case "CapitalFinalCredito":
                        lista = lista.OrderBy(s => s.CapitalFinalCredito).ToList();
                        break;

                    case "CapitalFinalCredito_Desc":
                        lista = lista.OrderByDescending(s => s.CapitalFinalCredito).ToList();
                        break;
                    case "Observacion":
                        lista = lista.OrderBy(s => s.Observacion).ToList();
                        break;

                    case "Observacion_Desc":
                        lista = lista.OrderByDescending(s => s.Observacion).ToList();
                        break;
                    case "Estado":
                        lista = lista.OrderBy(s => s.Estado).ToList();
                        break;

                    case "Estado_Desc":
                        lista = lista.OrderByDescending(s => s.Estado).ToList();
                        break;

                    default:
                        lista = lista.OrderByDescending(s => s.CreditoId).ToList();
                        break;
                }
               
            }
            List<int> ids = new List<int>();
            foreach (credito c in lista)
            {
                c.cliente = db.cliente.Find(c.ClienteId);
                c.cuota = db.cuota.Where(cu => cu.CreditoId == c.CreditoId).ToList();
                c.SaldoCapital = 0;
                c.SaldoInteres = 0;
                foreach (cuota cu in c.cuota)
                {
                    c.SaldoCapital = c.SaldoCapital + cu.calcularSaldoxCapital();
                    c.SaldoInteres = c.SaldoInteres + cu.calcularSaldoxInteres();
                }
                if (todos == false)
                    if ((int)(c.SaldoCapital + c.SaldoInteres) <= 0 || c.Estado==false)
                        ids.Add(c.CreditoId);
            }
            foreach (int id in ids)
                lista.RemoveAll(c => c.CreditoId == id);


            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["RegistrosPorPagina"].ToString());
            int pageNumber = (page ?? 1);
            return View(lista.ToPagedList(pageNumber, pageSize));

            //return  View(lista);
        }

        // GET: Credito/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(id));
            int intId = Convert.ToInt32(idDecrypted);
            credito credito = db.credito.Find(intId);
            if (credito == null)
            {
                return HttpNotFound();
            }
            foreach (cuota c in credito.cuota)
            {
                c.SaldoCapital = c.calcularSaldoxCapital();
                c.SaldoInteres = c.calcularSaldoxInteres();
            }
            return View(credito);
        }

        public credito getCreate()
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);

            ViewBag.ClienteId = new SelectList(db.cliente.Where(c => c.Estado == true && c.EmpresaId == empresaId).OrderBy(e => e.Nit), "ClienteId", "Nombre");
            ViewBag.TipoCuotaId = new SelectList(db.tipocuota.Where(c => c.Estado == true).OrderBy(e => e.Nombre), "TipoCuotaId", "Nombre");
            ViewBag.DivisionCreditoId = new SelectList(db.divisioncredito.Where(c => c.Estado == true).OrderBy(e => e.Nombre), "DivisionCreditoId", "Nombre");
            ViewBag.UsuarioId = new SelectList(db.usuario.Where(c => c.Estado == true && c.EmpresaId == empresaId).OrderBy(e => e.UsuNombre), "UsuarioId", "UsuNombre");
            credito cr = new credito();
            cr.Fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            cr.Estado = true;
            cr.EmpresaId = empresaId;
            return cr;
        }
        // GET: Credito/Create
        public ActionResult Create()
        {
            return View(getCreate());
        }

        public ActionResult CreateMobil()
        {
            return View(getCreate()); 
        }

        // POST: Credito/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string button, [Bind(Include = "CreditoId,Fecha,ClienteId,Valor,Interes,Meses,TipoCuotaId,PrimCuota,DivisionCreditoId,InteresPrimCuota,CapitalFinalCredito,Observacion,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion,EmpresaId,UsuarioId")] credito credito)
        {
            if (button.Equals("Cancelar"))
                return RedirectToAction("Index");


            if (ModelState.IsValid)
            {
                credito.CalcularCuotas();
                if (button.Equals("Crear Credito"))
                {
                    int empresaId = 0;
                    if (Session["EmpresaId"] != null)
                        Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
                    consecutivo c = db.consecutivo.Find(empresaId);
                    c.CreditoNro = c.CreditoNro + 1;
                    credito.CreditoNro = c.CreditoNro;
                    db.credito.Add(credito);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            ViewBag.ClienteId = new SelectList(db.cliente.Where(c => c.Estado == true && c.EmpresaId == credito.EmpresaId).OrderBy(e => e.Nit), "ClienteId", "Nombre", credito.ClienteId);
            ViewBag.TipoCuotaId = new SelectList(db.tipocuota.Where(c => c.Estado == true).OrderBy(e => e.Nombre), "TipoCuotaId", "Nombre", credito.TipoCuotaId);
            ViewBag.DivisionCreditoId = new SelectList(db.divisioncredito.Where(c => c.Estado == true).OrderBy(e => e.Nombre), "DivisionCreditoId", "Nombre", credito.DivisionCreditoId);
            ViewBag.UsuarioId = new SelectList(db.usuario.Where(c => c.Estado == true && c.EmpresaId == credito.EmpresaId).OrderBy(e => e.UsuNombre), "UsuarioId", "UsuNombre");
            return View(credito);
        }

        // GET: Credito/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(id));
            int intId = Convert.ToInt32(idDecrypted);
            credito credito = db.credito.Find(intId);
            if (credito == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.cliente.Where(c => c.Estado == true && c.EmpresaId==credito.EmpresaId).OrderBy(e => e.Nit), "ClienteId", "Nombre", credito.ClienteId);
            ViewBag.TipoCuotaId = new SelectList(db.tipocuota.Where(c => c.Estado == true).OrderBy(e => e.Nombre), "TipoCuotaId", "Nombre", credito.TipoCuotaId);
            ViewBag.DivisionCreditoId = new SelectList(db.divisioncredito.Where(c => c.Estado == true).OrderBy(e => e.Nombre), "DivisionCreditoId", "Nombre", credito.DivisionCreditoId);
            ViewBag.UsuarioId = new SelectList(db.usuario.Where(c => c.Estado == true && c.EmpresaId == credito.EmpresaId).OrderBy(e => e.UsuNombre), "UsuarioId", "UsuNombre",credito.UsuarioId);
            if (credito.tieneAbonosActivos())
            {
                ViewBag.Layout = 1;
                ViewBag.action = "index";
                ViewBag.controller = "Credito";
                ViewBag.tipo = "bg-danger";
                ViewBag.mensaje = "El credito " + credito.CreditoId + " tiene abonos activos, No es posible modificar un credito con abonos activos";
                return View("../Shared/Mensaje");
            }
            else
                return View(credito);
        }

        // POST: Credito/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string button, [Bind(Include = "CreditoId,Fecha,ClienteId,Valor,Interes,Meses,TipoCuotaId,PrimCuota,DivisionCreditoId,InteresPrimCuota,CapitalFinalCredito,Observacion,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion,CreditoNro,EmpresaId,UsuarioId")] credito credito)
        {
            if (button.Equals("Cancelar"))
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                if (button.Equals("Calcular"))
                    credito.CalcularCuotas();

                if (button.Equals("Modificar Credito"))
                {
                    // credito.CalcularCuotas();
                    var currentCuotas = db.cuota.Where(c => c.CreditoId == credito.CreditoId);
                    foreach (cuota ss in currentCuotas)
                    {
                        db.cuota.Remove(ss);
                        //db.Entry(ss).State = EntityState.Deleted;
                    }

                    db.Entry(credito).State = EntityState.Modified;
                    credito.CalcularCuotas();

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


            }
            ViewBag.ClienteId = new SelectList(db.cliente.Where(c => c.Estado == true && c.EmpresaId == credito.EmpresaId).OrderBy(e => e.Nit), "ClienteId", "Nit", credito.ClienteId);
            ViewBag.TipoCuotaId = new SelectList(db.tipocuota.Where(c => c.Estado == true).OrderBy(e => e.Nombre), "TipoCuotaId", "Nombre", credito.TipoCuotaId);
            ViewBag.DivisionCreditoId = new SelectList(db.divisioncredito.Where(c => c.Estado == true).OrderBy(e => e.Nombre), "DivisionCreditoId", "Nombre", credito.DivisionCreditoId);
            ViewBag.UsuarioId = new SelectList(db.usuario.Where(c => c.Estado == true && c.EmpresaId == credito.EmpresaId).OrderBy(e => e.UsuNombre), "UsuarioId", "UsuNombre", credito.UsuarioId);
            return View(credito);
        }

        // GET: Credito/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            credito credito = db.credito.Find(id);
            if (credito == null)
            {
                return HttpNotFound();
            }
            return View(credito);
        }

        // POST: Credito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            credito credito = db.credito.Find(id);
            db.credito.Remove(credito);
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
