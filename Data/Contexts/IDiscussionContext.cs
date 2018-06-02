using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data.Contexts
{
    interface IDiscussionContext
    {
        Discussion GetSingle(int discussionId);

        List<Discussion> GetAll(string filterInput = "");

        bool Add(Discussion discussion);

        bool Edit(Discussion discussion);

        bool Delete(int DiscussionId);

        bool LikeUnlike(int userId, int discussionId);
    }
}
