using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class CommentLogic
    {
        CommentRepository repo = new CommentRepository(StorageType.Database);

        /// <summary>
        /// Gets all comments that belong to a discussion.
        /// </summary>
        /// <param name="discussionId"></param>
        /// <returns></returns>
        public List<Comment> GetAllByDiscussionId(int discussionId) => repo.GetComments(discussionId);

        /// <summary>
        /// Likes or unlikes the comment.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public bool LikeUnlike(int userId, int commentId) => repo.LikeUnlike(userId, commentId);

        /// <summary>
        /// Adds and checks the newly submitted comment.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool Add(int userId, string content)
        {
            if (String.IsNullOrEmpty(content))
                return false;

            var submitter = new User()
            {
                UserId = userId
            };

            var comment = new Comment()
            {
                Submitter = submitter,
                Content = content
            };

            bool response = repo.Add(comment);

            return response;
        }

        /// <summary>
        /// Hides or unhides the comment.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public bool HideUnhide(int commentId)
        {
            Comment comment = repo.GetComment(commentId);

            if (comment.Hidden == true)
            {
                comment.Hidden = false;
            }
            else
            {
                comment.Hidden = true;
            }

            bool edited = repo.Edit(comment);

            return edited;
        }
    }
}
