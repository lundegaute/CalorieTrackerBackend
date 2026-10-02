using Books.Services;
using Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CalorieTracker.Controllers.BookControllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookController : ControllerBase
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }


    [HttpGet("GetBooks")]
    public async Task<ActionResult<ApiResponse<string>>> GetBooks()
    {
        var books = await _bookService.GetBooks();

        return Ok(books);
    }
    [HttpPost("AddBook")]
    public async Task<ActionResult<ApiResponse<string>>> AddBook()
    {
        var books = await _bookService.GetBooks();

        return Ok(books);
    }
    [HttpPut("UpdateBook")]
    public async Task<ActionResult<ApiResponse<string>>> UpdateBook()
    {
        var books = await _bookService.GetBooks();

        return Ok(books);
    }
    [HttpDelete("DeleteBook")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteBook()
    {
        var books = await _bookService.GetBooks();

        return Ok(books);
    }




}
