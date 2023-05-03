using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWiki_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addViewAndSproc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER VIEW dbo.GetOnlyBookDetails
                AS
                SELECT  m.ISBN,m.Title,m.Price FROM dbo.Books m
            ");

            migrationBuilder.Sql(@"CREATE OR ALTER VIEW dbo.GetAllBookDetails
                AS
                SELECT  * FROM dbo.Books m
            ");

            migrationBuilder.Sql(@"CREATE PROCEDURE dbo.getBookDetailById
                    @bookId int
                AS   

                    SET NOCOUNT ON;  
                    SELECT  *  FROM dbo.Books b
                    WHERE b.BookId=@bookId
                GO  
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.GetOnlyBookDetails");
            migrationBuilder.Sql("DROP VIEW dbo.GetAllBookDetails");
            migrationBuilder.Sql("DROP PROCEDURE dbo.getBookDetailById");

        }
    }
}

/**
 *Now, when we are working with a stored procedure or view that will be executed on a DB set.
 *If we are retrieving all of the entries from book, we can use the book db set that we have.
 *But if you are working on a subset, we need to create a class that corresponds to this subset that we have.
 *
 *So basically there is one limitation with stored procs are views right now that you basically need a 
 *corresponding DB set that will be the exact parameters or properties that are returned from the view
 *or store proc If there is one thing less or one thing more, it will not work.
 *
 */