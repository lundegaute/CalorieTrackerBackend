using Books.Models;

namespace Books.DTO.Requests;

public class UpdateBookRequest
{
    public int BookId { get; set; }
    public string? Title { get; set; }

    public int AuthorId { get; set; }
    public int GenreId { get; set; }

    public string? Series { get; set; }
    public int? Pages { get; set; }
    public DateOnly? StartedReading { get; set; }
    public DateOnly? EndedReading { get; set; }
    public int? ReleaseYear { get; set; }
} 
