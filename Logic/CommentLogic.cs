using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class CommentLogic
    {
        CommentRepository repo = new CommentRepository(StorageType.Database);

        public List<Comment> GetAllByDiscussionId(int discussionId) => repo.GetComments(discussionId);

        public bool LikeUnlike(int userId, int commentId) => repo.LikeUnlike(userId, commentId);

        public bool Add(int userId, string content)
        {
            if (String.IsNullOrEmpty(content))
                return false;

            var submitter = new User()
            {
                UserId = userId
            };

            var comment = new Comment()
            {
                Submitter = submitter,
                Content = content
            };

            bool response = repo.Add(comment);

            return response;
        }

        public bool HideUnhide(int commentId)
        {
            Comment comment = repo.GetComment(commentId);

            if (comment.Hidden == true)
            {
                comment.Hidden = false;
            }
            else
            {
                comment.Hidden = true;
            }

            bool edited = repo.Edit(comment);

            return edited;
        }
    }
}
