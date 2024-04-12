using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kursach.ViewModel;
using kursach.View;
using kursach.Model;

namespace kursach.Helper
{
    public class FindAuthor
    {
        int id;
        public FindAuthor(int id)
        {
            this.id = id;
        }
        public bool AuthorPredicate(Author author)
        {
            return author.Id == id;
        }
        public bool BookPredicate(Book book)
        {
            return book.Id == id;
        }
    }
}
