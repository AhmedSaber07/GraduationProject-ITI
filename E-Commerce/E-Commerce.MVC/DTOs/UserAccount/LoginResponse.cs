namespace E_Commerce.MVC.DTOs.UserAccount
{
    public class LoginResponse
    {
        public string token { get; set; }
       public DateTime expiration { get; set; }
        public RegisterDto _user { get; set; }   
    }
}
