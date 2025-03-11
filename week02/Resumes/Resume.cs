using System;
using System.Collections.Generic;

public class Resume
{
    // Member variables
    public string _personName;
    public List<Job> _jobs;

    // Constructor
    public Resume(string personName)
    {
        _personName = personName;
        _jobs = new List<Job>();
    }

    // Method to display resume details
    public void Display()
    {
        Console.WriteLine($"Name: {_personName}");
        Console.WriteLine("Jobs:");
        foreach (Job job in _jobs)
        {
            job.Display();
        }
    }
}