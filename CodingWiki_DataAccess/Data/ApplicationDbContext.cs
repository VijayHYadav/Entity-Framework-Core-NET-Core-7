using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data source=(localdb)\\MSSQLLocalDB; Initial Catalog=CodingWiki");
        }
    }
}

/*
 * 9. Create ApplicationDbContext and Book Model
 * 
 * This DB context is responsible to provide all the logic that is needed for entity framework core.
 * Like if we have to access a database, we want to retrieve, update, delete something or we have to create tables.
 * 
 * DB set: DB set will basically be responsible or rather be the classes of the tables that we want in our application.
 * whatever name we give to our DB set will be the name of the table that gets added in database.
 * 
 * 13. Apply Migration
 * 
 * right now we have created the table for our book model in our database, and we applied the migration
 * 
 * 14. Migration Snapshot
 * 
 * EFMigrationsHistory: you can see we have the migration name that was applied.
 * Right here we have the migration name that was applied as well as the Dotnet core version.
 * Main thing to notice here is the migration ID. This is how entity framework code will track which migrations 
 * have been applied on the database. That way, if we add a new migration, it will not apply the migration that
 * has already been applied on the database and it will automatically apply only the migrations that have not 
 * been pushed to the database.
 *  
 * Here it keeps a snapshot of what has been created in database. That way when we add a migration, it knows 
 * that hey, in database these are the columns and if a new 
 * column is added in, let's say the book model, it will know that the column does not exist in database.Based
 * on the snapshot. And then it will add that in the new migration. That is how EF core will track all the
 * changes that are there between the current code and the database.
 * When we practice this, we add more migration, we update more database, everything will make much sensce

 */

/**
 * Connection String of SqlServer and localdb
 * 
 * localdb: Data source=(localdb)\\MSSQLLocalDB; Initial Catalog=FootballLeage_EfCore
 * 
 * SqlServer: options.UseSqlServer("Server=VIJAYY\\SQLEXPRESS;Database=CodingWiki;TrustServerCertificate=True;
 * Trusted_Connection=True;");
 */