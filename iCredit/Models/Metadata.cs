using CrediAdmin.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic;

namespace CrediAdmin.Models
{
    [MetadataType(typeof(empresaMetaData))]
    public partial class empresa : ITrackableEntity,IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            icreditEntities storeContext = new icreditEntities();
            if(!String.IsNullOrEmpty(Nit))
             if(!Nit.Trim().Equals(""))
                if (storeContext.empresa.Any(c => c.Nit.Trim().ToUpper() == Nit.Trim().ToUpper() &&
                    c.EmpresaId != EmpresaId  ))
                        yield return new ValidationResult("Ya existe una empresa con el mismo Nit!", new string[] { "Nit" });
            if (storeContext.empresa.Any(c => c.EmpEmail.Trim().ToUpper() == EmpEmail.Trim().ToUpper() &&
                c.EmpresaId != EmpresaId))
                yield return new ValidationResult("Ya existe una empresa con el mismo Correo Electronico!", new string[] { "EmpEmail" });
        }
    }

    public class empresaMetaData
    {
        private const string RegExEmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        bool _activo = true;//set Default Value here
        public int EmpresaId { get; set; }
        //[Required(ErrorMessage = "Campo Nit Requerido")]
        [MaxLength(30, ErrorMessage = "El nit debe tener maximo 30 caracteres")]
        [Display(Name = "Nit")]
        public string Nit { get; set; }
        [Uppercase]
        [Required]
        [Display(Name = "Nombre de la Empresa")]
        public string Nombre { get; set; }
        [Uppercase]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        public string Telefono { get; set; }
      
        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(RegExEmailPattern, ErrorMessage = "Email no valido")]
        [Display(Name = "Email")]
        public string EmpEmail { get; set; }

        [Display(Name = "Logo")]
        [DataType(DataType.ImageUrl)]
        public string LogoUrl { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el año actual")]
        [Display(Name = "Periodo Actual")]
        [Range(2000, Int32.MaxValue)]
        public int EmpAnioActual { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el mes actual")]
        public int EmpMesActual { get; set; }

        public bool Estado { get { return _activo; } set { _activo = value; } }

        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }

    }


    [MetadataType(typeof(socioMetaData))]
    public partial class socio : ITrackableEntity,IValidatableObject
    {
        public decimal? calcularAportes(DateTime fecha)
        {
            decimal? sumaAportes = 0;
            foreach (aporte a in aporte)
                if (a.Fecha <= fecha)
                    sumaAportes = sumaAportes + (a.Estado == true ? a.Valor : 0);


            return sumaAportes;
        }

        public decimal? calcularRetiros(DateTime fecha)
        {
            decimal? sumaRetiros = 0;
            foreach (retiro a in retiro)
                if (a.Fecha <= fecha)
                    sumaRetiros = sumaRetiros + (a.Estado == true ? a.Valor : 0);


            return sumaRetiros;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CrediAdminContext storeContext = new CrediAdminContext();
            if (storeContext.socio.Any(c => c.Nit.Trim().ToUpper() == Nit.Trim().ToUpper() && c.SocioId != SocioId && c.EmpresaId == EmpresaId))
                yield return new ValidationResult("Ya existe un socio con el mismo Nit!", new string[] { "Nit" });
        }
    }

    public class socioMetaData
    {
        bool _activo = true;//set Default Value here
        public int SocioId { get; set; }
        [Required]
        public string Nit { get; set; }
        [Required, MaxLength(100, ErrorMessage = "El nombre debe tener maximo 100 caracteres")]
        [UIHint("Nombre del socio")]
        [Uppercase]
        public string Nombre { get; set; }
        public int EmpresaId { get; set; }
       
        public bool Estado { get { return _activo; } set { _activo = value; } }

        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }




       
    }

    [MetadataType(typeof(aporteMetaData))]
    public partial class aporte : ITrackableEntity, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CrediAdminContext storeContext = new CrediAdminContext();
            int mes = Fecha.Month;
            int anio=Fecha.Year;
            socio s=storeContext.socio.Find(SocioId);
            if(s!=null)
             if (s.empresa.EmpAnioActual!=anio || s.empresa.EmpMesActual!=mes)
                yield return new ValidationResult("No es posible hacer aportes en un periodo diferente al de la empresa!", new string[] { "Fecha" });
        }
    }

    public class aporteMetaData
    {
        bool _activo = true;//set Default Value here
        DateTime _fecha = DateTime.Now;

        public int AporteId { get; set; }
        [Range(1, 100000000, ErrorMessage = "Ingresar un valor superior o igual a {1}")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? Valor { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Por favor digite una fecha valida (ej: dia/mes/año)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }


        public int SocioId { get; set; }
       // public virtual Socio Socio { get; set; }
        [Required(ErrorMessage = "Campo Concepto Requerido")]
        public int ConceptoAporteId { get; set; }
       // public virtual ConceptoAporte ConceptoAporte { get; set; }
        
        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        [Uppercase]
        public string Observacion { get; set; }


        public bool Estado { get { return _activo; } set { _activo = value; } }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }
    }

    [MetadataType(typeof(retiroMetaData))]
    public partial class retiro : ITrackableEntity, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CrediAdminContext storeContext = new CrediAdminContext();
            int mes = Fecha.Month;
            int anio = Fecha.Year;
            socio s = storeContext.socio.Find(SocioId);
            if (s != null)
                if (s.empresa.EmpAnioActual != anio || s.empresa.EmpMesActual != mes)
                    yield return new ValidationResult("No es posible hacer retiro en un periodo diferente al de la empresa!", new string[] { "Fecha" });
        }
    }

    public class retiroMetaData
    {
        bool _activo = true;//set Default Value here
        DateTime _fecha = DateTime.Now;
        public int RetiroId { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Ingresar un valor superior o igual a {1}")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? Valor { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Por favor digite una fecha valida (ej: dia/mes/año)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public int SocioId { get; set; }
        [Required(ErrorMessage = "Campo Concepto Requerido")]
        public int ConceptoRetiroId { get; set; }
        //public virtual ConceptoRetiro ConceptoRetiro { get; set; }
        [Display(Name = "Observacion")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        [Uppercase]
        public string Observacion { get; set; }
        public bool Estado { get { return _activo; } set { _activo = value; } }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }
    }


    [MetadataType(typeof(conceptoaporteMetadata))]
    public partial class conceptoaporte : ITrackableEntity, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CrediAdminContext storeContext = new CrediAdminContext();
            if (storeContext.conceptoaporte.Any(c => c.Nombre.Trim().ToUpper() == Nombre.Trim().ToUpper() && c.ConceptoAporteId != ConceptoAporteId && c.EmpresaId == EmpresaId))
                yield return new ValidationResult("Ya existe este concepto de Aporte!", new string[] { "Nombre" });
        }
    }

    public class conceptoaporteMetadata
    {
        bool _activo = true;//set Default Value here
        public int ConceptoAporteId { get; set; }

        [Uppercase]
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int EmpresaId { get; set; }
        public virtual empresa empresa { get; set; }

        public bool Estado { get { return _activo; } set { _activo = value; } }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }
    }

    [MetadataType(typeof(conceptoretiroMetadata))]
    public partial class conceptoretiro : ITrackableEntity, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CrediAdminContext storeContext = new CrediAdminContext();
            if (storeContext.conceptoretiro.Any(c => c.Nombre.Trim().ToUpper() == Nombre.Trim().ToUpper() && c.ConceptoRetiroId != ConceptoRetiroId 
                && c.EmpresaId == EmpresaId))
                yield return new ValidationResult("Ya existe este concepto de Retiro!", new string[] { "Nombre" });
        }
    }


    public class conceptoretiroMetadata
    {
        bool _activo = true;//set Default Value here
        public int ConceptoRetiroId { get; set; }
        
        [Required]
        [Uppercase]
        public string Nombre { get; set; }
        
        [Required]
        public int EmpresaId { get; set; }
        public virtual empresa empresa { get; set; }

        public bool Estado { get { return _activo; } set { _activo = value; } }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }
    }



   

  
    [MetadataType(typeof(ciudadMetaData))]
    public partial class ciudad : ITrackableEntity//, IValidatableObject
    {
        
    }
    public class ciudadMetaData
    {
        bool _activo = true;//set Default Value here
        public int CiudadId { get; set; }
        [Uppercase]
        public string Nombre { get; set; }

        [Required]
        public int EmpresaId { get; set; }
        public virtual empresa empresa { get; set; }

        public bool Estado { get { return _activo; } set { _activo = value; } }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }
    }


    [MetadataType(typeof(clienteMetaData))]
    public partial class cliente : ITrackableEntity, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            icreditEntities storeContext = new icreditEntities();
            if (storeContext.cliente.Any(c => c.Nit.Trim().ToUpper() == Nit.Trim().ToUpper() &&
                c.ClienteId != ClienteId && c.EmpresaId == EmpresaId))
                yield return new ValidationResult("Ya existe un cliente con el mismo Nit!", new string[] { "Nit" });
        }

        public int getEmpresa()
        {
            return EmpresaId;
        }

    }


    public class clienteMetaData
    {
        private const string RegExEmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        bool _activo = true;//set Default Value here

        public int ClienteId { get; set; }
        [Display(Name = "Documento")]
        [Required, MaxLength(30, ErrorMessage = "El documento debe tener maximo 30 caracteres")]
        public string Nit { get; set; }
        
        [Required(ErrorMessage = "Campo requerido")]
        [Uppercase]
        public string Nombre { get; set; }
        [Uppercase]
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        [Required]
        [RegularExpression(RegExEmailPattern, ErrorMessage = "Email no valido")]
        public string Email { get; set; }
        [Required]
        public int EmpresaId { get; set; }
        //public virtual Empresa Empresa { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public int CiudadId { get; set; }
        

        public bool Estado { get { return _activo; } set { _activo = value; } }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }



       
    }
