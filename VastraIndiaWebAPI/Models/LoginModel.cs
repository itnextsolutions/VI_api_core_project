namespace VastraIndiaWebAPI.Models
{
    public class LoginModel
    {
        public string username { get; set; }

        public string password { get; set; }
    }

    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
    }
}
