using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> entries;

    public Journal()
    {
        entries = new List<Entry>();
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
                // Escape quotes and commas in the content
                string escapedPrompt = entry.Prompt.Replace("\"", "\"\"");
                string escapedResponse = entry.Response.Replace("\"", "\"\"");
                outputFile.WriteLine($"\"{entry.Date}\",\"{escapedPrompt}\",\"{escapedResponse}\"");
            }
        }
        Console.WriteLine("Journal saved successfully!");
    }

    public void LoadFromFile(string filename)
    {
        entries.Clear();
        string[] lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            string[] parts = line.Split(new[] { "\",\"" }, StringSplitOptions.None);
            if (parts.Length == 3)
            {
                string date = parts[0].Trim('"');
                string prompt = parts[1].Trim('"');
                string response = parts[2].Trim('"');
                entries.Add(new Entry(date, prompt, response));
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
}
