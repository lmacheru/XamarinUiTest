using System;
using System.Reflection;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using VodaiOS.Properties;
using VodaiOS;

[TestFixture]
public class ForgetPassword
{
	AndroidApp app;
    
    [SetUp]
	public void SetUp()
	{
		Core.SetUp(ref app);
    }

	[Test]
	public void ForgetpassTest()
    {
        String testname = MethodBase.GetCurrentMethod().Name;
        Core.EnvPicker(testname);
        Core.SoftwareUpdate(testname);

        try
        { 
            // Welcome screen verification
		
				app.WaitForElement(x => x.Text("Welcome to My Vodacom"), timeout: TimeSpan.FromSeconds(30));
		
            app.WaitForElement(x => x.Marked(Core.OR["PleaseEnterNumberLabel"]));
            app.WaitForElement(x => x.Text(Core.OR["Cellphonenumber"]));
            Core.TakeScreenShot("OTP", testname);

            app.Tap(x => x.Marked(Core.OR["CellphoneNumberTextBox"]));

            // Bypass OTP authentication step
            app.EnterText(x => x.Marked(Core.OR["CellphoneNumberTextBox"]), Core.dictProfiles["otpbypassnum"]);
            app.WaitForElement(x => x.Marked(Core.OR["ProceedButton"]));
            app.Tap(x => x.Marked(Core.OR["ProceedButton"])); 
            Core.TakeScreenShot("Login screen", testname);

            //Forget password verification using by entering 20 digits sim card number

            app.Tap(x => x.Marked(Core.OR["UserNameTextBox"]));
            app.ClearText(x => x.Marked(Core.OR["UserNameTextBox"]));
            app.Tap(x => x.Marked(Core.OR["UserNameTextBox"]));
            app.EnterText(x => x.Marked(Core.OR["UserNameTextBox"]), Core.dictProfiles["forgetpassnum"]);
            app.DismissKeyboard();
            Core.TakeScreenShot("Login", testname);

            app.Tap(x => x.Marked(Core.OR["ForgottenPasswordButton"]));
            app.WaitForElement(x => x.Marked(Core.OR["SmsSwitchBtn"]), timeout: TimeSpan.FromSeconds(10));
            Core.TakeScreenShot("Forgot Password", testname);

            app.Tap(x => x.Marked(Core.OR["SmsSwitchBtn"]));
            app.WaitForElement(x => x.Marked(Core.OR["ProceedButtonForgrt"]), timeout: TimeSpan.FromSeconds(5));
            Core.TakeScreenShot("SMS Switch Button", testname);

            app.Tap(x => x.Marked(Core.OR["ProceedButtonForgrt"]));
            app.WaitForElement(x => x.Text("Enter your 20 digit SIM card number."), timeout: TimeSpan.FromSeconds(5));
            
            app.Tap(x => x.Marked(Core.OR["SimTextBox"]));
            app.EnterText(x => x.Marked(Core.OR["SimTextBox"]), "89440000000021198942");
            Core.TakeScreenShot("SIM Number", testname);

            app.Tap(x => x.Marked(Core.OR["ProceedButtonForgrt"]));
            Core.WaitForLoadingScreen();
            Core.TakeScreenShot("Security question", testname);

            app.Tap(x => x.Marked(Core.OR["CancelBtn"]));
         }
         finally
         {
             Core.TakeScreenShot("Last Screenshot", testname);

         }
    }

}
