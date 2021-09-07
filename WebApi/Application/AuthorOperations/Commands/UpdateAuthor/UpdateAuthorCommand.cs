using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.FirstOrDefault(b => b.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar mevcut değil");

            if (_context.Authors.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != AuthorId))
                throw new InvalidOperationException("Aynı isimli yazar zaten mevcut");

            author.Name = Model.Name.Trim() == default ? author.Name : Model.Name;
            author.Birthday = Model.Birthday == default ? author.Birthday : Model.Birthday;
            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }

}