namespace ShoppingSpree.Models;
using System;
class StartUp
{
    static void Main(string[] args)
    {
        List<Person> people = new();
        List<Product> products = new();
        string[] personMoney = Console.ReadLine()
                .Split(";", StringSplitOptions.RemoveEmptyEntries);
        string[] productCost = Console.ReadLine()
                    .Split(";", StringSplitOptions.RemoveEmptyEntries);

        try
        {
            foreach (var item in personMoney)
            {
                string[] infoPersonMoney = item
                    .Split("=", StringSplitOptions.RemoveEmptyEntries);
                Person person = new Person(infoPersonMoney[0], decimal.Parse(infoPersonMoney[1]));
                people.Add(person);
            }
            foreach (var item in productCost)
            {
                string[] infoProductCost = item
                    .Split("=", StringSplitOptions.RemoveEmptyEntries);
                Product product = new Product(infoProductCost[0], decimal.Parse(infoProductCost[1]));
                products.Add(product);
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        string input = string.Empty;
        while ((input = Console.ReadLine()) != "END")
        {
            string[] personProduct = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Person person = people.FirstOrDefault(p => p.Name == personProduct[0]);
            Product product = products.FirstOrDefault(p => p.Name == personProduct[1]);

            Console.WriteLine(person.AddProduct(product)); 
        }

        foreach (var person in people)
        {
            Console.WriteLine(person);
        }
    }
}