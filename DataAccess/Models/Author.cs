using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{	
	public class Author(string name)
    {
        public int Id { get; set; }
        public string? Name { get; set; } = name;
        public ICollection<Book> Books { get; set; } = [];
    }
}
