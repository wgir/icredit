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
    
    public partial class cliente
    {
        public cliente()
        {
            this.credito = new HashSet<credito>();
        }
    
        public int ClienteId { get; set; }
        public string Nit { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int EmpresaId { get; set; }
        public string CreadoPor { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public Nullable<int> CiudadId { get; set; }
        public bool Estado { get; set; }
    
        public virtual ciudad ciudad { get; set; }
        public virtual empresa empresa { get; set; }
        public virtual ICollection<credito> credito { get; set; }
    }
}
