using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DbOperations;
using WebApi.Entites;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuhtor
{
    public class UpdateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public UpdateAuthorCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenNonExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 99;
            command.Model = new UpdateAuthorModel();

            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar mevcut değil");
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var author = new Author { Name = "WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn", Birthday = DateTime.Now.AddYears(-9) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel { Name = author.Name };

            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimli yazar zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 1;
            var model = new UpdateAuthorModel
            {
                Name = "WhenValidInputsAreGiven_Author_ShouldBeUpdated",
                Birthday = new DateTime(1564, 04, 01)
            };
            command.Model = model;

            FluentActions.Invoking(()=>command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(a=>a.Id == command.AuthorId);
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Birthday.Should().Be(model.Birthday);
        }
    }

}