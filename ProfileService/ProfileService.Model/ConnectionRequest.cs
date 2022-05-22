using System;

namespace ProfileService.Model
{
	public class ConnectionRequest
	{
        public Guid Id { get; set; }
        public Profile RequestingProfile { get; set; }
        public Profile RequestedProfile { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

