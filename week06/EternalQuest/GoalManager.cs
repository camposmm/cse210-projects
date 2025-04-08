using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private readonly List<Goal> _goals;
    private int _score;
    private int _level;
    private int _streak;
    private DateTime _lastRecordedDate;
    private readonly Dictionary<int, string> _achievements;
    private readonly Dictionary<string, bool> _unlockedAchievements;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _level = 1;
        _streak = 0;
        _lastRecordedDate = DateTime.MinValue;
        _achievements = new Dictionary<int, string>
        {
            {500, "Novice Goal Setter"},
            {1000, "Goal Achiever"},
            {2500, "Master Planner"},
            {5000, "Legendary Champion"}
        };
        _unlockedAchievements = new Dictionary<string, bool>();
    }

    public void Start()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nWelcome to Eternal Quest - Your Personal Goal Tracker!");
        Console.ResetColor();
        
        while (true)
        {
            UpdateLevel();
            CheckAchievements();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nLevel: {_level} | Score: {_score} | Streak: {_streak} days");
            Console.ResetColor();
            
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. View Achievements");
            Console.WriteLine("7. Daily Challenge");
            Console.WriteLine("8. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoalDetails();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    DisplayAchievements();
                    break;
                case "7":
                    DailyChallenge();
                    break;
                case "8":
                    Console.WriteLine("Goodbye! Keep pursuing your eternal quest!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void UpdateLevel()
    {
        _level = _score / 1000 + 1;
    }

    private void CheckAchievements()
    {
        foreach (var achievement in _achievements)
        {
            if (_score >= achievement.Key && !_unlockedAchievements.ContainsKey(achievement.Value))
            {
                _unlockedAchievements[achievement.Value] = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n★ Achievement Unlocked: {achievement.Value} ★");
                Console.ResetColor();
            }
        }
    }

    private void DisplayAchievements()
    {
        Console.WriteLine("\nYour Achievements:");
        if (_unlockedAchievements.Count == 0)
        {
            Console.WriteLine("No achievements yet. Keep working on your goals!");
        }
        else
        {
            foreach (var achievement in _unlockedAchievements)
            {
                Console.WriteLine($"✓ {achievement.Key}");
            }
        }
    }

    private void DailyChallenge()
    {
        Console.WriteLine("\nDaily Challenge:");
        Console.WriteLine("Complete any goal today to maintain your streak!");
        Console.WriteLine($"Current streak: {_streak} days");
        Console.WriteLine($"Bonus points available: {_streak * 10}");
    }

    public void CreateGoal()
    {
        Console.WriteLine("\nThe types of Goals are:");
        Console.WriteLine("1. Simple Goal (one-time completion)");
        Console.WriteLine("2. Eternal Goal (repeating)");
        Console.WriteLine("3. Checklist Goal (requires multiple completions)");
        Console.Write("Which type of goal would you like to create? ");
        string typeChoice = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();

        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine());

        switch (typeChoice)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, description, points));
                break;
            case "2":
                _goals.Add(new EternalGoal(name, description, points));
                break;
            case "3":
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("What is the bonus for accomplishing it that many times? ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }

        Console.WriteLine("Goal created successfully!");
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals available to record.");
            return;
        }

        ListGoalNames();
        Console.Write("Which goal did you accomplish? ");
        if (!int.TryParse(Console.ReadLine(), out int goalNumber) || goalNumber < 1 || goalNumber > _goals.Count)
        {
            Console.WriteLine("Invalid goal selection.");
            return;
        }

        Goal goal = _goals[goalNumber - 1];
        goal.RecordEvent();
        
        // Update streak if recording on a new day
        DateTime today = DateTime.Today;
        if (_lastRecordedDate != today)
        {
            if (_lastRecordedDate == today.AddDays(-1))
            {
                _streak++;
            }
            else if (_lastRecordedDate != DateTime.MinValue)
            {
                _streak = 1;
            }
            _lastRecordedDate = today;
        }

        int pointsEarned = goal.Points;
        string message = $"Congratulations! You earned {pointsEarned} points!";

        if (goal is ChecklistGoal checklistGoal && checklistGoal.IsComplete())
        {
            pointsEarned += checklistGoal.Bonus;
            message = $"Congratulations! You earned {goal.Points + checklistGoal.Bonus} points (including bonus)!";
        }

        // Add streak bonus
        if (_streak > 0)
        {
            int streakBonus = _streak * 10;
            pointsEarned += streakBonus;
            message += $"\n+{streakBonus} bonus points for your {_streak}-day streak!";
        }

        _score += pointsEarned;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void SaveGoals()
    {
        Console.Write("Enter the filename to save goals: ");
        string filename = Console.ReadLine();

        try
        {
            using StreamWriter writer = new StreamWriter(filename);
            writer.WriteLine(_score);
            writer.WriteLine(_streak);
            writer.WriteLine(_lastRecordedDate.ToString("yyyy-MM-dd"));
            
            foreach (Goal goal in _goals)
            {
                writer.WriteLine(goal.GetStringRepresentation());
            }
            
            Console.WriteLine("Goals saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }

    public void LoadGoals()
    {
        Console.Write("Enter the filename to load goals: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);
            _goals.Clear();
            _score = int.Parse(lines[0]);
            _streak = int.Parse(lines[1]);
            _lastRecordedDate = DateTime.Parse(lines[2]);

            for (int i = 3; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(':');
                string type = parts[0];
                string[] details = parts[1].Split(',');

                switch (type)
                {
                    case "SimpleGoal":
                        var simpleGoal = new SimpleGoal(details[0], details[1], int.Parse(details[2]));
                        if (bool.Parse(details[3])) simpleGoal.RecordEvent();
                        _goals.Add(simpleGoal);
                        break;
                    case "EternalGoal":
                        var eternalGoal = new EternalGoal(details[0], details[1], int.Parse(details[2]));
                        int timesRecorded = int.Parse(details[3]);
                        for (int j = 0; j < timesRecorded; j++) eternalGoal.RecordEvent();
                        _goals.Add(eternalGoal);
                        break;
                    case "ChecklistGoal":
                        var checklistGoal = new ChecklistGoal(
                            details[0], 
                            details[1], 
                            int.Parse(details[2]), 
                            int.Parse(details[4]), 
                            int.Parse(details[3]));
                        int completed = int.Parse(details[5]);
                        for (int j = 0; j < completed; j++) checklistGoal.RecordEvent();
                        _goals.Add(checklistGoal);
                        break;
                }
            }
            
            Console.WriteLine("Goals loaded successfully!");
            CheckAchievements(); // Check if loaded score unlocks any achievements
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading goals: {ex.Message}");
        }
    }

    private void ListGoalNames()
    {
        Console.WriteLine("\nYour Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{i + 1}. ");
            Console.ResetColor();
            Console.WriteLine(_goals[i].ShortName);
        }
    }

    public void ListGoalDetails()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals have been created yet.");
            return;
        }

        Console.WriteLine("\nYour Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{i + 1}. ");
            Console.ResetColor();
            
            if (_goals[i].IsComplete())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(_goals[i].GetDetailsString());
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(_goals[i].GetDetailsString());
            }
        }
    }
}