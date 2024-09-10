namespace CarRent.Views.Models.Account;

public class LoginModel
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}