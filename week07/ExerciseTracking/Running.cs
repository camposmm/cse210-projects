namespace ExerciseTracking
{
    public class Running : Activity
    {
        private double distance; // in miles

        public Running(DateTime date, int durationMinutes, double distance)
            : base(date, durationMinutes)
        {
            this.distance = distance;
        }

        public override double GetDistance() => distance;
        public override double GetSpeed() => (distance / DurationMinutes) * 60;
        public override double GetPace() => DurationMinutes / distance;
    }
}