using Books.DataContext;
using Books.Helper;
using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Repository;

public class BookRepository(BooksDataContext context)
{
    private readonly BooksDataContext _context = context;

    public async Task<List<Book>> GetBooksAsync(int userID)
    {
        var books = await _context.Books
            .Include(x => x.Author)
            .Include(x => x.Genre)
            .ToListAsync();

        return books;
    }

    public async Task<bool> AddBookAsync(Book book)
    {
        _context.Books.Add(book);
        var changes = DatabaseTracker.Count<Book>(EntityState.Added, _context);
        await _context.SaveChangesAsync();
        return changes == 1;


    }



}
