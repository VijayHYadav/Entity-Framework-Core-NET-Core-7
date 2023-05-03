using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class GetOnlyBookDetails
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
    }
}
