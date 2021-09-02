using System;
using System.Linq;
using WebApi.BookOperations.GetBooks;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public GetBookByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BooksViewModel Handle()
        {
            var book = _dbContext.Books.Where(b => b.Id == BookId)
             .Select(b => new BooksViewModel
             {
                 Title = b.Title,
                 PageCount = b.PageCount,
                 PublishDate = b.PublishDate.ToString("dd/MM/yyy"),
                 Genre = ((GenreEnum)b.GenreId).ToString()
             }).FirstOrDefault();

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı.");

            return book;
        }
    }

}