using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Threading;
using System.Data.Entity.ModelConfiguration.Conventions;
using CrediAdmin.Models;
using CrediAdmin.Util;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity;

namespace CrediAdmin.Models
{
    public class CrediAdminContext : icreditEntities
    {


        //public CrediAdminContext()
        //    : base("name=DBConnectionString")
        //{
           
        //}

       
        
        public override int SaveChanges()
        {
            try
            {
                ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;
                //Find all Entities that are Added/Modified that inherit from my EntityBase
                IEnumerable<ObjectStateEntry> objectStateEntriesModified =
                    from e in context.ObjectStateManager.GetObjectStateEntries(EntityState.Modified)
                    where
                        e.IsRelationship == false &&
                        e.Entity != null //&& typeof(Socio).IsAssignableFrom(e.Entity.GetType())
                    select e;
                // Set the modification information before the modified entities are saved in the database
                foreach (var entry in objectStateEntriesModified)
                {
                    var trackable = entry.Entity as ITrackableEntity;
                    if (trackable != null)
                    {
                        if (String.IsNullOrEmpty(trackable.ModificadoPor))
                            trackable.ModificadoPor = HttpContext.Current.User.Identity.GetUserId();

                        //HttpContext.Current.User.Identity.Name;
                        trackable.FechaModificacion = DateTime.Now;
                    }
                }

                IEnumerable<ObjectStateEntry> objectStateEntriesAdded =
                   from e in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added)
                   where
                       e.IsRelationship == false &&
                       e.Entity != null //&& typeof(Socio).IsAssignableFrom(e.Entity.GetType())
                   select e;
                // Set the creation information before the new entities are saved in the database
                foreach (var entry in objectStateEntriesAdded)
                {
                    var trackable = entry.Entity as ITrackableEntity;
                    if (trackable != null)
                    {
                        // if (String.IsNullOrEmpty(trackable.CreadoPor))
                        try
                        {
                            trackable.CreadoPor = HttpContext.Current.User.Identity.GetUserId();
                        }
                        catch (Exception e)
                        {

                        }
                        //trackable.CreadoPor = HttpContext.Current.User.Identity.Name;
                        trackable.Estado = true;
                        trackable.FechaCreacion = DateTime.Now;
                    }
                }


                //recorre los atributos de las prpiedades de las clases y donde tengan como atribto uppercase
                //lo transmorma en mayusculas
                IEnumerable<DbEntityEntry> changedEntities = ChangeTracker.Entries().Where(e => e.State == System.Data.Entity.EntityState.Added || e.State == System.Data.Entity.EntityState.Modified);
                TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
                changedEntities.ToList().ForEach(entry =>
                {
                    var properties = from attributedProperty in entry.Entity.GetType().GetProperties()
                                     where attributedProperty.PropertyType == typeof(string)
                                     select new
                                     {
                                         entry,
                                         attributedProperty,
                                         attributes = attributedProperty.GetCustomAttributes(true)
                                             .Where(attribute => attribute is Uppercase)
                                     };
                    properties = properties.Where(p => p.attributes.Count() > 0);
                    int cant = properties.Count();

                    properties.ToList().ForEach(p =>
                    {
                        p.attributes.ToList().ForEach(att =>
                        {
                            if (att is Uppercase)
                            {
                                if ((string)p.entry.CurrentValues[p.attributedProperty.Name] != null)
                                    p.entry.CurrentValues[p.attributedProperty.Name] = textInfo.ToUpper(((string)p.entry.CurrentValues[p.attributedProperty.Name]));
                            }
                        });
                    });
                });

                return base.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                //foreach (var eve in e.GetBaseException)
                //{
                string m = e.Message;
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        m = m + "- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage;
                //    }


                //}
                throw;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    string m = "Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State + " has the following validation errors:";
                    foreach (var ve in eve.ValidationErrors)
                    {
                        m = m + "- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage;
                    }
                }
                throw;
            }

        }







    }
}