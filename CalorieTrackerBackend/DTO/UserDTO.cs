

using System.ComponentModel.DataAnnotations;

namespace CalorieTracker.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class ResponseUserDTO
    {
        public required int Id { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
    }
}