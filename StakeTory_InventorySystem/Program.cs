using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StakeTory_InventorySystem;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=localhost;Database=inventory;Uid=root;Pwd";
        InventorySystem inventorySystem = new InventorySystem(connectionString);

        // Login
        Console.WriteLine("Please enter your username:");
        string username = Console.ReadLine();
        Console.WriteLine("Please enter your password:");
        string password = Console.ReadLine();

        bool loggedIn = inventorySystem.Login(username, password);

        if (loggedIn)
        {
            Console.WriteLine("Login successful!");

            // Perform inventory operations
            // ...
        }
        else
        {
            Console.WriteLine("Login failed. Invalid username or password.");
        }

        Console.ReadLine();
    }
}
