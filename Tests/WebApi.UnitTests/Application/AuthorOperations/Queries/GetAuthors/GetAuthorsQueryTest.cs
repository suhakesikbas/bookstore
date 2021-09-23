using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DbOperations;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQueryTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Authors_ShouldBeReturn()
        {
             GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);

            FluentActions.Invoking(() => query.Handle()).Should().NotBeNull();
        }
    }
}