using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entites;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {


                if (!context.Genres.Any())
                {
                    context.Genres.AddRange(
                        new Genre
                        {
                            Name = "Personal Growth"
                        },
                        new Genre
                        {
                            Name = "Science Fiction"
                        },
                        new Genre
                        {
                            Name = "Novel"
                        }
                    );
                }

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
                }

                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(
                        new Author
                        {
                            Id = 1,
                            Name = "John Doe",
                            Birthday = new DateTime(1973,07,10)
                        },
                         new Author
                         {
                             Id = 2,
                             Name = "Michael J. Frost",
                             Birthday = new DateTime(1949,07,26)
                         },
                         new Author
                         {
                             Id = 3,
                             Name = "Matthew S. Remmers",
                             Birthday = new DateTime(1945,07,15)
                         }
                    );
                }

                context.SaveChanges();
            }
        }
    }

}