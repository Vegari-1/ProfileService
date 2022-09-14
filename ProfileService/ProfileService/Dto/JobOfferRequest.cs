namespace ProfileService.Dto
{
    public class JobOfferRequest
    {
        public string ApiKey { get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public string[] Qualifications { get; set; }
        public string CompanyLink { get; set; }
    }
}
