

using Books.Models;

namespace Books.DTO.Requests;


public class BookRequest
{
    public required string Title { get; set; }

    public int AuthorId { get; set; }
    public Author? Author { get; set; }

    public int GenreId { get; set; }
    public required Genre? Genre { get; set; }

    public string? Series { get; set; }
    public int Pages { get; set; }
    public DateTime? StartedReading { get; set; }
    public DateTime? EndedReading { get; set; }
    public int ReleaseYear { get; set; }
}

public class AuthorRequest
{
    public string? FirstName { get; set; } = "Unknown";
    public string? LastName { get; set; } = "Unknown";
    public DateTime? Birth { get; set; }
    public string? WebUrl { get; set; }
}

public class GenreRequest
{
    public required string Name { get; set; }
}