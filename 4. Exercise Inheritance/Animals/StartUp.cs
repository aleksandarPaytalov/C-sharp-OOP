using System;
using System.Runtime.CompilerServices;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                string animalType = Console.ReadLine();

                if (animalType == "Beast!")
                {
                    break;
                }

                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    switch (animalType)
                    {
                        case "Dog":
                            Dog dog = new(input[0], int.Parse(input[1]), input[2]);
                            PrintTheAnimal(animalType, dog);
                            break;
                        case "Cat":
                            Cat cat = new(input[0], int.Parse(input[1]), input[2]);
                            PrintTheAnimal(animalType, cat);
                            break;
                        case "Frog":
                            Frog frog = new(input[0], int.Parse(input[1]), input[2]);
                            PrintTheAnimal(animalType, frog);
                            break;
                        case "Kittens":
                            Kitten kitten = new(input[0], int.Parse(input[1]));
                            PrintTheAnimal(animalType, kitten);
                            break;
                        case "Tomcat":
                            Tomcat tomcat = new(input[0], int.Parse(input[1]));
                            PrintTheAnimal(animalType, tomcat);
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
        }
        private static void PrintTheAnimal<T>(string animalType, T animal) where T : Animal
        {
            Console.WriteLine(animalType);
            Console.WriteLine(animal.ToString());
        }
    }
}
