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
        var books = await _bookRepository.GetUsersBooksAsync(userID);
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

    public async Task<ApiResponse<string>> UpdateBook(int userID, UpdateBookRequest req)
    {
        var book = await _bookRepository.GetBookAsync(userID, req.BookId);
        if (book is null)
            return ApiResponse<string>.NotFound(["Book not found"]);

        var res = await _bookRepository.UpdateBookAsync(book, req);
        return res;
    }

    public async Task<ApiResponse<string>> DeleteBook()
    {
        return ApiResponse<string>.Success("Delete not implemented yet", 200);
    }

    public async Task<ApiResponse<string>> UpdateBookColor(int userID, List<UpdateBookColorRequest> reqs)
    {
        var bookIDs = reqs.Select(x => x.BookId).ToList();
        var books = await _bookRepository.GetBooksAsync(userID, bookIDs);
        if (books is null || books.Count == 0)
            return ApiResponse<string>.NotFound(["No book found"]);

        var reqDict = reqs.ToDictionary(x => x.BookId);
        var changes = await _bookRepository.UpdateBookHexColor(books, reqDict);

        return ApiResponse<string>.Success($"Colors changed: {changes}", 200);


    }
}
