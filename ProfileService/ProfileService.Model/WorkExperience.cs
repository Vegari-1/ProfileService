﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileService.Model
{
    [Table("WorkExperiences", Schema = "profile")]
    public class WorkExperience
	{
        public Guid Id { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}

