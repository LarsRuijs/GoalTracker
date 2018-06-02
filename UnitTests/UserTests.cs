using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UserTests
    {
        UserLogic uLogic = new UserLogic();

        [TestMethod]
        public void TestRegisterEmptyFields()
        {
            var user1 = new User() { Email = "", Username = "lars", Password = "lars" };
            var user2 = new User() { Email = "bob@gmail.com", Username = "", Password = "bob" };
            var user3 = new User() { Email = "henk@outlook.com", Username = "henk", Password = "" };
            var user4 = new User() { Email = "champ@hotmail.com", Username = "champ", Password = "champ" };

            var failList = new List<string>();
            var successList = new List<string>();

            // Logic aanroepen
            // Check fail 1 : user1
            failList.Add(uLogic.Register(user1.Username, user1.Password, user1.Email));
            // Check fail 2 : user2
            failList.Add(uLogic.Register(user2.Username, user2.Password, user2.Email));
            // Check fail 3 : user3
            failList.Add(uLogic.Register(user3.Username, user3.Password, user3.Email));

            // Check success 1 : user4
            successList.Add(uLogic.Register(user4.Username, user4.Password, user4.Email));

            // De lijst van fails worden
            for (int i = 0; i < failList.Count; i++)
            {
                if (failList[i] == "")
                {
                    Assert.Fail("Check " + (i + 1) + " van de fails is gefaald.");
                }
            }

            for (int i = 1; i < successList.Count; i++)
            {
                if (successList[i] != "")
                {
                    Assert.Fail("Check " + (i + 1) + " van de successes is gefaald.");
                }
            }
        }

        [TestMethod]
        public void TestRegisterValidEmail()
        {
            var user1 = new User() { Email = "karel", Username = "karel", Password = "karel" };
            var user2 = new User() { Email = "emma@hotmail.nl", Username = "emma", Password = "emma" };
            var user3 = new User() { Email = "piet@gmail.nl", Username = "piet", Password = "piet" };
            var user4 = new User() { Email = "jeroen@gmail.nl", Username = "jeroen", Password = "jeroen" };

            var failList = new List<string>();
            var successList = new List<string>();

            // Logic aanroepen     
            // Check fail 1 : user1
            failList.Add(uLogic.Register(user1.Username, user1.Password, user1.Email));
            
            // Check success 1 : user2
            successList.Add(uLogic.Register(user2.Username, user2.Password, user2.Email));
            // Check success 2 : user3
            successList.Add(uLogic.Register(user3.Username, user3.Password, user3.Email));
            // Check success 3 : user4
            successList.Add(uLogic.Register(user4.Username, user4.Password, user4.Email));

            // Resultaten van de fail checks
            for (int i = 0; i < failList.Count; i++)
            {
                if (failList[i] == "")
                {
                    Assert.Fail("Check " + (i + 1) + " van de fails is gefaald.");
                }
            }

            // Resultaten van de success checks
            for (int i = 0; i < successList.Count; i++)
            {
                if (successList[i] != "")
                {
                    Assert.Fail("Check " + (i + 1) + " van de successes is gefaald.");
                }
            }
        }

        [TestMethod]
        public void TestRegisterAlreadyInUse()
        {
            var user1 = new User() { Email = "user@gmail.com", Username = "user", Password = "123" };
            var user2 = new User() { Email = "admin@outlook.com", Username = "admin", Password = "123" };
            var user3 = new User() { Email = "batman@outlook.com", Username = "Batman", Password = "123" };

            var failList = new List<string>();
            var successList = new List<string>();

            // Logic aanroepen
            // Check fail 1 : user1
            failList.Add(uLogic.Register(user1.Username, user1.Password, user1.Email));
            // Check fail 2 : user2
            failList.Add(uLogic.Register(user2.Username, user1.Password, user2.Email));  
            
            // Check success 1 : user3
            successList.Add(uLogic.Register(user3.Username, user1.Password, user3.Email));

            // Resultaten van de fail checks
            for (int i = 0; i < failList.Count; i++)
            {
                if (failList[i] == "")
                {
                    Assert.Fail("Check " + (i + 1) + " van de fails is gefaald.");
                }
            }

            // Resultaten van de success checks
            for (int i = 0; i < successList.Count; i++)
            {
                if (successList[i] != "")
                {
                    Assert.Fail("Check " + (i + 1) + " van de successes is gefaald.");
                }
            }
        }

        [TestMethod]
        public void TestLoginEmptyFields()
        {
            var user1 = new User() { Username = "", Password = "bob" };
            var user2 = new User() { Username = "henk", Password = "" };
            var user3 = new User() { Username = "user", Password = "test" };

            var failList = new List<User>();
            var successList = new List<User>();

            // Logic aanroepen
            // Check fail 1 : user1
            failList.Add(uLogic.Login(user1.Username, user1.Password));
            // Check fail 2 : user2
            failList.Add(uLogic.Login(user2.Username, user2.Password));

            // Check success 1 : user3
            successList.Add(uLogic.Login(user3.Username, user3.Password));

            // Resultaten van de fail checks
            for (int i = 0; i < failList.Count; i++)
            {
                if (failList[i].Username != null)
                {
                    Assert.Fail("Check " + (i + 1) + " van de fails is gefaald.");
                }
            }

            // Resultaten van de success checks
            for (int i = 0; i < successList.Count; i++)
            {
                if (successList[i].Username == null)
                {
                    Assert.Fail("Check " + (i + 1) + " van de successes is gefaald.");
                }
            }
        }

        [TestMethod]
        public void TestLoginNoMatchFound()
        {
            var user1 = new User() { Username = "admin", Password = "test" };
            var user2 = new User() { Username = "user", Password = "1234" };
            var user3 = new User() { Username = "admin", Password = "1234" };

            var failList = new List<User>();
            var successList = new List<User>();

            // Logic aanroepen
            // Check fail 1 : user1
            failList.Add(uLogic.Login(user1.Username, user1.Password));
            // Check fail 2 : user2
            failList.Add(uLogic.Login(user2.Username, user2.Password));

            // Check success 1 : user3
            successList.Add(uLogic.Login(user3.Username, user3.Password));

            // Resultaten van de fail checks
            for (int i = 0; i < failList.Count; i++)
            {
                if (failList[i] != null)
                {
                    Assert.Fail("Check " + (i + 1) + " van de fails is gefaald.");
                }
            }

            // Resultaten van de success checks
            for (int i = 0; i < successList.Count; i++)
            {
                if (successList[i] == null)
                {
                    Assert.Fail("Check " + (i + 1) + " van de successes is gefaald.");
                }
            }
        }

        GoalLogic gLogic = new GoalLogic();

        [TestMethod]
        public void TestCreateGoalRequiredFields()
        {
            var goal1 = new Goal() { UserId = 1, Title = "This is a goal", Info = "info" /*EndDT = Convert.ToDateTime("")*/ };
            var goal2 = new Goal() { UserId = 1, Title = "", Info = "info", EndDT = Convert.ToDateTime("2018-10-10 00:00:00") };
            var goal3 = new Goal() { UserId = 1, Title = "I'm going to write a book", Info = "info", EndDT = Convert.ToDateTime("2019-12-12 00:00:00") };

            var failList = new List<bool>();
            var successList = new List<bool>();

            // Logic aanroepen
            // Check fail 1 : goal1
            failList.Add(gLogic.Add(goal1.UserId, goal1.Title, goal1.Info, goal1.StartDT, goal1.EndDT));
            // Check fail 2 : goal2
            failList.Add(gLogic.Add(goal2.UserId, goal2.Title, goal2.Info, goal2.StartDT, goal2.EndDT));

            // Check success 1 : goal3
            successList.Add(gLogic.Add(goal3.UserId, goal3.Title, goal3.Info, goal3.StartDT, goal3.EndDT));

            for (int i = 0; i < failList.Count; i++)
            {
                if (failList[i])
                {
                    Assert.Fail("Check " + (i + 1) + " van de fails heeft gefaald.");
                }
            }

            // Resultaten van de success checks
            for (int i = 0; i < successList.Count; i++)
            {
                if (!successList[i])
                {
                    Assert.Fail("Check " + (i + 1) + " van de successes heeft gefaald.");
                }
            }
        }

        [TestMethod]
        public void TestCreateGoalInvalidDeadline()
        {
            var goal1 = new Goal() { UserId = 1, Title = "This is goal1", Info = "info", EndDT = Convert.ToDateTime("2017-06-04") };
            var goal2 = new Goal() { UserId = 1, Title = "This is goal2", Info = "info", StartDT = Convert.ToDateTime("2018-01-12"), EndDT = Convert.ToDateTime("2018-05-29") };
            var goal3 = new Goal() { UserId = 1, Title = "This is goal3", Info = "info", StartDT = Convert.ToDateTime("2018-07-12"), EndDT = Convert.ToDateTime("2018-06-23") };
            var goal4 = new Goal() { UserId = 1, Title = "This is goal4", Info = "info", EndDT = Convert.ToDateTime("2018-06-10") };
            var goal5 = new Goal() { UserId = 1, Title = "This is goal5", Info = "info", StartDT = Convert.ToDateTime("2018-03-02"), EndDT = Convert.ToDateTime("2019-12-10") };

            var failList = new List<bool>();
            var successList = new List<bool>();

            // Logic aanroepen
            // Check fail 1 : goal1
            failList.Add(gLogic.Add(goal1.UserId, goal1.Title, goal1.Info, goal1.StartDT, goal1.EndDT));
            // Check fail 2 : goal2
            failList.Add(gLogic.Add(goal2.UserId, goal2.Title, goal2.Info, goal2.StartDT, goal2.EndDT));
            // Check fail 3 : goal3
            failList.Add(gLogic.Add(goal3.UserId, goal3.Title, goal3.Info, goal3.StartDT, goal3.EndDT));

            // Check success 1 : goal4
            successList.Add(gLogic.Add(goal4.UserId, goal4.Title, goal4.Info, goal4.StartDT, goal4.EndDT));
            // Check success 2 : goal5
            successList.Add(gLogic.Add(goal5.UserId, goal5.Title, goal5.Info, goal5.StartDT, goal5.EndDT));

            for (int i = 0; i < failList.Count; i++)
            {
                if (failList[i])
                {
                    Assert.Fail("Check " + (i + 1) + " van de fails heeft gefaald.");
                }
            }

            // Resultaten van de success checks
            for (int i = 0; i < successList.Count; i++)
            {
                if (!successList[i])
                {
                    Assert.Fail("Check " + (i + 1) + " van de successes heeft gefaald.");
                }
            }
        }
    }
}
