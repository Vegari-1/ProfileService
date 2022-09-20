using System;

namespace ProfileService.Model
{
    public class JobOffer
    {
        public string PositionName { get; set; }
        public string Description { get; set; }
        public string[] Qualifications { get; set; }
        public string CompanyLink { get; set; }
    }
}
