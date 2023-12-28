using ConsoleApp.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;


namespace ConsoleApp.Services;

public class CustomerService : ICustomerService
{
    private readonly IFileService _fileService;

    public CustomerService (IFileService fileService)
    {
        _fileService = fileService;
    }

    private readonly string _filePath = @"c:\Hongs-projects\Contacts\Customer.json";
    private List<ICustomer> _customerList = new List<ICustomer>();
    
    //Lägg till i kundlistan
    public bool AddToList(ICustomer customer)
    {
        try
        {
            if (!_customerList.Any(x => x.Email == customer.Email))
            {
                _customerList.Add(customer);
                var json = JsonConvert.SerializeObject(_customerList, new JsonSerializerSettings
                { 
                    TypeNameHandling = TypeNameHandling.Objects,
                    Formatting = Formatting.Indented
                });

                _fileService.SaveToFile(_filePath, json);
                return true;

            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;      
    }        
    //Hämtar kunden från kundlistan
    public IEnumerable<ICustomer> GetCustomersFromList()
    {
        try
        {
            var content = _fileService.GetContentFromFile(_filePath);
            if (!string.IsNullOrEmpty(content))
            {
                _customerList = JsonConvert.DeserializeObject<List<ICustomer>>(content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    Formatting = Formatting.Indented
                })!;
            }

                return _customerList;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public ICustomer GetCustomerFromList(string email)
    {
        try
        {
            GetCustomersFromList();
            {
               var customer = _customerList.FirstOrDefault(x => x.Email == email);
            }
        }
        catch (Exception ex) { Debug.WriteLine("CustomerService - GetCustomerFromList:: " + ex.Message); }
        return null!;
    }

    //Tar bort kunden från kundlistan
    public bool RemoveCustomerFromList(string email)
    {
        try
        {
            //Hämtar kunden från kundlistan
            GetCustomerFromList();

            //Hitta kunden med sökmetod email
            var customer = _customerList.FirstOrDefault(x => x.Email == email);

            //kunden finns, tar bort den
            if (customer != null)
            {
                //Tar bort kunden
                _customerList.Remove(customer);

                //Justera om kundlisan
                string json = JsonConvert.SerializeObject(_customerList, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    Formatting = Formatting.Indented
                });

                //Spara kundlistan
                var result = _fileService.SaveToFile(_filePath, json);
                return true;
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message);
        } return false;
    }

    public object GetCustomerFromList()
    {
        throw new NotImplementedException();
    }
}
