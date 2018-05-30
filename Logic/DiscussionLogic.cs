using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class DiscussionLogic
    {
        DiscussionRepository repo = new DiscussionRepository(StorageType.Database);

        public List<Discussion> GetAllDiscussions() => repo.GetDiscussions();

        public List<Discussion> FilterDiscussions(string filterInput) => repo.GetDiscussions(filterInput);

        public Discussion GetDiscussion(int discussionId) => repo.GetDiscussionById(discussionId);

        public bool LikeUnlike(int userId, int discussionId) => repo.LikeUnlike(userId, discussionId);

        public bool LockUnlock(int discussionId)
        {
            Discussion discussion = repo.GetDiscussionById(discussionId);

            if (discussion.Locked == true)
            {
                discussion.Locked = false;
            }
            else
            {
                discussion.Locked = true;
            }

            bool edited = repo.Edit(discussion);

            return edited;
        }

        public bool Create(Discussion discussion)
        {
            // Checkt of de vereiste inputvelden ingevuld zijn
            if (String.IsNullOrEmpty(discussion.Title) || String.IsNullOrEmpty(discussion.Content))
                return false;

            bool created = repo.Add(discussion);

            return created;
        }

        public bool Edit(Discussion discussion)
        {
            // Checkt of de vereiste inputvelden ingevuld zijn
            if (String.IsNullOrEmpty(discussion.Title) || String.IsNullOrEmpty(discussion.Content))
                return false;

            bool edited = repo.Edit(discussion);

            return edited;
        }

        public bool Delete(int discussionId) => repo.Delete(discussionId);
    }
}
