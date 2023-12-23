using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using ConsoleApp.Services;

namespace ConsoleApp.Tests;

public class CustomerService_Tests
{
    [Fact]
    public void AddToListShould_AddOneCustomerToCustomerList_ThenReturnTrue()
    {
        // Arrange

        ICustomer customer = new Customer { FirstName = "Hong", LastName = "Nguyen", PhongNumer = "0700 261765", Email = "hong@email.com", Address = "Kungsgatan 30" };
        ICustomerService customerService = new CustomerService();


        // Act
        bool result = customerService.AddToList(customer);


        // Assert
        Assert.True(result);

    }

    [Fact]
    public void GetALLFromListShould_GetAllCustomersInCustomerList_ThenReturnListOfCustomer()
    {
        // Arrange 

        ICustomerService customerService = new CustomerService();
        ICustomer customer = new Customer { FirstName = "Hong", LastName = "Nguyen", PhongNumer = "0700 261765", Email = "hong@email.com", Address = "Kungsgatan 30" };
        customerService.AddToList(customer);

        // Act
        IEnumerable<ICustomer> result = customerService.GetAllFromList();

        // Assert
        Assert.NotNull(result);   
        Assert.True(result.Any());
        ICustomer returnedCustomer = result.FirstOrDefault()!;
        Assert.Equal(1, returnedCustomer.Id); 
    }

}
