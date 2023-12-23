using ConsoleApp.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ConsoleApp.Services;

public class CustomerService : ICustomerService
{
    private readonly IFileService _fileService;

    public CustomerService(IFileService fileService)
    {
        _fileService = fileService;
    }

    public CustomerService()
    {
    }

    private readonly string _filePath = @"c:\Hongs-projects\Customer.json";
    private readonly List<ICustomer> _customerList = new List<ICustomer>();
    
    public bool AddToList(ICustomer customer)
    {
        try
        {
            customer.Id = _customerList.Count + 1;

            _customerList.Add(customer);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
       
    }

    public IEnumerable<ICustomer> GetAllFromList()
    {
        try
        {
                return _customerList;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
