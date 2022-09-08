namespace ProfileService.Dto
{
    public class ProfileSimpleResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Picture { get; set; }
    }
}
