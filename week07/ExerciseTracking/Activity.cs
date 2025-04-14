using System;

namespace ExerciseTracking
{
    public abstract class Activity
    {
        private DateTime date;
        private int durationMinutes;

        protected Activity(DateTime date, int durationMinutes)
        {
            this.date = date;
            this.durationMinutes = durationMinutes;
        }

        public DateTime Date => date;
        public int DurationMinutes => durationMinutes;

        public abstract double GetDistance();
        public abstract double GetSpeed();
        public abstract double GetPace();

        public virtual string GetSummary()
        {
            return $"{Date.ToString("dd MMM yyyy")} {GetType().Name} ({DurationMinutes} min) - " +
                   $"Distance: {GetDistance():F1} miles, Speed: {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
        }
    }
}