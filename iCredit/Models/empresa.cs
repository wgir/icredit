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
    
    public partial class empresa
    {
        public empresa()
        {
            this.ciudad = new HashSet<ciudad>();
            this.cliente = new HashSet<cliente>();
            this.conceptoaporte = new HashSet<conceptoaporte>();
            this.conceptoretiro = new HashSet<conceptoretiro>();
            this.paramcorreo = new HashSet<paramcorreo>();
            this.retirointeres = new HashSet<retirointeres>();
            this.socio = new HashSet<socio>();
            this.usuario = new HashSet<usuario>();
            this.credito = new HashSet<credito>();
        }
    
        public int EmpresaId { get; set; }
        public string Nit { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }
        public string CreadoPor { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string LogoUrl { get; set; }
        public string EmpEmail { get; set; }
        public Nullable<int> EmpAnioActual { get; set; }
        public Nullable<int> EmpMesActual { get; set; }
    
        public virtual ICollection<ciudad> ciudad { get; set; }
        public virtual ICollection<cliente> cliente { get; set; }
        public virtual ICollection<conceptoaporte> conceptoaporte { get; set; }
        public virtual ICollection<conceptoretiro> conceptoretiro { get; set; }
        public virtual consecutivo consecutivo { get; set; }
        public virtual ICollection<paramcorreo> paramcorreo { get; set; }
        public virtual ICollection<retirointeres> retirointeres { get; set; }
        public virtual ICollection<socio> socio { get; set; }
        public virtual ICollection<usuario> usuario { get; set; }
        public virtual ICollection<credito> credito { get; set; }
    }
}
