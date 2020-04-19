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
    public partial class EstadoSocio
    {
        public int EmpresaId { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public string SocioNit { get; set; }
        public string Nombre { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? TotalAportes { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? AportesxPeriodo { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? UtilidadRecomendada { get; set; }

       
        
    }
}