using System;
using System.Collections.Generic;

namespace ProfileService.Model
{
    public enum Gender
    {
        MALE, FEMALE, OTHER
    }

	public class Profile
	{
		public Guid Id { get; set; }
        public bool Public { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; }
        public Guid? ImageId { get; set; }
        public virtual Image Image { get; set; }

        public virtual ICollection<Block> Blocked { get; set; }
        public virtual ICollection<Block> BlockedBy { get; set; }

        public ICollection<Skill> Skills { get; set; }
        public ICollection<Education> Education { get; set; }
        public ICollection<WorkExperience> WorkExperiences { get; set; }
    }
}

