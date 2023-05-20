using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StakeTory_InventorySystem
{
    public class InventorySystem
    {
        private List<Item> inventory;
        private string connectionString;

        public InventorySystem(string connectionString)
        {
            inventory = new List<Item>();
            this.connectionString = connectionString;
        }

        public bool Login(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM users WHERE Username = @Username AND Password = @Password";
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                int count = Convert.ToInt32(command.ExecuteScalar());

                return count > 0;
            }
        }


        public void AddItem(Item item)
        {
            inventory.Add(item);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();

                command.CommandText = "INSERT INTO items (Code, Name, Quantity, Price) VALUES (@Code, @Name, @Quantity, @Price)";
                command.Parameters.AddWithValue("@Code", item.Code);
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@Quantity", item.Quantity);
                command.Parameters.AddWithValue("@Price", item.Price);

                command.ExecuteNonQuery();
            }
        }

        public void DisplayItems()
        {
            Console.WriteLine("Inventory Items:");
            Console.WriteLine("----------------");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM items";

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string code = reader.GetString("Code");
                        string name = reader.GetString("Name");
                        int quantity = reader.GetInt32("Quantity");
                        double price = reader.GetDouble("Price");

                        Console.WriteLine($"Code: {code}");
                        Console.WriteLine($"Name: {name}");
                        Console.WriteLine($"Quantity: {quantity}");
                        Console.WriteLine($"Price: ${price}");
                        Console.WriteLine("----------------");
                    }
                }
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

        public void UpdateQuantity(string code, int quantity)
        {
            Item item = FindItem(code);

            if (item != null)
            {
                item.Quantity = quantity;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();

                    command.CommandText = "UPDATE items SET Quantity = @Quantity WHERE Code = @Code";
                    command.Parameters.AddWithValue("@Quantity", item.Quantity);
                    command.Parameters.AddWithValue("@Code", item.Code);

                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Quantity updated successfully.");
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }

        private Item FindItem(string code)
        {
            foreach (Item item in inventory)
            {
                if (item.Code == code)
                {
                    return item;
                }
            }

            return null;
        }
    }
}

