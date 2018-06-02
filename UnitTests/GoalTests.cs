using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    class GoalTests
    {
        GoalLogic gLogic = new GoalLogic();

        public void TestCreateGoalRequiredFields()
        {
            var goal1 = new Goal() { Title = "This is a goal" /*EndDT = Convert.ToDateTime("")*/ };
            var goal2 = new Goal() { Title = "", EndDT = Convert.ToDateTime("2018-10-10 00:00:00") };
            var goal3 = new Goal() { Title = "I'm going to write a book", EndDT = Convert.ToDateTime("2019-12-12 00:00:00") };

            var failList = new List<bool>();
            var successList = new List<bool>();

            // Logic aanroepen
            // Check fail 1 : goal1
            failList.Add(gLogic.Add(goal1));
            // Check fail 2 : goal2
            failList.Add(gLogic.Add(goal2));

            // Check success 1 : goal3
            successList.Add(gLogic.Add(goal3));

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
            var goal1 = new Goal() { Title = "This is goal1", EndDT = Convert.ToDateTime("2017-06-04") };
            var goal2 = new Goal() { Title = "This is goal2", StartDT = Convert.ToDateTime("2018-01-12"), EndDT = Convert.ToDateTime("2018-05-29") };
            var goal3 = new Goal() { Title = "This is goal3", StartDT = Convert.ToDateTime("2018-07-12"), EndDT = Convert.ToDateTime("2018-06-23") };
            var goal4 = new Goal() { Title = "This is goal4", EndDT = Convert.ToDateTime("2018-06-10") };
            var goal5 = new Goal() { Title = "This is goal5", StartDT = Convert.ToDateTime("2018-03-02"), EndDT = Convert.ToDateTime("2019-12-10") };

            var failList = new List<bool>();
            var successList = new List<bool>();

            // Logic aanroepen
            // Check fail 1 : goal1
            failList.Add(gLogic.Add(goal1));
            // Check fail 2 : goal2
            failList.Add(gLogic.Add(goal2));
            // Check fail 3 : goal3
            failList.Add(gLogic.Add(goal3));

            // Check success 1 : goal4
            successList.Add(gLogic.Add(goal4));
            // Check success 2 : goal5
            successList.Add(gLogic.Add(goal5));

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
