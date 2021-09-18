using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var Authors = _context.Authors.ToList();
            List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(Authors);

            return vm;
        }

    }

    public class AuthorsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
    }

}