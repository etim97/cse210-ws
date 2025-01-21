

using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    // JournalEntry class
    public class JournalEntry
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string Date { get; set; }

        public JournalEntry(string prompt, string response, string date)
        {
            Prompt = prompt;
            Response = response;
            Date = date;
        }

        public override string ToString()
        {
            return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
        }
    }

    // Journal class
    public class Journal
    {
        private List<JournalEntry> _entries;
        private List<string> _prompts;

        public Journal()
        {
            _entries = new List<JournalEntry>();
            _prompts = new List<string>
            {
                "Who was the most interesting person I interacted with today?",
                "What was the best part of my day?",
                "How did I see the hand of the Lord in my life today?",
                "What was the strongest emotion I felt today?",
                "If I had one thing I could do over today, what would it be?"
            };
        }

        public void WriteNewEntry()
        {
            Random random = new Random();
            string prompt = _prompts[random.Next(_prompts.Count)];
            Console.WriteLine($"Prompt: {prompt}");
            Console.Write("Your Response: ");
            string response = Console.ReadLine();
            string date = DateTime.Now.ToString("yyyy-MM-dd");

            _entries.Add(new JournalEntry(prompt, response, date));
            Console.WriteLine("Entry saved successfully!\n");
        }

        public void DisplayJournal()
        {
            if (_entries.Count == 0)
            {
                Console.WriteLine("No entries in the journal yet.\n");
                return;
            }

            Console.WriteLine("\nJournal Entries:");
            foreach (var entry in _entries)
            {
                Console.WriteLine(entry);
            }
        }

        public void SaveJournalToFile()
        {
            Console.Write("Enter the filename to save the journal: ");
            string filename = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var entry in _entries)
                {
                    writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
                }
            }

            Console.WriteLine("Journal saved successfully!\n");
        }

        public void LoadJournalFromFile()
        {
            Console.Write("Enter the filename to load the journal: ");
            string filename = Console.ReadLine();

            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.\n");
                return;
            }

            _entries.Clear();

            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        _entries.Add(new JournalEntry(parts[1], parts[2], parts[0]));
                    }
                }
            }

            Console.WriteLine("Journal loaded successfully!\n");
        }
    }

    // Main program class
    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            string choice;

            do
            {
                Console.WriteLine("Journal Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a file");
                Console.WriteLine("4. Load the journal from a file");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        journal.WriteNewEntry();
                        break;
                    case "2":
                        journal.DisplayJournal();
                        break;
                    case "3":
                        journal.SaveJournalToFile();
                        break;
                    case "4":
                        journal.LoadJournalFromFile();
                        break;
                    case "5":
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            } while (choice != "5");
        }
    }
}
