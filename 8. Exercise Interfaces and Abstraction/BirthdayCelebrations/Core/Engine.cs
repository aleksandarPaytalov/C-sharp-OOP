using BirthdayCelebrations.Core.Interfaces;
using BirthdayCelebrations.Models;
using BirthdayCelebrations.Models.Interfaces;

namespace BirthdayCelebrations.Core
{
    internal class Engine : IEngine
    {
        public void Run()
        {
            List<IBirthdate> birthdays = new();

            string input = string.Empty;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] current = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string cmd = current[0];

                if (cmd == "Citizen")
                {
                    string name = current[1];
                    int age = int.Parse(current[2]);
                    string id = current[3];
                    string birthdayDate = current[4];
                    IBirthdate citizen = new Citizen(id, name, age, birthdayDate);
                    birthdays.Add(citizen);
                }
                else if (cmd == "Pet")
                {
                    string name = current[1];
                    string birthdayDate = current[2];
                    IBirthdate pet = new Pet(name, birthdayDate);
                    birthdays.Add(pet);
                }              
            }

            string birthdayYear = Console.ReadLine();

            if ( )
            foreach (var celebration in birthdays)
            {
                if (celebration.Birthday.EndsWith(birthdayYear))
                {
                    Console.WriteLine(celebration.Birthday);
                }
            }
        }
    }
}
