using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.UITest.Android;
using System.IO;
using Xamarin.UITest.Queries;

namespace Mobile
{
    public class Core
    {
        static AndroidApp app;
        public static Dictionary<string, string> LoginData = new Dictionary<string, string>();
        public static Dictionary<string, string> Elements = new Dictionary<string, string>();
        public static double nbr = 1;

        public static void SetUp(ref AndroidApp _app)
        {
            string asmname;
            asmname = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            if (asmname == "VerifiOne")
            {
                _app = Xamarin.UITest.ConfigureApp.Android.EnableLocalScreenshots().DeviceSerial(Properties.Resources.Emulator1).ApkFile(Mobile.Properties.Resources.apkPath).StartApp();
                app = _app;
            }
            else if (asmname == "VerifiTwo")
            {
                _app = Xamarin.UITest.ConfigureApp.Android.EnableLocalScreenshots().DeviceSerial(Properties.Resources.Emulator2).ApkFile(Mobile.Properties.Resources.apkPath).StartApp();
                app = _app;
            }
            else
            {
                _app = Xamarin.UITest.ConfigureApp.Android.EnableLocalScreenshots().DeviceSerial(Properties.Resources.Emulator3).ApkFile(Mobile.Properties.Resources.apkPath).StartApp();
                app = _app;
            }
            nbr = 1;
        }

        public static void WaitForLoadingScreen()
        {
            Func<AppQuery, AppQuery> loadingScreenQuery = e => e.Marked("Spinner");
            app.WaitForNoElement(loadingScreenQuery, "Timed out waiting for Loading screen to disappear.", timeout: TimeSpan.FromSeconds(20));
        }

        public static void EnvPicker(string testname)
        {
            app.WaitForElement(x => x.Text(Properties.Resources.env), timeout: TimeSpan.FromSeconds(10));
            
            app.Tap(x => x.Text(Mobile.Properties.Resources.env));

            if (Mobile.Properties.Resources.env.Equals("LIVE PROD"))
            {
                PopulateDictionary(@"Envtestdata/LIVE PROD.txt");
                PopulateOR(@"EnvtestOR/LIVE PROD OR.txt");
                //PopulateOR(@"Envtestdata/OR.txt");
            }
            else if (Mobile.Properties.Resources.env.Equals("PRODA External"))
            {
                PopulateDictionary(@"Envtestdata/PRODA External.txt");
                // PopulateOR(@"EnvtestOR/LIVE PROD OR.txt");
                PopulateOR(@"EnvtestOR/PRODA External OR.txt");
            }
            else if (Mobile.Properties.Resources.env.Equals("UAT External"))
            {
                PopulateDictionary(@"Envtestdata/UAT External.txt");
                PopulateOR(@"EnvtestOR/UAT OR.txt");
                //  PopulateOR(@"Envtestdata/OR.txt");
            }

        }
        #region Dictionary
        public static void PopulateDictionary(string path)
        {
            string[] txtFileLines = File.ReadAllLines(path);
            foreach (var line in txtFileLines)
            {
                string[] str = line.Split(',');
                if (txtFileLines.Contains(Core.LoginData[str[0]] = str[1]))
                {
                    LoginData.Add(str[0], str[1]);

                }
                else
                {
                    Core.LoginData[str[0]] = str[1];
                    continue;
                }
            }

        }

        public static void PopulateOR(string path)
        {
            string[] txtFileLines = File.ReadAllLines(path);
            foreach (var line in txtFileLines)
            {
                string[] str = line.Split(',');
                if (txtFileLines.Contains(Core.Elements[str[0]] = str[1]))
                {
                    Elements.Add(str[0], str[1]);

                }
                else
                {
                    Core.Elements[str[0]] = str[1];
                    continue;
                }
            }

        }
        #endregion Dictionary
        #region Reports
        public static void ScreenMove(String testname, String str)
        {
            String appScreen = "Screenshot " + Properties.Resources.env + Properties.Resources.apkVersion + " ";
            String machineName = System.Environment.MachineName;
            String networkShareLocation = @"\\10.100.6.111\d$\Screen\";

            if (Properties.Resources.Exectype == "Local")
            {
                try
                {
                    MoveFiles(".", networkShareLocation + appScreen + machineName, testname, str);
                }

                catch
                {
                    MoveFiles(".", ".\\" + appScreen, testname, str);
                }

            }
            else
            {
                app.Screenshot("Last Screenshot");
               
            }
        }
     
        public static void TakeScreenShot(String name, String testname)
        {
            if (Properties.Resources.Exectype == "Local")
            {
                string str = "Scr-" + nbr + "-" + testname + "-" + name + ".png";
                app.Screenshot(name).MoveTo(@".\" + str);

                nbr += 01;
                ScreenMove(testname, str);
            }
            else if (Properties.Resources.Exectype == "Cloud")
            {
                app.Screenshot(name);
            }
        }
        
        public static void MoveFiles(string fromPath, string toPath, string className, String str)
        {
            string[] files = System.IO.Directory.GetFiles(fromPath, str);//"*.png"
            string currentDateFolder = DateTime.Now.ToLongDateString();

            string tmStamp = DateTime.Now.ToString("hh-mm-ss tt");//For whole Date and Time, change to DateTime.Now.ToString("yyyyMMddHHmmssfff")
                                                                  //  string fname = "Scr"
                                                                  //    string toPath1 = Directory.CreateDirectory(toPath + "\\" + fname);
            DirectoryInfo dirInfo = null;
            if (!Directory.Exists(toPath + "\\" + currentDateFolder))
            {
                dirInfo = Directory.CreateDirectory(toPath + "\\" + currentDateFolder);
                // You can use this info if you need it  
            }
            else
            {
                dirInfo = new DirectoryInfo(toPath + "\\" + currentDateFolder);
            }

            if (dirInfo != null)
            {
                dirInfo = dirInfo.CreateSubdirectory(className);//+ " " + tmStamp
            }

            var newPath = dirInfo.FullName;
            foreach (var file in files)
            {
                File.Move(file, newPath + "\\" + Path.GetFileName(file));
            }
        }
        #endregion Reports

    }
}