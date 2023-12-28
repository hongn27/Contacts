
namespace ConsoleApp.Interfaces;

public interface ICustomerService
{
    /// <summary>
    /// Add a customer to the contact list
    /// </summary>
    /// <param name="customer">A contact made with ICustomer</param>
    /// <returns>Return true if succesfull, return false if failed or customer already exists</returns>
    bool AddToList(ICustomer customer);

    /// <summary>
    /// Get customers from GetCustomersFromList
    /// </summary>
    /// <returns>Return customer if list have item in it, else return null</returns>
    IEnumerable<ICustomer> GetCustomersFromList();
    
    /// <summary>
    /// Get a specific customer from GetCustomerFromList
    /// </summary>
    /// <param name="email">Enter email as a string</param>
    /// <returns>Return the found customer if it exist, else return null</returns>
    ICustomer GetCustomerFromList(string email);
   

    /// <summary>
    /// Remove a customer from the list with email
    /// </summary>
    /// <param name="email">Enter email as a string</param>
    /// <returns>Return true if succesfully removed, else returns false if failed or not found</returns>
    bool RemoveCustomerFromList(string email);

}
