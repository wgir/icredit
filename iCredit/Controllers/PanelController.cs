using CrediAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrediAdmin.Controllers
{
    [SessionExpire]
    [Authorize]
    public class PanelController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();
        private CuotasxCobrarController cxc = new CuotasxCobrarController();
        private CuotasPagadasController cp = new CuotasPagadasController();
        // GET: Panel
        public ActionResult Index()
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            //ViewBag.nombreEmpresa = db.empresa.Where(e => e.EmpresaId == empresaId).FirstOrDefault().Nombre;
            ViewBag.cantidadClientes = db.cliente.Where(c => c.EmpresaId == empresaId && c.Estado==true).ToList().Count();
            int cantCreditosConSaldo = 0;
            List<credito> creditos = db.credito.Where(c => c.EmpresaId == empresaId && c.Estado == true).ToList();
            foreach(credito c in creditos)
            {
                if ((c.calcularTotalInteres() - c.calcularAbonoInteres(null) + c.calcularTotalCapital() - c.calcularAbonoCapital(null)) > 0)
                    cantCreditosConSaldo++;
            }


            ViewBag.cantidadCreditos = cantCreditosConSaldo;
                //db.credito.Where(c => c.EmpresaId == empresaId && c.Estado==true && (c.calcularTotalInteres() - c.calcularAbonoInteres(null))>0 ).ToList().Count();

            DateTime iniMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime finMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));


            ViewBag.cantidadCxC= cxc.getCuotasxCobrar(empresaId, finMes.ToString("dd/MM/yyyy"), 0).Count();
            ViewBag.cantidadCP = cp.consulta(empresaId,iniMes.ToString("dd/MM/yyyy"), finMes.ToString("dd/MM/yyyy")).Count();
            //return this.Index(ViewBag.CurrentFilter, controlador, UsuarioId);

            return View();
        }
    }
}