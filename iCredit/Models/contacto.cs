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
    
    public partial class contacto
    {
        public int ContactoId { get; set; }
        public string ConNombre { get; set; }
        public string ConTelefono { get; set; }
        public string ConEmail { get; set; }
        public string ConObserva { get; set; }
        public string CreadoPor { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public bool Estado { get; set; }
    }
}
