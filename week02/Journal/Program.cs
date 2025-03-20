using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        string[] prompts = {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What made me laugh today?",
            "What is one thing I learned today?",
            "What am I grateful for today?",
            "What challenge did I overcome today?",
            "What is one thing I want to remember from today?"
        };

        string[] encouragements = {
            "Keep going! You're doing great!",
            "Every entry brings you closer to your goals.",
            "You're making progress, one entry at a time.",
            "Your journal is a reflection of your growth.",
            "Take a moment to appreciate how far you've come."
        };

        // Password protection
        Console.Write("Enter password to access the journal: ");
        string inputPassword = Console.ReadLine();
        if (!journal.CheckPassword(inputPassword))
        {
            Console.WriteLine("Incorrect password. Exiting program.");
            return;
        }

        while (true)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Search entries by keyword");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Random rand = new Random();
                    string prompt = prompts[rand.Next(prompts.Length)];
                    Console.WriteLine($"Prompt: {prompt}");
                    Console.Write("Your response: ");
                    string response = Console.ReadLine();
                    Console.Write("Rate your mood (1-5): ");
                    int mood = int.Parse(Console.ReadLine());
                    string date = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
                    journal.AddEntry(new Entry(date, prompt, response, mood));
                    Console.WriteLine(encouragements[rand.Next(encouragements.Length)]);
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    Console.Write("Enter filename to save (e.g., journal.csv): ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveToFile(saveFilename);
                    break;
                case "4":
                    Console.Write("Enter filename to load (e.g., journal.csv): ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadFromFile(loadFilename);
                    break;
                case "5":
                    Console.Write("Enter a keyword to search for: ");
                    string keyword = Console.ReadLine();
                    journal.SearchEntries(keyword);
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}