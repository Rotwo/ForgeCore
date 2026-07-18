using System.ComponentModel.DataAnnotations;

namespace ForgeCore.Auth.Contracts.Requests
{
    public class EmailPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [MaxLength(64)]
        public string Password { get; set; } = string.Empty;

        public string DisplayName { get; set; } = "Player";
    }
}