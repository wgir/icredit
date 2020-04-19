using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using CrediAdmin.Models;

namespace CrediAdmin.ViewModels
{
    public partial class CreditoViewModel
    {
        
        private credito _credito;
        public CreditoViewModel(credito c,List<cuota> cuotas)
        {
            _credito = c;
            _credito.cuota = cuotas;
        }
       
    }
}