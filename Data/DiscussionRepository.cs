using Data.Contexts;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class DiscussionRepository
    {
        private IDiscussionContext context;

        public DiscussionRepository(StorageType st)
        {
            switch (st)
            {
                case StorageType.Memory: context = new DiscussionMemory(); break;
                case StorageType.Database: context = new DiscussionSql(); break;
            }
        }

        public bool Add(Discussion discussion) => context.Add(discussion);

        public bool Edit(Discussion discussion) => context.Edit(discussion);

        public bool Delete(int discussionId) => context.Delete(discussionId);

        public List<Discussion> GetDiscussions(string filterInput = "") => context.GetDiscussions(filterInput);

        public Discussion GetDiscussionById(int discussionId) => context.GetDiscussionById(discussionId);

        public bool LikeUnlike(int userId, int discussionId) => context.LikeUnlike(userId, discussionId);
    }
}
