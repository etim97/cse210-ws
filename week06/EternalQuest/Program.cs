using System;
using System.Collections.Generic;
using System.IO;

// Base Class: Goal
abstract class Goal
{
    public string Name { get; protected set; }
    public int Points { get; protected set; }
    public bool IsCompleted { get; protected set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        IsCompleted = false;
    }

    public virtual void RecordProgress()
    {
        Console.WriteLine($"Progress recorded for {Name}.");
    }

    public abstract string GetGoalType();
    public abstract string SaveData();
}

// Derived Class: SimpleGoal
class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) { }

    public override void RecordProgress()
    {
        IsCompleted = true;
        Console.WriteLine($"{Name} completed! You earned {Points} points.");
    }

    public override string GetGoalType() => "Simple Goal";
    public override string SaveData() => $"{GetGoalType()}|{Name}|{Points}|{IsCompleted}";
}

// Derived Class: EternalGoal
class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordProgress()
    {
        Console.WriteLine($"{Name} recorded! You earned {Points} points.");
    }

    public override string GetGoalType() => "Eternal Goal";
    public override string SaveData() => $"{GetGoalType()}|{Name}|{Points}";
}

// Derived Class: ChecklistGoal
class ChecklistGoal : Goal
{
    private int targetCount;
    private int currentCount;
    private int bonusPoints;

    public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) 
        : base(name, points)
    {
        this.targetCount = targetCount;
        this.bonusPoints = bonusPoints;
        currentCount = 0;
    }

    public override void RecordProgress()
    {
        currentCount++;
        Console.WriteLine($"{Name} progress: {currentCount}/{targetCount} completed.");

        if (currentCount == targetCount)
        {
            IsCompleted = true;
            Console.WriteLine($"Goal completed! You earned {Points + bonusPoints} points.");
        }
    }

    public override string GetGoalType() => "Checklist Goal";
    public override string SaveData() => $"{GetGoalType()}|{Name}|{Points}|{targetCount}|{currentCount}|{bonusPoints}";
}

// Main Program
class Program
{
    static List<Goal> goals = new List<Goal>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nEternal Quest - Goal Tracker");
            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. Record Progress");
            Console.WriteLine("3. View Goals");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": CreateGoal(); break;
                case "2": RecordProgress(); break;
                case "3": DisplayGoals(); break;
                case "4": SaveGoals(); break;
                case "5": LoadGoals(); break;
                case "6": return;
                default: Console.WriteLine("Invalid choice. Try again."); break;
            }
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("Choose Goal Type: 1. Simple  2. Eternal  3. Checklist");
        string type = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter point value: ");
        int points = int.Parse(Console.ReadLine());

        if (type == "1") goals.Add(new SimpleGoal(name, points));
        else if (type == "2") goals.Add(new EternalGoal(name, points));
        else if (type == "3")
        {
            Console.Write("Enter target count: ");
            int targetCount = int.Parse(Console.ReadLine());

            Console.Write("Enter bonus points: ");
            int bonusPoints = int.Parse(Console.ReadLine());

            goals.Add(new ChecklistGoal(name, points, targetCount, bonusPoints));
        }

        Console.WriteLine("Goal created!");
    }

    static void RecordProgress()
    {
        DisplayGoals();
        Console.Write("Enter goal number to record progress: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < goals.Count)
        {
            goals[index].RecordProgress();
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
        }
    }

    static void DisplayGoals()
    {
        Console.WriteLine("\nYour Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            string status = goals[i].IsCompleted ? "[✔]" : "[ ]";
            Console.WriteLine($"{i + 1}. {status} [{goals[i].GetGoalType()}] {goals[i].Name}");
        }
    }

    static void SaveGoals()
    {
        List<string> goalData = new List<string>();
        foreach (Goal goal in goals)
        {
            goalData.Add(goal.SaveData());
        }

        File.WriteAllLines("goals.txt", goalData);
        Console.WriteLine("Goals saved successfully!");
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.txt"))
        {
            goals.Clear();
            string[] lines = File.ReadAllLines("goals.txt");

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                string type = parts[0];
                string name = parts[1];
                int points = int.Parse(parts[2]);

                if (type == "Simple Goal")
                {
                    bool isCompleted = bool.Parse(parts[3]);
                    SimpleGoal goal = new SimpleGoal(name, points);
                    if (isCompleted) goal.RecordProgress();
                    goals.Add(goal);
                }
                else if (type == "Eternal Goal")
                {
                    goals.Add(new EternalGoal(name, points));
                }
                else if (type == "Checklist Goal")
                {
                    int targetCount = int.Parse(parts[3]);
                    int currentCount = int.Parse(parts[4]);
                    int bonusPoints = int.Parse(parts[5]);

                    ChecklistGoal goal = new ChecklistGoal(name, points, targetCount, bonusPoints);
                    for (int i = 0; i < currentCount; i++)
                    {
                        goal.RecordProgress();
                    }
                    goals.Add(goal);
                }
            }

            Console.WriteLine("Goals loaded successfully!");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }
}
