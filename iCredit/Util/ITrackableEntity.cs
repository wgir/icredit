using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrediAdmin.Util
{
    interface ITrackableEntity
    {
        bool Estado { get; set; }
        string CreadoPor { get; set; }
        string ModificadoPor { get; set; }
        DateTime? FechaCreacion { get; set; }
        DateTime? FechaModificacion { get; set; }
    }
}
