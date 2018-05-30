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

        public bool Create(Comment comment) => repo.Add(comment);

        public List<Comment> GetComments(int discussionId) => repo.GetComments(discussionId);

        public bool LikeUnlike(int userId, int commentId) => repo.LikeUnlike(userId, commentId);

        public bool LockUnlock(int commentId)
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
