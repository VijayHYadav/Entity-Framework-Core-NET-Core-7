// See https://aka.ms/new-console-template for more information
using CodingWiki_DataAccess.Data;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

//using(ApplicationDbContext context = new())
//{
//    context.Database.EnsureCreated();

//    if(context.Database.GetPendingMigrations().Count() > 0)
//    {
//        context.Database.Migrate();
//    }
//}

GetAllBooks();

void GetAllBooks()
{
    using var context = new ApplicationDbContext();
    var books = context.Books.ToList();
    foreach (var book in books)
    {
        Console.WriteLine(book.Title + "-" + book.ISBN);
    }
}

/**
 * 53. Database Helper Methods
 * 
 * EnsureCreated: This will do is if your database is not created, when the execution comes over here, it will
 * first find the connection string and it will try to determine whether the database exists or not.
 * The ensure created here will create the database if that does not exist.Our database already exist, so nothing will 
 * happen even if this is executed.
 *
 * GetPendingMigrations(): This will do is it will get all the migrations that have not yet been applied to the database.
 * And if the migrations are not applied, we can use the method context.Database.Migrate(); to apply all the pending
 * migrations to database. This way when we run the application, if any migrations are pending, it will automatically be
 * applied to the database.
 *
 * 54. Get All Books using EF-Core
 * context.Books.ToList()
 */