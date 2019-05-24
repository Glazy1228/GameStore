using System;
using System.Web.Mvc;
using GameStore.WEB.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.WEB.Tests
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void LoginViewTest()
        {
            AccountController controller = new AccountController();

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
