using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrediAdmin.Models;
using System.Net.Mail;
using CrediAdmin.ViewModels;
using CrediAdmin.Util;
using System.Configuration;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Amazon;

namespace CrediAdmin.Controllers
{ 
    public class EnviarCorreoController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();

       
        public static bool enviarCorreo(int empresaRemitente, string email, string asunto, string mensaje)
        {
            CrediAdminContext db = new CrediAdminContext();
            paramcorreo paramcorreo = (paramcorreo)db.paramcorreo.FirstOrDefault(em => em.EmpresaId == empresaRemitente);
            if (paramcorreo != null)
            {
                empresa empresa = (empresa)db.empresa.Find(empresaRemitente);
                EnviarCorreo enviarcorreo = new EnviarCorreo()
                {
                    Empresa = empresa.Nombre,
                    Servidor = paramcorreo.Servidor,
                    Puerto = paramcorreo.Puerto,
                    Usuario = paramcorreo.Usuario,
                    Password = paramcorreo.Password,
                    Destinatario = email,
                    Asunto = asunto, //Nombre.ToUpper()+" Bienvenido a CrediAdmin",
                    Mensaje = mensaje,
                    //"<p>Ya puede ingresar a nuestra plataforma para administrar sus creditos.</p> <p>Nombre de Usuario:" + EmpEmail + "</p><p>Contraseña:" + clave + "</p>",
                    Adjunto = ""
                };

                var fromAddress = new MailAddress(enviarcorreo.Usuario, enviarcorreo.Empresa);
                var toAddress = new MailAddress(enviarcorreo.Destinatario, "");
                string fromPassword = enviarcorreo.Password;
                string subject = enviarcorreo.Asunto;
                string body = enviarcorreo.Mensaje;

                System.Net.Mail.Attachment attachment = null;
                if (!String.IsNullOrEmpty(enviarcorreo.Adjunto))
                    attachment = new System.Net.Mail.Attachment(enviarcorreo.Adjunto);

                var smtp = new SmtpClient
                {
                    Host = enviarcorreo.Servidor,
                    Port = enviarcorreo.Puerto,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    // UseDefaultCredentials = true,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
                };
                MailMessage message = new MailMessage(fromAddress, toAddress);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                if (attachment != null)
                    message.Attachments.Add(attachment);
                try
                {
                    smtp.Send(message);
                    //Response.Redirect(urlPapa, true);   
                    return true;
                }
                catch (Exception ex)
                {
                    //ViewBag.mensaje = "Exception caught in CreateTestMessage2(): {0}" + ex.ToString();
                    Console.WriteLine("Exception caught in CreateTestMessage2(): {0}", ex.ToString());
                }
            }
            return false;
        }

        

