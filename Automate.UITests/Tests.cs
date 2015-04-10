using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace Automate.UITests
{
    [TestFixture]
    public class Tests
    {
        private AndroidApp _app;

        [SetUp]
        public void BeforeEachTest()
        {
            _app = ConfigureApp.Android.StartApp();
        }

        [Test]
        public void ClickingButtonTwiceShouldChangeItsLabel()
        {
            Func<AppQuery, AppQuery> MyButton = c => c.Button("myButton");

            _app.Tap(MyButton);
            _app.Tap(MyButton);
            AppResult[] results = _app.Query(MyButton);
            _app.Screenshot("Button clicked twice.");

            Assert.AreEqual("2 clicks!", results[0].Text);
        }
    }
}

