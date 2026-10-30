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
        var res = await _bookService.AddBook(userID, req);

        if (res.StatusCode == 404)
            return NotFound(res);
        if (res.StatusCode == 400)
            return BadRequest(res);
        return Ok(res);
    }

    [HttpPut("UpdateBook")]
    public async Task<ActionResult<ApiResponse<string>>> UpdateBook([FromBody] UpdateBookRequest req)
    {
        var userID = User.GetUserId();
        var res = await _bookService.UpdateBook(userID, req);

        return Ok(res);
    }

    [HttpDelete("DeleteBook")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteBook()
    {
        var userID = User.GetUserId();
        var books = await _bookService.GetBooks(userID);

        return Ok(books);
    }

    [HttpPost("UpdateHex")]
    public async Task<ActionResult<ApiResponse<string>>> UpdateBookHexColor([FromBody] List<UpdateBookColorRequest> reqs)
    {
        var userID = User.GetUserId();
        var res = await _bookService.UpdateBookColor(userID, reqs);
        if (res.StatusCode == 404)
            return NotFound(res);
        return Ok(res);
    }


}

#endif