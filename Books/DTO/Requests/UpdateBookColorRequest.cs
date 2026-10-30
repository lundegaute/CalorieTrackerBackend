namespace Books.DTO.Requests;


public class UpdateBookColorRequest
{
    public required int BookId { get; set; }
    public required string Hex { get; set; }
}
