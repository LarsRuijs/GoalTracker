using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Data.Contexts
{
    class DiscussionMemory : IDiscussionContext
    {
        List<Discussion> discussions = new List<Discussion>();

        public DiscussionMemory()
        {
            var submitter1 = new User() { UserId = 4, Email = "charlie@hotmail.com", Username = "Charlie" };
            discussions.Add(new Discussion() { DiscussionId = 3, Submitter = submitter1, Title = "Dit is een discusion", Content = "Dit is de content", Likes = 4, PostDT = Convert.ToDateTime("2017-05-31 23:12:38") });
            var submitter2 = new User() { UserId = 11, Email = "jeff@hotmail.com", Username = "MyNameIsJeff" };
            discussions.Add(new Discussion() { DiscussionId = 23, Submitter = submitter2, Title = "This is so sad.", Content = "Can we get 50 likes?", Likes = 1, PostDT = Convert.ToDateTime("2017-03-21 21:14:54") });
        }

        public bool Add(Discussion discussion)
        {
            discussions.Add(discussion);

            return true;
        }

        public bool Delete(int DiscussionId)
        {
            discussions.RemoveAll(d => d.DiscussionId == DiscussionId);

            return true;
        }

        public bool Edit(Discussion discussion)
        {
            try
            {
                discussions.RemoveAll(d => d.DiscussionId == discussion.DiscussionId);
            }
            catch
            {
                return false;
            }

            discussions.Add(discussion);

            return true;
        }

        public Discussion GetSingle(int discussionId) => discussions.FirstOrDefault(d => d.DiscussionId == discussionId);

        public List<Discussion> GetAll(string filterInput = "")
        {
            var list = new List<Discussion>();

            foreach (var discussion in discussions.Where(d =>
                (d.Submitter.Username.Contains(filterInput) ||
                d.Title.Contains(filterInput) ||
                d.Content.Contains(filterInput))))
            {
                list.Add(discussion);
            }

            list = list.OrderByDescending(d => d.PostDT).ToList();

            return list;
        }

        public bool LikeUnlike(int userId, int discussionId)
        {
            throw new NotImplementedException();
        }
    }
}
