namespace microkart.identity.Services.Identity.API.Models;

public class ApplicationUserViewModel
{
    [Required]
    public string FirstName { get; set; } = "";

    [Required]
    public string LastName { get; set; } = "";

    [Required]
    public string Email { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";

    [Required]
    public string ConfirmPassword { get; set; } = "";

    [Required]
    public string Street { get; set; } = "";

    [Required]
    public string City { get; set; } = "";

    [Required]
    public string State { get; set; } = "";

    [Required]
    public string Country { get; set; } = "";

    [Required]
    public string ZipCode { get; set; } = "";
    
    [Required]
    public string ReturnURl { get; set; } = "";
}
