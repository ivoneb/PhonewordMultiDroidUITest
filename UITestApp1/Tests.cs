using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;
using System.Reflection;

namespace UITestApp1
{
    // Try in square brackets, TestFixture(Platform.Android)
    [TestFixture]
    public class Tests
    {
        AndroidApp app;
        string PathToAPK;

        [SetUp]
        public void BeforeEachTest()
        {
            string currentFile = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            FileInfo fi = new FileInfo(currentFile);
            string dir = fi.Directory.Parent.Parent.Parent.FullName;
            PathToAPK = Path.Combine(dir, "Phoneword", "bin", "Debug", "Phoneword.Phoneword.APK");
            // The last parameter is the solution name + the Droid project name + .APK
            // Depending on your config, you may need to replace "Release" with "Debug" (Ensure "Shared Runtime" is disabled)
            // The path should be the same as what is hardcoded below:
            //PathToAPK = "../../../Phoneword/bin/Debug/Phoneword.Phoneword.apk";
            app = ConfigureApp
                .Android
                .ApkFile(PathToAPK)
                .StartApp();
            // There is another option above .EnableLocalScreenshots()
        }

        [Test]
        public void AppLaunches()
        {
            app.Repl(); // Starts Read-Evaluate-Print-Loop console app
            //app.Screenshot("First screen.");
            /* string labelStr = app.Query("PhoneNumberText")[0].Text;
            Assert.AreEqual("1-855-XAMARIN", labelStr); */
        }

        [Test]
        public void CheckInitialText()
        {
            string labelStr = app.Query("PhoneNumberText")[0].Text;
            Assert.AreEqual("1-855-XAMARIN", labelStr);
        }


        [Test]
        public void ChangePhoneNum()
        {
            app.Tap("PhoneNumberText");
            app.ClearText();
            app.EnterText("PhoneNumberText", "ABCDEF");
            app.Tap("CallButton");
            string labelStr = app.Query("CallButton")[0].Text;
            Assert.AreEqual("Call 222333", labelStr);
        }

    }
}

