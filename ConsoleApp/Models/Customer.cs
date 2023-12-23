using ConsoleApp.Interfaces;

namespace ConsoleApp.Models;

public class Customer : ICustomer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhongNumer { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
}
