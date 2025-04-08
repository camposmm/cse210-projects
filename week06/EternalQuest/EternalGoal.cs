public class EternalGoal : Goal
{
    private int _timesRecorded;

    public EternalGoal(string name, string description, int points) 
        : base(name, description, points)
    {
        _timesRecorded = 0;
    }

    public override void RecordEvent()
    {
        _timesRecorded++;
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetDetailsString()
    {
        return $"{base.GetDetailsString()} (Completed {_timesRecorded} times)";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{_shortName},{_description},{_points},{_timesRecorded}";
    }
}