using System;

namespace ProfileService.Model
{
	public class Education
	{
        public Guid Id { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
        public string Field { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}

