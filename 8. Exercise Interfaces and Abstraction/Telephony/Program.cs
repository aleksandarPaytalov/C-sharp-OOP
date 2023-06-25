using Telephony.Models;
using Telephony.Models.Interfaces;

string[] phoneNumbers = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
string[] webSites = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries);


foreach (var number in phoneNumbers)
{
    try
    {
        if (number.Length == 7)
        {
            ICallable stationary = new Stationary();
            Console.WriteLine(stationary.Call(number));
        }
        else
        {
            ICallable smartphone = new Smartphone();
            Console.WriteLine(smartphone.Call(number));           
        }
    
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

foreach (var url in webSites)
{
    try
    {        
        IBrowsable website = new Smartphone();
        Console.WriteLine(website.Browse(url));        
    }

    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
}