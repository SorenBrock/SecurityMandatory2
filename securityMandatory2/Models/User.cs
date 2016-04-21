namespace securityMandatory2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool RememberMe = true;
    }
}