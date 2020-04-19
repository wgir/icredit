using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using CrediAdmin.Models;

namespace CrediAdmin.ViewModels
{
    public partial class SocioViewModel
    {
        public int SocioId { get; set; }
        public string Nit { get; set; }
        public string Nombre { get; set; }
        public int EmpresaId { get; set; }
        public virtual empresa empresa { get; set; }
        public bool Estado { get; set; }
          [DisplayFormat(DataFormatString = "{0:c}")]
        public double Aportes { get; set; }
          [DisplayFormat(DataFormatString = "{0:c}")]
        public double Retiros { get; set; }
          [DisplayFormat(DataFormatString = "{0:c}")]
        public double Saldo { get; set; }
    }
}