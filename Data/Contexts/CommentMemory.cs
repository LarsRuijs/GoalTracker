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
        List<CommentLike> likes = new List<CommentLike>();

        public CommentMemory()
        {
            var commentId1 = 16;
            var commentId2 = 18;

            var submitter1 = new User() { UserId = 55, Email = "sheep@gmail.com", Username = "BeepBeep" };
            comments.Add(new Comment() { CommentId = commentId1, DiscussionId = 3, Submitter = submitter1, Content = "What?", Likes = likes.Where(l => l.CommentId == commentId1).Count(), PostDT = Convert.ToDateTime("2018-03-03 09:23:58") });
            var submitter2 = new User() { UserId = 56, Email = "penguin@gmail.com", Username = "NOOTNOOT" };
            comments.Add(new Comment() { CommentId = commentId2, DiscussionId = 3, Submitter = submitter2, Content = "This is what...", Likes = likes.Where(l => l.CommentId == commentId2).Count(), PostDT = Convert.ToDateTime("2018-03-03 09:44:22") });
        }

        public bool Add(Comment comment)
        {
            comments.Add(comment);

            comment = CreateNecessaryCommentData(comment);

            return true;
        }

        Random rnd = new Random();

        private Comment CreateNecessaryCommentData(Comment comment)
        {
            if (comment.CommentId < 0)
            {
                var unique = false;

                while (!unique)
                {
                    comment.CommentId = rnd.Next(1, 100);

                    if (comments.Exists(d => d.CommentId != comment.CommentId))
                    {
                        unique = true;
                    }
                }
            }

            UserMemory um = new UserMemory();

            comment.Submitter = um.GetSingle(comment.Submitter.UserId);

            comment.PostDT = DateTime.Now;

            return comment;
        }

        public bool Edit(Comment comment)
        {
            var commentToEdit = comments.Find(d => d.CommentId == comment.CommentId);

            commentToEdit.Hidden = comment.Hidden;

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
            if (likes.Exists(l => l.UserId == userId && l.CommentId == commentId))
            {
                likes.RemoveAll(l => l.UserId == userId && l.CommentId == commentId);
            }
            else
            {
                likes.Add(new CommentLike() { UserId = userId, CommentId = commentId });
            }

            return true;
        }
    }
}
