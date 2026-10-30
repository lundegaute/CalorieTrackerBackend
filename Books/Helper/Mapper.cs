using Books.DTO;
using Books.DTO.Requests;
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
                AuthorId = book.Author.Id,
                FirstName = book.Author?.FirstName ?? "Unknown",
                LastName = book.Author?.LastName ?? "Unknown",
                Birth = book.Author?.Birth ?? null,
                WebUrl = book.Author?.WebUrl ?? null,

            },
            Genre = new GenreDTO
            {
                GenreId = book.Genre.Id,
                Name = book.Genre?.Name ?? "Unknown",
            },
            Series = book.Series,
            StartedReading = book.StartedReading,
            EndedReading = book.EndedReading,
            Pages = book.Pages,
            ReleaseYear = book.ReleaseYear,
            Hex = book.Hex,
        };
    }

    public static Book MapBookRequestToBook(int userID, AddBookRequest req, int authorId, int genreId)
    {
        return new Book
        {
            UserId = userID,
            Title = req.Title,
            AuthorId = authorId,
            GenreId = genreId,
            Series = req.Series,
            Pages = req.Pages,
            ReleaseYear = req.ReleaseYear,
            StartedReading = req.StartedReading,
            EndedReading = req.EndedReading,
        };
    }
}
