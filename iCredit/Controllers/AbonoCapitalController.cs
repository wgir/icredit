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
    public class AbonoCapitalController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();

        // GET: AbonoCapital
        public ActionResult Index(string creditoId,string campos1, string filtro1, string currentCampo, string currentFilter,string sortOrder, int? page)
        {
            string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(creditoId));
            int intCreditoId = Convert.ToInt32(idDecrypted);

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
						  listaFiltro.Add(new SelectListItem { Text = "AbonoCapitalId", Value = "AbonoCapitalId" });
			  ViewBag.SortAbonoCapitalId = sortOrder == "AbonoCapitalId" ? "AbonoCapitalId_Desc" : "AbonoCapitalId";
						  listaFiltro.Add(new SelectListItem { Text = "CreditoId", Value = "CreditoId" });
			  ViewBag.SortCreditoId = sortOrder == "CreditoId" ? "CreditoId_Desc" : "CreditoId";
						  listaFiltro.Add(new SelectListItem { Text = "Fecha", Value = "Fecha" });
			  ViewBag.SortFecha = sortOrder == "Fecha" ? "Fecha_Desc" : "Fecha";
						  listaFiltro.Add(new SelectListItem { Text = "Valor", Value = "Valor" });
			  ViewBag.SortValor = sortOrder == "Valor" ? "Valor_Desc" : "Valor";
						  listaFiltro.Add(new SelectListItem { Text = "Observacion", Value = "Observacion" });
			  ViewBag.SortObservacion = sortOrder == "Observacion" ? "Observacion_Desc" : "Observacion";
						  listaFiltro.Add(new SelectListItem { Text = "AbonoNro", Value = "AbonoNro" });
			  ViewBag.SortAbonoNro = sortOrder == "AbonoNro" ? "AbonoNro_Desc" : "AbonoNro";
						  listaFiltro.Add(new SelectListItem { Text = "FechaEnvio", Value = "FechaEnvio" });
			  ViewBag.SortFechaEnvio = sortOrder == "FechaEnvio" ? "FechaEnvio_Desc" : "FechaEnvio";
						  listaFiltro.Add(new SelectListItem { Text = "Estado", Value = "Estado" });
			  ViewBag.SortEstado = sortOrder == "Estado" ? "Estado_Desc" : "Estado";
			            ViewBag.campos1 = listaFiltro;
            var q = "select * from abonocapital";
            List<abonocapital> lista;
            if (!String.IsNullOrEmpty(campos1) && !String.IsNullOrEmpty(filtro1))
            {
                 if (!campos1.ToUpper().Equals("ESTADO"))
                    q = q + " where upper(" + campos1 + ") like '%" + filtro1.Trim().ToUpper() + "%'";
                else
                    q = q + " where (CASE WHEN estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END)= '" + filtro1.Trim().ToUpper() + "'";

                lista = db.Database.SqlQuery< abonocapital >(q).ToList();
            }
            else
			{
                							var abonocapital = db.abonocapital.Include(a => a.credito).Where(a=>a.CreditoId==intCreditoId).OrderByDescending(a=>a.AbonoCapitalId);
											lista=abonocapital.ToList();
								
				switch (sortOrder)
                {

								  case "AbonoCapitalId":
					lista = lista.OrderBy(s => s.AbonoCapitalId).ToList();
					break;
				   
				   case "AbonoCapitalId_Desc":
					lista = lista.OrderByDescending(s => s.AbonoCapitalId).ToList();
					break;
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
								  case "Valor":
					lista = lista.OrderBy(s => s.Valor).ToList();
					break;
				   
				   case "Valor_Desc":
					lista = lista.OrderByDescending(s => s.Valor).ToList();
					break;
								  case "Observacion":
					lista = lista.OrderBy(s => s.Observacion).ToList();
					break;
				   
				   case "Observacion_Desc":
					lista = lista.OrderByDescending(s => s.Observacion).ToList();
					break;

                   case "AbonoCapitalNro":
					lista = lista.OrderBy(s => s.AbonoCapitalNro).ToList();
					break;
				   
				   case "AbonoNro_Desc":
                    lista = lista.OrderByDescending(s => s.AbonoCapitalNro).ToList();
					break;
								  case "FechaEnvio":
					lista = lista.OrderBy(s => s.FechaEnvio).ToList();
					break;
				   
				   case "FechaEnvio_Desc":
					lista = lista.OrderByDescending(s => s.FechaEnvio).ToList();
					break;
								  case "Estado":
					lista = lista.OrderBy(s => s.Estado).ToList();
					break;
				   
				   case "Estado_Desc":
					lista = lista.OrderByDescending(s => s.Estado).ToList();
					break;
				                }
			}
            ViewBag.creditoId = creditoId;
			int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["RegistrosPorPagina"].ToString()); 
            int pageNumber = (page ?? 1);
            return View(lista.ToPagedList(pageNumber, pageSize));

    		//return  View(lista);
        }

        // GET: AbonoCapital/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(id));
            int intId = Convert.ToInt32(idDecrypted);

            abonocapital abonocapital = db.abonocapital.Find(intId);

            if (abonocapital == null)
            {
                return HttpNotFound();
            }
            return View(abonocapital);
        }

        // GET: AbonoCapital/Create
        public ActionResult Create(string creditoId)
        {

            ViewBag.creditoId = creditoId; 
            string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(creditoId));
            int intId = Convert.ToInt32(idDecrypted);
            credito cre = db.credito.Find(intId);
            ViewBag.empresaId = MiUtil.encriptar(db.credito.Find(intId).EmpresaId.ToString());
            decimal? saldoTotal = cre.saldoTotalAFecha(DateTime.Now);
                //new SelectList(db.credito.Where(c=>c.Estado==true).OrderBy(e=>e.TipoCuotaId), "CreditoId", "TipoCuotaId");
            return View(new abonocapital { Fecha=DateTime.Now, Valor= saldoTotal });
        }

        // POST: AbonoCapital/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(abonocapital abonocapital,string empresaId,string creditoId)
        {
            ModelState.Clear();
            abonocapital.CreditoId = Convert.ToInt32(MiUtil.desEncriptar(HttpUtility.UrlDecode(creditoId)));
            TryValidateModel(abonocapital);
            if (ModelState.IsValid)
            {
                string ok = "";
                consecutivo consec = db.consecutivo.Find(Convert.ToInt32(MiUtil.desEncriptar(HttpUtility.UrlDecode(empresaId))));
                consec.AbonoCapitalNro = consec.AbonoCapitalNro + 1;
                abonocapital.AbonoCapitalNro = consec.AbonoCapitalNro;

                credito cre = db.credito.Find(Convert.ToInt32(MiUtil.desEncriptar(HttpUtility.UrlDecode(creditoId))));
                List<cuota> cuotas = db.cuota.Where(c => c.CreditoId == abonocapital.CreditoId).ToList();
                backup_cuota(abonocapital, cuotas);
                ok = cre.abonoACapital(abonocapital.Fecha, abonocapital.Valor);
                   
                if (ok == "")
                {
                    db.abonocapital.Add(abonocapital);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { creditoId = creditoId });
                }else
                    ModelState.AddModelError("", ok);
            }
            ViewBag.empresaId=empresaId;
            ViewBag.creditoId = creditoId; 
            //ViewBag.CreditoId = new SelectList(db.credito.Where(c=>c.Estado==true).OrderBy(e=>e.TipoCuotaId), "CreditoId", "TipoCuotaId", abonocapital.CreditoId);
            return View(abonocapital);
        }


        public void backup_cuota(abonocapital abonocapital,List<cuota> cuotas)
        {
           // List<cuota> cuotas = db.cuota.Where(c => c.CreditoId == abonocapital.CreditoId).ToList();
            foreach(cuota c in cuotas)
            {
                backup_cuota bak = new backup_cuota
                {
                    CreditoId = c.CreditoId,
                    CuotaId = c.CuotaId,
                    Numero = c.Numero,
                    Fecha = c.Fecha,
                    AbonoCapital = c.AbonoCapital,
                    AbonoInteres = c.AbonoInteres,
                    AjusteAbonoCapital = c.AjusteAbonoCapital,
                    AjusteAbonoInteres = c.AjusteAbonoInteres,
                    SaldoCapital=c.SaldoCapital,
                    SaldoInteres=c.SaldoInteres
                };
                abonocapital.backup_cuota.Add(bak);
            }
        }
        // GET: AbonoCapital/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(id));
            int intId = Convert.ToInt32(idDecrypted);
            abonocapital abonocapital = db.abonocapital.Find(intId);
            if (abonocapital == null)
            {
                return HttpNotFound();
            }
            ViewBag.creditoId = MiUtil.encriptar(abonocapital.CreditoId.ToString());
            //new SelectList(db.credito.Where(c=>c.Estado==true).OrderBy(e=>e.TipoCuotaId), "CreditoId", "TipoCuotaId", abonocapital.CreditoId);
            credito cr = db.credito.Find(abonocapital.CreditoId);

            ViewBag.empresaId = MiUtil.encriptar(cr.EmpresaId.ToString());

            return View(abonocapital);
        }

        // POST: AbonoCapital/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(abonocapital abonocapital,string empresaId,string creditoId)
        {
            ModelState.Clear();
            abonocapital.CreditoId = Convert.ToInt32(MiUtil.desEncriptar(HttpUtility.UrlDecode(creditoId)));
            TryValidateModel(abonocapital);
            if (ModelState.IsValid)
            {
                string ok = "";
                var q = @"update cuota a,backup_cuota b 
                            set a.ajusteAbonoCapital=b.ajusteAbonoCapital,
                            a.ajusteAbonoInteres=b.ajusteAbonoInteres
                            where a.cuotaid=b.cuotaid";
                q = q + " and b.abonocapitalid='" + abonocapital.AbonoCapitalId + "'";

                //credito cre = db.credito.Find(Convert.ToInt32(MiUtil.desEncriptar(HttpUtility.UrlDecode(creditoId))));
                db.Entry(abonocapital).State = EntityState.Modified;
                if (abonocapital.Estado == false)
                    db.Database.ExecuteSqlCommand(q);

                db.SaveChanges();
                return RedirectToAction("Index", new { creditoId = creditoId });
                //}
                //else
                //    ModelState.AddModelError("", ok);
            }
            ViewBag.empresaId = empresaId;
            ViewBag.creditoId = creditoId;
            //new SelectList(db.credito.Where(c => c.Estado == true).OrderBy(e => e.TipoCuotaId), "CreditoId", "TipoCuotaId", abonocapital.CreditoId);
            return View(abonocapital);
        }

        // GET: AbonoCapital/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            abonocapital abonocapital = db.abonocapital.Find(id);
            if (abonocapital == null)
            {
                return HttpNotFound();
            }
            return View(abonocapital);
        }

        // POST: AbonoCapital/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            abonocapital abonocapital = db.abonocapital.Find(id);
            db.abonocapital.Remove(abonocapital);
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
