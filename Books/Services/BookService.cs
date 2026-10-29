using Shared.DTO;
using Books.Services.Interface;
using Books.Repository;
using Books.DTO;
using Books.Helper;
using Books.DTO.Requests;

namespace Books.Services;

public class BookService(BookRepository bookRepository, AuthorRepository authorRepository, GenreRepository genreRepository) : IBookService
{
    private readonly BookRepository _bookRepository = bookRepository;
    private readonly AuthorRepository _authorRepository = authorRepository;
    private readonly GenreRepository _genreRepository = genreRepository;

    public async Task<ApiResponse<List<BookDTO>>> GetBooks(int userID)
    {
        var books = await _bookRepository.GetBooksAsync(userID);
        var booksDto = books.Select(book => book.MapBookToBookDTO()).ToList();

        return ApiResponse<List<BookDTO>>.Success(booksDto, 200);
    }

    public async Task<ApiResponse<string>> AddBook(int userID, AddBookRequest req)
    {
        var author = await _authorRepository.GetAuthorAsync(req.AuthorId);
        var genre = await _genreRepository.GetGenreAsync(req.GenreId);

        if (author is null )
            return ApiResponse<string>.NotFound(["Author not found"]);
        if ( genre is null )
            return ApiResponse<string>.NotFound(["Genre not found"]);

        var newBook = Mapper.MapBookRequestToBook(userID, req, author.Id, genre.Id);
        var res = await _bookRepository.AddBookAsync(newBook);

        if (res)
            return ApiResponse<string>.Success("Book added successfully", 200);
        else
            return ApiResponse<string>.Failure(["Book was not added, try again"], ["Sql error"], 400);
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
