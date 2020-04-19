using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrediAdmin.Models;
using CrediAdmin.ViewModels;
using Rotativa;
using Rotativa.Options;
using CrediAdmin.Util;


namespace CrediAdmin.Controllers
{
    public class AbonoController: Controller //: PdfViewController
   // public class AbonoController: PdfViewController
    {
        private CrediAdminContext db = new CrediAdminContext();

        //
        // GET: /Abono/
        [SessionExpire]
        [Authorize]

        public ViewResult Index(string id,string controlador,string fechaActual)
        {
            string idDecrypted = MiUtil.desEncriptar(id);
            int intId = Convert.ToInt32(idDecrypted);
            
            cuota cuota = db.cuota.Find(intId);
            ViewBag.CuotaId = intId;
            ViewBag.CreditoId = cuota.CreditoId;
            ViewBag.CreditoNro = cuota.credito.CreditoNro;
            ViewBag.NombreCliente = cuota.credito.cliente.Nombre;
            ViewBag.fechaActual = fechaActual;

            ViewBag.cuotaNumero = cuota.Numero;
            ViewBag.saldo = cuota.calcularSaldoxCapital() + cuota.calcularSaldoxInteres();
            var abonos = db.abono.Include(a => a.cuota).Include(a=>a.cuota.credito).Where(s => s.cuota.CreditoId == cuota.CreditoId).OrderByDescending(a=>a.AbonoId) ;
            ViewBag.controlador = controlador;
            ViewBag.Cuota = cuota;
            return View(abonos.ToList());
        }

        // GET: /Abono/Details/5

        public ViewResult Details(string id,string controlador)
        {
            ViewBag.controlador = controlador;
            

            string idDecrypted = MiUtil.desEncriptar(id);
            int intId = Convert.ToInt32(idDecrypted);

            abono abono = db.abono.Find(intId);
            ViewBag.AbonoId = intId;
            return View(abono);
        }

        //
        // GET: /Abono/Create

        public ActionResult Create(string id,string controlador, string fechaActual)
        {
            string idDecrypted = MiUtil.desEncriptar(id);
            int intId = Convert.ToInt32(idDecrypted);
            cuota cuota = db.cuota.Find(intId);
            ViewBag.cuotaId = intId;
            ViewBag.CreditoId = cuota.CreditoId;
            ViewBag.CreditoNro = cuota.credito.CreditoNro;
            ViewBag.cuotaNumero = cuota.Numero;
            ViewBag.saldo = cuota.calcularSaldoxCapital() + cuota.calcularSaldoxInteres();
            ViewBag.EmpresaId = cuota.credito.EmpresaId;
            ViewBag.fechaActual = fechaActual;
            //ViewBag.CuotaId = new SelectList(db.Cuotas, "CuotaId", "CuotaId", cuotaId);

            abono a=new abono();
            a.CuotaId = intId;
            a.cuota = cuota;
            a.Valor = ViewBag.saldo;
            a.Paga = a.Valor;
            a.Fecha = DateTime.Now;
            a.Estado = true;
            ViewBag.controlador = controlador;
            return View(a);
        } 

        //
        // POST: /Abono/Create
        [SessionExpire]
        [Authorize]
        [HttpPost]
        public ActionResult Create(abono abono,string controlador,int EmpresaId,string fechaActual)
        {
            ViewBag.EmpresaId = EmpresaId;
            if (ModelState.IsValid)
            {
                if (abono.Paga > abono.Valor)
                    abono.Devolucion = abono.Paga - abono.Valor;
                else
                {
                    abono.Valor = abono.Paga;
                    abono.Devolucion = 0;
                }
                consecutivo c = db.consecutivo.Find(EmpresaId);
                c.AbonoNro = c.AbonoNro + 1;
                abono.AbonoNro = c.AbonoNro;
                
                db.abono.Add(abono);
                db.SaveChanges();
                // return RedirectToAction("EnviarCorreoFromAWS", new { abonoId = MiUtil.encriptar(abono.AbonoId.ToString()), abonoNro = abono.AbonoNro });
                //return RedirectToAction("Index", controlador, new { id = MiUtil.encriptar(abono.CuotaId.ToString()), controlador = controlador, fechaActual=fechaActual });  
                return RedirectToAction("Index", new { id = MiUtil.encriptar(abono.CuotaId.ToString()), controlador = controlador, fechaActual = fechaActual });
            }
            ViewBag.CuotaId = abono.CuotaId;
            cuota cuota = db.cuota.Find(abono.CuotaId);
            abono.cuota = cuota;

            ViewBag.cuotaId = abono.CuotaId;
            ViewBag.CreditoId = cuota.CreditoId;
            ViewBag.CreditoNro = cuota.credito.CreditoNro;
            ViewBag.cuotaNumero = cuota.Numero;
            ViewBag.saldo = cuota.calcularSaldoxCapital() + cuota.calcularSaldoxInteres();
            ViewBag.controlador = controlador;
            //ViewBag.CuotaId = new SelectList(db.Cuotas, "CuotaId", "CuotaId", abono.CuotaId);
            return View(abono);
        }
        
        //
        // GET: /Abono/Edit/5
 
        public ActionResult Edit(string id,string controlador)
        {
            string idDecrypted = MiUtil.desEncriptar(id);
            int intId = Convert.ToInt32(idDecrypted);

            ViewBag.controlador = controlador;
            abono abono = db.abono.Find(intId);
            cuota cuota = db.cuota.Find(abono.CuotaId);
            abono.cuota = cuota;
            ViewBag.CuotaId = cuota.CuotaId;
            ViewBag.idcuota = cuota.CuotaId;
            ViewBag.cuotaNumero = cuota.Numero;
            ViewBag.CreditoId = cuota.CreditoId;
           // ViewBag.CuotaId = new SelectList(db.Cuotas, "CuotaId", "CuotaId", abono.CuotaId);

            return View(abono);
        }

        //
        // POST: /Abono/Edit/5

        [HttpPost]
        public ActionResult Edit(abono abono, string controlador)
        {
            ViewBag.controlador = controlador;
            if (ModelState.IsValid)
            {
                db.Entry(abono).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id =  MiUtil.encriptar(abono.CuotaId.ToString()), controlador = controlador });
            }
            cuota cuota = db.cuota.Find(abono.CuotaId);
            ViewBag.cuotaId = cuota.CuotaId;
            ViewBag.cuotaNumero = cuota.Numero;
            ViewBag.CreditoId = cuota.CreditoId;
            ViewBag.CuotaId = abono.CuotaId;
            ViewBag.idcuota = cuota.CuotaId;
          //  ViewBag.CuotaId = new SelectList(db.Cuotas, "CuotaId", "CuotaId", abono.CuotaId);
          
