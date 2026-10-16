using Shared.DTO;

namespace Books.Services;

public class BookService
{

    public async Task<ApiResponse<string>> GetBooks()
    {

        return ApiResponse<string>.Success("Working", 200);
    }

    public async Task<ApiResponse<string>> AddBook()
    {

        return ApiResponse<string>.Success("Adding", 200);
    }
    public async Task<ApiResponse<string>> UpdateBook()
    {

        return ApiResponse<string>.Success("Updating", 200);
    }
    public async Task<ApiResponse<string>> DeleteBook()
    {

        return ApiResponse<string>.Success("Delete", 200);
    }
}
