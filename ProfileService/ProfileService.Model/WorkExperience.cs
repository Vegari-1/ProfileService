namespace ProfileService.Model
{
	public class WorkExperience
	{
        public Guid Id { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

