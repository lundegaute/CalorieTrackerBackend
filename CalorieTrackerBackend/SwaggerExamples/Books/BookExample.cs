using Books.DTO.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CalorieTracker.SwaggerExamples.Books;


public class BookExample : IExamplesProvider<AddBookRequest>
{
    public AddBookRequest GetExamples()
    {
        return new AddBookRequest
        {
            UserId = 1,
            Title = "The Way of Kings",
            Series = "The Stormlight Archive",
            AuthorId = 1,
            GenreId = 1,
            StartedReading = new DateOnly(2019, 3, 3),
            EndedReading = new DateOnly(2019, 3, 18),
            Pages = 1001,
            ReleaseYear = 2010
        };
    }
}
