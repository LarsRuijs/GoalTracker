using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Comment : Post
    {
        public int CommentId { get; set; }
        public int DiscussionId { get; set; }
        public bool Hidden { get; set; }

        public Comment(int commentId, User submitter, string content, DateTime postDT, int likes, bool hidden)
        {
            CommentId = commentId;
            Submitter = submitter;
            Content = content;
            PostDT = postDT;
            Likes = likes;
            Hidden = hidden;
        }

        public Comment() { }
    }
}
