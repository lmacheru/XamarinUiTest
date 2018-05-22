using System;
using System.Reflection;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using VodaiOS.Properties;
using VodaiOS;
using System.Threading;

[TestFixture]
public class ActivateNXTLVL
{
    AndroidApp app;
    [SetUp]
    public void SetUp()
    {
        Core.SetUp(ref app);
    }

    [Test]
    public void ActivateNXTLVLTest()
    {
        String testname = MethodBase.GetCurrentMethod().Name;
        Core.EnvPicker(testname);
        Core.SoftwareUpdate(testname);
        Core.OTPBypass(testname);
        try
        {
            Console.WriteLine(testname);
            Core.performLogin(Core.dictProfiles["nxtlvlusername"], Core.dictProfiles["GlobalPassword"], testname);
            Core.WaitForLoadingScreen();
            Core.Menu(testname);

            // Go to Slider Menu & select Acivate to NXT LVL

            app.Tap(x => x.Text(Core.OR["Competitionsandpromotions"]));
            app.WaitForElement(x => x.Text("NXT LVL Show"));

           try{
                app.WaitForElement(x => x.Text(Core.OR["ActivateNXTLVL"]), timeout: TimeSpan.FromSeconds(5));
                app.Tap(x => x.Text(Core.OR["ActivateNXTLVL"]));

                app.WaitForElement(x => x.Text(Core.OR["NxtLvlRegistration"]), timeout: TimeSpan.FromSeconds(5));
                app.WaitForElement(x => x.Text(Core.OR["SouthAfricanID"]), timeout: TimeSpan.FromSeconds(5));
                Core.TakeScreenShot("Activate NXT LVL", testname);

                app.Tap(x => x.Marked(Core.OR["IdTextBoxfld"]));

                app.EnterText(x => x.Marked(Core.OR["IdTextBoxfld"]), "9803305680249");
                app.WaitForElement(x => x.Text(Core.OR["TandC"]), timeout: TimeSpan.FromSeconds(15));

                app.Tap(x => x.Text(Core.OR["TandC"]));
                try
                {
                    app.WaitForElement(x => x.Text(Core.OR["T&C"]));
                }
                catch
                {
                    app.WaitForElement(x => x.Text("Terms and Conditions"));
                }
                app.ScrollDown();
                Core.TakeScreenShot(Core.OR["TandC"] + " swiped up", testname);

                app.ScrollUp();
                Core.TakeScreenShot(Core.OR["TandC"] + " swiped down", testname);

                app.WaitForElement(x => x.Marked(Core.OR["BackBtnIcon"]), timeout: TimeSpan.FromSeconds(15));
                app.Tap(x => x.Marked(Core.OR["BackBtnIcon"]));

                app.WaitForElement(x => x.Text(Core.OR["Activatebtn"]), timeout: TimeSpan.FromSeconds(5));
                Core.TakeScreenShot("Back button", testname);

                app.Tap(x => x.Text(Core.OR["Activatebtn"]));

                try
                {
                    app.WaitForElement(x => x.Text(Core.OR["WereSorryMsg"]), timeout: TimeSpan.FromSeconds(5));
                    app.WaitForElement(x => x.Text("The ID number entered is incorrect. Finger trouble maybe? Please try again and check that there are no white spaces between the numbers"), timeout: TimeSpan.FromSeconds(5));

                    app.WaitForElement(x => x.Text("Ok"), timeout: TimeSpan.FromSeconds(5));
                    Core.TakeScreenShot("Incorrect ID", testname);

                    app.Tap(x => x.Text(Core.OR["Ok"]));
                }
                catch
                {
                    app.WaitForElement(x => x.Text(Core.OR["WereSorryMsg"]), timeout: TimeSpan.FromSeconds(5));
                    app.WaitForElement(x => x.Text("Unfortunately you need to be under 25 in order to qualify for NXT LVL."), timeout: TimeSpan.FromSeconds(5));

                    app.WaitForElement(x => x.Text("Ok"), timeout: TimeSpan.FromSeconds(25));
                    Core.TakeScreenShot("over aged", testname);
                    app.Tap(x => x.Text(Core.OR["Ok"]));

                }

                try
                {
                    app.WaitForElement(x => x.Text("Ok"), timeout: TimeSpan.FromSeconds(5));
                    Core.TakeScreenShot("Success", testname);

                    app.Tap(x => x.Text(Core.OR["Ok"]));
                }
                catch {
                    Core.TakeScreenShot("Deactivate", testname);

                }

            }
            catch { }

            try
            {
                app.WaitForElement(x => x.Marked(Core.OR["BackBtnIcon"]), timeout: TimeSpan.FromSeconds(15));
                app.Tap(x => x.Marked(Core.OR["BackBtnIcon"]));
                app.WaitForElement(x => x.Marked(Core.OR["BackBtnIcon"]), timeout: TimeSpan.FromSeconds(15));
                app.Tap(x => x.Marked(Core.OR["BackBtnIcon"]));
            }
            catch { }

            //Logout
              Core.Logout(testname);
        
        }
        finally
        {
            Core.TakeScreenShot("Last Screenshot", testname);
        }
    }
}
