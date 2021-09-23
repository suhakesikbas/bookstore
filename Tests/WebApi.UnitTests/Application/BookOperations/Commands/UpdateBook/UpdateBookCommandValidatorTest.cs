using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTest
    {


        [Theory]
        [InlineData(-1,"Introduction to Algorithms", 0, 0, 0)]
        [InlineData(1,"Introduction to Algorithms", 0, 0, 0)]
        [InlineData(1,"Introduction to Algorithms", 0, 1, 0)]
        [InlineData(1,"Introduction to Algorithms", 0, 0, 1)]
        [InlineData(1,"Introduction to Algorithms", 1, 0, 0)]
        [InlineData(1,"", 0, 0, 0)]
        [InlineData(1,"", 100, 0, 0)]
        [InlineData(1,"", 100, 1, 1)]
        [InlineData(1,"Lor", 100, 1, 1)]
        [InlineData(1,"    ", 100, 1, 1)]
        [InlineData(0,"Introduction to Algorithms", 100, 1, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title, int pageCount, int genreId, int authorId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.Model = new UpdateBookModel
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.AddYears(-1),
                GenreId = genreId,
                AuthorId = authorId
            };
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;
            command.Model = new UpdateBookModel
            {
                Title = "Introduction to Algorithms",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1,
                AuthorId = 1
            };
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;
            command.Model = new UpdateBookModel
            {
                Title = "Introduction to Algorithms",
                PageCount = 100,
                PublishDate = DateTime.Now.AddYears(-1),
                GenreId = 1,
                AuthorId = 1
            };
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Equals(0);
        }
    }

}