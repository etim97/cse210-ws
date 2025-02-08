using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(new Reference("Proverbs", 3, 5, 6), 
                "Trust in the Lord with all your heart and lean not on your own understanding."),
            new Scripture(new Reference("John", 3, 16), 
                "For God so loved the world that He gave His only begotten Son, that whoever believes in Him shall not perish but have eternal life."),
            new Scripture(new Reference("Philippians", 4, 13), 
                "I can do all things through Christ who strengthens me."),
            new Scripture(new Reference("Isaiah", 41, 10), 
                "Do not fear, for I am with you; do not be dismayed, for I am your God. I will strengthen you and help you.")
        };

        Random rnd = new Random();
        Scripture scripture = scriptures[rnd.Next(scriptures.Count)];

        Console.Clear();
        Console.WriteLine("✨ Welcome to the Scripture Memorization Challenge! ");
        Console.WriteLine("\nYour goal: Memorize the scripture before it disappears!");
        Console.WriteLine("Press ENTER to start...");

        Console.ReadLine();

        DateTime startTime = DateTime.Now; // Track time for challenge mode

        while (!scripture.IsCompletelyHidden())
        {
            Console.Clear();
            scripture.Display();
            Console.WriteLine("\nPress ENTER to hide more words or type 'quit' to exit.");

            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                Console.WriteLine("\n🙏 Keep studying! See you next time.");
                return;
            }

            scripture.HideWords();
            MotivateUser(scripture); // AI mentor gives encouragement
        }

        Console.Clear();
        scripture.Display();
        TimeSpan elapsedTime = DateTime.Now - startTime;
        Console.WriteLine($"\n🎉 Well done! You memorized the scripture in {elapsedTime.Seconds} seconds! 🎉");
    }

    static void MotivateUser(Scripture scripture)
    {
        Random rnd = new Random();
        string[] encouragements = {
            "💡 You're doing great! Keep going!",
            "🚀 You're crushing it! Stay focused!",
            "🎯 Just a few more words to go!",
            "🔥 Wow, you're memorizing like a pro!",
            "🌟 Almost there! You got this!"
        };

        if (!scripture.IsCompletelyHidden())
        {
            Console.WriteLine(encouragements[rnd.Next(encouragements.Length)]);
            Thread.Sleep(1000); // Small pause to add suspense
        }
    }
}

// Reference Class (No changes)
class Reference
{
    public string Book { get; }
    public int Chapter { get; }
    public int StartVerse { get; }
    public int? EndVerse { get; }

    public Reference(string book, int chapter, int startVerse, int? endVerse = null)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse == null ? $"{Book} {Chapter}:{StartVerse}" : $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
    }
}

// Word Class (No changes)
class Word
{
    public string Text { get; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public override string ToString()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}

// Scripture Class (Enhanced hiding mechanism)
class Scripture
{
    private Reference Reference { get; }
    private List<Word> Words { get; }

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void Display()
    {
        Console.WriteLine($"\n {Reference}");
        Console.WriteLine(string.Join(" ", Words));
    }

    public void HideWords()
    {
        Random random = new Random();
        int remainingVisible = Words.Count(w => !w.IsHidden);
        int wordsToHide = Math.Max(1, remainingVisible / 4); // Hide progressively

        List<Word> visibleWords = Words.Where(w => !w.IsHidden).ToList();
        if (visibleWords.Count == 0) return;

        for (int i = 0; i < wordsToHide; i++)
        {
            if (visibleWords.Count == 0) break;
            int index = random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public bool IsCompletelyHidden()
    {
        return Words.All(w => w.IsHidden);
    }
}
