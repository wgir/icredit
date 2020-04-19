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
    public partial class EstadoEmpresa
    {
        public int EmpresaId { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? CapitalTotalRecaudado { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? InteresTotalRecaudado { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? InteresTotalRetirado { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? TotalEnCaja { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? CapitalxCobrar { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? InteresxCobrar { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? TotalxCobrar { get; set; }

        public IList<EstadoSocio> esocios { get; set; }

      
    }
}