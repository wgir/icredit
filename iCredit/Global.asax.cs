using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CrediAdmin.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading;
using System.Globalization;
using CrediAdmin.Util;
//using CrediAdmin.Migrations;

namespace CrediAdmin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
                     
          //  Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-ES");

           // Database.SetInitializer<ApplicationDbContext>(new IdentityInitializer());
            
            //using (var context = new ApplicationDbContext())
            //{
            //    context.Database.Initialize(force: true);
            //}

            //Database.SetInitializer<BaseDatosContext>(new DBInitializer());

          
            //// Forces initialization of database on model changes.
            //using (var context = new BaseDatosContext())
            //{
            //    context.Database.Initialize(force: true);
            //}
            //la siguente linea se habilita en el momento que se deseen hacer migraciones de la base de datos
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<BaseDatosContext, Configuration>()); 

            
        }

       /* public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new SessionExpireAttribute());
       
        }*/
    }
}
