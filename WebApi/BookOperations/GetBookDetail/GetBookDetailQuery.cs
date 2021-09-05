using System;
using System.Linq;
using AutoMapper;
using WebApi.BookOperations.GetBooks;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookDetailQuery(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(b => b.Id == BookId)
            //  .Select(b => new BookDetailViewModel
            //  {
            //      Title = b.Title,
            //      PageCount = b.PageCount,
            //      PublishDate = b.PublishDate.ToString("dd/MM/yyy"),
            //      Genre = ((GenreEnum)b.GenreId).ToString()
            //  })
             .FirstOrDefault();

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı.");

             BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);

            return vm;
        }
    }

    public class BookDetailViewModel{
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }

}