            return View(abono);
        }


        public ImpresionAbono impresionAbono(int abonoId)
        {
            ImpresionAbono ia = null;
            abono abono = (abono)db.abono.Find(abonoId);
            if(abono.Estado)
            {
                cuota cuota = (cuota)db.cuota.Find(abono.CuotaId);
                credito credito = (credito)db.credito.Find(cuota.CreditoId);
                empresa empresa = (empresa)db.empresa.Find(credito.cliente.EmpresaId);
                ia = new ImpresionAbono();
                ia.LogoEmpresa = "~/Uploads/Logos/" + empresa.LogoUrl;
                ia.EmpresaId = empresa.EmpresaId;
                ia.EmpresaNit = empresa.Nit;
                ia.EmpresaNombre = empresa.Nombre;
                ia.ClienteNit = credito.cliente.Nit;
                ia.ClienteNombre = credito.cliente.Nombre;
                ia.EmailCliente = credito.cliente.Email;
                ia.CreditoId = credito.CreditoId;
                ia.CreditoNro = credito.CreditoNro;
                ia.CreditoValor = credito.Valor;
                ia.CreditoCantCuotas = credito.cuota.Count;
                ia.CuotaNro = cuota.Numero;
                ia.CuotaValor = cuota.AbonoCapital + cuota.AbonoInteres + MiUtil.nullTodecimal(cuota.AjusteAbonoCapital) + MiUtil.nullTodecimal(cuota.AjusteAbonoInteres);
                ia.AbonoId = abonoId;
                ia.AbonoNro = abono.AbonoNro;
                ia.AbonoFecha = abono.Fecha;
                ia.AbonoValor = abono.Valor;
                decimal? porcenInteres=cuota.porcentajeInteres();
                decimal? porcenCapital = cuota.porcentajeCapital();
                ia.AbonoInteres = ia.AbonoValor * (porcenInteres / 100);
                ia.AbonoCapital = ia.AbonoValor * (porcenCapital / 100);
                ia.CreditoSaldoInteres = credito.calcularTotalInteres() - credito.calcularAbonoInteres(abono.Fecha);
                ia.CreditoSaldoCapital = credito.calcularTotalCapital() - credito.calcularAbonoCapital(abono.Fecha);
                ia.TotalAbono = ia.AbonoCapital + ia.AbonoInteres;
                ia.SaldoCuota = cuota.calcularSaldoxCapital(abono.Fecha) + cuota.calcularSaldoxInteres(abono.Fecha);
            //ia.CuotaValor - ia.TotalAbono;
            }
            return ia;
        }
        public ActionResult ComprobanteAbono(int id)
        {
            return PartialView("ComprobanteAbono", impresionAbono(id));
        }

        public byte[] crearArchivo(int id,int abonoNro)
        {
            var pdfResult = new ActionAsPdf("ComprobanteAbono",new { id = id }) { FileName = "Comprobante.pdf" };
            var binary = pdfResult.BuildPdf(this.ControllerContext);
            try
            {
                string file = this.nombreArchivo(id,abonoNro);
                using (System.IO.FileStream fs = System.IO.File.Create(file))
                {
                    fs.Write(binary, 0, (int)binary.Length);
                }
            }catch(Exception e)
            {}
            return binary;
        }
        
        public ActionResult Imprimir(string id)
        {
           //return File(this.crearArchivo(id), "application/pdf");
            
            string idDecrypted = MiUtil.desEncriptar(id);
            int intId = Convert.ToInt32(idDecrypted);

            return PartialView("ComprobanteAbono", impresionAbono(intId));
        }

        public string nombreArchivo(int abonoId,int abonoNro)
        {

            return AppDomain.CurrentDomain.BaseDirectory + "pdfs\\Abono_" + abonoId.ToString() +"_"+abonoNro.ToString()+ ".pdf";
        }

        //public ActionResult Enviar(int abonoId, int cuotaId, string controlador)
        //{
        //    this.crearArchivo(abonoId);
        //    string file = this.nombreArchivo(abonoId);
        //    ImpresionAbono ia = impresionAbono(abonoId);
        //   // EnviarCorreoController enviarcorreo = new EnviarCorreoController();
        //   // return enviarcorreo.EnviarCorreo(ia.EmpresaId, ia.EmailCliente, file, "Comprobante de Abono", "Adjunto encontratra el comprobante de abono");
        //    //ia.EmailCliente, file);
        //    return RedirectToAction("EnviarCorreo", "EnviarCorreo", new { controlador=controlador, empresaId = ia.EmpresaId, dirEmail = ia.EmailCliente, archivo = file, asunto = "Comprobante de Abono", mensaje = "Adjunto encontratra el comprobante de abono" });
        //    //return RedirectToAction(("Index", new { id = cuotaId, controlador = controlador });
        //}

        public ActionResult EnviarCorreo(string abonoId,int abonoNro)
        {
            string idDecrypted = MiUtil.desEncriptar(abonoId);
            int intId = Convert.ToInt32(idDecrypted);

       
            ViewBag.abonoId = intId;

           
            ImpresionAbono ia = impresionAbono(intId);
            if(ia!=null)
            {
                crearArchivo(intId, abonoNro);
                string file = nombreArchivo(intId, abonoNro);
                ViewBag.abonoId = abonoId;
                paramcorreo paramcorreo = (paramcorreo)db.paramcorreo.FirstOrDefault(em => em.EmpresaId == ia.EmpresaId);
                if (paramcorreo != null)
                {
                    //  paramcorreos.ToList().Where(em => em.EmpresaId == empresaId);
                    //db.ParamCorreos.Find(empresaId);
                    empresa empresa = (empresa)db.empresa.Find(ia.EmpresaId);
                    EnviarCorreo enviarcorreo = new EnviarCorreo()
                    {
                        Empresa = empresa.Nombre,
                        Servidor = paramcorreo.Servidor,
                        Puerto = paramcorreo.Puerto,
                        Usuario = paramcorreo.Usuario,
                        Password = paramcorreo.Password,
                        Destinatario = ia.EmailCliente,
                        Asunto = "Comprobante de Abono",
                        Mensaje = "Adjunto encontrara el comprobante de abono",
                        Adjunto = file

                    };
                    return View("EnviarCorreo", enviarcorreo);
              }else
                {
                    ViewBag.Layout = 0;
                    ViewBag.action = "index";
                    ViewBag.controller = "Home";
                    ViewBag.tipo = "bg-warning";
                    ViewBag.mensaje = "Debe definir los parametros de correo para poder enviar correos";
                    return View("../Shared/Mensaje");
                }
            }
            else
            {
                ViewBag.Layout = 0;
                ViewBag.action = "index";
                ViewBag.controller = "Home";
                ViewBag.tipo = "bg-warning";
                ViewBag.mensaje = "No es posible enviar comprobantes de abonos inactivos";
                return View("../Shared/Mensaje");
            }
        }

        public ActionResult EnviarCorreoFromAWS(string abonoId, int abonoNro)
        {
            string idDecrypted = abonoId;
                //MiUtil.desEncriptar(abonoId);
            int intId = Convert.ToInt32(idDecrypted);


            ViewBag.abonoId = intId;


            ImpresionAbono ia = impresionAbono(intId);
            if (ia != null)
            {
                crearArchivo(intId, abonoNro);
                string file = nombreArchivo(intId, abonoNro);
                ViewBag.abonoId = abonoId;
                paramcorreo paramcorreo = (paramcorreo)db.paramcorreo.FirstOrDefault(em => em.EmpresaId == 1);
                if (paramcorreo != null)
                {
                    //  paramcorreos.ToList().Where(em => em.EmpresaId == empresaId);
                    //db.ParamCorreos.Find(empresaId);
                    empresa empresa = (empresa)db.empresa.Find(ia.EmpresaId);
                    EnviarCorreo enviarcorreo = new EnviarCorreo()
                    {
                        Empresa = empresa.Nombre,
                        Servidor = paramcorreo.Servidor,
                        Puerto = paramcorreo.Puerto,
                        Usuario = paramcorreo.Usuario,
                        Password = paramcorreo.Password,
                        Destinatario = ia.EmailCliente,
                        Asunto = "Comprobante de Abono",
                        Mensaje = "Adjunto encontrara el comprobante de abono",
                        Adjunto = file

                    };
                    return View("EnviarCorreo", enviarcorreo);
                }
                else
                {
                    ViewBag.Layout = 0;
                    ViewBag.action = "index";
                    ViewBag.controller = "Home";
                    ViewBag.tipo = "bg-warning";
                    ViewBag.mensaje = "Debe definir los parametros de correo para poder enviar correos";
                    return View("../Shared/Mensaje");
                }
            }
            else
            {
                ViewBag.Layout = 0;
                ViewBag.action = "index";
                ViewBag.controller = "Home";
                ViewBag.tipo = "bg-warning";
                ViewBag.mensaje = "No es posible enviar comprobantes de abonos inactivos";
                return View("../Shared/Mensaje");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}