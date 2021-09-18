using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.DbOperations;
using AutoMapper;
using FluentValidation;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Commands.DeleteBook;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            BookDetailViewModel result;
            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
            // try
            // {
            //     query.BookId = id;
            //     GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            //     validator.ValidateAndThrow(query);
            //     result = query.Handle();
            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }
            return Ok(result);
        }

        // [HttpGet]
        // public Book Get([FromQuery] string id)
        // {
        //     int bookId = Convert.ToInt32(id);
        //     var book = BookList.FirstOrDefault(b => b.Id == bookId);
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            // try
            // {
            //     command.Model = newBook;
            //     CreateBookCommandValidator validator = new CreateBookCommandValidator();
            //     validator.ValidateAndThrow(command);
            //     command.Handle();
            //     // ValidationResult result = validator.Validate(command);
            //     // if (!result.IsValid)
            //     //     foreach (var item in result.Errors)
            //     //         Console.WriteLine("Özellik " + item.PropertyName + ", Error Message : " + item.ErrorMessage);
            //     // else
            //     //     command.Handle();


            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updateBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            // try
            // {
            //     command.BookId = id;
            //     command.Model = updateBook;
            //     UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            //     validator.ValidateAndThrow(command);
            //     command.Handle();
            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            // try
            // {
            //     command.BookId = id;
            //     DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            //     validator.ValidateAndThrow(command);
            //     command.Handle();
            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }
            return Ok();
        }
    }

}