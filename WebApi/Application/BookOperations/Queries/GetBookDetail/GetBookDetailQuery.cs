using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQuery(IBookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _context.Books.Include(x=>x.Genre).Include(x=>x.Author).Where(b => b.Id == BookId)
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
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }

}