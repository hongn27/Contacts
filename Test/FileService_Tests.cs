using ConsoleApp.Interfaces;
using ConsoleApp.Services;

namespace ConsoleApp.Tests;

public class FileService_Tests
{
    [Fact]
    public void SaveToFileShould_ReturnTrue_IfFilePathExists()
    {
        // Arrange
        IFileService fileService = new FileService();
        string filePath = @"c:\Hongs-projects\Contacts\test.json";
        string content = "Test content";

        // Act
        bool result = fileService.SaveToFile(filePath, content);

        //Assert
        Assert.True(result );
    }

    [Fact]
    public void SaveToFileShould_ReturnFalse_IfFilePathDoNotExists()
    {

        // Arrange
        IFileService fileService = new FileService();
        string filePath = @$"c:\{Guid.NewGuid()}test.json";
        string content = "Test content";

        // Act
        bool result = fileService.SaveToFile(filePath, content);

        //Assert
        Assert.False(result);
    }



}
