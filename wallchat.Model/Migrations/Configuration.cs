using System;
using System.Data.Entity.Migrations;
using wallchat.Model.App.EF;
using wallchat.Model.App.Entity;
using wallchat.Model.App.Enums;

namespace wallchat.Model.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration < DatabaseContext >
    {
        public Configuration ()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed ( DatabaseContext context )
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            
           context.Roles.AddOrUpdate (
               p => p.Id,
               new Role
               {
                   RoleName = "user"
               },
               new Role
               {
                   RoleName = "manager"
               });

            context.Users.AddOrUpdate (
                p => p.Id,
                new User
                {
                    UserName = "Vasya",
                    PasswordHash = "123456",
                    DateRegistration = DateTime.Now
                },
                new User
                {
                    UserName = "darthvasya",
                    PasswordHash = "123456",
                    DateRegistration = DateTime.Now
                }
                );

            context.Clients.AddOrUpdate (
                p => p.Id,
                new Client
                {
                    Id = "jsApp",
                    Secret = "secret",
                    ApplicationType = ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "*",
                    Name = "JS client"
                },
                new Client
                {
                    Id = "native",
                    Secret = "secret",
                    ApplicationType = ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "*",
                    Name = "NativeConfidential"
                }
            );
        }
    }
}