using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using CrediAdmin.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrediAdmin.ViewModels
{
    public partial class RegistroCliente
    {
        [Display(Name = "Fecha")]
        public DateTime? FechaCreacion { get; set; }
        
        [Uppercase]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
        
        [Uppercase]
        [Display(Name = "Documento")]
        public string Cli_Nit { get; set; }

        [Uppercase]
        [Display(Name = "Nombre")]
        public string Cli_Nombre { get; set; }

        [Uppercase]
        [Display(Name = "Apellido")]
        public string Cli_Apellido { get; set; }

        [Uppercase]
        [Display(Name = "Tema")]
        public string Tem_Nombre { get; set; }

        [Uppercase]
        [Display(Name = "Subtema")]
        public string Sub_Nombre { get; set; }
        
        [Uppercase]
        [Display(Name = "Tipificiacion")]
        public string Tip_Nombre { get; set; }


        
     

        }
    }
