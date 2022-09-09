namespace ProfileService.Dto
{
    public class EducationRequest
    {
        public string School { get; set; }
        public string Degree { get; set; }
        public string Field { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
