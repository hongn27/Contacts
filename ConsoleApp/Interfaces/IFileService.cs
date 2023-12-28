namespace ConsoleApp.Interfaces;

public interface IFileService
{
    /// <summary>
    /// Save a customer to a file
    /// </summary>
    /// <param name="filePath">Enter filepath with exension (eg.c:\Hongs-projects\Customer.json)</param>
    /// <param name="content">Enter as a string</param>
    /// <returns>Return true if save was succesfull, or falase if it failed</returns>
    bool SaveToFile(string filePath, string content);

    /// <summary>
    /// Get content from a file
    /// </summary>
    /// <param name="filePath">Enter filepath with extension (eg.c:\Hongs-projects\Customer.json)</param>
    /// <returns>Return content if file exist, else return null</returns>
    string GetContentFromFile(string filePath);

    /// <summary>
    /// Remove a customer from the list with email
    /// </summary>
    /// <param name="email">Enter email as a string</param>
    /// <returns>Return true if succesfully removed, else returns false if failed or not found</returns>
    //bool RemoveCustomer(string email);
}
