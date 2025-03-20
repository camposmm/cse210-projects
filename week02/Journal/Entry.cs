public class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }
    public int Mood { get; set; } // New property for mood tracking

    public Entry(string date, string prompt, string response, int mood)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
        Mood = mood;
    }

    public override string ToString()
    {
        return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\nMood: {Mood}/5\nWord Count: {WordCount()}\n";
    }

    public int WordCount()
    {
        return Response.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }
}