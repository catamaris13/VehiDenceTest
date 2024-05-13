namespace VehiDenceAPI.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string username { get; set; }
        public string PhoneNo { get; set; }
        public bool IsValid { get; set; }
        public string Token { get; set; }

    }
}
