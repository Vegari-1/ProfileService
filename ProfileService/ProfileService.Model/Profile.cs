using System;
using System.Collections.Generic;

namespace ProfileService.Model
{
	public class Profile
	{
		public Guid Id { get; set; }
        public bool Public { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; }
        public Guid? ImageId { get; set; }
        public virtual Image Image { get; set; }

        public virtual ICollection<Block> Blocked { get; set; }
        public virtual ICollection<Block> BlockedBy { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Education> Education { get; set; }
        public virtual ICollection<WorkExperience> WorkExperiences { get; set; }
    }
}

