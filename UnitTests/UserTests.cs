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
            failList.Add(uLogic.Register(user1));
            // Check fail 2 : user2
            failList.Add(uLogic.Register(user2));
            // Check fail 3 : user3
            failList.Add(uLogic.Register(user3));

            // Check success 1 : user4
            successList.Add(uLogic.Register(user4));

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
            var user2 = new User() { Email = "jeff@jeff.nl", Username = "jeff", Password = "jeff" };
            var user3 = new User() { Email = "emma@hotmail.nl", Username = "emma", Password = "emma" };
            var user4 = new User() { Email = "piet@gmail.nl", Username = "piet", Password = "piet" };
            var user5 = new User() { Email = "jeroen@gmail.nl", Username = "jeroen", Password = "jeroen" };

            var failList = new List<string>();
            var successList = new List<string>();

            // Logic aanroepen     
            // Check fail 1 : user1
            failList.Add(uLogic.Register(user1));
            // Check fail 2 : user2
            failList.Add(uLogic.Register(user2));
            
            // Check success 1 : user3
            successList.Add(uLogic.Register(user3));
            // Check success 2 : user4
            successList.Add(uLogic.Register(user4));
            // Check success 3 : user5
            successList.Add(uLogic.Register(user5));

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
            var user1 = new User() { Email = "user@gmail.com", Username = "user" };
            var user2 = new User() { Email = "admin@outlook.com", Username = "admin" };
            var user3 = new User() { Email = "batman@outlook.com", Username = "Batman" };

            var failList = new List<string>();
            var successList = new List<string>();

            // Logic aanroepen
            // Check fail 1 : user1
            failList.Add(uLogic.Register(user1));
            // Check fail 2 : user2
            failList.Add(uLogic.Register(user2));  
            
            // Check success 1 : user3
            successList.Add(uLogic.Register(user3));

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
            failList.Add(uLogic.Login(user1.Username, user1.Email));
            // Check fail 2 : user2
            failList.Add(uLogic.Login(user2.Username, user2.Email));

            // Check success 1 : user3
            successList.Add(uLogic.Login(user3.Username, user3.Username));

            // Resultaten van de fail checks
            for (int i = 0; i < failList.Count; i++)
            {
                if (!String.IsNullOrEmpty(failList[i].Username))
                {
                    Assert.Fail("Check " + (i + 1) + " van de fails is gefaald.");
                }
            }

            // Resultaten van de success checks
            for (int i = 0; i < successList.Count; i++)
            {
                if (String.IsNullOrEmpty(successList[i].Username))
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
            failList.Add(uLogic.Login(user1.Username, user1.Email));
            // Check fail 2 : user2
            failList.Add(uLogic.Login(user2.Username, user2.Email));

            // Check success 1 : user3
            successList.Add(uLogic.Login(user3.Username, user3.Username));

            // Resultaten van de fail checks
            for (int i = 0; i < failList.Count; i++)
            {
                if (!String.IsNullOrEmpty(failList[i].Username))
                {
                    Assert.Fail("Check " + (i + 1) + " van de fails is gefaald.");
                }
            }

            // Resultaten van de success checks
            for (int i = 0; i < successList.Count; i++)
            {
                if (String.IsNullOrEmpty(successList[i].Username))
                {
                    Assert.Fail("Check " + (i + 1) + " van de successes is gefaald.");
                }
            }
        }
    }
}
