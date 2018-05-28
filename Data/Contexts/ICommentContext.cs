using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contexts
{
    interface ICommentContext
    {
        bool Add(Comment comment);

        List<Comment> GetComments(int discussionId);

        bool LikeUnlike(int userId, int commentId);
    }
}
