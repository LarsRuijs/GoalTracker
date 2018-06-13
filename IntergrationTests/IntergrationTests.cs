using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;

namespace IntergrationTests
{
    [TestClass]
    public class UnitTest1
    {
        UserLogic uLogic = new UserLogic();
        GoalLogic gLogic = new GoalLogic();

        [TestMethod]
        public void TestUsers()
        {
            // User registeren
            User user = new User()
            {
                Username = "test",
                Password = "abc",
                Email = "test@gmail.com"
            };

            Assert.AreEqual("", uLogic.Register(user.Username, user.Password, user.Email));

            // User inloggen
            // Moet met username en email kunnen inloggen.
            Assert.AreEqual(user.Username, uLogic.Login(user.Username, user.Password).Username);
            Assert.AreEqual(user.Username, uLogic.Login(user.Email, user.Password).Username);

            User get = uLogic.Login(user.Username, user.Password);

            // User aanpassen
            User edit = new User()
            {
                UserId = get.UserId,
                Username = "editedUser",
                Password = "abc",
                Email = "editeduser@gmail.com"
            };

            Assert.IsTrue(uLogic.Edit(edit));

            Assert.AreEqual(edit.Username, uLogic.Login(edit.Username, edit.Password).Username);
            Assert.AreEqual(edit.Username, uLogic.Login(edit.Email, edit.Password).Username);
        }

        [TestMethod]
        public void TestGoals()
        {            
            var userId = 99;

            // Goal aanmaken
            var goal = new Goal()
            {
                Title = "I am going to write a book!",
                Info = "it is going to be great!",
                StartDT = Convert.ToDateTime("05-05-2018"),
                EndDT = Convert.ToDateTime("12-12-2018")
            };
            
            Assert.IsTrue(gLogic.Add(userId, goal.Title, goal.Info, goal.StartDT, goal.EndDT));

            var get = gLogic.GetAllByUserId(userId)[0];

            Assert.AreEqual(goal.Title, get.Title);

            // Goal aanpassen
            var edit = new Goal()
            {
                GoalId = get.GoalId,
                Title = "I am going to write a book!",
                Info = "",
                StartDT = null,
                EndDT = Convert.ToDateTime("12-12-2018"),
                Progress = 34,
                UserId = userId
            };

            Assert.IsTrue(gLogic.Edit(edit));

            // Goal afronden
            get = gLogic.GetAllByUserId(userId)[0];

            Assert.AreEqual(edit.Info, get.Info);

            Assert.IsTrue(gLogic.FinishGoal(get.GoalId));

            get = gLogic.GetAllByUserId(userId)[0];

            Assert.AreEqual(100, get.Progress);
            Assert.AreEqual(GoalStatus.Finished, get.Status);
        }
    }
}
