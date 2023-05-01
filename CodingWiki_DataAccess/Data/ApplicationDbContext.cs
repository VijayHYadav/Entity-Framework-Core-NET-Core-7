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
        public DbSet<Category> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<SubCategory> SubCategorys { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<Fluent_BookDetail> BookDetails_Fluent { get; set; }
        public DbSet<Fluent_Book> Fluent_Books { get; set; }
        public DbSet<Fluent_Author> Fluent_Authors { get; set; }
        public DbSet<Fluent_Publisher> Fluent_Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data source=(localdb)\\MSSQLLocalDB; Initial Catalog=CodingWiki");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Fluent_BookDetail>().ToTable("Fluent_BookDetails");
            modelBuilder.Entity<Fluent_BookDetail>().Property(u => u.NumberOfChapters).HasColumnName("NoOfChapters");
            modelBuilder.Entity<Fluent_BookDetail>().Property(u => u.NumberOfChapters).IsRequired();
            modelBuilder.Entity<Fluent_BookDetail>().HasKey(u => u.BookDetail_Id);
            modelBuilder.Entity<Fluent_BookDetail>().HasOne(b => b.Book).WithOne(b => b.BookDetail)
                .HasForeignKey<Fluent_BookDetail>(u=>u.Book_Id);

            modelBuilder.Entity<Fluent_Book>().Property(u => u.ISBN).HasMaxLength(50);
            modelBuilder.Entity<Fluent_Book>().Property(u => u.ISBN).IsRequired();
            modelBuilder.Entity<Fluent_Book>().HasKey(u => u.BookId);
            modelBuilder.Entity<Fluent_Book>().Ignore(u => u.Price);
            modelBuilder.Entity<Fluent_Book>().HasOne(u => u.Publisher).WithMany(u => u.Books)
                .HasForeignKey(u => u.Publisher_Id);

            modelBuilder.Entity<Fluent_Author>().HasKey(u => u.Author_Id);
            modelBuilder.Entity<Fluent_Author>().Property(u => u.FirstName).HasMaxLength(50);
            modelBuilder.Entity<Fluent_Author>().Property(u => u.FirstName).IsRequired();
            modelBuilder.Entity<Fluent_Author>().Property(u => u.LastName).IsRequired();
            modelBuilder.Entity<Fluent_Author>().Ignore(u => u.FullName);

            modelBuilder.Entity<Fluent_Publisher>().HasKey(u => u.Publisher_Id);
            modelBuilder.Entity<Fluent_Publisher>().Property(u => u.Name).IsRequired();

            modelBuilder.Entity<Book>().Property(u => u.Price).HasPrecision(10, 5);

            // With this, the composite key will be defined.
            modelBuilder.Entity<BookAuthorMap>().HasKey(u => new { u.Author_Id, u.Book_Id });

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Spider without Duty", ISBN = "123B12", Price = 10.99m, Publisher_Id=1 },
                new Book { BookId = 2, Title = "Fortune of time", ISBN = "12123B12", Price = 11.99m, Publisher_Id = 1 }
            );

            var bookList = new Book[]
            {
                new Book {BookId = 3, Title = "Fake Sunday", ISBN = "77652", Price = 20.99m, Publisher_Id = 2 },
                new Book {BookId = 4, Title = "Cookie Jar", ISBN = "CC12B12", Price = 25.99m, Publisher_Id = 3},
                new Book {BookId = 5, Title = "Cloudy Forest", ISBN = "90392B33", Price = 25.99m , Publisher_Id = 3}
            };

            modelBuilder.Entity<Book>().HasData(bookList);

            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Publisher_Id = 1, Name = "Pub 1 Jimmy", Location = "Chicago" },
                new Publisher { Publisher_Id = 2, Name = "Pub 2 John", Location = "New York" },
                new Publisher { Publisher_Id = 3, Name = "Pub 3 Ben", Location = "Hawaii" }
            );

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
 * 
 * 15. Remove Migration and Update Existing Table
 * 
 * >> add-migration changePriceColumnToDecimalInBooksTable
 * >> Remove-Migration
 * >> add-migration changePriceColumnToDecimalInBooksTable
 * >> update-database
 * 
 * 21. When to Add Migration?
 * 
 * - Add a new class / table in the database.
 * - Add a new property / column to table.
 * - Modify existing property / column in a table.
 * - Delete existing property/ column in a table.
 * - Delete a class / table in the database.
 * Advice: One advice that I will give you when you are working with entity framework core is always 
 * make small changes and keep your migrations as small as possible.
 * Because if a migration is small, you can validate all the changes that are going through in the 
 * migration to make sure that it looks as it is expected to be, and that should always be checked 
 * before you update.
 * You might be tempted to alter some things in the old migration or even remove any old migration.
 * Never ever do that. Never try to delete a migration from the migrations folder unless you know what 
 * exactly you are doing or unless you are super experienced with entity framework core.
 * Because removing a migration will break multiple things in your application and it will haunt you down
 * the road. You should always make the changes in the models or the application DB context where we 
 * have the on model creating helper method.
 * 
 * 22. Rolling back to Old Migrations
 * 
 * >> update-database AddBookToDb
 * >> update-database
 * 
 * 24. More Commands
 * 
 * >> get-migration
 * >> drop-database
 * >> update-database
 * 
 * 25. Seed Data Using Migration
 * >> add-migration seedBookTable
 * >> update-database
 */

/**
 * Connection String of SqlServer and localdb
 * 
 * localdb: Data source=(localdb)\\MSSQLLocalDB; Initial Catalog=FootballLeage_EfCore
 * 
 * SqlServer: options.UseSqlServer("Server=VIJAYY\\SQLEXPRESS;Database=CodingWiki;TrustServerCertificate=True;
 * Trusted_Connection=True;");
 */