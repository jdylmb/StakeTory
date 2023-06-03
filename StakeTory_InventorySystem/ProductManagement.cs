using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StakeTory_InventorySystem
{
    public class ProductManagement
    {
        private List<Product> products;
        private string filepath;

        public ProductManagement(string filepath)
        {
            this.filepath = filepath;
            LoadProducts();
        }

        public List<Product> GetProducts()
        {
            return products;
        }
        private void LoadProducts()
        {
            if (File.Exists(this.filepath))
            {
                string json = File.ReadAllText(this.filepath);
                products = JsonConvert.DeserializeObject<List<Product>>(json);
            }
            else
            {
                products = new List<Product>();
            }
        }

        private void SaveProduct()
        {
            string json = JsonConvert.SerializeObject(products);
            File.WriteAllText(this.filepath, json);
        }


    }
}


using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        string inventoryFilePath = "inventory.json";
        InventorySystem inventorySystem = new InventorySystem(inventoryFilePath);

        while (true)
        {
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.WriteLine("Select an option:");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateAccount(inventorySystem);
                    break;
                case "2":
                    Login(inventorySystem);
                    break;
                case "3":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void CreateAccount(InventorySystem inventorySystem)
    {
        Console.WriteLine("Please enter a username:");
        string username = Console.ReadLine();
        Console.WriteLine("Please enter a password:");
        string password = Console.ReadLine();

        bool accountCreated = inventorySystem.CreateAccount(username, password);

        if (accountCreated)
        {
            Console.WriteLine("Account created successfully!");
        }
        else
        {
            Console.WriteLine("Failed to create account. Username may already exist.");
        }
    }

    static void Login(InventorySystem inventorySystem)
    {
        Console.WriteLine("Please enter your username:");
        string username = Console.ReadLine();
        Console.WriteLine("Please enter your password:");
        string password = Console.ReadLine();

        bool isAuthenticated = inventorySystem.Login(username, password);

        if (isAuthenticated)
        {
            Console.WriteLine("Login successful!");
        }
        else
        {
            Console.WriteLine("Invalid username or password. Login failed.");
        }
    }
}

class Item
{
    public string Code { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}

class InventorySystem
{
    private List<Item> inventory;
    private string inventoryFilePath;

    public InventorySystem(string inventoryFilePath)
    {
        this.inventoryFilePath = inventoryFilePath;
        LoadInventory();
    }

    public bool CreateAccount(string username, string password)
    {
        // Check if the username already exists
        if (inventory.Exists(item => item.Code.Equals(username, StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }

        // Create a new user account
        Item newItem = new Item
        {
            Code = username,
            Name = username, // You can customize this as per your requirements
            Quantity = 0,    // You can customize this as per your requirements
            Price = 0        // You can customize this as per your requirements
        };

        inventory.Add(newItem);
        SaveInventory();

        return true;
    }

    public bool Login(string username, string password)
    {
        // Check if the username and password match an existing user account
        return inventory.Exists(item => item.Code.Equals(username, StringComparison.OrdinalIgnoreCase) && item.Name.Equals(password));
    }

    private void LoadInventory()
    {
        if (File.Exists(inventoryFilePath))
        {
            string json = File.ReadAllText(inventoryFilePath);
            inventory = JsonConvert.DeserializeObject<List<Item>>(json);
        }
        else
        {
            inventory = new List<Item>();
        }
    }

    private void SaveInventory()
    {
        string json = JsonConvert.SerializeObject(inventory);
        File.WriteAllText(inventoryFilePath, json);
    }

    // Rest of the methods (AddItem, DisplayItems, UpdateQuantity, DisplayItem

    public void AddItem(Item item)
    {
        inventory.Add(item);
        SaveInventory();
    }

    public void DisplayItems()
    {
        Console.WriteLine("Inventory Items:");
        Console.WriteLine("----------------");

        foreach (var item in inventory)
        {
            Console.WriteLine($"Code: {item.Code}");
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Quantity: {item.Quantity}");
            Console.WriteLine($"Price: ${item.Price}");
            Console.WriteLine("----------------");
        }
    }

    public void UpdateQuantity(string code, int quantity)
    {
        Item item = FindItem(code);

        if (item != null)
        {
            item.Quantity = quantity;
            SaveInventory();
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }

    public void DisplayItem(string code)
    {
        Item item = FindItem(code);

        if (item != null)
        {
            Console.WriteLine($"Code: {item.Code}");
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Quantity: {item.Quantity}");
            Console.WriteLine($"Price: ${item.Price}");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }

    public Item FindItem(string code)
    {
        return inventory.Find(item => item.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
    }
    