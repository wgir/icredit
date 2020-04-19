using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrediAdmin.Models;
using CrediAdmin.ViewModels;
using CrediAdmin.Util;

namespace CrediAdmin.Controllers
{
    [SessionExpire]
    [Authorize]
    public class CuotasPagadasController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();
        //
        // GET: /CuotasxCobrarDefault1/

        public ActionResult Index(string controlador, string iniMes, string finMes)
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
               Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            if (iniMes==null)
              iniMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
          if (finMes==null)
              finMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).ToString("dd/MM/yyyy");
          
          ViewBag.iniMes1 = iniMes;
          ViewBag.finMes1 = finMes;
          ViewBag.controlador = controlador;
          IEnumerable<Cuotas>   lista=consulta(empresaId,iniMes, finMes);
          ViewBag.totalAbonos = lista.Sum(l => l.Abonos);
          return View(lista);
            

            
        }
       

        public IEnumerable<Cuotas> consulta(int empresaId,string iniMes, string finMes)
        {
           // int empresaId = Convert.ToInt32(Session["EmpresaId"]);
            string strfecha1 = "",strfecha2="";
          
            
            if (MiUtil.isDate(iniMes))
                strfecha1 = MiUtil.fechaToSQL(DateTime.ParseExact(iniMes, "dd/MM/yyyy", null), 0);

            if (MiUtil.isDate(finMes))
                strfecha2 = MiUtil.fechaToSQL(DateTime.ParseExact(finMes, "dd/MM/yyyy", null), 0);

            

            var q = @"SELECT        cuota.CuotaId, cliente.Nit, cliente.Nombre, credito.CreditoId,credito.CreditoNro, cuota.Numero, cuota.Fecha,
                        cuota.AbonoCapital+IFNULL(cuota.AjusteAbonoCapital,0) as AbonoCapital,
                        cuota.AbonoInteres+IFNULL(cuota.AjusteAbonoInteres,0) as AbonoInteres, 
                        cuota.AbonoCapital + cuota.AbonoInteres  + IFNULL(cuota.AjusteAbonoCapital,0)+IFNULL(cuota.AjusteAbonoInteres,0) AS TotalCuota,   
                        max(CASE WHEN Abono.Fecha IS NULL 
                        THEN curdate() ELSE Abono.Fecha END) as FechaAbono,
                        SUM(IFNULL(abono.Valor, 0)) AS Abonos,
                        Cuota.AbonoCapital + Cuota.AbonoInteres+ IFNULL(Cuota.AjusteAbonoCapital,0)+IFNULL(Cuota.AjusteAbonoInteres,0)-SUM(CASE WHEN Abono.Valor IS NULL 
                         THEN  0.0 ELSE Abono.Valor END) AS SaldoCuota
                          FROM            credito INNER JOIN
                         cuota ON credito.CreditoId = cuota.CreditoId LEFT OUTER JOIN
                         cliente ON credito.ClienteId = cliente.ClienteId LEFT OUTER JOIN
                         abono ON cuota.CuotaId = abono.CuotaId  AND Abono.Estado = 1";
            q = q + "  WHERE        (credito.Estado = 1)  AND ((Cuota.Fecha between '" + strfecha1 + "' and '" + strfecha2 + "')";
            //q = q + " or ((abono.Fecha between '" + strfecha1 + "' and '" + strfecha2 + "')))";
            q = q + " )";
            q =q+" AND (cliente.EmpresaId = '"+empresaId.ToString()+"')";
            q = q + "  GROUP BY cuota.CuotaId, cliente.Nit, cliente.Nombre, credito.CreditoId, cuota.Numero, cuota.Fecha, ";
            q = q + "   cuota.AbonoCapital, cuota.AbonoInteres, credito.Estado  ORDER BY credito.Fecha DESC";



            var cp = db.Database.SqlQuery<Cuotas>(q);
            var final = from c in cp where (c.Abonos + 1 > (c.AbonoCapital + c.AbonoInteres)) select c;
            return final.ToList();
           
          }

    }

   
}
