using Books.DTO.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CalorieTracker.SwaggerExamples.Books;

public class UpdateBookColorExample : IExamplesProvider<List<UpdateBookColorRequest>>
{
    public List<UpdateBookColorRequest> GetExamples()
    {
        return [ new UpdateBookColorRequest { BookId = 1, Hex = "#2a452a" } ];
    }
}
