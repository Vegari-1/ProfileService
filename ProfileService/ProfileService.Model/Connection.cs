﻿using System;

namespace ProfileService.Model
{
	public class Connection : IConnection
	{
        public Guid Id { get; set; }
        public Guid Profile1 { get; set; }
        public Guid Profile2 { get; set; }
        public DateTime Timestamp { get; set; }
    }
}