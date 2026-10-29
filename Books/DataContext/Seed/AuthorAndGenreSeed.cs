using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.DataContext.Seed;

public static class AuthorAndGenreSeed
{
    public async static Task SeedDefaultData(BooksDataContext _context)
    {
        List<string> genres = ["Fantasy", "Sci-Fi", "Crime", "Biography", "Non-fiction"];
        var authors = new List<Author>
        {
            new Author
            {
                FirstName = "Brandon",
                LastName = "Sanderson",
                Birth = new DateOnly(1975, 12, 19),
                WebUrl = "https://www.brandonsanderson.com"
            },
            new Author
            {
                FirstName = "Steven",
                LastName = "Erikson",
                Birth = new DateOnly(1959, 10, 7),
                WebUrl = "https://steven-erikson.org"
            },
            new Author
            {
                FirstName = "Robin",
                LastName = "Hobb",
                Birth = new DateOnly(1952, 3, 5),
                WebUrl = "http://www.robinhobb.com"
            },
            new Author
            {
                FirstName = "George R.R.",
                LastName = "Martin",
                Birth = new DateOnly(1948, 9, 20),
                WebUrl = "https://georgerrmartin.com"
            },
            new Author
            {
                FirstName = "J.R.R.",
                LastName = "Tolkien",
                Birth = new DateOnly(1892, 1, 3),
                WebUrl = "https://www.tolkiensociety.org"
            },
            new Author
            {
                FirstName = "Joe",
                LastName = "Abercrombie",
                Birth = new DateOnly(1974, 12, 31),
                WebUrl = "https://joeabercrombie.com"
            },
            new Author
            {
                FirstName = "Cixin",
                LastName = "Liu",
                Birth = new DateOnly(1963, 6, 23),
                WebUrl = null
            },
            new Author
            {
                FirstName = "Brent",
                LastName = "Weeks",
                Birth = new DateOnly(1977, 3, 7),
                WebUrl = "https://www.brentweeks.com"
            }
        };
    

        var existingAuthors = await _context.Authors.ToListAsync();
        var existingGenres = await _context.Genres.Select(x => x.Name).ToListAsync();
        foreach(var genre in genres.Except(existingGenres, StringComparer.OrdinalIgnoreCase)) {
            _context.Genres.Add( new Genre { Name = genre });
        }
        foreach(var author in authors.Except(existingAuthors) )
        {
            _context.Authors.Add(author);
        }
        await _context.SaveChangesAsync();


        var erikson = await _context.Authors.FirstOrDefaultAsync(x => x.LastName == "Erikson");
        var fantasy = await _context.Genres.FirstOrDefaultAsync(x => x.Name == "Fantasy");
        var bookExist = await _context.Books.AnyAsync(x => x.Title == "Gardens of the Moon" && x.UserId == 1);

        if ( erikson is not null && fantasy is not null && !bookExist)
        {
            var GardensOfTheMoon = new Book
            {
                UserId = 1,
                Title = "Gardens of the Moon",
                AuthorId = erikson.Id,
                GenreId = fantasy.Id,
                Series = "Book of the Fallen",
                Pages = 850,
                StartedReading = new DateOnly(2020, 10, 1),
                EndedReading = new DateOnly(2020, 10, 20),
                ReleaseYear = 1999,
            };
            _context.Books.Add(GardensOfTheMoon);
        }







    }
}
