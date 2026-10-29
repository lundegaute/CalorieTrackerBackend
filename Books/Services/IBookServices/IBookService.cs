using Books.DTO;
using Books.DTO.Requests;
using Shared.DTO;

namespace Books.Services.Interface;

public interface IBookService
{
    Task<ApiResponse<string>> AddBook(int userID, AddBookRequest req);
    public Task<ApiResponse<List<BookDTO>>> GetBooks(int userID);
}
