using Books.DataContext;
using Books.DTO.Requests;
using Books.Helper;
using Books.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DTO;

namespace Books.Repository;

public class BookRepository(BooksDataContext context)
{
    private readonly BooksDataContext _context = context;

    public async Task<Book?> GetBookAsync(int userID, int bookID)
    {
        return await _context.Books
            .FirstOrDefaultAsync(book => book.UserId == userID && book.Id == bookID);
    }

    public async Task<List<Book>> GetUsersBooksAsync(int userID)
    {
        var books = await _context.Books
            .Include(x => x.Author)
            .Include(x => x.Genre)
            .ToListAsync();
        return books;
    }

    public async Task<List<Book>> GetBooksAsync(int userID, List<int> bookIDs)
    {
        return await _context.Books
            .Where(b => bookIDs.Contains(b.Id) && b.UserId == userID)
            .ToListAsync();
    }

    public async Task<bool> AddBookAsync(Book book)
    {
        _context.Books.Add(book);
        var changes = DatabaseTracker.Count<Book>(EntityState.Added, _context);
        await _context.SaveChangesAsync();
        return changes == 1;
    }

    public async Task<ApiResponse<string>> UpdateBookAsync(Book book, UpdateBookRequest req)
    {
        book.Title = req.Title ?? "";
        book.AuthorId = req.AuthorId;
        book.GenreId = req.GenreId;
        book.StartedReading = req.StartedReading;
        book.EndedReading = req.EndedReading;
        book.Series = req.Series ?? "";
        book.Pages = req.Pages ?? 0;
        _context.SaveChanges();

        if (_context.ChangeTracker.HasChanges())
            return ApiResponse<string>.Success("No changes", 200);
        
        await _context.SaveChangesAsync();
        return ApiResponse<string>.Success("Book updated", 200); 
    }

    public async Task<bool> DeleteBook(int userID, int bookID)
    {
        throw new NotImplementedException("Not sure if this should be implemented");
    }

    public async Task<int> UpdateBookHexColor(List<Book> books, Dictionary<int, UpdateBookColorRequest> reqs)
    {
        foreach(var book in books)
        {
            if (!reqs.TryGetValue(book.Id, out var req)) continue;
            book.Hex = req.Hex;
        }
        var changes = DatabaseTracker.Count<Book>(EntityState.Modified, _context);
        await _context.SaveChangesAsync();
        return changes;
    }

}
