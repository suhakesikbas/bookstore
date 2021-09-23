using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteGenreCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenNonExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 99;

            FluentActions.Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü mevcut değil");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var deletedGenre = _context.Genres.SingleOrDefault(a=>a.Id == 1);
            deletedGenre.Should().BeNull();
        }
        
    }
}