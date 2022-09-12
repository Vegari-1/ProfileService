using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileService.Model
{
    [Table("Blocks", Schema = "profile")]
    public class Block
    {
        public Guid Id { get; set; }

        public Guid BlockerId { get; set; }
        public virtual Profile Blocker { get; set; }

        public Guid BlockedId { get; set; }
        public virtual Profile Blocked { get; set; }
    }
}
