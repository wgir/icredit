using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrediAdmin.ViewModels;
using CrediAdmin.Models;
using CrediAdmin.Util;
namespace CrediAdmin.Controllers
{
    [SessionExpire]
    [Authorize]
    public class EstadoEmpresaController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();
      //  MiUtil ut = new MiUtil();
        //
        // GET: /EstadoEmpresa/

        public ViewResult Index(string anio,string mes)
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            int mesActual = DateTime.Now.Month, anioActual = DateTime.Now.Year;
            if(anio!=null && mes!=null)
                  mesActual = Convert.ToInt32(mes);
                       
            ViewBag.mes = MiUtil.getMeses(mesActual);
            

            var q = @"SELECT DISTINCT year(Cuota.Fecha) AS anio
                    FROM            Cuota INNER JOIN
                    Credito ON Cuota.CreditoId = Credito.CreditoId  INNER JOIN
                    Cliente ON Credito.ClienteId=Cliente.ClienteId
                    WHERE        (Credito.Estado = 1) and (Cliente.EmpresaId='" + empresaId.ToString() + "')";
            ViewBag.anio = MiUtil.llenarCombo(db, q, anioActual.ToString());

           // Empresa e=db.Empresas.First();
            //ViewBag.EmpresaId = new SelectList(db.Empresas.Where(u => u.Activo == true), "EmpresaId", "Nombre",e.EmpresaId);
            //var em = db.empresa.Where(u => u.Estado == true);
            //int empresaId = Convert.ToInt32(Session["EmpresaId"]);
            //ViewBag.EmpresaId = new SelectList(em.Where(u => u.EmpresaId == empresaId), "EmpresaId", "Nombre", empresaId);


            return Index(empresaId, anioActual.ToString(), mesActual);
           // }
 
            
        }
       
        [HandleError]
        [HttpPost]
        public ViewResult Index(int EmpresaId,string anio,int mes)
        {
            int anioi = Convert.ToInt32(anio);
            if (anioi == 0)
                anioi=DateTime.Now.Year;

            empresa empresa = (empresa)db.empresa.Find(EmpresaId);
            DateTime finMes=new DateTime(anioi, mes,  DateTime.DaysInMonth(anioi, mes));
            EstadoEmpresa ee = new EstadoEmpresa();
            ee.CapitalTotalRecaudado = 0;
            ee.CapitalxCobrar = 0;
            ee.InteresTotalRecaudado = 0;
            ee.InteresxCobrar = 0;
            ee.TotalEnCaja = 0;
            ee.TotalxCobrar = 0;
            decimal? sumaAportexperiodo = 0, sumaTotalAportes = 0, sumaCreditos = 0, sumaRecaudado = 0;

            foreach (retirointeres ri in empresa.retirointeres)
                if (ri.Estado && ri.Fecha<=finMes)
                    ee.InteresTotalRecaudado = ee.InteresTotalRecaudado-ri.Valor;
            
            foreach (cliente c in empresa.cliente)
                foreach (credito cr in c.credito)
                    if (cr.Estado && cr.Fecha<=finMes)
                    {
                       sumaCreditos = sumaCreditos + cr.Valor;
                       ee.CapitalxCobrar = ee.CapitalxCobrar+(cr.calcularTotalCapital()-cr.calcularAbonoCapital(finMes));
                       ee.InteresxCobrar = ee.InteresxCobrar + (cr.calcularTotalInteres() - cr.calcularAbonoInteres(finMes));
                       sumaRecaudado = sumaRecaudado+ cr.calcularAbonoCapital(finMes);
                       ee.InteresTotalRecaudado = ee.InteresTotalRecaudado + cr.calcularAbonoInteres(finMes);
                    }
            
            ee.esocios = new List<EstadoSocio>();
            foreach (socio s in empresa.socio.OrderBy(e=>e.Nombre))
            {
                 EstadoSocio es = new EstadoSocio();
                 es.SocioNit = s.Nit;
                 es.Nombre = s.Nombre; 
                 DateTime iniMes=new DateTime(anioi,mes, 1);
                 es.TotalAportes = s.calcularAportes(finMes) - s.calcularRetiros(finMes);
                 //es.AportesxPeriodo = s.calcularAportes(iniMes.AddDays(-1)) - s.calcularRetiros(finMes);
                 es.AportesxPeriodo = s.calcularAportes(finMes) - s.calcularRetiros(finMes);
                 sumaAportexperiodo = sumaAportexperiodo + es.AportesxPeriodo;
                 sumaTotalAportes = sumaTotalAportes + es.TotalAportes;
                 ee.esocios.Add(es);
            }
            ee.CapitalTotalRecaudado = sumaTotalAportes - sumaCreditos + sumaRecaudado;


            if (sumaAportexperiodo != 0)
                foreach (EstadoSocio es in ee.esocios)
                    es.UtilidadRecomendada = ee.InteresTotalRecaudado * ((es.AportesxPeriodo * 100 / sumaAportexperiodo) / 100);

            ee.TotalxCobrar = ee.CapitalxCobrar + ee.InteresxCobrar;
            ee.TotalEnCaja = ee.CapitalTotalRecaudado + ee.InteresTotalRecaudado;

            
            var em = db.empresa.Where(u => u.Estado == true);

            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);

            //int empresaId = Convert.ToInt32(Session["EmpresaId"]);
            ViewBag.EmpresaId = new SelectList(em.Where(u => u.EmpresaId == empresaId), "EmpresaId", "Nombre", empresaId);

           // ViewBag.EmpresaId = new SelectList(db.Empresas.Where(u => u.Activo == true), "EmpresaId", "Nombre", EmpresaId);
            int mesActual = DateTime.Now.Month;
            ViewBag.mes = MiUtil.getMeses(mesActual);
            var q = @"SELECT DISTINCT year(Cuota.Fecha) AS anio
                    FROM            Cuota INNER JOIN
                    Credito ON Cuota.CreditoId = Credito.CreditoId  INNER JOIN
                    Cliente ON Credito.ClienteId= Cliente.ClienteId
                    WHERE        (Credito.Estado = 1) and (Cliente.EmpresaId='" + empresaId.ToString() + "')";
           
            ViewBag.anio = MiUtil.llenarCombo(db, q, anioi.ToString());

            return View(ee);
        }
       
        //
        // GET: /EstadoEmpresa/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /EstadoEmpresa/Create

       
       
       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}