using Data.Contexts;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class CommentRepository
    {
        private ICommentContext context;

        public CommentRepository(StorageType st)
        {
            switch (st)
            {
                case StorageType.Memory: throw new NotImplementedException();
                case StorageType.Database: context = new CommentSql(); break;
            }
        }

        public bool Add(Comment comment) => context.Add(comment);

        public List<Comment> GetComments(int discussionId) => context.GetComments(discussionId);

        public bool LikeUnlike(int userId, int commentId) => context.LikeUnlike(userId, commentId);
    }
}
