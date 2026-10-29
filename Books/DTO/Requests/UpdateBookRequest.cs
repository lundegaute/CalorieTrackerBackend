using Books.Models;

namespace Books.DTO.Requests;

public class UpdateBookRequest
{
    public string? Title { get; set; }

    public int AuthorId { get; set; }
    public int GenreId { get; set; }

    public string? Series { get; set; }
    public int? Pages { get; set; }
    public DateTime? StartedReading { get; set; }
    public DateTime? EndedReading { get; set; }
    public int? ReleaseYear { get; set; }
} 
