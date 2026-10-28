using Books.DataContext;
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




}
