using SixMinApi.Models;
using System.Collections.Generic;

namespace SixMinApi.Data
{
    public static class InMemoryStore
    {
        public static List<Author> Authors = new List<Author>
        {
            new Author { ID = 1, Name = "Robert C. Martin" },
            new Author { ID = 2, Name = "Jeffrey Richter" }
        };

        public static List<Book> Books = new List<Book>
        {
            new Book { ID = 1, Title = "Clean Code", AuthorID = 1, PublicationYear = 2008 },
            new Book { ID = 2, Title = "CLR via C#", AuthorID = 2, PublicationYear = 2012 },
            new Book { ID = 3, Title = "The Clean Coder", AuthorID = 1, PublicationYear = 2011 }
        };
    }
}