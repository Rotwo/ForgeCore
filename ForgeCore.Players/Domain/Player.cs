using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeCore.Players.Domain
{
    public class Player
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
