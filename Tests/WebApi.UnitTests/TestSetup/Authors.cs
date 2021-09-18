using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entites;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            if (!context.Authors.Any())
            {
                context.Authors.AddRange(
                    new Author
                    {
                        Id = 1,
                        Name = "John Doe",
                        Birthday = new DateTime(1973, 07, 10)
                    },
                     new Author
                     {
                         Id = 2,
                         Name = "Michael J. Frost",
                         Birthday = new DateTime(1949, 07, 26)
                     },
                     new Author
                     {
                         Id = 3,
                         Name = "Matthew S. Remmers",
                         Birthday = new DateTime(1945, 07, 15)
                     }
                );
                context.SaveChanges();
            }
        }
    }

}