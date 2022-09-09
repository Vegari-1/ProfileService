﻿using System;

namespace ProfileService.Model
{
	public class Skill
	{
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}

