#if HAS_BOOKS
using Books.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Books.DTO;
using Books.DTO.Requests;


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
    public async Task<ActionResult<ApiResponse<List<BookDTO>>>> GetBooks()
    {
        var userID = User.GetUserId();
        var books = await _bookService.GetBooks(userID);
        return Ok(books);
    }

    [HttpPost("AddBook")]
    public async Task<ActionResult<ApiResponse<string>>> AddBook([FromBody] AddBookRequest req)
    {
        var userID = User.GetUserId();
        var books = await _bookService.GetBooks(userID);

        return Ok(books);
    }

    [HttpPut("UpdateBook")]
    public async Task<ActionResult<ApiResponse<string>>> UpdateBook()
    {
        var userID = User.GetUserId();
        var books = await _bookService.GetBooks(userID);

        return Ok(books);
    }

    [HttpDelete("DeleteBook")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteBook()
    {
        var userID = User.GetUserId();
        var books = await _bookService.GetBooks(userID);

        return Ok(books);
    }




}

#endif