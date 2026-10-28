using Books.DTO;
using Shared.DTO;

namespace Books.Services.Interface;

public interface IBookService
{

    public Task<ApiResponse<List<BookDTO>>> GetBooks(int userID);
}
