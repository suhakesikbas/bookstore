using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DbOperations;
using Xunit;

namespace Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        [Fact]
        public void WhenValidInputsAreGiven_Genres_ShouldBeReturn()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            FluentActions.Invoking(() => query.Handle()).Should().NotBeNull();
        }
    }

}