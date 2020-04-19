using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrediAdmin.Models;
using CrediAdmin.ViewModels;
using CrediAdmin.Util;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity;


namespace CrediAdmin.Controllers
{
    [SessionExpire]
    [Authorize]
    public class CuotasxCobrarController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();
        //
        // GET: /CuotasxCobrarDefault1/
        
        public ActionResult Index(string controlador,string fechaActual)
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
           string usuId = User.Identity.GetUserId();
           int? UsuarioId=0;
           if (HttpContext.User.IsInRole("Cobrador"))
           {
               usuario u = db.usuario.Where(c => c.Estado == true && c.EmpresaId == empresaId && c.aspnetusersId.Equals(usuId)).FirstOrDefault();
               ViewBag.UsuarioId = new SelectList(db.usuario.Where(c => c.Estado == true && c.EmpresaId == empresaId && c.aspnetusersId.Equals(usuId)).OrderBy(e => e.UsuNombre), "UsuarioId", "UsuNombre",u.UsuarioId);
               UsuarioId=u.UsuarioId;
           }else
               ViewBag.UsuarioId = new SelectList(db.usuario.Where(c => c.Estado == true && c.EmpresaId == empresaId).OrderBy(e => e.UsuNombre), "UsuarioId", "UsuNombre");

           DateTime finMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
           ViewBag.controlador = controlador;
           if (String.IsNullOrEmpty(fechaActual))
                ViewBag.CurrentFilter = finMes.ToString("dd/MM/yyyy");
            else
               ViewBag.CurrentFilter = fechaActual;

            
           
           return this.Index(ViewBag.CurrentFilter, controlador,UsuarioId);
            

            
        }
        [HandleError]
        [HttpPost]
        public ActionResult Index(string fecha, string  controlador,int? UsuarioId)
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);

           // string usuId = User.Identity.GetUserId();
            if (HttpContext.User.IsInRole("Cobrador"))
                ViewBag.UsuarioId = new SelectList(db.usuario.Where(c => c.Estado == true && c.EmpresaId == empresaId && c.UsuarioId==UsuarioId).OrderBy(e => e.UsuNombre), "UsuarioId", "UsuNombre", UsuarioId);
            else
                ViewBag.UsuarioId = new SelectList(db.usuario.Where(c => c.Estado == true && c.EmpresaId == empresaId).OrderBy(e => e.UsuNombre), "UsuarioId", "UsuNombre",UsuarioId);

            string strfecha="";
            // DateTime finMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            if (MiUtil.isDate(fecha))
            {
                ViewBag.CurrentFilter = fecha;
                strfecha = MiUtil.fechaToSQL(DateTime.ParseExact(fecha, "dd/MM/yyyy", null), 0);
            }
            /*
            var q = @"SELECT  cuota.CuotaId, cliente.Nit, cliente.Nombre, credito.CreditoId,credito.CreditoNro, cuota.Numero, cuota.Fecha, cuota.AbonoCapital, cuota.AbonoInteres, 
                         cuota.AbonoCapital + cuota.AbonoInteres + cuota.AjusteAbonoCapital+ cuota.AjusteAbonoInteres AS TotalCuota, SUM(IFNULL(abono.Valor, 0)) AS Abonos,
                            (select count(0) from cuota where credito.CreditoId=cuota.CreditoId) as CantCuotas
                          FROM            credito INNER JOIN
                         cuota ON credito.CreditoId = cuota.CreditoId LEFT OUTER JOIN
                         cliente ON credito.ClienteId = cliente.ClienteId LEFT OUTER JOIN
                         abono ON cuota.CuotaId = abono.CuotaId and abono.Estado=1";
            q=q+"  WHERE        (credito.Estado = 1)  AND (Cuota.Fecha <= '"+strfecha+"') AND (cliente.EmpresaId = {0})";
            if(UsuarioId>0)
                q=q+" and credito.usuarioId='"+UsuarioId.ToString()+"'";
            q = q + "  GROUP BY cuota.CuotaId, cliente.Nit, cliente.Nombre, credito.CreditoId, cuota.Numero, cuota.Fecha, ";
            q = q + "   cuota.AbonoCapital, cuota.AbonoInteres, credito.Estado  ORDER BY cliente.Nombre asc";
            */
            ViewBag.controlador = controlador;
            //var cxc = db.Database.SqlQuery<Cuotas>(q, empresaId);
            //var final = from c in cxc where(c.Abonos < (c.AbonoCapital + c.AbonoInteres)) select c; 
            //return View(final.ToList());
            return View(getCuotasxCobrar(empresaId,fecha,UsuarioId));





        }

        public IEnumerable<Cuotas>  getCuotasxCobrar(int empresaId,string fecha, int? UsuarioId)
        {
            string strfecha = "";
            if (MiUtil.isDate(fecha))
            {
                ViewBag.CurrentFilter = fecha;
                strfecha = MiUtil.fechaToSQL(DateTime.ParseExact(fecha, "dd/MM/yyyy", null), 0);
            }

            var q = @"SELECT  cuota.CuotaId, cliente.Nit, cliente.Nombre, credito.CreditoId,credito.CreditoNro, cuota.Numero, cuota.Fecha, 
                         cuota.AbonoCapital+ IFNULL(cuota.AjusteAbonoCapital,0) as AbonoCapital,
                         cuota.AbonoInteres+ IFNULL(cuota.AjusteAbonoInteres,0) as AbonoInteres, 
                         cuota.AbonoCapital + cuota.AbonoInteres + IFNULL(cuota.AjusteAbonoCapital,0)+ IFNULL(cuota.AjusteAbonoInteres,0) AS TotalCuota, 
                         SUM(IFNULL(abono.Valor, 0)) AS Abonos,
                         (select count(0) from cuota where credito.CreditoId=cuota.CreditoId) as CantCuotas
                          FROM            credito INNER JOIN
                         cuota ON credito.CreditoId = cuota.CreditoId LEFT OUTER JOIN
                         cliente ON credito.ClienteId = cliente.ClienteId LEFT OUTER JOIN
                         abono ON cuota.CuotaId = abono.CuotaId and abono.Estado=1";
            q = q + "  WHERE        (credito.Estado = 1)  AND (Cuota.Fecha <= '" + strfecha + "') AND (cliente.EmpresaId = {0})";
            if (UsuarioId > 0)
                q = q + " and credito.usuarioId='" + UsuarioId.ToString() + "'";
            q = q + "  GROUP BY cuota.CuotaId, cliente.Nit, cliente.Nombre, credito.CreditoId, cuota.Numero, cuota.Fecha, ";
            q = q + "   cuota.AbonoCapital, cuota.AbonoInteres, credito.Estado  ORDER BY cliente.Nombre DESC";

            

            
            var cxc = db.Database.SqlQuery<Cuotas>(q, empresaId);
            var final = from c in cxc where (c.Abonos < (c.AbonoCapital + c.AbonoInteres)) select c;
            return final.ToList();
        }

    }
}
