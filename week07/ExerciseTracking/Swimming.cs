namespace ExerciseTracking
{
    public class Swimming : Activity
    {
        private int laps;
        private const double LapLengthMeters = 50;
        private const double MetersToMiles = 0.000621371;

        public Swimming(DateTime date, int durationMinutes, int laps)
            : base(date, durationMinutes)
        {
            this.laps = laps;
        }

        public override double GetDistance() => laps * LapLengthMeters * MetersToMiles;
        public override double GetSpeed() => (GetDistance() / DurationMinutes) * 60;
        public override double GetPace() => DurationMinutes / GetDistance();

        public override string GetSummary()
        {
            return $"{Date.ToString("dd MMM yyyy")} {GetType().Name} ({DurationMinutes} min) - " +
                   $"Distance: {GetDistance():F1} miles, Speed: {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile, " +
                   $"Laps: {laps}";
        }
    }
}