using System;

class Program
{
    static void Main(string[] args)
    {
      Console.WriteLine("Welcome to the program!");

        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();

        Console.Write("Please enter your favorite number: ");
        int favoriteNumber = int.Parse(Console.ReadLine());

        DisplaySquareMessage(name, favoriteNumber);
    }

    static void DisplaySquareMessage(string name, int number)
    {
        int square = number * number;
        Console.WriteLine($"{name}, the square of your number is {square}");
    }
}  
    
