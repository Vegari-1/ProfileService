namespace ProfileService.Dto
{
    public class ConnectionRequestResponse
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
    }
}
