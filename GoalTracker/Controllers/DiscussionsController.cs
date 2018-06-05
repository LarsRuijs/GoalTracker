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

            model.Discussions = logic.GetAll();

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(DiscussionsIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Discussions = logic.GetAllByFilter(model.FilterInput);

            return View(model);
        }

        [HttpPost]
        public IActionResult LockUnlockDiscussion(int DiscussionId)
        {
            var success = logic.LockUnlock(DiscussionId);

            return RedirectToAction("Index", new { DiscussionId });
        }

        [HttpPost]
        public IActionResult HideUnhideComment(int CommentId, int DiscussionId)
        {
            var success = cLogic.HideUnhide(CommentId);

            return RedirectToAction("Single", new { DiscussionId });
        }

        [HttpPost]
        public IActionResult DeleteDiscussion(int DiscussionId)
        {
            var deleted = logic.Delete(DiscussionId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Single(int DiscussionId)
        {
            var model = new SingleDiscussionViewModel();

            model.Discussion = logic.GetSingle(DiscussionId);
            model.Discussion.Comments = cLogic.GetAllByDiscussionId(DiscussionId);

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

            int id = Convert.ToInt32(User.Claims.Where(c => c.Type == "Id")
                .Select(c => c.Value).SingleOrDefault());

            cLogic.Add(id, model.Entry);

            return RedirectToAction("Single", new { model.DiscussionId });
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

            return RedirectToAction("Single", new { DiscussionId });
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

            bool created = logic.Add(id, model.Title, model.Content);

            if (!created)
            {
                ModelState.AddModelError("", "Creating of the discussion failed.");

                return View(model);
            }

            return RedirectToAction("Index"); ;
        }
    }
}