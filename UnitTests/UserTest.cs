using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace UnitTests
{
    [TestClass]
    public class UserTest
    {
        UserLogic uLogic = new UserLogic();

        [TestMethod]
        public void TestRegisterOnEmptyFields()
        {
            // De users worden geregistreerd. 
            var user1 = new User() { Email = "", Username = "lars", Password = "lars" };
            var user2 = new User() { Email = "bob@gmail.com", Username = "", Password = "bob" };
            var user3 = new User() { Email = "henk@outlook.com", Username = "henk", Password = "" };
            var user4 = new User() { Email = "champ@hotmail.com", Username = "champ", Password = "champ" };

            // Registreren.
            string check1 = uLogic.Register(user1);
            string check2 = uLogic.Register(user2);
            string check3 = uLogic.Register(user3);
            string check4 = uLogic.Register(user4);

            // Resultaten checken
            if (check1 == "")
                Assert.Fail("Check 1 failed.");

            if (check2 == "")
                Assert.Fail("Check 2 failed.");

            if (check3 == "")
                Assert.Fail("Check 3 failed.");

            if (check4 != "")
                Assert.Fail("Check 4 failed.");
        }
    }
}
