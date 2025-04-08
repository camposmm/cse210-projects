/*
 * Eternal Quest Program
 * ---------------------
 * This program helps users track various types of goals and earn points for completing them.
 * 
 * Creative Enhancements:
 * 1. Added a "Level" system where users level up based on points earned
 * 2. Implemented special achievements/badges for completing certain milestones
 * 3. Added a "Streak" counter for consecutive days recording goals
 * 4. Created a "Daily Challenge" system with bonus points
 * 5. Added color coding to make the interface more visually appealing
 * 6. Implemented a progress bar for checklist goals
 */
using System;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}