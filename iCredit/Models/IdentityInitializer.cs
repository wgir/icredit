using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Owin;
using CrediAdmin.Models;

namespace CrediAdmin.Models
{
    public class IdentityInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

           this.addUser(context,"Administrador","administrador","administrador@CrediAdmin.com", "administrador");
           //this.addUser(context,"Agente", "agente", "agente@CrediAdmin.com", "agente");
           //this.addUser(context, "Agente", "1023939136", "C1023939136@CrediAdmin.com", "1023939136");
           //this.addUser(context, "Agente", "1070329472", "C1070329472@CrediAdmin.com", "1070329472");
           //this.addUser(context, "Agente", "1012379941", "C1012379941@CrediAdmin.com", "1012379941");

//            MARY HELLEN ROJAS PEREZ 	ASESOR 	1023939136
//LEIDY JOHANA MONTENEGRO MORENO	ASESOR 	1070329472
//YEIMY ALEJANDRA IZQUIERDO MANCERA	ASESOR 	1012379941

        }


        public void addUser(ApplicationDbContext context,string rolleName, string username, string userEmail, string userPassword)
        {
            // Access the application context and create result variables.
            //Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(context);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);
            // Then, you create the "canEdit" role if it doesn't already exist.
            if (!roleMgr.RoleExists(rolleName))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = rolleName });
            }

            // Create a UserManager object based on the UserStore object and the ApplicationDbContext  
            // object. Note that you can create new objects and use them as parameters in
            // a single line of code, rather than using multiple lines of code, as you did
            // for the RoleManager object.
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            {
                UserName = username,
                Email = userEmail
            };
            IdUserResult = userMgr.Create(appUser, userPassword);

            // If the new "canEdit" user was successfully created, 
            // add the "canEdit" user to the "canEdit" role. 
            if (!userMgr.IsInRole(userMgr.FindByEmail(userEmail).Id, rolleName))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail(userEmail).Id, rolleName);
            }
        }
    }
}