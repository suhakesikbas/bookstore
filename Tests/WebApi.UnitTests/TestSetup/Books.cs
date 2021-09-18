using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entites;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {

            if (!context.Books.Any())
            {
                context.Books.AddRange(
                new List<Book>(){
                        new Book{
                            Id=1,
                            Title="Matsoft",
                            GenreId=1,
                            AuthorId=1,
                            PageCount=200,
                            PublishDate=new DateTime(2003,02,21)
                        },
                        new Book{
                            Id=2,
                            Title="Lotlux",
                            GenreId=2,
                            AuthorId=2,
                            PageCount=250,
                            PublishDate=new DateTime(2015,12,16)
                        },
                        new Book{
                            Id=3,
                            Title="Tresom",
                            GenreId=2,
                            AuthorId=3,
                            PageCount=540,
                            PublishDate=new DateTime(2010,05,01)
                        }
                }
                );
                context.SaveChanges();
            }

        }
    }

}