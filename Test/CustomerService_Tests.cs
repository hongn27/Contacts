using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using ConsoleApp.Services;
using Moq;

namespace ConsoleApp.Tests;

public class CustomerService_Tests
{
    [Fact]
    public void AddToListShould_AddOneCustomerToCustomerList_ThenReturnTrue()
    {
        // Arrange

        ICustomer customer = new Customer { FirstName = "Hong", LastName = "Nguyen", PhoneNumber = "0700 261765", Email = "hong@email.com", Address = "Kungsgatan 30" };

        var mockFileService = new Mock<IFileService>();
        ICustomerService customerService = new CustomerService(mockFileService.Object);

        // Act
        bool result = customerService.AddToList(customer);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetALLFromListShould_GetAllCustomersInCustomerList_ThenReturnListOfCustomer()
    {
        // Arrange 
        var json = "[{\"$type\":\"ConsoleApp.Models.Customer, ConsoleApp\",\"Id\":1,\"FirstName\":\"Hong\",\"LastName\":\"Nguyen\",\"PhoneNumber\":\"0700 261765\",\"Email\":\"hong@email.com\",\"Address\":\"Kungsgatan 30\",}] ";

        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(x => x.GetContentFromFile(It.IsAny<string>())).Returns(json);

        ICustomerService customerService = new CustomerService(mockFileService.Object);
      
        // Act
        IEnumerable<ICustomer> result = customerService.GetCustomersFromList();

        // Assert
        Assert.NotNull(result);   
        Assert.True(result.Any());
        ICustomer returnedCustomer = result.FirstOrDefault()!;
        Assert.Equal(1, returnedCustomer.Id); 
    }

}
