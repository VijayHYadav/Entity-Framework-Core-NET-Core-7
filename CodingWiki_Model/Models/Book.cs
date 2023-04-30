using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class Book
    {
        // [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        [MaxLength(20)]
        [Required]
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        [NotMapped]
        public string PriceRange { get; set; }
    }
}

/*
 * 11. Add Migration (Add-Migration AddBookToDb)
 * 
 * [Key]: Once we add that, then it will automatically tell entity framework core that this column will be the
 * primary key of the book table.
 * 
 * public int Id { get; set; } If I only had this integer as an ID and if I did not have the key annotation entity framework
 * core will automatically assume that this is the primary key of the table and that assumption is based on two condition if
 * the property name is ID, and if your table does not have explicitly defined a key it will assume that this ID is the
 * primary key else if you have any other name.
 * But if it ends with an ID here and if key is not defined on any other property, then this column will also be assumed as
 * the primary key of the table.
 * 
 * 
 * 26. Update Table and Column Name
 * 
 * [Table("tb_Category")]
 * public class Category
 * 
 * [Column("BookISBN")]
 * public string ISBN { get; set; }
 * 
 * 27. Key and Required Data Annotations
 * 
 * [Required]
 * public string Title { get; set; }
 * 
 * [Key]
 * public int Category_Id { get; set; }
 * 
 * 28. MaxLength and NotMapped
 * 
 * [MaxLength(50)]
 * public string Title { get; set; }
 * 
 * [NotMapped]
 * public double DiscountPrice { get; set; }
 * not mapped basically means that there is some property which is doing some computation, but that
 * property should not be added as a new column.
 */