
using Books.DataContext;
using Books.Models;

namespace Books.Repository;

public class AuthorRepository(BooksDataContext context)
{
    private readonly BooksDataContext _context = context;

    public async Task<Author?> GetAuthorAsync(int authorID)
    {
        var author = await _context.Authors.FindAsync(authorID);
        return author;
    }

}
