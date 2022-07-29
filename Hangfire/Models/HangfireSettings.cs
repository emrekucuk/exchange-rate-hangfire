namespace Hangfire.Models
{
    public class HangfireSettings
    {
        public UserModel User { get; set; }
        public class UserModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
