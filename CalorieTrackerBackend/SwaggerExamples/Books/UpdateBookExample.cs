using Books.DTO.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CalorieTracker.SwaggerExamples.Books;


public class UpdateBookExample : IExamplesProvider<UpdateBookRequest>
{
    public UpdateBookRequest GetExamples()
    {
        return new UpdateBookRequest
        {
            Title = "The Way Of kings",
            ReleaseYear = 2010,
            StartedReading = new DateOnly(2020, 4, 10),
            EndedReading = new DateOnly(2020, 4, 16),
            AuthorId = 1,
            GenreId = 1,
            Pages = 1200,
            Series = "The Stormlight Archive"
        };
    }
}