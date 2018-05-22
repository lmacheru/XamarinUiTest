using System;
using System.Reflection;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using VodaiOS.Properties;
using System.Threading;
using VodaiOS;

[TestFixture]
public class HybRechargeHistory
{
    AndroidApp app;

    [SetUp]
    public void SetUp()
    {
        Core.SetUp(ref app);
    }


    [Test]
    public void HybRechargeHistoryTest()
    {
        String testname = MethodBase.GetCurrentMethod().Name;
        Core.EnvPicker(testname);
        Core.SoftwareUpdate(testname);
        Core.OTPBypass(testname);
        try
        {
            Core.performLogin(Core.dictProfiles["nxtlvlusername"], Core.dictProfiles["GlobalPassword"], testname);
            Core.WaitForLoadingScreen();
            Core.Menu(testname);

            
            app.WaitForElement(x => x.Text(Core.OR["MyAccount"]), timeout: TimeSpan.FromSeconds(10));
            app.Tap(x => x.Text(Core.OR["MyAccount"]));
            // Go to slider menu & verify MY BILL menu option 

            app.WaitForElement(x => x.Text(Core.OR["MyBill"]), timeout: TimeSpan.FromSeconds(20));

            app.Tap(x => x.Text(Core.OR["MyBill"]));
            app.WaitForElement(x => x.Text(Core.OR["MyBill"]), timeout: TimeSpan.FromSeconds(10));
            app.WaitForElement(x => x.Text(Core.OR["Rechargehistory"]), timeout: TimeSpan.FromSeconds(15));
            Core.TakeScreenShot("My Bill", testname);

            app.Tap(x => x.Text(Core.OR["Rechargehistory"]));
            Core.WaitForLoadingScreen();

            try
            {
                app.WaitForElement(x => x.Text("There's no recharge history to display for this period."), timeout: TimeSpan.FromSeconds(10));
                app.WaitForElement(x => x.Text(Core.OR["Ok"]), timeout: TimeSpan.FromSeconds(5));
                Core.TakeScreenShot("No recharge history", testname);

                app.Tap(x => x.Text(Core.OR["Ok"]));
            }
            catch
            {
                app.WaitForElement(x => x.Text(Core.OR["Rechargehistory"]), timeout: TimeSpan.FromSeconds(20));
                Core.TakeScreenShot("Recharge History", testname);
            }

            Core.WaitForLoadingScreen();
            app.Tap(x => x.Marked(Core.OR["BackBtnIcon"]));
            Core.TakeScreenShot("Back button", testname);

            app.Tap(x => x.Marked(Core.OR["BackBtnIcon"]));            

            //Logout
            Core.Logout(testname);
        }
        finally
        {
            Core.TakeScreenShot("Last Screenshot", testname);
        }
    }
}
