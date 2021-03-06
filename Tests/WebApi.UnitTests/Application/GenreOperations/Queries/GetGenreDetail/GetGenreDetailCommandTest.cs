using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using Xunit;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        [Fact]
        public void WhenNonExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 99;

            FluentActions.Invoking(() => query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı!");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeReturn()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 1;

            FluentActions.Invoking(() => query.Handle())
            .Should().NotBeNull();
        }
    }
}