class Program
{
    static void Main(string[] args)
    {
        // Example scripture: John 3:16
        Reference reference = new Reference("John", 3, 16);
        string text = "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.";
        Scripture scripture = new Scripture(reference, text);

        while (true)
        {
            scripture.Display();

            // Display progress
            int visibleWords = scripture.GetVisibleWordCount();
            Console.WriteLine($"\nWords remaining: {visibleWords}");
            ProvideEncouragement(visibleWords);

            // Prompt user
            Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords(3); // Hide 3 words at a time

            if (scripture.IsCompletelyHidden())
            {
                scripture.Display();
                Console.WriteLine("\nCongratulations! You've memorized the entire scripture!");
                break;
            }
        }
    }

    static void ProvideEncouragement(int visibleWords)
    {
        if (visibleWords <= 5)
        {
            Console.WriteLine("You're almost there! Keep going!");
        }
        else if (visibleWords <= 10)
        {
            Console.WriteLine("Great job! You're making progress!");
        }
        else
        {
            Console.WriteLine("Keep it up! You can do it!");
        }
    }
}