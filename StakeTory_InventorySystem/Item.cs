using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StakeTory_InventorySystem
{
    public class Item
    {
        public string Code { get; }
        public string Name { get; }
        public int Quantity { get; set; }
        public double Price { get; }

        public Item(string code, string name, int quantity, double price)
        {
            Code = code;
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }
}
