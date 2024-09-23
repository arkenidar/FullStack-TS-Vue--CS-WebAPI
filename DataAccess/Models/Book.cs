using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{	
	public class Book(string title, int authorId, int publicationYear)
    {
        public int Id { get; set; }
        public string Title { get; set; } = title;
        public int AuthorId { get; set; } = authorId;
        public Author? Author { get; set; }
        public int PublicationYear { get; set; } = publicationYear;
    }
}
