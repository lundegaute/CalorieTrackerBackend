

namespace CalorieTracker.DTO;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public List<string> Errors { get; set;} = new List<string>();
    public List<string> Types { get; set;} = new List<string>();
    public int StatusCode { get; set;}

    public static ApiResponse<T> Success(T data, int statusCode)
    {
        return new ApiResponse<T>
        {
            IsSuccess = true,
            Data = data,
            StatusCode = statusCode
        };
    } 
    public static ApiResponse<T> Failure(List<string> errors, List<string> types, int statusCode)
    {
        return new ApiResponse<T>
        {
            IsSuccess = false,
            Errors = errors,
            Types = types,
            StatusCode = statusCode
        };
    } 
}