using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using CashFlowManager.Models;

namespace CashFlowManager.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                try
                {
                    if (!WebSecurity.Initialized)
                    {
                        WebSecurity.InitializeDatabaseConnection("SimpleMembership", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                        var roles = (SimpleRoleProvider)Roles.Provider;
                        var membership = (SimpleMembershipProvider)Membership.Provider;
                        if (!roles.RoleExists("Admin"))
                        {
                            roles.CreateRole("Admin");
                        }
                        if (!roles.RoleExists("User"))
                        {
                            roles.CreateRole("User");
                        }
                        if (membership.GetUser("Admin", false) == null)
                        {
                            membership.CreateUserAndAccount("Admin", "Admin");
                        }

                        var r = roles.GetRolesForUser("Admin");
                        int i = 0;

                        // If user has no roles 
                        if (r.Length == 0)
                        {
                            roles.AddUsersToRoles(new[] { "Admin" }, new[] { "Admin" });     
                        }

                        // User may have another role so checking if they have the admin role
                        while (i < r.Length)
                        {
                            if (r[i] != "Admin")
                            {
                                roles.AddUsersToRoles(new[] { "Admin" }, new[] { "Admin" });        
                            }
                            i++;
                        }

                        

                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Something is wrong", ex);
                }

                //Database.SetInitializer<UsersContext>(null);

                //try
                //{
                //    using (var context = new UsersContext())
                //    {
                //        if (!context.Database.Exists())
                //        {
                //            // Create the SimpleMembership database without Entity Framework migration schema
                //            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                //        }
                //    }

                //    WebSecurity.InitializeDatabaseConnection("CashFlowManagerEntities", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                //}
                //catch (Exception ex)
                //{
                //    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                //}
            }
        }
    }
}
