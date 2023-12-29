using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using System;
using System.Diagnostics;

namespace ConsoleApp.Services
{
    public class MenuService : IMenuService
    {
        private readonly ICustomerService _customerService;

        public MenuService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public int GetMenuChoice()
        {
            Console.Write("");
            var choice = Console.ReadLine();
            return Convert.ToInt32(choice);
        }

        public string GetMenuTitle(string choice)
        {
            //Retur till huvudmenyn baserat på valet
            switch (choice)
            {
                case "1":
                    return "Add Customer";
                case "2":
                    return "Show All Customers";
                case "3":
                    return "Show Specific Customer";
                case "4":
                    return "Remove Customer";
                case "5":
                    return "Exit";
                default:
                    return "Invalid Choice";

            }
        }
        public void DisplayMenu()
        {
            int menuIndex = 0;

            try
            {
                while (true)
                {
                    //Menyn (menuIndex)
                    Console.Clear();
                    Console.WriteLine("---Customer List---\n");

                    for (int i = 1; i <= 5; i++)
                    {
                        if (i == menuIndex + 1 && i != 5)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("->");

                        }
                        else if (i == menuIndex + 1 && i == 5)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("->");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                        switch (i)
                        {
                            case 1:
                                Console.WriteLine("Add Customer");
                                break;
                            case 2:
                                Console.WriteLine("Show All Customers");
                                break;
                            case 3:
                                Console.WriteLine("Show Specific Customer");
                                break;
                            case 4:
                                Console.WriteLine("Remove Customer");
                                break;
                            case 5:
                                Console.WriteLine("Exit");
                                break;
                        }
                        Console.ResetColor();
                    }
                    //Hantera piltangenten
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        menuIndex = (menuIndex + 1) % 5;
                    }
                    else if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        menuIndex = (menuIndex - 1 + 5) % 5;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        //Åtgärder på valda menyalternativ i menuIndex
                        switch (menuIndex + 1)
                        {
                            case 1:
                                AddCustomerMenu();
                                break;
                            case 2:
                                ShowCustomersMenu();
                                break;
                            case 3:
                                ShowCustomerMenu();
                                break;
                            case 4:
                                RemoveCustomer();
                                break;
                            case 5:
                                Exit();
                                break;
                        }
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); 
            }
        }

        private void RemoveCustomer()
        {
            try
            {
                Console.Clear();
                Console.WriteLine($"---- {GetMenuTitle("4")} ----");

                //Tar bort kunden från listan med sökmetod "email"
                Console.Write("Enter the email of the customer you want to remove: ");
                var email = Console.ReadLine()!.ToLower();

                //Kunden hittas med email
                var customer = _customerService.GetCustomersFromList().FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                //Om kunden hittas, ta bort den från listan
                if (customer != null)
                {
                    Console.WriteLine($"FirstName: {customer.FirstName}");
                    Console.WriteLine($"LastName: {customer.LastName}");
                    Console.WriteLine($"Phone: {customer.PhoneNumber}");
                    Console.WriteLine($"Email: {customer.Email}");
                    Console.WriteLine($"Adress: {customer.Address}");
                    Console.WriteLine("---------------------");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Are your sure you want to remove this customer from the list? (y/n): ");
                    var message = Console.ReadLine()!;
                    message = message.ToUpper();
                    Console.ResetColor();

                    if (message == "Y")
                    {
                        //Försök att ta bort kunden från listan
                        bool isRemoved = _customerService.RemoveCustomerFromList(email);

                        // Om kunden har tagits bort, visar meddelande om att det lyckades
                        if (isRemoved)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("---- Customer Removed ----\n");
                            Console.ResetColor();
                            ConfirmContiue();
                        }

                        //Om kunden inte har tagits bort, visar felmeddelande
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("---- Customer Not Removed ----\n");
                            Console.ResetColor();
                            ConfirmContiue();
                        }
                    }
                }
                //Om kunden inte hittas, visar felmeddelande
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Customer not found");
                    Console.ResetColor();
                    ConfirmContiue();
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
        //Söka efter kunden med sökmetod "email"
        private void ShowCustomerMenu()
        {
            try
            {
                Console.Clear();
                Console.WriteLine($"--- {GetMenuTitle("3")} ---\n");
                Console.Write("Enter first name of the customer you want to view: ");
                var email = Console.ReadLine()!;

                //var customer = _customerService.GetCustomersFromList().FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                var customer = _customerService.GetCustomersFromList().FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (customer != null)
                {
                    Console.WriteLine($"FirstName: {customer.FirstName}");
                    Console.WriteLine($"LastName: {customer.LastName}");
                    Console.WriteLine($"Phone: {customer.PhoneNumber}");
                    Console.WriteLine($"Email: {customer.Email}");
                    Console.WriteLine($"Adress: {customer.Address}");
                    Console.WriteLine("----------------------------");

                    ConfirmContiue();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Customer not found");
                    Console.ResetColor();
                    ConfirmContiue();

                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
        private static void ConfirmContiue()
        {
            Console.WriteLine("Press any key to return to main menu");
            Console.ReadKey();
        }
        private void Exit()
        {
            Console.Clear();
            Console.Write("Are you sure you want to exit? (y/n): ");
            var message = Console.ReadLine()!;
            message = message.ToUpper();

            if (message == "Y")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Thank you and welcome back");
                Console.ResetColor();
                Console.WriteLine("Press any key to Exit");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else
            {
                DisplayMenu();
            }
        }
        public void ShowCustomersMenu()
        {
            try
            {
                Console.Clear();
                Console.WriteLine($"--- {GetMenuTitle("2")} ---\n" +
                    $"");
                var customers = _customerService.GetCustomersFromList();

                if (customers.Count() == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No customer found");
                    Console.ResetColor();
                    ConfirmContiue();
                    return;

                }
                else
                {
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"FirstName: {customer.FirstName}");
                        Console.WriteLine($"LastName: {customer.LastName}");
                        Console.WriteLine($"Phone: {customer.PhoneNumber}");
                        Console.WriteLine($"Email: {customer.Email}");
                        Console.WriteLine($"Adress: {customer.Address}");
                        Console.WriteLine("------------------------");
                    }
                }
                ConfirmContiue();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void AddCustomerMenu()
        {
            //Lägg till ny kund
            ICustomer customer = new Customer();

            Console.Clear();

            //Lägg till kundens uppgifter
            Console.WriteLine($"--- {GetMenuTitle("1")} ---");

            Console.Write("Enter first name:");
            customer.FirstName = Console.ReadLine()!;

            Console.Write("Enter last name:");
            customer.LastName = Console.ReadLine()!;

            Console.Write("Enter phone number:");
            customer.PhoneNumber = Console.ReadLine()!;

            Console.Write("Enter email:");
            customer.Email = Console.ReadLine()!;

            Console.Write("Enter Address:");
            customer.Address = Console.ReadLine()!;

            //Lägg till kunden i listan
            _customerService.AddToList(customer);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--- Customer added ---");
            Console.ResetColor();

            Console.WriteLine("Would you like to add another customer? (y/n)");
            var message = Console.ReadLine()!;

            message = message.ToUpper();
            if (message == "Y")
            {
                AddCustomerMenu();
            }
            else
            {
                DisplayMenu();
            }
            Console.ReadKey();
            DisplayMenu();
        }
    }
}
