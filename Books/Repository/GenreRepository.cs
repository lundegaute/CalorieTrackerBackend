using Books.DataContext;
using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Repository;

public class GenreRepository(BooksDataContext context)
{
    private readonly BooksDataContext _context = context;
    
    public async Task<Genre?> GetGenreAsync(int genreID)
    {
        var genre = await _context.Genres.FindAsync(genreID);
        return genre;
    }
}