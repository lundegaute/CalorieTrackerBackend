using Shared.DTO;
using Books.Services.Interface;
using Books.Repository;
using Books.DTO;
using Books.Helper;

namespace Books.Services;

public class BookService(BookRepository bookRepository) : IBookService
{
    private readonly BookRepository _bookRepository = bookRepository;

    public async Task<ApiResponse<List<BookDTO>>> GetBooks(int userID)
    {
        var books = await _bookRepository.GetBooksAsync(userID);
        var booksDto = books.Select(book => book.MapBookToBookDTO()).ToList();

        return ApiResponse<List<BookDTO>>.Success(booksDto, 200);
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
