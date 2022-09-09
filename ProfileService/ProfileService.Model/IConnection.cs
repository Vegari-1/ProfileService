using System;

namespace ProfileService.Model
{
    public interface IConnection
    {
        public Guid Id { get; set; }
        public Guid Profile1 { get; set; }
        public Guid Profile2 { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
