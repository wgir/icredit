using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace CrediAdmin.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
     //   [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Uppercase]
      //  [AdditionalHtml(Size = 20, MaxLength = 100, AccessKey = "", Disabled = false, TabIndex = 0, CssClass = "text-box single-line")]
        [Display(Name = "Nombre de Usuario")]
        public string Usuario { get; set; }

        [Required]
        [Uppercase]
     //   [AdditionalHtml(Size = 20, MaxLength = 100, AccessKey = "", Disabled = false, TabIndex = 0, CssClass = "text-box single-line")]
        //[DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Clave { get; set; }


        //[Required]
        //[StringLength(200, ErrorMessage = "Minimo {2} caracteres.", MinimumLength = 1)]
        //[AdditionalHtml(Size = 50, MaxLength = 100, AccessKey = "", Disabled = false, TabIndex = 0, CssClass = "text-box single-line")]
        //[Display(Name = "Direccion IP")]
        //public string IP { get; set; }


        [Display(Name = "Recordarme?")]
        public bool RememberMe { get; set; }

       
    }

   
}
