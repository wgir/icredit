using CrediAdmin.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using CrediAdmin.Util;
using System.Configuration;
namespace CrediAdmin.Controllers
{
  //  [Authorize]
    public class HomeController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();
        private ApplicationSignInManager _signInManager;
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

         public ApplicationSignInManager SignInManager
         {
             get
             {
                 return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
             }
             private set
             {
                 _signInManager = value;
             }
         }

    //    [FindRefreshDetectFilter]
        public ActionResult Index(int? saliendo)
        {
            string urlPrevia = "";
            string urlActual = System.Web.HttpContext.Current.Request.Url.ToString();
            if(System.Web.HttpContext.Current.Request.UrlReferrer!=null)
                urlPrevia=System.Web.HttpContext.Current.Request.UrlReferrer.ToString();

            if (!Request.IsAuthenticated && saliendo == null && !urlPrevia.Equals("") && !urlPrevia.Equals(urlActual) )
            {
                loghome log = db.loghome.Find(1);
                log.Visitantes = log.Visitantes + 1;
                db.SaveChanges();
            }
            if (Session["EmpresaId"] == null)
            {
                try {
                    Request.GetOwinContext().Authentication.SignOut();
                //var accountController = new AccountController();
                //accountController.LogOff();
                }
                catch (Exception ex) { }
            }

           // RouteData.Values["IsRefreshed"] = false;
            ViewBag.controlador = "Home";
            ViewBag.Login = new LoginViewModel();
            empresa e = new empresa();
            return View(e);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            //return View();
            return RedirectToAction("Home", "Index");
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            empresa e = new empresa();
            ViewBag.Login = model;
            if (!ModelState.IsValid)
                       return View("Index",e);
           
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            //var result = await SignInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, shouldLockout: false);
            var result = await validar(model);
            switch (result)
            {
                case SignInStatus.Success:
                    aspnetusers user=db.aspnetusers.Where(us=>us.UserName.ToUpper().Equals(model.Login.ToUpper())).FirstOrDefault();
                    usuario uxe = db.usuario.Where(u => u.aspnetusersId.Equals(user.Id)).FirstOrDefault();
                    if (uxe != null)
                    {
                        Session["EmpresaNombre"] = uxe.empresa.Nombre;
                        Session["EmpresaId"] = uxe.EmpresaId;
                        return RedirectToAction("Index", "Panel");
                        //return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View("Index", e);
                    }
                   // break;
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Datos no validos");
                    return View("Index", e);
            }
        }

