using System;

class Program
{
    static void Main(string[] args)
     
    {
    
        int magicNumber = 18; 
        int guess;

        Console.WriteLine("What is the magic number? " + magicNumber);

        do
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }
        } while (guess != magicNumber);
    }
} 

        
    
