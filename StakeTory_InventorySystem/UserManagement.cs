using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StakeTory_InventorySystem
{
    public class UserManagement
    {
        private List<User> users;
        private string filepath;

        public UserManagement(string filepath)
        {
            this.filepath = filepath;
            LoadUsers();
        }

        public List<User> GetUsers()
        {
            return users;
        }
        private void LoadUsers()
        {
            if (File.Exists(this.filepath))
            {
                string json = File.ReadAllText(this.filepath);
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }
            else
            {
                users = new List<User>();
            }
        }
        public bool CreateAccount(string username, string password, string fullName)
        {
            // Check if the username already exists
            if (users.Exists(item => item.username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            // Create a new user account
            User newItem = new User
            {
                username = username,
                password = password, // You can customize this as per your requirements
                fullName = fullName,    // You can customize this as per your requirements
            };

            users.Add(newItem);
            SaveUsers();

            return true;
        }

        public bool Login(string username, string password)
        {
            // Check if the username and password match an existing user account
            return users.Exists(item => item.username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        private void SaveUsers()
        {
            string json = JsonConvert.SerializeObject(users);
            File.WriteAllText(this.filepath, json);
        }
    }
}

