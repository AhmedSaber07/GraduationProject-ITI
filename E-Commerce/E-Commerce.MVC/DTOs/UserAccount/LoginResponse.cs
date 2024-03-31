namespace E_Commerce.MVC.DTOs.UserAccount
{
    public class LoginResponse
    {
        public string token { get; set; }
       public DateTime expiration { get; set; }
        public RegisterDto userDate { get; set; }   
    }
}
