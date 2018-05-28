using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoalTracker.ViewModels;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Models;

namespace GoalTracker.Controllers
{
    [Authorize]
    public class DiscussionsController : Controller
    {
        DiscussionLogic logic = new DiscussionLogic();
        CommentLogic cLogic = new CommentLogic();

        public IActionResult Index()
        {
            var model = new DiscussionsIndexViewModel();

            model.Discussions = logic.GetAllDiscussions();

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(DiscussionsIndexViewModel model)
        {
            model.Discussions = logic.FilterDiscussions(model.FilterInput);

            return View(model);
        }

        [HttpPost]
        public IActionResult LockDiscussion(int DiscussionId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Single(int DiscussionId)
        {
            var model = new SingleDiscussionViewModel();

            model.Discussion = logic.GetDiscussion(DiscussionId);
            model.Discussion.Comments = cLogic.GetComments(DiscussionId);

            int id = Convert.ToInt32(User.Claims.Where(c => c.Type == "Id")
                .Select(c => c.Value).SingleOrDefault());

            model.UserId = id;

            return View(model);
        }

        [HttpPost]
        public IActionResult PostComment(SingleDiscussionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var submitter = new User() { UserId = model.UserId };

            var comment = new Comment()
            {
                DiscussionId = model.DiscussionId,
                Submitter = submitter,
                Content = model.Entry             
            };

            cLogic.Create(comment);

            return RedirectToAction("Single", new { DiscussionId = model.DiscussionId });
        }

        [HttpPost]
        public IActionResult LikeUnlikeDiscussion(int DiscussionId)
        {
            int id = Convert.ToInt32(User.Claims.Where(c => c.Type == "Id")
                .Select(c => c.Value).SingleOrDefault());

            var feedback = logic.LikeUnlike(id, DiscussionId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult LikeUnlikeComment(int CommentId, int DiscussionId)
        {
            int id = Convert.ToInt32(User.Claims.Where(c => c.Type == "Id")
                .Select(c => c.Value).SingleOrDefault());

            var feedback = cLogic.LikeUnlike(id, CommentId);

            return RedirectToAction("Single", new { DiscussionId = DiscussionId });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDiscussionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int id = Convert.ToInt32(User.Claims.Where(c => c.Type == "Id")
                .Select(c => c.Value).SingleOrDefault());

            var submitter = new User();
            submitter.UserId = id;

            var discussion = new Discussion()
            {
                Submitter = submitter,
                Title = model.Title,
                Content = model.Content
            };

            bool created = logic.Create(discussion);

            if (!created)
            {
                ModelState.AddModelError("", "Creating of the discussion failed.");

                return View(model);
            }

            return RedirectToAction("Index"); ;
        }
    }
}