        public async Task<SignInStatus> validar(LoginViewModel model)
        {
            return await SignInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, shouldLockout: false);
        }

        public JsonResult CrearEmpresa(string Nombre, string EmpEmail)
        {
            ModelState.Clear();
            empresa e = new empresa();
            e.Nombre=Nombre;
            e.EmpEmail = EmpEmail;
            e.Nit = "";
            e.EmpAnioActual = DateTime.Now.Year;
            e.EmpMesActual = DateTime.Now.Month;
            //FechaCreacion=new DateTime(DateTime.Now.Year, DateTime.Now.Month,DateTime.Now.Day)
            string s = "";
            bool ok = false;
            if (TryValidateModel(e))
            {
                if (ModelState.IsValid)
                {
                    string AspUserId = "";
                    try
                    {
                        string email = EmpEmail.Trim().ToUpper();
                        if (!db.aspnetusers.Any(c => c.UserName.Trim().ToUpper() == email))
                        {
                            string[] arr1 = crearUsuario("Administrador", EmpEmail, UserManager).Split(',');
                            AspUserId = arr1[0];
                            string AspUserPasswd = arr1[1];
                            if (!AspUserId.Equals(""))
                            {
                                string url="http://"+ConfigurationManager.AppSettings["URLServer"].ToString()+"/icredit";
                                string mensaje = "<p <span style=\"font-size: 10pt;\">Apreciado&nbsp;" + Nombre + "</span></p>";
                                mensaje = mensaje + "<p <span style=\"font-size: 10pt;\">Ingresando a&nbsp;<a style=\"font-family: Calibri, sans-serif; font-size: 11pt;\" href=\""+url+"\">iCredit.com</a><span style=\"color: #1f497d; font-family: Calibri, sans-serif;\">,&nbsp;</span><span style=\"color: #1f497d; font-family: Calibri, sans-serif;\">&nbsp;</span>podr&aacute;s utilizar nuestra plataforma para administrar tus creditos </span></p>";
                                mensaje = mensaje + "<p <span style=\"font-size: 10pt;\">Nombre de Usuario: " + EmpEmail + "</span></p>";
                                mensaje = mensaje + "<p <span style=\"font-size: 10pt;\">Clave: " + AspUserPasswd + "</span></p>";
                                mensaje = mensaje + "<p><span style=\"font-size: 10pt;\">&nbsp;</span></p>";

                                //Nombre + "" + AspUserPasswd;
                                if (EnviarCorreoController.enviarFromAWS(1, EmpEmail, "Bienvenido a iCredit", mensaje))
                                {
                                    using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                                    {
                                        try
                                        {
                                            db.empresa.Add(e);
                                            usuario uxe = new usuario();
                                            consecutivo con = new consecutivo();
                                            con.empresa = e;
                                            con.CreditoNro = 0;
                                            con.RetiroInteresNro = 0;
                                            con.AbonoNro = 0;
                                            db.consecutivo.Add(con);
                                            uxe.UsuNombre = Nombre;
                                            uxe.aspnetusersId = AspUserId;
                                            uxe.UsuEmail = EmpEmail;
                                            uxe.empresa = e;

                                            db.usuario.Add(uxe);
                                            db.SaveChanges();
                                          //  EnviarCorreoController.sendVerificationEmailAmazonSES(EmpEmail);
                                            dbTran.Commit();
                                            ok = true;
                                            iniciarEmpresa(e.EmpresaId);
                                           
                                        }
                                        catch (Exception ex)
                                        {
                                            ok = false;
                                            eliminarUsuarioAspNet(AspUserId);
                                            s = String.Format(ex.ToString());
                                            dbTran.Rollback();
                                            
                                        }
                                    }
                                }
                                else
                                {
                                    s = "No fue posible enviar correo electronico";
                                    eliminarUsuarioAspNet(AspUserId);
                                }
                            }
                        }
                        else
                            s = "Este Correo Electronico ya se encuentra Registrado";
                    }
                    catch (Exception ex)
                    {
                        s = ex.ToString().Substring(0, 200);
                        //   ViewBag.mensaje = "Exception caught in CreateTestMessage2(): {0}" + ex.ToString();
                        //  Console.WriteLine("Exception caught in CreateTestMessage2(): {0}", ex.ToString());
                        eliminarUsuarioAspNet(AspUserId);
                    }

                }
            }


                if (!ok)
                {
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            s = s + error.ErrorMessage;
                        }
                    }
                }
                // return Json(new { success = false });
           
            return Json(new { success = ok, Error = s }, JsonRequestBehavior.AllowGet);
        }

        public string crearUsuario(string rol,string EmpEmail,ApplicationUserManager um)
        {
            //string rol = "Administrador";
            var user = new ApplicationUser { UserName = EmpEmail, Email =EmpEmail };
            string passwd = GenerarContrasena();
                    //   var result = await UserManager.CreateAsync(user, model.Password);
            IdentityResult result = um.Create(user, passwd);
            if (result.Succeeded)
             {
                        um.AddToRole(user.Id, rol);
                        return user.Id+","+passwd;
             }
                   // AddErrors(result);
               
            
            // If we got this far, something failed, redisplay form
                    return "";
        }

        public void eliminarUsuarioAspNet(string userId)
        {
            
            CrediAdminContext storeContext = new CrediAdminContext();
            var item = storeContext.aspnetusers.Find(userId);
            if (item != null)
            {
                storeContext.aspnetusers.Remove(item);
                storeContext.SaveChanges();
            }
        }

        

        public static string GenerarContrasena()
        {
            Random rand = new Random();
            string password = Membership.GeneratePassword(6, 0);
            password = Regex.Replace(password, @"[^a-zA-Z0-9]", m => rand.Next(0, 9).ToString());
            return password;
        }
        


        /*Inicia tablas generales de la empresa q entrra por parametro con datos de la empresa 1
         **/
        public void iniciarEmpresa(int empresaId)
        {
            var ca = db.conceptoaporte.Where(e=>e.EmpresaId==1);
            foreach(conceptoaporte item in ca)
            {
                conceptoaporte caporte = new conceptoaporte();
                caporte.Nombre = item.Nombre;
                caporte.EmpresaId = empresaId;
                db.conceptoaporte.Add(caporte);
            }
            var ce = db.conceptoretiro.Where(e => e.EmpresaId == 1);
            foreach (conceptoretiro item in ce)
            {
                conceptoretiro cretiro = new conceptoretiro();
                cretiro.Nombre = item.Nombre;
                cretiro.EmpresaId = empresaId;
                db.conceptoretiro.Add(cretiro);
            }

            var ci = db.ciudad.Where(e => e.EmpresaId == 1);
            foreach (ciudad item in ci)
            {
                ciudad ciudad = new ciudad();
                ciudad.Nombre = item.Nombre;
                ciudad.EmpresaId = empresaId;
                db.ciudad.Add(ciudad);
            }
            db.SaveChanges();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Acerca de Nosotros.";

            return View();
        }

        public ActionResult Contact(string mensaje)
        {
            if (mensaje == null)
                ViewBag.mensajeRegistroContacto = "";
            else
                ViewBag.mensajeRegistroContacto = mensaje;

            ViewBag.Message = "Pagina de Contacto.";
            contacto c = new contacto();
            return View(c);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}