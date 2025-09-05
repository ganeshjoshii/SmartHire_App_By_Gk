using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserDetails
{
    [Key]
    public Guid Id { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;
    public string HouseNo { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State {  get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Linkedin { get; set; } = string.Empty;
    public string GitHub { get; set; } = string.Empty;
    public string Portfolio { get; set; } = string.Empty;

    public Guid UserRegisterId { get; set; }

    [ForeignKey("UserRegisterId")]
    public UserRegisters UserRegister { get; set; } = null!;
}
