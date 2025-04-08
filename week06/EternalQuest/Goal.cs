public abstract class Goal
{
    protected string _shortName;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    public string ShortName => _shortName;
    public string Description => _description;
    public int Points => _points;

    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public virtual string GetDetailsString()
    {
        return $"[{(IsComplete() ? "X" : " ")}] {_shortName}: {_description}";
    }
    public abstract string GetStringRepresentation();
    
    // Helper method for progress bars
    protected string GetProgressBar(int current, int target, int width = 10)
    {
        int progress = (int)Math.Round((double)current / target * width);
        progress = Math.Min(progress, width);
        return $"[{new string('=', progress)}{new string(' ', width - progress)}]";
    }
}