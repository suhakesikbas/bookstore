using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNonExistBookIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 99;

            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var deletedBook = _context.Books.SingleOrDefault(a => a.Id == 1);
            deletedBook.Should().BeNull();
        }
    }
}