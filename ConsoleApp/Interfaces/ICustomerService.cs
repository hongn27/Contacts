
namespace ConsoleApp.Interfaces;

public interface ICustomerService
{
    bool AddToList(ICustomer customer);

    IEnumerable<ICustomer> GetAllFromList();
}
