using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> entries;
    private string password;

    public Journal()
    {
        entries = new List<Entry>();
        password = "journal123"; // Default password
    }

    public void AddEntry(Entry entry)
    {
        entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                string escapedPrompt = entry.Prompt.Replace("\"", "\"\"");
                string escapedResponse = entry.Response.Replace("\"", "\"\"");
                outputFile.WriteLine($"\"{entry.Date}\",\"{escapedPrompt}\",\"{escapedResponse}\",{entry.Mood}");
            }
        }
        Console.WriteLine("Journal saved successfully!");

        // Create a backup
        CreateBackup(filename);
    }

    public void LoadFromFile(string filename)
    {
        entries.Clear();
        string[] lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            string[] parts = line.Split(new[] { "\",\"" }, StringSplitOptions.None);
            if (parts.Length == 4)
            {
                string date = parts[0].Trim('"');
                string prompt = parts[1].Trim('"');
                string response = parts[2].Trim('"');
                int mood = int.Parse(parts[3].Trim('"'));
                entries.Add(new Entry(date, prompt, response, mood));
            }
        }
        Console.WriteLine("Journal loaded successfully!");
    }

    public void SearchEntries(string keyword)
    {
        Console.WriteLine($"Searching for entries containing: {keyword}");
        bool found = false;
        foreach (var entry in entries)
        {
            if (entry.Response.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(entry);
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("No matching entries found.");
        }
    }

    private void CreateBackup(string filename)
    {
        string backupFolder = "Backups";
        if (!Directory.Exists(backupFolder))
        {
            Directory.CreateDirectory(backupFolder);
        }
        string backupFile = Path.Combine(backupFolder, $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
        File.Copy(filename, backupFile, true);
        Console.WriteLine($"Backup created: {backupFile}");
    }

    public bool CheckPassword(string inputPassword)
    {
        return inputPassword == password;
    }
}
