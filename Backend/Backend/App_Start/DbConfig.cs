using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Backend.Models;
using Backend.Migrations;


namespace Backend
{
    public static class DbConfig
    {
        public static void RunDbMigrations()
        {     
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

            using (var context = new ApplicationDbContext())
            {
                context.Database.Initialize(false);
            }
        }
    }
}