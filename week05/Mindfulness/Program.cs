using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    // Base Activity class
    public abstract class Activity
    {
        protected string _name;
        protected string _description;
        protected int _duration;

        public Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public void DisplayStartingMessage()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to the {_name} Activity.");
            Console.WriteLine();
            Console.WriteLine(_description);
            Console.WriteLine();
            Console.Write("How long, in seconds, would you like for your session? ");
            _duration = int.Parse(Console.ReadLine());
            
            Console.WriteLine();
            Console.WriteLine("Get ready...");
            ShowSpinner(3);
        }

        public void DisplayEndingMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!!");
            ShowSpinner(3);
            Console.WriteLine();
            Console.WriteLine($"You have completed another {_duration} seconds of the {_name} Activity.");
            ShowSpinner(3);
        }

        public void ShowSpinner(int seconds)
        {
            for (int i = 0; i < seconds * 2; i++)
            {
                Console.Write("|");
                Thread.Sleep(250);
                Console.Write("\b \b");
                Console.Write("/");
                Thread.Sleep(250);
                Console.Write("\b \b");
                Console.Write("-");
                Thread.Sleep(250);
                Console.Write("\b \b");
                Console.Write("\\");
                Thread.Sleep(250);
                Console.Write("\b \b");
            }
        }

        public void ShowCountDown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }

        public abstract void Run();
    }

    // Breathing Activity
    public class BreathingActivity : Activity
    {
        public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        {
        }

        public override void Run()
        {
            DisplayStartingMessage();
            
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(_duration);
            
            while (DateTime.Now < endTime)
            {
                Console.WriteLine();
                Console.Write("Breathe in... ");
                ShowCountDown(4);
                Console.WriteLine();
                Console.Write("Breathe out... ");
                ShowCountDown(6);
            }
            
            DisplayEndingMessage();
        }
    }

    // Reflection Activity
    public class ReflectingActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectingActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        {
        }

        public override void Run()
        {
            DisplayStartingMessage();
            
            Console.WriteLine("Consider the following prompt:");
            Console.WriteLine();
            Console.WriteLine($"--- {GetRandomPrompt()} ---");
            Console.WriteLine();
            Console.WriteLine("When you have something in mind, press enter to continue.");
            Console.ReadLine();
            
            Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
            Console.Write("You may begin in: ");
            ShowCountDown(5);
            Console.Clear();
            
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(_duration);
            
            while (DateTime.Now < endTime)
            {
                Console.Write($"> {GetRandomQuestion()} ");
                ShowSpinner(5);
                Console.WriteLine();
            }
            
            DisplayEndingMessage();
        }

        private string GetRandomPrompt()
        {
            Random random = new Random();
            int index = random.Next(_prompts.Count);
            return _prompts[index];
        }

        private string GetRandomQuestion()
        {
            Random random = new Random();
            int index = random.Next(_questions.Count);
            return _questions[index];
        }
    }

    // Listing Activity
    public class ListingActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private int _count;

        public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
        }

        public override void Run()
        {
            DisplayStartingMessage();
            
            Console.WriteLine("List as many responses as you can to the following prompt:");
            Console.WriteLine($"--- {GetRandomPrompt()} ---");
            Console.Write("You may begin in: ");
            ShowCountDown(5);
            Console.WriteLine();
            
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(_duration);
            
            List<string> items = new List<string>();
            
            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                items.Add(Console.ReadLine());
            }
            
            _count = items.Count;
            Console.WriteLine();
            Console.WriteLine($"You listed {_count} items!");
            
            DisplayEndingMessage();
        }

        private string GetRandomPrompt()
        {
            Random random = new Random();
            int index = random.Next(_prompts.Count);
            return _prompts[index];
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program Menu");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Exit");
                Console.Write("Select an activity (1-4): ");
                
                string choice = Console.ReadLine();
                
                Activity activity = null;
                
                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        break;
                    case "2":
                        activity = new ReflectingActivity();
                        break;
                    case "3":
                        activity = new ListingActivity();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Thread.Sleep(1500);
                        continue;
                }
                
                activity.Run();
            }
        }
    }
}