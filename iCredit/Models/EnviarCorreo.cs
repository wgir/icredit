using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using CrediAdmin.Util;
using System.Data;


namespace CrediAdmin.Models
{
    public class EnviarCorreo
    {
        private const string RegExEmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public string Empresa { get; set; }
        [Required]
        public string Servidor { get; set; }
        [Required]
        public int Puerto { get; set; }
        [RegularExpression(RegExEmailPattern, ErrorMessage = "Email no valido")]
        public string Usuario { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Required]
        public string Destinatario { get; set; } //corre del destinatario
        [Required]
        public string Asunto { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Mensaje { get; set; }
        public string Adjunto { get; set; }
        public string Query { get; set; }

        public EnviarCorreo()
        {
            //MiUtil ut = new MiUtil("Sea");
            //var q = "select * from paramcorreo";
            //DataTable t = ut.tabla(q);
            //foreach (DataRow r in t.Rows)
            //{
            //   Servidor = r["Servidor"].ToString();
            //   Puerto= Convert.ToInt32(r["Puerto"].ToString());
            //}
            
            //if (from.Equals("GH"))
            //   q = "select * from remitenteCorreo where Usuario='gestion.human@assbasalud.gov.co'";
            //if (from.Equals("M"))
            //    q = "select * from remitenteCorreo where Usuario='capacitaciones@assbasalud.gov.co'";
            //t = ut.tabla(q);
            //foreach (DataRow r in t.Rows)
            //{
            //        Usuario = r["Usuario"].ToString();
            //        Password = r["Clave"].ToString();
            //}



        }

        public bool enviar(string asunto,string to, string mensaje, string adjunto)
        {
            if (!to.Equals(""))
            {
                var fromAddress = new MailAddress(this.Usuario, "Assbasalud");
                var toAddress = new MailAddress(to, to);
                string fromPassword = this.Password;
                string subject = asunto;
                string body = mensaje;


                System.Net.Mail.Attachment attachment = null;
                if (adjunto.Length > 0)
                    attachment = new System.Net.Mail.Attachment(adjunto);

                var smtp = new SmtpClient
                {
                    Host = this.Servidor,
                    Port = this.Puerto,
                    //EnableSsl = true,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true
                    /* UseDefaultCredentials = false,
                     Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)*/
                };
                MailMessage message = new MailMessage(fromAddress, toAddress);
                message.Subject = subject;
                message.Body = body;
                if (adjunto.Length > 0)
                    message.Attachments.Add(attachment);

                try
                {
                    smtp.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                     ex.ToString());
                }
            }
            return false;
        }
    }
}