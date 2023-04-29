﻿using CodingWiki_Model.Models;
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
 */

/**
 * Connection String of SqlServer and localdb
 * 
 * localdb: Data source=(localdb)\\MSSQLLocalDB; Initial Catalog=FootballLeage_EfCore
 * 
 * SqlServer: options.UseSqlServer("Server=VIJAYY\\SQLEXPRESS;Database=CodingWiki;TrustServerCertificate=True;
 * Trusted_Connection=True;");
 */