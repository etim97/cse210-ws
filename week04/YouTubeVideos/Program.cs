using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Create 3-4 videos
        List<Video> videos = new List<Video>
        {
            new Video("How to Code in C#", "TechGuru", 600),
            new Video("Top 10 AI Tools", "AI Insider", 420),
            new Video("The Future of Quantum Computing", "Science Daily", 900)
        };

        // Add comments to each video
        videos[0].AddComment(new Comment("Wisdom" , "This was super helpful!"));
        videos[0].AddComment(new Comment("Samuel", "Great explanation, thanks!"));
        videos[0].AddComment(new Comment("Charlie", "I finally understand C# now."));

        videos[1].AddComment(new Comment("Daniel", "AI is taking over the world!"));
        videos[1].AddComment(new Comment("Eve", "Loved this breakdown of tools!"));
        videos[1].AddComment(new Comment("Frank", "This was really insightful."));

        videos[2].AddComment(new Comment("Grace", "Quantum computing is fascinating."));
        videos[2].AddComment(new Comment("Etim", "Can’t wait for more research in this field!"));
        videos[2].AddComment(new Comment("Ivy", "Wow, this blew my mind."));

        // Iterate through and display all video details
        foreach (var video in videos)
        {
            video.DisplayInfo();
            Console.WriteLine(new string('-', 40)); // Divider for readability
        }
    }
}


class Video
{
    private string _title;
    private string _author;
    private int _length; // in seconds
    private List<Comment> _comments;

    public Video(string title, string author, int length)
    {
        _title = title;
        _author = author;
        _length = length;
        _comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return _comments.Count;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"📹 Title: {_title}");
        Console.WriteLine($"👤 Author: {_author}");
        Console.WriteLine($"⏳ Length: {_length} seconds");
        Console.WriteLine($"💬 Comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        
        foreach (var comment in _comments)
        {
            Console.WriteLine($" - {comment.GetCommentDetails()}");
        }
    }
}
class Comment
{
    private string _name;
    private string _text;

    public Comment(string name, string text)
    {
        _name = name;
        _text = text;
    }

    public string GetCommentDetails()
    {
        return $"{_name}: \"{_text}\"";
    }
}
