using Books.DTO;
using Books.Models;

namespace Books.Helper;

public static class Mapper
{

    public static BookDTO MapBookToBookDTO(this Book book)
    {
        return new BookDTO
        {
            UserId = book.UserId,
            Title = book.Title,
            Author = new AuthorDTO
            {
                FirstName = book.Author?.FirstName ?? "Unknown",
                LastName = book.Author?.LastName ?? "Unknown",
                Birth = book.Author?.Birth ?? null,
                WebUrl = book.Author?.WebUrl ?? null,

            },
            Genre = new GenreDTO
            {
                Name = book.Genre?.Name ?? "Unknown",
            },
            Series = book.Series,
            StartedReading = book.StartedReading,
            EndedReading = book.EndedReading,
            Pages = book.Pages,
            ReleaseYear = book.ReleaseYear,
        };
    }
}