        public static bool enviarFromAWS(int empresaRemitente, string email, string asunto, string mensaje)
        {
            CrediAdminContext db = new CrediAdminContext();
            paramcorreo paramcorreo = (paramcorreo)db.paramcorreo.FirstOrDefault(em => em.EmpresaId == empresaRemitente);
            if (paramcorreo != null)
            {
                empresa empresa = (empresa)db.empresa.Find(empresaRemitente);
                EnviarCorreo enviarcorreo = new EnviarCorreo()
                {
                    Empresa = empresa.Nombre,
                    Servidor = paramcorreo.Servidor,
                    Puerto = paramcorreo.Puerto,
                    Usuario = paramcorreo.Usuario,
                    Password = paramcorreo.Password,
                    Destinatario = email,
                    Asunto = asunto, //Nombre.ToUpper()+" Bienvenido a CrediAdmin",
                    Mensaje = mensaje,
                    //"<p>Ya puede ingresar a nuestra plataforma para administrar sus creditos.</p> <p>Nombre de Usuario:" + EmpEmail + "</p><p>Contraseña:" + clave + "</p>",
                    Adjunto = ""
                };

                var fromAddress = new MailAddress(enviarcorreo.Usuario, enviarcorreo.Empresa);
                var toAddress = new MailAddress(enviarcorreo.Destinatario, "");
                string fromPassword = enviarcorreo.Password;
                string subject = enviarcorreo.Asunto;
                string body = enviarcorreo.Mensaje;

            String FROM =enviarcorreo.Usuario; // "SENDER@EXAMPLE.COM";   // Replace with your "From" address. This address must be verified.
            String TO = email;//"RECIPIENT@EXAMPLE.COM";  // Replace with a "To" address. If your account is still in the
            // sandbox, this address must be verified.

            String SUBJECT = asunto;//"Amazon SES test (SMTP interface accessed using C#)";
            String BODY = mensaje;//"This email was sent through the Amazon SES SMTP interface by using C#.";

            // Supply your SMTP credentials below. Note that your SMTP credentials are different from your AWS credentials.
            String SMTP_USERNAME =  ConfigurationManager.AppSettings["AWSAccessKey"].ToString(); 
            //  "AKIAIZOJPFIFCACIJ53A";  // Replace with your SMTP username. 
            String SMTP_PASSWORD = ConfigurationManager.AppSettings["AWSSecretKey"].ToString();
             //   "Au9jMcd8Z07RmDNYgEvxs9f+3rpf46RruJ+1LgGEbzr5";  // Replace with your SMTP password.

            // Amazon SES SMTP host name. This example uses the US West (Oregon) region.
            String HOST = ConfigurationManager.AppSettings["AWSServer"].ToString(); 

            // Port we will connect to on the Amazon SES SMTP endpoint. We are choosing port 587 because we will use
            // STARTTLS to encrypt the connection.
            const int PORT = 587;

            MailMessage message = new MailMessage(fromAddress, toAddress);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            System.Net.Mail.Attachment attachment = null;
            if (!String.IsNullOrEmpty(enviarcorreo.Adjunto))
                attachment = new System.Net.Mail.Attachment(enviarcorreo.Adjunto);

            // Create an SMTP client with the specified host name and port.
            using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(HOST, PORT))
            {
                // Create a network credential with your SMTP user name and password.
                client.Credentials = new System.Net.NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

                // Use SSL when accessing Amazon SES. The SMTP session will begin on an unencrypted connection, and then 
                // the client will issue a STARTTLS command to upgrade to an encrypted connection using SSL.
                client.EnableSsl = true;

                // Send the email. 
                try
                {
                  //  Console.WriteLine("Attempting to send an email through the Amazon SES SMTP interface...");
                    //client.Send(FROM, TO, SUBJECT, BODY);
                    client.Send(message);
                    //Console.WriteLine("Email sent!");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The email was not sent.");
                    Console.WriteLine("Error message: " + ex.Message);
                }
            }

            
            }
            return false;
        }

        public static bool sendVerificationEmailAmazonSES(string email)
        {
            string accesKey =  ConfigurationManager.AppSettings["AWSAccessKey"].ToString();
            string secretKey = ConfigurationManager.AppSettings["AWSSecretKey"].ToString();
            String HOST = "https://"+ConfigurationManager.AppSettings["AWSServer"].ToString();
            var region = RegionEndpoint.USWest2;
            AmazonSimpleEmailServiceConfig amConfig = new AmazonSimpleEmailServiceConfig { ServiceURL = HOST, RegionEndpoint = region };

            bool result = false;
            //try {
                    
                    //.GetBySystemName("us-west-2");
                    Amazon.SimpleEmail.Model.VerifyEmailAddressRequest request = new Amazon.SimpleEmail.Model.VerifyEmailAddressRequest();
                    Amazon.SimpleEmail.Model.VerifyEmailAddressResponse response = new Amazon.SimpleEmail.Model.VerifyEmailAddressResponse();
                    Amazon.SimpleEmail.AmazonSimpleEmailServiceClient client = new AmazonSimpleEmailServiceClient(accesKey, secretKey, region);
                        //, amConfig);
                
                    if (client != null)
                    {

                        request.EmailAddress = email.Trim();
                        response = client.VerifyEmailAddress(request);

                        if (!string.IsNullOrEmpty(response.ResponseMetadata.RequestId))
                        {
                            result = true;
                        }
                    }
            //}
            //catch (Exception ex)
            // {
            //     Console.WriteLine(ex.Message);
            //   }

            return result;

          //  //INITIALIZE AWS CLIENT//
          // // AmazonSimpleEmailServiceConfig amConfig = new AmazonSimpleEmailServiceConfig();
          // // amConfig.UseSecureStringForAwsSecretKey  = false;
          ////  amConfig.c
          //  //String HOST = "http://"+ConfigurationManager.AppSettings["AWSServer"].ToString();
          //  try
          //  {
          //      //AmazonSimpleEmailServiceConfig amConfig = new AmazonSimpleEmailServiceConfig { ServiceURL = HOST };
              
          //      string HOST = ConfigurationManager.AppSettings["AWSServer"].ToString(); 
          //      const int PORT = 587;
          //      // Create an SMTP client with the specified host name and port.
          //      using (AmazonSimpleEmailServiceClient client = new AmazonSimpleEmailServiceClient(accesKey, secretKey))
          //      {
          //          // Create a network credential with your SMTP user name and password.
          //          client.Credentials = new System.Net.NetworkCredential(accesKey, secretKey);

          //          // Use SSL when accessing Amazon SES. The SMTP session will begin on an unencrypted connection, and then 
          //          // the client will issue a STARTTLS command to upgrade to an encrypted connection using SSL.
          //          //client.EnableSsl = true;

          //          //AmazonSimpleEmailServiceClient amzClient = new AmazonSimpleEmailServiceClient(accesKey, secretKey);
          //          //ConfigurationManager.AppSettings["AWSAccessKey"].ToString(),
          //          //ConfigurationManager.AppSettings["AWSSecretKey"].ToString(), amConfig);

          //          //VERIFY EMAIL//
          //          VerifyEmailAddressRequest veaRequest = new VerifyEmailAddressRequest();
          //          veaRequest.EmailAddress = email;// "Your_Email_To_Verify";
          //          VerifyEmailAddressResponse veaResponse = client.VerifyEmailAddress(veaRequest);
          //      }
          //      // int x = 0;
          //  }
          //  catch (Exception ex)
          //  {
          //      Console.WriteLine(ex.Message);

          //      //return false;
          //  }
            //Response.Write(veaResponse..ResponseMetadata.RequestId);
        }


       
        //[HttpPost]
        //public ActionResult EnviarCorreo(EnviarCorreo enviarcorreo, string button, string urlPapa, string abonoId)
        //{
        //    //if (button.Equals("Cancelar"))
        //    //    return RedirectToAction("Index", "CuotasxCobrar", new { controlador = controlador });

        //    if (ModelState.IsValid)
        //    {
        //        var fromAddress = new MailAddress(enviarcorreo.Usuario,enviarcorreo.Empresa);
        //        var toAddress = new MailAddress(enviarcorreo.Destinatario,"");
        //        string fromPassword = enviarcorreo.Password;
        //        string subject = enviarcorreo.Asunto;
        //        string body = enviarcorreo.Mensaje;

        //        System.Net.Mail.Attachment attachment;
        //        attachment = new System.Net.Mail.Attachment(enviarcorreo.Adjunto);
                
        //        var smtp = new SmtpClient
        //        {
        //            Host = enviarcorreo.Servidor,
        //            Port = enviarcorreo.Puerto,
        //            EnableSsl = true,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //           // UseDefaultCredentials = true,
        //            UseDefaultCredentials = false,
        //            Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
        //        };
        //        MailMessage message = new MailMessage(fromAddress, toAddress);
        //        message.Subject = subject;
        //        message.Body = body;
        //        message.Attachments.Add(attachment);
        //        try
        //        {
        //            smtp.Send(message);
        //            Response.Redirect(urlPapa, true);     
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.mensaje = "Exception caught in CreateTestMessage2(): {0}"+ ex.ToString();
        //            Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",ex.ToString());
        //        }
        //    }
           
        //    //return View("../Shared/Mensaje");
        //   // return View("");
        //    return RedirectToAction("EnviarAbono", new { abonoId = abonoId, urlPapa = urlPapa });
        //}


        public JsonResult EnviarAbono(string Servidor,int Puerto,string Usuario, string Empresa, string Destinatario, string Password, string Asunto,
           string Mensaje, string Adjunto,string AbonoId)
        {
            EnviarCorreo c = new EnviarCorreo();
            c.Servidor = Servidor;
            c.Puerto = Puerto;
            c.Usuario = Usuario;
            c.Empresa = Empresa;
            c.Destinatario = Destinatario;
            c.Password = Password;
            c.Asunto = Asunto;
            c.Mensaje = Mensaje;
            c.Adjunto = Adjunto;
            
            //FechaCreacion=new DateTime(DateTime.Now.Year, DateTime.Now.Month,DateTime.Now.Day)
            string s = "";
            bool ok = false;
            if (TryValidateModel(c))
            {
                if (ModelState.IsValid)
                {
                    var fromAddress = new MailAddress(Usuario, Empresa);
                    var toAddress = new MailAddress(Destinatario, "");
                    string fromPassword = Password;
                    string subject = Asunto;
                    string body = Mensaje;

                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(Adjunto);

                    var smtp = new SmtpClient
                    {
                        Host = Servidor,
                        Port = Puerto,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        // UseDefaultCredentials = true,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    MailMessage message = new MailMessage(fromAddress, toAddress);
                    message.Subject = subject;
                    message.Body = body;
                    message.Attachments.Add(attachment);
                    try
                    {
                        smtp.Send(message);
                        //Response.Redirect(urlPapa, true);
                        ok = true;
                        //string idDecrypted = MiUtil.desEncriptar(AbonoId);
                        int intAbonoId = 0;
                        Int32.TryParse(AbonoId, out intAbonoId);
                        abono ab = db.abono.Find(intAbonoId);
                        //abono ab = db.abono.Find(AbonoId);
                        if(ab!=null)
                        { 
                         ab.FechaEnvio = DateTime.Now;
                         db.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        s = ex.ToString().Substring(0, 200);
                        ViewBag.mensaje = "Exception caught in CreateTestMessage2(): {0}" + ex.ToString();
                        Console.WriteLine("Exception caught in CreateTestMessage2(): {0}", ex.ToString());
                    }

                }
            }
            if (!ok)
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        s =s+ error.ErrorMessage;
                    }
                }
            }
            // return Json(new { success = false });
            return Json(new { success = ok, Error = s }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult EnviarAbonoDesdeAWS(string Servidor, int Puerto, string Usuario, string Empresa, string Destinatario, string Password, string Asunto,
          string Mensaje, string Adjunto, string AbonoId)
        {
            EnviarCorreo c = new EnviarCorreo();
            c.Servidor = Servidor;
            c.Puerto = Puerto;
            c.Usuario = Usuario;
            c.Empresa = Empresa;
            c.Destinatario = Destinatario;
            c.Password = Password;
            c.Asunto = Asunto;
            c.Mensaje = Mensaje;
            c.Adjunto = Adjunto;

             // Supply your SMTP credentials below. Note that your SMTP credentials are different from your AWS credentials.
            String SMTP_USERNAME =  ConfigurationManager.AppSettings["AWSAccessKey"].ToString(); 
            //  "AKIAIZOJPFIFCACIJ53A";  // Replace with your SMTP username. 
            String SMTP_PASSWORD = ConfigurationManager.AppSettings["AWSSecretKey"].ToString();
             //   "Au9jMcd8Z07RmDNYgEvxs9f+3rpf46RruJ+1LgGEbzr5";  // Replace with your SMTP password.
                        // Amazon SES SMTP host name. This example uses the US West (Oregon) region.
            String HOST = ConfigurationManager.AppSettings["AWSServer"].ToString();
            DateTime? fechaEnvio = DateTime.Now;

            //FechaCreacion=new DateTime(DateTime.Now.Year, DateTime.Now.Month,DateTime.Now.Day)
            string s = "";
            bool ok = false;
            if (TryValidateModel(c))
            {
                if (ModelState.IsValid)
                {
                    var fromAddress = new MailAddress(Usuario, Empresa);
                    var toAddress = new MailAddress(Destinatario, "");
                    string fromPassword = Password;
                    string subject = Asunto;
                    string body = Mensaje;

                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(Adjunto);

                    
                    MailMessage message = new MailMessage(fromAddress, toAddress);
                    message.Subject = subject;
                    message.Body = body;
                    message.Attachments.Add(attachment);

                    // Create an SMTP client with the specified host name and port.
                    using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(HOST, Puerto))
                    {
                        // Create a network credential with your SMTP user name and password.
                        client.Credentials = new System.Net.NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

                        // Use SSL when accessing Amazon SES. The SMTP session will begin on an unencrypted connection, and then 
                        // the client will issue a STARTTLS command to upgrade to an encrypted connection using SSL.
                        client.EnableSsl = true;
                        // Send the email. 
                        try
                        {
                            //  Console.WriteLine("Attempting to send an email through the Amazon SES SMTP interface...");
                            //client.Send(FROM, TO, SUBJECT, BODY);
                            client.Send(message);
                            //Console.WriteLine("Email sent!");
                            ok = true;
                            //string idDecrypted = MiUtil.desEncriptar(AbonoId);
                            int intAbonoId = 0;
                            //Int32.TryParse(idDecrypted, out intAbonoId);
                            Int32.TryParse(AbonoId, out intAbonoId);
                            abono ab = db.abono.Find(intAbonoId);
                            if (ab != null)
                            {
                                ab.FechaEnvio = DateTime.Now;
                                db.SaveChanges();
                                fechaEnvio=ab.FechaEnvio;
                            }

                        }
                        catch (Exception ex)
                        {
                            s = ex.ToString().Substring(0, 200);
                            ViewBag.mensaje = "Exception caught in CreateTestMessage2(): {0}" + ex.ToString();
                            Console.WriteLine("Exception caught in CreateTestMessage2(): {0}", ex.ToString());
                        }
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
            return Json(new { success = ok, Error = s, FechaEnvio = fechaEnvio.ToString() }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}