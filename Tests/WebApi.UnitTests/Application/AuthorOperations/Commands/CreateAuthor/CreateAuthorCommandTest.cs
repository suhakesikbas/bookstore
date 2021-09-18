using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DbOperations;
using WebApi.Entites;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTest(CommonTestFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var author = new Author { Name = "WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn", Birthday = DateTime.Now.AddYears(-9) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model = new CreateAuthorModel{Name = author.Name};
            
            FluentActions.Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel{Name="Cengiz Aytmatov",Birthday=DateTime.Now.AddYears(-9)};
            command.Model = model;

            FluentActions.Invoking(()=>command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(a=>a.Name == model.Name);
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Birthday.Should().Be(model.Birthday);

        }
    }

}