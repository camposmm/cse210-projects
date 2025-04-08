public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private readonly int _target;
    private readonly int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus) 
        : base(name, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }

    public int AmountCompleted => _amountCompleted;
    public int Target => _target;
    public int Bonus => _bonus;

    public override void RecordEvent()
    {
        _amountCompleted++;
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        string baseString = base.GetDetailsString();
        string progress = $"{_amountCompleted}/{_target}";
        string progressBar = GetProgressBar(_amountCompleted, _target);
        return $"{baseString} {progressBar} {progress} (Bonus: {_bonus})";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{_shortName},{_description},{_points},{_bonus},{_target},{_amountCompleted}";
    }
}