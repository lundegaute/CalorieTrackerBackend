

namespace Books.Models;

public class Book : BaseModel
{
    public required string Title { get; set; }

    public int AuthorId { get; set; }
    public Author? Author {  get; set; }

    public int GenreId { get; set; }
    public Genre? Genre { get; set; }

    public string? Series {  get; set; }
    public int Pages { get; set; }
    public DateTime? StartedReading { get; set; }
    public DateTime? EndedReading { get; set; }
    public int ReleaseYear { get; set; }
}

public class Author : BaseModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime? Birth {  get; set; }
    public string? WebUrl { get; set; }
}

public class Genre : BaseModel
{
    public required string Name { get; set; }
}