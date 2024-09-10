using System.ComponentModel.DataAnnotations;

namespace CarRent.Views.Models.Account;

public class ResetPasswordModel
{
    public string UserId { get; set; }
    public string Token { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}