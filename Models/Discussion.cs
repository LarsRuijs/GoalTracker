using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Discussion : Post
    {
        public int DiscussionId { get; set; }
        public string Title { get; set; }
        public bool Locked { get; set; }
        public List<Comment> Comments { get; set; }

        public Discussion(int discussionId, User submitter, string title, string content, DateTime postDT, int likes, bool locked, List<Comment> comments)
        {
            DiscussionId = discussionId;
            Submitter = submitter;
            Title = title;
            Content = content;
            PostDT = postDT;
            Likes = likes;
            Locked = locked;
            Comments = comments;
        }

        public Discussion()
        {
            Comments = new List<Comment>();
        }
    }
}
