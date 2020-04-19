using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
//using System.Data.Objects.DataClasses;
using CrediAdmin.Models;

namespace CrediAdmin.ViewModels
{
    public partial class Cuotas
    {
       // [Key]
        public int CuotaId { get; set; }
        public string Nit { get; set; }
        public string Nombre { get; set; }
        public int CreditoId { get; set; }
        public int CreditoNro { get; set; }
        public int Numero { get; set; }
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Fecha { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? AbonoCapital { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? AbonoInteres { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? TotalCuota { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FechaAbono { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? Abonos { get; set; }
        public int CantCuotas { get; set; }
        public decimal? SaldoCuota { get; set; }
        public int Page { get; set; }
    }
}