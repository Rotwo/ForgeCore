using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeCore.Shared.DTOs
{
    public class AuthResultDto
    {
        public Guid AccountId { get; set; }
        public Guid SessionId { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
