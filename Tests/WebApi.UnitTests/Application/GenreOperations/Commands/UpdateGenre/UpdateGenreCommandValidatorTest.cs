using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTest
    {
        [Theory]
        [InlineData(-1, "")]
        [InlineData(0, "Abc")]
        [InlineData(1, " ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId, string name)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel
            {
                Name = name
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel{
                Name = "WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError",
                IsActive = false
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }

}