using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange(
                    new List<Book>(){
                        new Book{
                            Id=1,
                            Title="Matsoft",
                            GenreId=1,
                            PageCount=200,
                            PublishDate=new DateTime(2003,02,21)
                        },
                        new Book{
                            Id=2,
                            Title="Lotlux",
                            GenreId=2,
                            PageCount=250,
                            PublishDate=new DateTime(2015,12,16)
                        },
                        new Book{
                            Id=3,
                            Title="Tresom",
                            GenreId=2,
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