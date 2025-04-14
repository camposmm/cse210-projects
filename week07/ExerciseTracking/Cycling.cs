namespace ExerciseTracking
{
    public class Cycling : Activity
    {
        private double speed; // in mph

        public Cycling(DateTime date, int durationMinutes, double speed)
            : base(date, durationMinutes)
        {
            this.speed = speed;
        }

        public override double GetDistance() => (speed * DurationMinutes) / 60;
        public override double GetSpeed() => speed;
        public override double GetPace() => 60 / speed;
    }
}