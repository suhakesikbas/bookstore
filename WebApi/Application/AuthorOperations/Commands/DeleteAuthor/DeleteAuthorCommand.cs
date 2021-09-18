using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.FirstOrDefault(b => b.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar mevcut değil!");
            var bookOfAuthor = _context.Books.Any(b=>b.AuthorId == author.Id);
            if (bookOfAuthor)
                throw new InvalidOperationException("Yazara ait yayında olan kitap var. Önce kitabı silin.");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }

}