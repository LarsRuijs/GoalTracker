using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contexts
{
    interface ICommentContext
    {
        bool Add(Comment comment);

        bool Edit(Comment comment);

        List<Comment> GetComments(int discussionId);

        Comment GetComment(int commentId);

        bool LikeUnlike(int userId, int commentId);
    }
}
