namespace ProfileService.Dto
{
    public class EducationResponse
    {
        public Guid Id { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
        public string Field { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
