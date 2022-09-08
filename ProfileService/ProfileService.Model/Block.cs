using System;

namespace ProfileService.Model
{
    public class Block
    {
        public Guid BlockerId { get; set; }
        public virtual Profile Blocker { get; set; }

        public Guid BlockedId { get; set; }
        public virtual Profile Blocked { get; set; }
    }
}
