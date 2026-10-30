using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DTO;

public class BookDTO
{
    public required int UserId { get; set; }
    public required string Title { get; set; }
    public AuthorDTO? Author { get; set; }
    public GenreDTO? Genre { get; set; }
    public string? Series { get; set; }
    public int Pages { get; set; }
    public DateOnly? StartedReading { get; set; }
    public DateOnly? EndedReading { get; set; }
    public int ReleaseYear { get; set; }
    public string? Hex { get; set; }
}

public class AuthorDTO
{
    public required int AuthorId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateOnly? Birth { get; set; }
    public string? WebUrl { get; set; }
}

public class GenreDTO
{
    public required int GenreId { get; set; }
    public required string Name { get; set; }
}