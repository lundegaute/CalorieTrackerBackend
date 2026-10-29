

namespace Books.Models;

public class Book : BaseModel
{
    public int UserId { get; set; }
    public required string Title { get; set; }

    public int AuthorId { get; set; }
    public Author? Author {  get; set; }

    public int GenreId { get; set; }
    public Genre? Genre { get; set; }

    public string? Series {  get; set; }
    public int Pages { get; set; }
    public DateOnly? StartedReading { get; set; }
    public DateOnly? EndedReading { get; set; }
    public int ReleaseYear { get; set; }
}

public class Author : BaseModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateOnly? Birth {  get; set; }
    public string? WebUrl { get; set; }
}

public class Genre : BaseModel
{
    public required string Name { get; set; }
}