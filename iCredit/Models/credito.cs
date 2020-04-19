//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrediAdmin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class credito
    {
        public credito()
        {
            this.abonocapital = new HashSet<abonocapital>();
            this.cuota = new HashSet<cuota>();
        }
    
        public int CreditoId { get; set; }
        public System.DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<decimal> Interes { get; set; }
        public int Meses { get; set; }
        public string TipoCuotaId { get; set; }
        public bool PrimCuota { get; set; }
        public int DivisionCreditoId { get; set; }
        public bool InteresPrimCuota { get; set; }
        public bool CapitalFinalCredito { get; set; }
        public string Observacion { get; set; }
        public string CreadoPor { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public Nullable<int> CreditoNro { get; set; }
        public Nullable<int> EmpresaId { get; set; }
        public Nullable<int> UsuarioId { get; set; }
        public bool Estado { get; set; }
        public Nullable<decimal> SaldoCapital { get; set; }
        public Nullable<decimal> SaldoInteres { get; set; }
    
        public virtual ICollection<abonocapital> abonocapital { get; set; }
        public virtual cliente cliente { get; set; }
        public virtual divisioncredito divisioncredito { get; set; }
        public virtual empresa empresa { get; set; }
        public virtual tipocuota tipocuota { get; set; }
        public virtual ICollection<cuota> cuota { get; set; }
    }
}