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
        List<DiscussionLike> likes = new List<DiscussionLike>();

        public DiscussionMemory()
        {
            var discussionId1 = 3;
            var discussionId2 = 23;

            var submitter1 = new User() { UserId = 4, Email = "charlie@hotmail.com", Username = "Charlie" };
            discussions.Add(new Discussion() { DiscussionId = discussionId1, Submitter = submitter1, Title = "Dit is een discusion", Content = "Dit is de content", Likes = likes.Where(l => l.DiscussionId == discussionId1).Count(), PostDT = Convert.ToDateTime("2017-05-31 23:12:38") });
            var submitter2 = new User() { UserId = 11, Email = "jeff@hotmail.com", Username = "MyNameIsJeff" };            
            discussions.Add(new Discussion() { DiscussionId = discussionId2, Submitter = submitter2, Title = "This is so sad.", Content = "Can we get 50 likes?", Likes = likes.Where(l => l.DiscussionId == discussionId2).Count(), PostDT = Convert.ToDateTime("2017-03-21 21:14:54") });
        }

        public bool Add(Discussion discussion)
        {
            discussion = GetNecessaryDiscussionData(discussion);

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
            var discussionToEdit = discussions.Find(d => d.DiscussionId == discussion.DiscussionId);

            discussionToEdit.Locked = discussion.Locked;

            return true;
        }

        Random rnd = new Random();

        private Discussion GetNecessaryDiscussionData(Discussion discussion)
        {
            if (discussion.DiscussionId < 0)
            {
                var unique = false;

                while (!unique)
                {
                    discussion.DiscussionId = rnd.Next(1, 100);

                    if (discussions.Exists(d => d.DiscussionId != discussion.DiscussionId))
                    {
                        unique = true;
                    }
                }
            }

            UserMemory um = new UserMemory();

            discussion.Submitter = um.GetSingle(discussion.Submitter.UserId);

            discussion.PostDT = DateTime.Now;

            return discussion;
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
            if (likes.Exists(l => l.UserId == userId && l.DiscussionId == discussionId))
            {
                likes.RemoveAll(l => l.UserId == userId && l.DiscussionId == discussionId);
            }
            else
            {
                likes.Add(new DiscussionLike() { UserId = userId, DiscussionId = discussionId });
            }

            return true;
        }
    }  
}
