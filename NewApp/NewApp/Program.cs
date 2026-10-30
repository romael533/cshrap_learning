using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Welcome " + name + ", Tell me number of items to buy: ");

            string items = Console.ReadLine();
            int noofitmes = int.Parse(items);

            string[,] grid = new string[noofitmes, 3];
            int fulltotal = 0;

            for (int i = 0; i < noofitmes; i++)
            {
                Console.Write("tell the item name: ");
                string itemname = Console.ReadLine();

                Console.Write("tell the price: ");
                string price = Console.ReadLine();

                int pricevalue = int.Parse(price);

                Console.Write("tell quantity: ");
                string quantity = Console.ReadLine();
                int quantityvalue = int.Parse(quantity);

                int total = pricevalue * quantityvalue;
                Console.WriteLine("total amount is: " + total);
                fulltotal = fulltotal + total;
                grid[i, 0] = itemname;
                grid[i, 1] = quantity;
                grid[i, 2] = Convert.ToString(total);
            }
            Console.Write("The Purchased details are:");
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                Console.WriteLine("\n");
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    Console.Write(grid[row, col] + "\t");
                }
            }
            Console.WriteLine("\n");
            if (fulltotal > 500)
            {
                int discount = fulltotal * 10 / 100;
                fulltotal = fulltotal - discount;
                Console.WriteLine("Total discount amount is " + discount + "\n");
                Console.WriteLine("Fianl bill amount is " + fulltotal);
            }
            else
            {
                Console.WriteLine("Fianl bill amount is " + fulltotal);
            }
        }
    }
}
