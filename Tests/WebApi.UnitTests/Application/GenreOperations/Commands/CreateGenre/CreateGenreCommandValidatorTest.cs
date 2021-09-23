using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using Xunit;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTest
    {
        [Theory]
        [InlineData("      ")]
        [InlineData("")]
        [InlineData("Ro")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel
            {
                Name = name
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_SholdNotBeReturnError()
        {
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel
            {
                Name = "Computer Science"
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }

}