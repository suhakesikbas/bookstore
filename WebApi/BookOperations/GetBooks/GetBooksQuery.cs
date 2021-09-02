using WebApi.DbOperations;
using System.Collections.Generic;
using System.Linq;
using System;
using WebApi.Common;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;

        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BooksViewModel> Handle()
        {
            return _dbContext.Books.OrderBy(b => b.Id)
            .Select(b => new BooksViewModel
            {
                Title = b.Title,
                PageCount = b.PageCount,
                PublishDate = b.PublishDate.ToString("dd/MM/yyy"),
                Genre = ((GenreEnum)b.GenreId).ToString()
            }).ToList();
        }

    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}