using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.FirstOrDefault(b => b.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Kitap türü mevcut değil");

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }

}