using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DbOperations;
using Xunit;

namespace Application.BookOperations.Queries.GetBooks
{
    public class GetBooksCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBooksCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        public void WhenValidInputsAreGiven_Books_SholdBeReturn()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            FluentActions.Invoking(() => query.Handle()).Should().NotBeNull();
        }
    }

}