/***********************************************************************************************************************/
    [MetadataType(typeof(creditoMetaData))]
    public partial class credito : ITrackableEntity, IValidatableObject
    {
       // public virtual IList<cuota> cuotas { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CrediAdminContext storeContext = new CrediAdminContext();
            int mes = Fecha.Month;
            int anio = Fecha.Year;
            empresa e = storeContext.empresa.Find(EmpresaId);
            if (e != null)
                if (e.EmpAnioActual != anio || e.EmpMesActual != mes)
                    yield return new ValidationResult("No es posible hacer credito en un periodo diferente al de la empresa!", new string[] { "Fecha" });

            if (Meses <= 0)
                yield return new ValidationResult("El valor de meses debe ser superior a cero", new string[] { "Meses" });
            if (Valor <= 0)
                yield return new ValidationResult("El valor del credito debe ser superior a cero", new string[] { "Valor" });
            // if(InteresPrimCuota==CapitalFinalCredito)
            //     yield return new ValidationResult("El valor del credito debe ser superior a cero", new string[] { "CapitalFinalCredito" });
            //CrediAdminContext storeContext = new CrediAdminContext();
            //if (storeContext.Abonos.Include(a => a.Cuota).Any(c => c.Nit.Trim().ToUpper() == Nit.Trim().ToUpper() && c.SocioId != SocioId))
            //    yield return new ValidationResult("Ya existe un socio con el mismo Nit!", new string[] { "Nit" });

        }

        public void CalcularCuotas()
        {
            this.cuota = new List<cuota>();
            cuota cuota;
            decimal? saldoAnterior = this.Valor;
            decimal? amortizacion = this.Valor / this.Meses;
            decimal? valorCuotaMensual;
            for (int i = 1; i <= this.Meses; i++)
            {
                cuota = new cuota
                {
                    CreditoId = this.CreditoId,
                    Numero = i,
                    Fecha = this.Fecha.AddMonths(i)
                };
                if (this.TipoCuotaId.Equals("FI"))
                {
                    double interes=Convert.ToDouble(this.Interes / 100);
                    double tiempo=Convert.ToDouble(this.Meses);
                    double val = Convert.ToDouble(this.Valor);
                    valorCuotaMensual = Convert.ToDecimal( Financial.Pmt( interes ,tiempo , val) * -1);
                    cuota.AbonoInteres = saldoAnterior * (this.Interes / 100);
                    cuota.AbonoCapital = valorCuotaMensual - cuota.AbonoInteres;
                    saldoAnterior = saldoAnterior - cuota.AbonoCapital;
                }
                if (this.TipoCuotaId.Equals("VA"))
                {
                    cuota.AbonoCapital = amortizacion;
                    cuota.AbonoInteres = saldoAnterior * (this.Interes / 100);
                    saldoAnterior = this.Valor - (amortizacion * i);
                        //(cuota.AbonoCapital + cuota.AbonoInteres);
                }

                if (this.TipoCuotaId.Equals("IM"))
                {
                    cuota.AbonoCapital = amortizacion;
                    cuota.AbonoInteres = this.Valor * (this.Interes / 100);
                    //cuota.Saldo = saldoAnterior - cuota.AbonoCapital;
                    saldoAnterior = saldoAnterior - cuota.AbonoCapital;

                }

                if (this.TipoCuotaId.Equals("CI"))
                {
                    cuota.AbonoCapital = 0;
                    cuota.AbonoInteres = this.Valor * (this.Interes / 100);
                    //cuota.Saldo = saldoAnterior - cuota.AbonoCapital;
                    saldoAnterior = saldoAnterior - cuota.AbonoCapital;
                    this.CapitalFinalCredito = true;

                }

                this.cuota.Add(cuota);

            }// fin for
           
            if (this.DivisionCreditoId != 30)
                this.DividirCuotas();
            if (this.CapitalFinalCredito)
                this.calcularCapitalFinalCredito();
            if (this.PrimCuota)
                this.PrimeraCuotaEmpiezaFechaCredito();
            //if (this.InteresPrimCuota)
            //    this.InteresPrimeraCuota();
        }

        //Calcula los abonos a intereses que se han hecho a una fecha dada
        public decimal? calcularAbonoInteres(DateTime? fecha)
        {
            decimal? totalAbonoInteres = 0;
            foreach (cuota c in this.cuota)
                totalAbonoInteres = totalAbonoInteres + c.AbonoAInteres(fecha);
            return totalAbonoInteres;
        }

        //Calcula los abonos a capital que se han hecho < una fecha dada
        public decimal? calcularAbonoCapital(DateTime? fecha)
        {
            decimal? totalAbonoCapital = 0;
            foreach (cuota c in this.cuota)
                totalAbonoCapital = totalAbonoCapital + c.AbonoACapital(fecha);
            return totalAbonoCapital;
        }

       

        public decimal? calcularTotalInteres()
        {
            decimal? totalAbonoInteres = 0;
            foreach (cuota ss in this.cuota)
                totalAbonoInteres = totalAbonoInteres + ss.AbonoInteres+ MiUtil.nullTodecimal(ss.AjusteAbonoInteres);
            return totalAbonoInteres;
        }

        public decimal? calcularTotalCapital()
        {
            decimal? totalAbonoCapital = 0;
            foreach (cuota ss in this.cuota)
                totalAbonoCapital = totalAbonoCapital + ss.AbonoCapital + MiUtil.nullTodecimal(ss.AjusteAbonoCapital);
            return totalAbonoCapital;
        }

        public void PrimeraCuotaEmpiezaFechaCredito()
        {
            var pos = 0;
            foreach (cuota ss in this.cuota)
            {
                ss.Fecha = this.Fecha.AddMonths(pos);
                pos++;
                if (pos == this.cuota.Count())
                    ss.AbonoInteres = 0;
            }

        }

        public void DividirCuotas()
        {
            IList<cuota> nuevasCuotas = new List<cuota>();
            cuota nuevaCuota;

            decimal? totalAbonoCapital = 0, totalAbonoInteres = 0, abonoCapxCuota, abonoIntxCuota;
            decimal? sumaAbonoCapital = 0, sumaAbonoInteres = 0, saldoxCuota = this.Valor;
            int nroCuotas = this.Meses * (30 / this.DivisionCreditoId);
            //  int pos = 0;
            //  nuevaCuota=this.Cuotas.ElementAt(0);
            //DateTime inicioCredito = this.cuota.ElementAt(0).Fecha;
            DateTime inicioCredito = System.Linq.Enumerable.ElementAt(this.cuota, 0).Fecha;

            totalAbonoCapital = this.calcularTotalCapital();
            totalAbonoInteres = this.calcularTotalInteres();

            abonoCapxCuota = totalAbonoCapital / nroCuotas;
            abonoIntxCuota = totalAbonoInteres / nroCuotas;
            for (int i = 1; i <= nroCuotas; i++)
            {
                saldoxCuota = saldoxCuota - abonoCapxCuota;
                nuevaCuota = new cuota
                {
                    CreditoId = this.CreditoId,
                    Numero = i,
                    Fecha = inicioCredito.AddDays(i * this.DivisionCreditoId),
                    AbonoCapital = abonoCapxCuota,
                    AbonoInteres = abonoIntxCuota//,
                    //Saldo = saldoxCuota
                };
                sumaAbonoCapital = sumaAbonoCapital + abonoCapxCuota;
                sumaAbonoInteres = sumaAbonoInteres + abonoIntxCuota;
                nuevasCuotas.Add(nuevaCuota);
            }
            //es posible que las cuotas no alcancen a pagar todo el valor que deberian pagar.. entonces se divide el valor faltant en todas las cuotas y se suman
            saldoxCuota = this.Valor;
            while (totalAbonoCapital > sumaAbonoCapital)
            {
                abonoCapxCuota = (totalAbonoCapital - sumaAbonoCapital) / nroCuotas;
                abonoIntxCuota = (totalAbonoInteres - sumaAbonoInteres) / nroCuotas;
                foreach (cuota c in nuevasCuotas)
                {
                    c.AbonoCapital = c.AbonoCapital + abonoCapxCuota;
                    c.AbonoInteres = c.AbonoInteres + abonoIntxCuota;
                    saldoxCuota = saldoxCuota - c.AbonoCapital;
                    //c.Saldo = saldoxCuota;
                    sumaAbonoCapital = sumaAbonoCapital + abonoCapxCuota;
                }

            }
            this.cuota = nuevasCuotas;
        }

        public void InteresPrimeraCuota()
        {
            //int pos = 0;
            //cuota nuevaCuota = new cuota
            //{
            //    CreditoId = this.CreditoId,
            //    Numero = 1,
            //    Fecha = this.Fecha,
            //    AbonoCapital = 0,
            //    AbonoInteres = this.calcularTotalInteres(),
            //    Saldo = this.Valor
            //};
            //this.cuota.Insert(0, nuevaCuota);
            ////System.Linq.Enumerable.ElementAt(this.cuota, 0).Fecha;
            //foreach (cuota ss in this.cuota)
            //{
            //    if (pos > 0)
            //    {
            //        ss.AbonoInteres = 0;
            //        ss.Numero++;
            //    }
            //    pos++;
            //}

        }

        public void calcularCapitalFinalCredito()
        {
            int pos = 1;
            decimal? saldoxCuota = this.Valor;
            foreach (cuota ss in this.cuota)
            {
                if (pos != this.cuota.Count())
                    ss.AbonoCapital = 0;
                else
                    ss.AbonoCapital = this.Valor;

                ss.AbonoInteres = saldoxCuota * (this.Interes / 100);
                //ss.Saldo = saldoxCuota - ss.AbonoCapital;
                saldoxCuota = saldoxCuota - ss.AbonoCapital;

                pos++;
            }
        }

        public bool tieneAbonosActivos()
        {
            bool abonoActivo = false;
            foreach (cuota c in this.cuota)
                foreach (abono a in c.abono)
                    if (a.Estado)
                        abonoActivo = true;
            return abonoActivo;
        }

        //public string registrarAbonoCapital(DateTime fechaAbono,decimal? valorAbono)
        //{
        //    string ok = "";
        //    bool encontro = false;
        //    decimal? ajustePrimeraCuota = 0;
        //    foreach (cuota cuota in this.cuota)
        //    { 
        //        if (cuota.Fecha.Month == fechaAbono.Month && cuota.Fecha.Year == fechaAbono.Year)
        //        {
        //            if (cuota.abono.Where(a => a.Estado == true).Any())
        //                ok = "No es posible abonar a capital en este periodo ya que existe un abono registrado";
        //            else
        //            {
        //                if (valorAbono > (cuota.AbonoCapital + cuota.AbonoInteres))
        //                {
        //                    //cuota.AjusteAbonoInteres= valorAbono>cuota.AjusteAbonoInteres?cuota.AjusteAbonoInteres*-1: valorAbono
        //                    decimal? ajusteInicial= valorAbono - cuota.AbonoInteres;
        //                    cuota.AjusteAbonoCapital = ajusteInicial - cuota.AbonoCapital;
        //                    ajustePrimeraCuota = cuota.AjusteAbonoCapital;
        //                    encontro = true;
        //                }else
        //                    ok = "El abono a capital debe ser superior al abono correspondiente al periodo";
        //            }
        //        }
        //        else
        //            if (cuota.AbonoCapital > 0 && Convert.ToInt32(MiUtil.fechaToPeriodo(cuota.Fecha)) > Convert.ToInt32(MiUtil.fechaToPeriodo(fechaAbono)))
        //                cuota.AjusteAbonoCapital = ajustePrimeraCuota*-1;
        //    }
        //    if(!encontro)
        //       ok="No se encontro la cuota en el periodo del abono a capital, la fecha del abono a capital debe coincidir con la fecha de una cuota del credito";
        //    return ok;
        //}

        public decimal? saldoTotalAFecha(DateTime fechaAbono)
        {
            decimal? saldoAnterior = this.Valor;
            foreach (cuota cuota in this.cuota)
            {
                bool tieneAbonos = cuota.abono.Where(a => a.Estado == true).Any();
                if (!tieneAbonos)
                    return saldoAnterior + (cuota.AbonoInteres + (cuota.AjusteAbonoInteres == null ? 0 : cuota.AjusteAbonoInteres));
                //+ cuota.AbonoInteres + (cuota.AjusteAbonoInteres == null ? 0 : cuota.AjusteAbonoInteres);
                else
                    saldoAnterior = saldoAnterior - (cuota.AbonoCapital + (cuota.AjusteAbonoCapital == null ? 0 : cuota.AjusteAbonoCapital));
            }
            return 0;
        }


        public string abonoACapital(DateTime fechaAbono,decimal? valorAbono)
        {
            decimal? saldoAnterior = this.Valor;
            double saldoCapital = 0;
            decimal? amortizacion = this.Valor / this.Meses;
            //decimal? rebajarCapital = 0; 
            decimal? cuotaRebajarCapital=0;
            int numeroCuota = 1;
            bool encontroPrimeraCuota = false;
            double tiempo = 0;
            foreach (cuota cuota in this.cuota)
            {
                //cuota.AjusteAbonoCapital = cuota.AbonoCapital;
                //cuota.AjusteAbonoInteres = cuota.AbonoInteres;
                bool tieneAbonos = cuota.abono.Where(a => a.Estado == true).Any();
                //if (Convert.ToInt32(MiUtil.fechaToPeriodo(cuota.Fecha)) <= Convert.ToInt32(MiUtil.fechaToPeriodo(fechaAbono)) && !tieneAbonos && !encontroPrimeraCuota)
                if (!tieneAbonos && !encontroPrimeraCuota)
                    {
                    if (valorAbono <= (cuota.AbonoCapital+ cuota.AjusteAbonoCapital + cuota.AbonoInteres + cuota.AjusteAbonoInteres))
                        return "El valor del abono a capital debe ser superior al abono correspondiente al periodo";

                    if (!this.TipoCuotaId.Equals("CI"))
                    {
                        cuota.AjusteAbonoCapital = valorAbono - (cuota.AbonoInteres + (cuota.AjusteAbonoInteres == null ? 0 : cuota.AjusteAbonoInteres));
                        cuota.AjusteAbonoInteres = valorAbono - cuota.AjusteAbonoCapital;
                    }
                    else
                    {
                        cuota.AjusteAbonoCapital = valorAbono ;
                        cuota.AjusteAbonoInteres = 0;
                    }
                    saldoAnterior = saldoAnterior - (cuota.AjusteAbonoCapital==null ? 0 : cuota.AjusteAbonoCapital);
                    //saldoAnterior = saldoAnterior + (cuota.AbonoInteres + (cuota.AjusteAbonoInteres == null ? 0 : cuota.AjusteAbonoInteres));
                    //saldoAnterior = saldoAnterior + (cuota.AbonoCapital + (cuota.AjusteAbonoCapital == null ? 0 : cuota.AjusteAbonoCapital));
                    saldoCapital = Convert.ToDouble(saldoAnterior);
                    encontroPrimeraCuota = true;
                    tiempo = Convert.ToDouble(this.Meses - cuota.Numero);
                }
                else
                {
                    if (encontroPrimeraCuota)
                    {
                        if (this.TipoCuotaId.Equals("FI"))
                        {
                            double interes = Convert.ToDouble(this.Interes / 100);
                            double val = Convert.ToDouble(this.Valor);
                            decimal valorCuotaMensual = Convert.ToDecimal(Financial.Pmt(interes, tiempo, saldoCapital) * -1);
                            cuota.AjusteAbonoInteres = saldoAnterior * (this.Interes / 100);
                            cuota.AjusteAbonoCapital = valorCuotaMensual -  cuota.AjusteAbonoInteres;
                            saldoAnterior = saldoAnterior - cuota.AjusteAbonoCapital;
                        }
                        if (this.TipoCuotaId.Equals("VA"))
                        {
                            cuota.AjusteAbonoCapital = amortizacion;
                            cuota.AjusteAbonoInteres = saldoAnterior * (this.Interes / 100);
                            saldoAnterior = cuota.AjusteAbonoCapital + cuota.AjusteAbonoInteres;
                        }

                        if (this.TipoCuotaId.Equals("IM"))
                        {
                            cuota.AjusteAbonoCapital = amortizacion;
                            cuota.AjusteAbonoInteres = this.Valor * (this.Interes / 100);
                            saldoAnterior = saldoAnterior - cuota.AjusteAbonoCapital;

                        }

                        if (this.TipoCuotaId.Equals("CI"))
                        {
                            cuota.AjusteAbonoCapital = 0;
                            cuota.AjusteAbonoInteres = saldoAnterior * (this.Interes / 100);
                            saldoAnterior = saldoAnterior - cuota.AjusteAbonoCapital;
                            this.CapitalFinalCredito = true;

                        }
                    }
                    else
                    {
                        saldoAnterior = saldoAnterior - (cuota.AbonoCapital + (cuota.AjusteAbonoCapital==null ? 0 : cuota.AjusteAbonoCapital));
                        
                    }
                }
                //rebajarCapital = rebajarCapital + (cuota.AbonoCapital + (cuota.AjusteAbonoCapital==null ? 0 : cuota.AjusteAbonoCapital));
                if (encontroPrimeraCuota)
                {
                    cuota.AjusteAbonoCapital = (cuota.AjusteAbonoCapital == null ? 0 : cuota.AjusteAbonoCapital) - cuota.AbonoCapital;
                    cuota.AjusteAbonoInteres = (cuota.AjusteAbonoInteres == null ? 0 : cuota.AjusteAbonoInteres) - cuota.AbonoInteres;
                }
                numeroCuota++;
            }
            if (!encontroPrimeraCuota)
                return "No se encontro la cuota a la cual se le debe empezar a descontar el abono a capital, es posible que se haya registrado un abono a la cuota del periodo (Se debe anular ese abono, y hacer el abono a capital incluyendo el valor del interes de la cuota)";

            return "";
        }

        //public string anularAbonoCapital(int abonoCapitalId)
        //{
        //    string ok = "";
        //    bool encontro = false;
        //    decimal? ajustePrimeraCuota = 0;
        //    bool encontroPrimeraCuota = false;
        //    foreach (cuota cuota in this.cuota)
        //    {
        //        if (cuota.AbonoInteres+cuota.AjusteAbonoCapital+cuota.AjusteAbonoInteres==valorAbono)
        //        {
        //            if (cuota.abono.Where(a => a.Estado == true).Any())
        //                ok = "No es posible anular el abono a capital, ya que existen abonos hechos despues de este abono";

        //            cuota.AjusteAbonoCapital = 0;
        //            cuota.AjusteAbonoInteres = 0; 
        //            encontroPrimeraCuota = true;
        //        }
        //        else
        //        {
        //            if (encontroPrimeraCuota == true)
        //            {
        //                if (cuota.abono.Where(a => a.Estado == true).Any())
        //                    ok = "No es posible anular el abono a capital, ya que existen abonos hechos despues de este abono";
        //                else
        //                {
        //                    cuota.AjusteAbonoCapital = 0;
        //                    cuota.AjusteAbonoInteres = 0;
        //                }
        //            }
        //        }
        //    }
        //    return ok;
        //}
    }

    public partial class creditoMetaData 
    {
        bool _activo = true;//set Default Value here
        //bool _valido = false;//set Default Value here
        decimal? _interes = 2;
        int _divisionCredito = 30;
        string _tipoCuota = "FI";
        DateTime _fecha = DateTime.Now;
        public int CreditoId { get; set; }

        [Required]
       // [DataType(DataType.Date, ErrorMessage = "Por favor digite una fecha valida (ej: dia/mes/año)")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        
        [Required(ErrorMessage = "Campo requerido")]
        public int ClienteId { get; set; }
        public virtual cliente cliente { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? Valor { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? SaldoCapital { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? SaldoInteres { get; set; }

        [Required]
        [Display(Name = "Interes Mensual %")]
        public decimal? Interes { get { return _interes; } set { _interes = value; } } // tasa mensual
        [Required]
        [Range(1, 120, ErrorMessage = "La cantidad de meses debe estar entre {1} y {2}")]
        public int Meses { get; set; }
        [Required]
        [Display(Name = "Tipo Cuota")]
        public string TipoCuotaId { get { return _tipoCuota; } set { _tipoCuota = value; } }//tipo cuota  FI :CUOTA FIJA -VA: CUOTA VARIABLE -  IM:INTERESES X MES
        //public virtual TipoCuota TipoCuota { get; set; }
        [Display(Name = "La primera cuota empieza el dia del credito")]
        public bool PrimCuota { get; set; } // primera cuota empieza el primer dia del credito
        [Display(Name = "Dividir el credito en")]
        public int DivisionCreditoId { get { return _divisionCredito; } set { _divisionCredito = value; } } // 30 - 15 - 7
        //public virtual DivisionCredito DivisionCredito { get; set; }
        [Display(Name = "Cobrar interes en la primera Cuota")]
        public bool InteresPrimCuota { get; set; } // Cobrar interes Priemera cuota
        [Display(Name = "Cobrar Capital al final del credito")]
        public bool CapitalFinalCredito { get; set; } // Cobrar capital final credito
        //public virtual IList<cuota> cuotas { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        [Uppercase]
        public string Observacion { get; set; }
        // public bool Valido { get { return _valido; } set { _valido = value; } }//indica si el credito es valido, despues de que calculan las cuotas para
        [Display(Name = "Cobrador")]
        public int UsuarioId { get; set; }

        public bool Estado { get { return _activo; } set { _activo = value; } }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }

      
    } //fin clase



    /*******************************************************************************************************************/
    [MetadataType(typeof(abonocapitalMetaData))]
    public partial class abonocapital : ITrackableEntity, IValidatableObject
    {
        //bool _activo = true;//set Default Value here
        //DateTime _fecha = DateTime.Now;
        //public bool Estado { get { return _activo; } set { _activo = value; } }
        //public string CreadoPor { get; set; }
        //public DateTime? FechaCreacion { get; set; }
        //public string ModificadoPor { get; set; }
        //public DateTime? FechaModificacion { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CrediAdminContext storeContext = new CrediAdminContext();
            int mes = Fecha.Month;
            int anio = Fecha.Year;
            credito cr = storeContext.credito.Find(CreditoId);
            if (cr != null)
                if (cr.empresa.EmpAnioActual != anio || cr.empresa.EmpMesActual != mes)
                    yield return new ValidationResult("No es posible crear o modificar un abono en un periodo diferente al de la empresa!", new string[] { "Fecha" });

            //CrediAdminContext storeContext = new CrediAdminContext();
            if (this.Estado == true)
                if (storeContext.abonocapital.Any(c => c.AbonoCapitalId == AbonoCapitalId && c.Estado == false))
                    yield return new ValidationResult("Este abono se encontraba inactivo. No es posible activarlo de nuevo!", new string[] { "Estado" });
        }
    }


    public class abonocapitalMetaData
    {

        bool _activo = true;//set Default Value here
        DateTime _fecha = DateTime.Now;

        public int AbonoCapitalId { get; set; }
        public int CreditoId { get; set; }
        public int AbonoCapitalNro { get; set; }
        public virtual credito credito { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Por favor digite una fecha valida (ej: dia/mes/año)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? Valor { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        [Uppercase]
        public string Observacion { get; set; }


        public bool Estado { get { return _activo; } set { _activo = value; } }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }

    }
/***************************************************************************************************************************/
    [MetadataType(typeof(cuotaMetaData))]
    public partial class cuota
    {
        public decimal? calcularSaldoxCapital()
        {
            decimal? sumaSaldo = this.AbonoCapital + MiUtil.nullTodecimal(this.AjusteAbonoCapital);
            decimal? porcentajeCapital = this.porcentajeCapital();
            foreach (abono item in abono)
                if (item.Estado)
                    sumaSaldo = sumaSaldo - (item.Valor * (porcentajeCapital / 100));
            return sumaSaldo; 
        }

        public decimal? calcularSaldoxCapital(DateTime fecha)
        {
            decimal? sumaSaldo = this.AbonoCapital + MiUtil.nullTodecimal(this.AjusteAbonoCapital);
            decimal? porcentajeCapital = this.porcentajeCapital();
            foreach (abono item in abono)
                if (item.Estado && item.Fecha<=fecha)
                    sumaSaldo = sumaSaldo - (item.Valor * (porcentajeCapital / 100));
            return sumaSaldo;
        }

        public decimal? calcularSaldoxInteres()
        {
            decimal? sumaSaldo = this.AbonoInteres + MiUtil.nullTodecimal(this.AjusteAbonoInteres);
            decimal? porcentajeInteres = this.porcentajeInteres();
            foreach (abono item in abono)
                if (item.Estado)
                    sumaSaldo = sumaSaldo - (item.Valor * (porcentajeInteres / 100));
            return sumaSaldo;
        }

        public decimal? calcularSaldoxInteres(DateTime fecha)
        {


            decimal? sumaSaldo = this.AbonoInteres + MiUtil.nullTodecimal(this.AjusteAbonoInteres);
            decimal? porcentajeInteres = this.porcentajeInteres();
            foreach (abono item in abono)
                if (item.Estado && item.Fecha<=fecha)
                    sumaSaldo = sumaSaldo - (item.Valor * (porcentajeInteres / 100));
            return sumaSaldo;
        }

        //

        public decimal? AbonoAInteres(DateTime? fecha)
        {
            decimal? sumaAbono = 0;
            decimal? porcentajeInteres = this.porcentajeInteres();
            foreach (abono item in abono)
            {
                if (item.Estado)
                    if (fecha != null)
                    {
                        if (item.Fecha <= fecha)
                            sumaAbono = sumaAbono + (item.Valor * (porcentajeInteres / 100));
                    }
                    else
                        sumaAbono = sumaAbono + (item.Valor * (porcentajeInteres / 100));
            }
            return sumaAbono;
            //Math.Round(sumaAbono*(porcentajeInteres/100), 2); 
        }

        public decimal? AbonoACapital(DateTime? fecha)
        {
            decimal? sumaAbono = 0;
            decimal? porcentajeCapital = this.porcentajeCapital();
            foreach (abono item in abono)
            {
                if (item.Estado)
                    if (fecha != null)
                    {
                        if (item.Fecha <= fecha)
                           sumaAbono = sumaAbono + (item.Valor * (porcentajeCapital / 100));
                    }else
                           sumaAbono = sumaAbono + (item.Valor * (porcentajeCapital / 100));
            }
            return sumaAbono;
            //Math.Round(sumaAbono * (porcentajeCapital / 100), 2); 
        }

        public decimal? porcentajeInteres()
        {
            if ((this.AbonoCapital + this.AbonoInteres + MiUtil.nullTodecimal(this.AjusteAbonoCapital) + MiUtil.nullTodecimal(this.AjusteAbonoInteres)) != 0)
                return ((this.AbonoInteres + MiUtil.nullTodecimal(this.AjusteAbonoInteres)) * 100) / (this.AbonoCapital + this.AbonoInteres + MiUtil.nullTodecimal(this.AjusteAbonoCapital) + MiUtil.nullTodecimal(this.AjusteAbonoInteres));
            return 0;
        }
        public decimal? porcentajeCapital()
        {
            if ((this.AbonoCapital + this.AbonoInteres + MiUtil.nullTodecimal(this.AjusteAbonoCapital) + MiUtil.nullTodecimal(this.AjusteAbonoInteres)) != 0)
                return ((this.AbonoCapital + MiUtil.nullTodecimal(this.AjusteAbonoCapital)) * 100) / (this.AbonoCapital + this.AbonoInteres + MiUtil.nullTodecimal(this.AjusteAbonoCapital) + MiUtil.nullTodecimal(this.AjusteAbonoInteres));
            return 0;
        }
    }

    public class cuotaMetaData 
    {
        public int CuotaId { get; set; }
       // public int CreditoId { get; set; }
        public int Numero { get; set; } // numero de la cuota
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Fecha { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? AbonoCapital { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? AbonoInteres { get; set; }
       
          [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? SaldoCapital { get; set; }
          [DisplayFormat(DataFormatString = "{0:c}")]
          public decimal? SaldoInteres { get; set; }



        

    }

    /*******************************************************************************************************************/
    [MetadataType(typeof(abonoMetaData))]
    public partial class abono : ITrackableEntity, IValidatableObject
    {
        //bool _activo = true;//set Default Value here
        //DateTime _fecha = DateTime.Now;
        //public bool Estado { get { return _activo; } set { _activo = value; } }
        //public string CreadoPor { get; set; }
        //public DateTime? FechaCreacion { get; set; }
        //public string ModificadoPor { get; set; }
        //public DateTime? FechaModificacion { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CrediAdminContext storeContext = new CrediAdminContext();
            int mes = Fecha.Month;
            int anio = Fecha.Year;
            cuota cr = storeContext.cuota.Find(CuotaId);
            if (cr != null)
                if (cr.credito.empresa.EmpAnioActual != anio || cr.credito.empresa.EmpMesActual != mes)
                    yield return new ValidationResult("No es posible crear o modificar un abono en un periodo diferente al de la empresa!", new string[] { "Fecha" });

            //CrediAdminContext storeContext = new CrediAdminContext();
            if (this.Estado == true)
                if (storeContext.abono.Any(c => c.AbonoId == AbonoId && c.Estado == false))
                    yield return new ValidationResult("Este abono se encontraba inactivo. No es posible activarlo de nuevo!", new string[] { "Estado" });
        }
    }


    public class abonoMetaData 
    {
       
        bool _activo = true;//set Default Value here
        DateTime _fecha = DateTime.Now;

        public int AbonoId { get; set; }
        public int CuotaId { get; set; }
        public int AbonoNro { get; set; }
        public virtual cuota cuota { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Por favor digite una fecha valida (ej: dia/mes/año)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? Valor { get; set; }

        [Range(0, Int32.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? Paga { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? Devolucion { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        [Uppercase]
        public string Observacion { get; set; }


        public bool Estado { get { return _activo; } set { _activo = value; } }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }

    }


    [MetadataType(typeof(paramcorreoMetaData))]
    public partial class paramcorreo : ITrackableEntity
    {
      
    }


    public partial class paramcorreoMetaData 
    {
        private const string RegExEmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
      

      //  public int Id { get; set; }
        [Required]
        [Display(Name = "Servidor (Gmail:smtp.gmail.com)")]
        public string Servidor { get; set; }
        [Required]
        [Display(Name = "Puerto (Gmail:587)")]
        public int Puerto { get; set; }
       // [RegularExpression(RegExEmailPattern, ErrorMessage = "Email no valido")]
        [Display(Name = "Usuario (Gmail:usuario@gmail.com)")]
        public string Usuario { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        public bool Estado { get; set; }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }

    }



    [MetadataType(typeof(usuarioMetaData))]
    public partial class usuario : ITrackableEntity, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            icreditEntities storeContext = new icreditEntities();
            if (storeContext.usuario.Any(c => c.UsuDocumento.Trim().ToUpper() == UsuDocumento.Trim().ToUpper() &&
                c.UsuarioId != UsuarioId && c.EmpresaId == EmpresaId))
                yield return new ValidationResult("Ya existe un usuario con el mismo Documento!", new string[] { "UsuDocumento" });

            if (storeContext.usuario.Any(c => c.UsuEmail.Trim().ToUpper() == UsuEmail.Trim().ToUpper() &&
                c.UsuarioId != UsuarioId))
                yield return new ValidationResult("Ya existe un usuario con el mismo Correo Electronico!", new string[] { "UsuEmail" });
        }

        public int getEmpresa()
        {
            return EmpresaId;
        }
      //  public bool Estado { get; set; }
    }


    public class usuarioMetaData 
    {
        private const string RegExEmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        bool _activo = true;//set Default Value here

        public int UsuarioId { get; set; }
        [Display(Name = "Documento")]
        [MaxLength(30, ErrorMessage = "El Nro del documento debe tener maximo 30 caracteres")]
        public string UsuDocumento { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        [Uppercase]
        public string UsuNombre { get; set; }
        
        [Display(Name = "Telefono")]
        public string UsuTelefono { get; set; }

       

        public string aspnetusersId { get; set; }
        [Required]
        public int EmpresaId { get; set; }
//        public virtual empresa empresa { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(RegExEmailPattern, ErrorMessage = "Email no valido")]
        [Display(Name = "Correo Electronico")]
        public string UsuEmail { get; set; }

        public bool Estado { get { return _activo; } set { _activo = value; } }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }
    }



    [MetadataType(typeof(retirointeresMetadata))]
    public partial class retirointeres : ITrackableEntity, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CrediAdminContext storeContext = new CrediAdminContext();
            int mes = Fecha.Month;
            int anio = Fecha.Year;
            empresa em = storeContext.empresa.Find(EmpresaId);
            if (em != null)
                if (em.EmpAnioActual != anio || em.EmpMesActual != mes)
                    yield return new ValidationResult("No es posible crear o modificar un retiro en un periodo diferente al de la empresa!", new string[] { "Fecha" });
        }

       
    }

    public class retirointeresMetadata
    {
        bool _activo = true;//set Default Value here
        DateTime _fecha = DateTime.Now;

        public int RetiroInteresId { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? Valor { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Por favor digite una fecha valida (ej: dia/mes/año)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }

        public int EmpresaId { get; set; }
        public virtual empresa empresa { get; set; }


        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        [Uppercase]
        public string Observacion { get; set; }

        public bool Estado { get { return _activo; } set { _activo = value; } }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }

    }


    [MetadataType(typeof(contactoMetadata))]
    public partial class contacto : ITrackableEntity//, IValidatableObject
    {
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    CrediAdminContext storeContext = new CrediAdminContext();
        //    int mes = Fecha.Month;
        //    int anio = Fecha.Year;
        //    empresa em = storeContext.empresa.Find(EmpresaId);
        //    if (em != null)
        //        if (em.EmpAnioActual != anio || em.EmpMesActual != mes)
        //            yield return new ValidationResult("No es posible crear o modificar un retiro en un periodo diferente al de la empresa!", new string[] { "Fecha" });
        //}
    }

    public class contactoMetadata
    {
        bool _activo = true;//set Default Value here
        private const string RegExEmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        DateTime _fecha = DateTime.Now;

        public int ContactoId { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Uppercase]
        [Display(Name = "Nombre")]
        public string ConNombre { get; set; }
        
        [Display(Name = "Telefono")]
        public string ConTelefono { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [RegularExpression(RegExEmailPattern, ErrorMessage = "Email no valido")]
        [Display(Name = "Email")]
        public string ConEmail { get; set; }


        [Required(ErrorMessage = "Campo requerido")]
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        [Uppercase]
        [Display(Name = "Observacion o Sugerencia")]
        public string ConObserva { get; set; }

        public bool Estado { get { return _activo; } set { _activo = value; } }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime? FechaModificacion { get; set; }

    }
}