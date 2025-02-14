using System;
using System.Collections.Generic;
using System.Threading;

public class Activity
{
    protected string Name;
    protected string Description;
    protected int Duration;

    public Activity(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public bool StartActivity()
    {
        Console.WriteLine($"\nStarting {Name}...");
        Console.WriteLine(Description);
        Console.Write("\nEnter the duration (in seconds): ");
        if (!int.TryParse(Console.ReadLine(), out Duration) || Duration <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive number.");
            return false;
        }
        Console.WriteLine("Prepare to begin...");
        PauseWithAnimation(3);
        return true;
    }

    public void EndActivity()
    {
        Console.WriteLine($"\nGreat job! You completed {Name} for {Duration} seconds.");
        PauseWithAnimation(3);
    }

    protected void PauseWithAnimation(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write("...");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing Activity", "This activity helps you relax by guiding deep breathing.") { }

    public void Run()
    {
        if (!StartActivity()) return;
        DateTime endTime = DateTime.Now.AddSeconds(Duration);

        while (DateTime.Now < endTime)
        {
            Console.WriteLine("\nBreathe in...");
            PauseWithAnimation(3);
            Console.WriteLine("Breathe out...");
            PauseWithAnimation(3);
        }

        EndActivity();
    }
}

public class ReflectionActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "What did you learn from it?",
        "How did you feel when it was complete?"
    };

    public ReflectionActivity() : base("Reflection Activity", "This activity helps you reflect on moments of strength and resilience.") { }

    public void Run()
    {
        if (!StartActivity()) return;
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];

        Console.WriteLine($"\n{prompt}");
        PauseWithAnimation(5);

        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        while (DateTime.Now < endTime)
        {
            string question = questions[rand.Next(questions.Count)];
            Console.WriteLine($"\n{question}");
            PauseWithAnimation(5);
        }

        EndActivity();
    }
}

public class ListingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing Activity", "This activity helps you list positive things in your life.") { }

    public void Run()
    {
        if (!StartActivity()) return;
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];

        Console.WriteLine($"\n{prompt}");
        PauseWithAnimation(3);

        List<string> userResponses = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(Duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            userResponses.Add(Console.ReadLine());
        }

        Console.WriteLine($"\nYou listed {userResponses.Count} items.");
        EndActivity();
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Activities:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an activity: ");
            string choice = Console.ReadLine();

            if (choice == "4") break;

            switch (choice)
            {
                case "1":
                    new BreathingActivity().Run();
                    break;
                case "2":
                    new ReflectionActivity().Run();
                    break;
                case "3":
                    new ListingActivity().Run();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    Thread.Sleep(2000); // Pause to allow the user to read the message
                    break;
            }
        }
    }
}
