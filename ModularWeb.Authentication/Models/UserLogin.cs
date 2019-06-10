namespace ModularWeb.Authentication.Models
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool SaveLoginInfo { get; set; }
    }
}