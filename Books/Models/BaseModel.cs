
using Books.Models.Interfaces;

namespace Books.Models;

public class BaseModel : IAuditable
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
