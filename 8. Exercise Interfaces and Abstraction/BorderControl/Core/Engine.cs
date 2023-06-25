using BorderControl.Core.Interfaces;
using BorderControl.Models;
using BorderControl.Models.Interfaces;


namespace BorderControl.Core
{
    internal class Engine : IEngine
    {
        public void Run()
        {
            List<IIdentible> population = new();

            string input = string.Empty;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] current = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = current[0];

                if (current.Length == 3)
                {                    
                    int age = int.Parse(current[1]);
                    string id = current[2];
                    IIdentible citizen = new Citizen(id, name, age);
                    population.Add(citizen);

                }
                else
                {
                    string id = current[1];
                    IIdentible robot = new Robot(id, name);
                    population.Add(robot);
                }
            }

            string idToDetain = Console.ReadLine();

            foreach (var figure in population)
            {
                if (figure.Id.EndsWith(idToDetain))
                {
                    Console.WriteLine(figure.Id);
                }
            }
        }
    }
}
