using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using GoalTracker.ViewModels;
using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace GoalTracker.Controllers
{
    [Authorize]
    public class GoalsController : Controller
    {
        GoalLogic logic = new GoalLogic();

        public IActionResult Index()
        {
            int id = Convert.ToInt32(User.Claims.Where(c => c.Type == "Id")
                .Select(c => c.Value).SingleOrDefault());

            var goalList = logic.GetGoalsByUserId(id);

            var model = new UserGoalsViewModel();

            model.Goals = goalList;

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int NewStatus, EditGoalViewModel model)
        {
            var goal = new Goal()
            {
                GoalId = model.GoalId,
                UserId = model.UserId,
                Title = model.Title,
                Info = model.Info,
                StartDT = model.StartDT,
                EndDT = model.EndDT,
                Progress = model.Progress,
                Status = (GoalStatus)NewStatus,
                Strikes = model.Strikes
            };

            var edited = logic.EditGoal(goal);

            if (!edited)
                throw new Exception("Editing failed.");    

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateGoalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int id = Convert.ToInt32(User.Claims.Where(c => c.Type == "Id")
                .Select(c => c.Value).SingleOrDefault());

            var goal = new Goal()
            {
                UserId = id,
                Title = model.Title,
                Info = model.Info,
                StartDT = model.StartDT,
                EndDT = model.EndDT
            };

            bool created = logic.CreateGoal(goal);

            if (!created)
            {
                ModelState.AddModelError("", "Creating of the goal failed.");

                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int goalId)
        {
            Goal goal = logic.GetGoalById(goalId);

            int id = Convert.ToInt32(User.Claims.Where(c => c.Type == "Id")
                .Select(c => c.Value).SingleOrDefault());

            if (goal.UserId != id)
                return RedirectToAction("Index");

            var model = new EditGoalViewModel()
            {
                GoalId = goal.GoalId,
                UserId = goal.UserId,
                Title = goal.Title,
                Info = goal.Info,
                StartDT = goal.StartDT,
                EndDT = goal.EndDT,
                Progress = goal.Progress,
                Status = goal.Status,
                Strikes = goal.Strikes
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditGoalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var goal = new Goal()
            {
                GoalId = model.GoalId,
                UserId = model.UserId,
                Title = model.Title,
                Info = model.Info,
                StartDT = model.StartDT,
                EndDT = model.EndDT,
                Progress = model.Progress,
                Status = model.Status,
                Strikes = model.Strikes
            };

            bool edited = logic.EditGoal(goal);

            if (!edited)
            {
                ModelState.AddModelError("", "Editing failed.");

                return View(model);
            }

            return RedirectToAction("Index");   
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UserGoals(int UserId)
        {
            var uLogic = new UserLogic();

            User user = uLogic.GetUser(UserId);

            List<Goal> goals = logic.GetGoalsByUserId(UserId);

            var model = new UserGoalsViewModel()
            {
                Username = user.Username,
                Goals = goals
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditUserGoal(int GoalId)
        {
            Goal goal = logic.GetGoalById(GoalId);

            var model = new EditGoalViewModel()
            {
                GoalId = goal.GoalId,
                UserId = goal.UserId,
                Title = goal.Title,
                Info = goal.Info,
                StartDT = goal.StartDT,
                EndDT = goal.EndDT,
                Progress = goal.Progress,
                Status = goal.Status,
                Strikes = goal.Strikes
            };

            return View(model);
        } 
        
        [HttpPost]
        public IActionResult EditUserGoal(EditGoalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var goal = new Goal()
            {
                GoalId = model.GoalId,
                UserId = model.UserId,
                Title = model.Title,
                Info = model.Info,
                StartDT = model.StartDT,
                EndDT = model.EndDT,
                Progress = model.Progress,
                Status = model.Status,
                Strikes = model.Strikes
            };

            bool edited = logic.EditGoal(goal);

            if (!edited)
            {
                ModelState.AddModelError("", "Editing failed.");

                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}