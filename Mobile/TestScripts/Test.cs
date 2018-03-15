using System;
using System.Reflection;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Mobile.Properties;
using Mobile;

[TestFixture]
public class Test
{
    AndroidApp app;

    [SetUp]
    public void SetUp()
    {
        Core.SetUp(ref app);
    }

    [Test]
    public void VerifyTest()
    {
        String testname = MethodBase.GetCurrentMethod().Name;
        try
        {
        }
        finally
        {
            Core.TakeScreenShot("Last Screenshot", testname);
        }
    }

}
