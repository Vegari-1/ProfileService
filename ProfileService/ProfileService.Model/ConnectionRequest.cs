using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileService.Model
{
    [Table("ConnectionRequests", Schema = "profile")]
    public class ConnectionRequest : IConnection
	{
        public Guid Id { get; set; }
        public Guid Profile1 { get; set; }
        public Guid Profile2 { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

