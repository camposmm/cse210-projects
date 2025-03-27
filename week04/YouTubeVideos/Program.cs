class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video("C# Tutorial", "Programming Guru", 600);
        Video video2 = new Video("Python Basics", "Code Master", 450);
        Video video3 = new Video("Web Development", "Dev Expert", 720);

        // Add comments to video1
        video1.AddComment(new Comment("User1", "Great tutorial!"));
        video1.AddComment(new Comment("User2", "Very helpful, thanks!"));
        video1.AddComment(new Comment("User3", "I learned a lot."));

        // Add comments to video2
        video2.AddComment(new Comment("User4", "Clear explanations."));
        video2.AddComment(new Comment("User5", "Loved the examples."));

        // Add comments to video3
        video3.AddComment(new Comment("User6", "Awesome content!"));
        video3.AddComment(new Comment("User7", "Can you do more on this topic?"));
        video3.AddComment(new Comment("User8", "Subscribed!"));

        // Store videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display video details
        foreach (var video in videos)
        {
            video.DisplayVideoDetails();
        }
    }
}