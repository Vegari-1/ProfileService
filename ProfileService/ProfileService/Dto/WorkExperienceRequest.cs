namespace ProfileService.Dto
{
    public class WorkExperienceRequest
    {
        public string Position { get; set; }
        public string Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
