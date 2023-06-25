using FoodShortage.Core.Interfaces;
using FoodShortage.Models;
using FoodShortage.Models.Interfaces;

namespace FoodShortage.Core
{
    internal class Engine : IEngine
    {
        public void Run()
        {
            List<IBuyer> buyers = new();

            int buyersCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < buyersCount; i++)
            {
                string[] info = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = info[0];

                if (info.Length == 4)
                {
                    IBuyer buyer = new Citizen(info[2], name, int.Parse(info[1]), info[3]);
                    buyers.Add(buyer);
                }
                else
                {
                    IBuyer buyer = new Rebel(name, int.Parse(info[1]), info[2]);
                    buyers.Add(buyer);
                }
            }

            string input = string.Empty;

            while ((input = Console.ReadLine()) != "End")
            {
                buyers.FirstOrDefault(b => b.Name == input)?.BuyFood();            
            }

            Console.WriteLine(buyers.Sum(b => b.Food));
        }
    }
}
