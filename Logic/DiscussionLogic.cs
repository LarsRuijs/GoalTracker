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

        /// <summary>
        /// Gets all discussions.
        /// </summary>
        /// <returns></returns>
        public List<Discussion> GetAll() => repo.GetAll();

        /// <summary>
        /// Gets all filtered list of discussions.
        /// </summary>
        /// <param name="filterInput"></param>
        /// <returns></returns>
        public List<Discussion> GetAllByFilter(string filterInput) => repo.GetAll(filterInput);

        /// <summary>
        /// Gets a single discussion.
        /// </summary>
        /// <param name="discussionId"></param>
        /// <returns></returns>
        public Discussion GetSingle(int discussionId) => repo.GetSingle(discussionId);

        /// <summary>
        /// Likes or unlikes the discussion.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="discussionId"></param>
        /// <returns></returns>
        public bool LikeUnlike(int userId, int discussionId) => repo.LikeUnlike(userId, discussionId);

        /// <summary>
        /// Deletes the discussion from the data.
        /// </summary>
        /// <param name="discussionId"></param>
        /// <returns></returns>
        public bool Delete(int discussionId) => repo.Delete(discussionId);

        /// <summary>
        /// Locks or unlocks the discussion.
        /// </summary>
        /// <param name="discussionId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds and checks the newly submitted discussion.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Edits the discussion.
        /// </summary>
        /// <param name="discussion"></param>
        /// <returns></returns>
        public bool Edit(Discussion discussion)
        {
            if (!CheckDiscussionInput(discussion.Title, discussion.Content))
                return false;

            bool response = repo.Edit(discussion);

            return response;
        }

        /// <summary>
        /// Checks for unapplyable input.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private bool CheckDiscussionInput(string title, string content)
        {
            if (String.IsNullOrEmpty(title) || String.IsNullOrEmpty(content))
                return false;

            return true;
        }
    }
}
