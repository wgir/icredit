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
    public partial class ImpresionAbono
    {
        public string LogoEmpresa { get; set; }
        public int EmpresaId { get; set; }
        public string EmpresaNit { get; set; }
        public string EmpresaNombre { get; set; }
        public string ClienteNit { get; set; }
        public string ClienteNombre { get; set; }
        public string EmailCliente { get; set; }
        public int CreditoId { get; set; }
        public int? CreditoNro { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? CreditoValor { get; set; }
        public int CreditoCantCuotas { get; set; }
        public int CuotaNro { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? CuotaValor { get; set; }
       
        public int AbonoId { get; set; }
        public int? AbonoNro { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime AbonoFecha { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? AbonoValor { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? AbonoInteres { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? AbonoCapital { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? TotalAbono { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? SaldoCuota { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? CreditoSaldoCapital { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? CreditoSaldoInteres { get; set; }

        
    }
}