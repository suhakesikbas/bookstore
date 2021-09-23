using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.Entites;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBookCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenNonExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 99;
            command.Model = new UpdateBookModel();

            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı.");
        }

        [Fact]
        public void WhenAlreadyExistBookNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var book = new Book { Title = "WhenAlreadyExistBookNameIsGiven_InvalidOperationException_ShouldBeReturn", GenreId = 1, AuthorId = 1, PageCount = 200, PublishDate = new DateTime(2003, 02, 21) };
            _context.Books.Add(book);
            _context.SaveChanges();

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 1;
            command.Model = new UpdateBookModel { Title = book.Title };

            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimli kitap zaten mevcut.");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 1;
            UpdateBookModel model = new UpdateBookModel { Title = "WhenValidInputsAreGiven_Book_ShouldBeUpdated", PageCount = 100, PublishDate = new DateTime(1990, 01, 10), GenreId = 1, AuthorId = 1 };
            command.Model = model;
            
            FluentActions.Invoking(() => command.Handle()).Invoke();
            
            var book = _context.Books.SingleOrDefault(b => b.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
        }


    }

}