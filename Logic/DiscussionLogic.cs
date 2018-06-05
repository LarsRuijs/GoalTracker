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

        public List<Discussion> GetAll() => repo.GetAll();

        public List<Discussion> GetAllByFilter(string filterInput) => repo.GetAll(filterInput);

        public Discussion GetSingle(int discussionId) => repo.GetSingle(discussionId);

        public bool LikeUnlike(int userId, int discussionId) => repo.LikeUnlike(userId, discussionId);

        public bool Delete(int discussionId) => repo.Delete(discussionId);

        public bool LockUnlock(int discussionId)
        {
            Discussion discussion = repo.GetSingle(discussionId);

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

        public bool Add(int userId, string title, string content)
        {
            if (!CheckDiscussionInput(title, content))
                return false;

            var submitter = new User()
            {
                UserId = userId
            };

            var discussion = new Discussion()
            {
                Submitter = submitter,
                Title = title,
                Content = content
            };

            bool response = repo.Add(discussion);

            return response;
        }

        public bool Edit(Discussion discussion)
        {
            if (!CheckDiscussionInput(discussion.Title, discussion.Content))
                return false;

            bool response = repo.Edit(discussion);

            return response;
        }

        private bool CheckDiscussionInput(string title, string content)
        {
            if (String.IsNullOrEmpty(title) || String.IsNullOrEmpty(content))
                return false;

            return true;
        }
    }
}
