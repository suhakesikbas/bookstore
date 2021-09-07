using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.FirstOrDefault(b => b.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar mevcut değil!");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }

}