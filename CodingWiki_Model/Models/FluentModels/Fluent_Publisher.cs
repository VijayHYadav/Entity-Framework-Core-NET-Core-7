using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class Fluent_Publisher
    {
        [Key]
        public int Publisher_Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Fluent_Book> Books { get; set; }
    }
}

/**
 * what if we want to retrieve all the details of books that a publisher has published?
 * we have added a navigation property here to retrieve all of the books that a publisher has published.
 */