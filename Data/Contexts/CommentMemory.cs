using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Data.Contexts
{
    class CommentMemory : ICommentContext
    {
        List<Comment> comments = new List<Comment>();

        public CommentMemory()
        {
            var submitter1 = new User() { UserId = 55, Email = "sheep@gmail.com", Username = "BeepBeep" };
            comments.Add(new Comment() { CommentId = 16, DiscussionId = 3, Submitter = submitter1, Content = "What?", Likes = 0, PostDT = Convert.ToDateTime("2018-03-03 09:23:58") });
            var submitter2 = new User() { UserId = 56, Email = "penguin@gmail.com", Username = "NOOTNOOT" };
            comments.Add(new Comment() { CommentId = 18, DiscussionId = 3, Submitter = submitter2, Content = "This is what...", Likes = 113, PostDT = Convert.ToDateTime("2018-03-03 09:44:22") });
        }

        public bool Add(Comment comment)
        {
            comments.Add(comment);

            return true;
        }

        public bool Edit(Comment comment)
        {
            try
            {
                comments.RemoveAll(c => c.CommentId == comment.CommentId);
            }
            catch
            {
                return false;
            }

            comments.Add(comment);

            return true;
        }

        public Comment GetComment(int commentId) => comments.FirstOrDefault(c => c.CommentId == commentId);

        public List<Comment> GetComments(int discussionId)
        {
            var list = new List<Comment>();

            foreach (var comment in comments.Where(c => c.DiscussionId == discussionId))
            {
                list.Add(comment);
            }

            list = list.OrderByDescending(c => c.PostDT).ToList();

            return list;
        }

        public bool LikeUnlike(int userId, int commentId)
        {
            throw new NotImplementedException();
        }
    }
}
