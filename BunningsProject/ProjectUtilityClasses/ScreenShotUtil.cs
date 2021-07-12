using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BunningsProject.ProjectUtilityClasses
{
    public class ScreenShotUtil
    {

        IWebDriver _driver;
        const string RUN_NUMBER = "1";
        string _filename = string.Empty;
        public static string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
        public static string screenshotDirectory = BaseDir + ConfigurationManager.AppSettings["Env"] + @"\" + DateTime.Now.Day + "_"
                                       + DateTime.Now.Month + $@"\Run{RUN_NUMBER}\" + FeatureContext.Current.FeatureInfo.Title;

        public static string screenshotDirectoryInterimSteps = screenshotDirectory + @"\InterimSteps";
        public static string screenshotDirectoryPassed = screenshotDirectory + @"\Passed";
        public static string screenshotDirectoryFailed = screenshotDirectory + @"\Failed";


        public ScreenShotUtil(IWebDriver driver)
        {
            _driver = driver;

            SetAcl(screenshotDirectoryPassed);
            SetAcl(screenshotDirectoryFailed);
            SetAcl(screenshotDirectoryInterimSteps);
        }


        public bool TakeScreenshot(string TestCaseResult)
        {

            string path = "";
            if (TestCaseResult == "TestCasePass")
            {
                path = screenshotDirectoryPassed + @"\" + ScenarioContext.Current.ScenarioInfo.Title + Regex.Replace(DateTime.Now.ToString(), @"[^0-9]+", "") + ".png";
            }

            else if (TestCaseResult == "TestCaseRunning")
            {
                path = screenshotDirectoryInterimSteps + @"\" + ScenarioContext.Current.ScenarioInfo.Title + Regex.Replace(DateTime.Now.ToString(), @"[^0-9]+", "") + ".png";
            }
            else
            {
                path = screenshotDirectoryFailed + @"\" + ScenarioContext.Current.ScenarioInfo.Title + Regex.Replace(DateTime.Now.ToString(), @"[^0-9]+", "") + ".png";
            }

            Screenshot screenshot = ((ITakesScreenshot)_driver).GetScreenshot();

            if (SaveScreenshotToFile(screenshot, path))
                return true;

            return false;
        }

        public bool SaveScreenshotToFile(Screenshot screenshot, string filename)
        {
            _filename = filename;

            if (File.Exists(filename))
            {
                string[] filenameWithoutExtension = filename.Split('.');
                _filename = filenameWithoutExtension[0] + "_" + DateTime.Now.ToString().ToDateTimeInStringFormat("HHmmtt") + "." + filenameWithoutExtension[1];
            }

            try
            {
                screenshot.SaveAsFile(_filename, ScreenshotImageFormat.Png);
                return true;
            }
            catch (Exception e)
            {
                _driver.Quit();
                throw new Exception(String.Format("Following error occurred while saving the screenshot: {0}", e.Message));
            }
        }


        private bool SetAcl(string directory)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(directory);
            FileSystemAccessRule fsar = new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow);
            DirectorySecurity ds = null;

            if (!di.Exists)
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            ds = di.GetAccessControl();
            ds.AddAccessRule(fsar);
            di.SetAccessControl(ds);

            FileSystemRights Rights = (FileSystemRights)0;
            Rights = FileSystemRights.FullControl;

            // *** Add Access Rule to the actual directory itself
            FileSystemAccessRule AccessRule = new FileSystemAccessRule("Users", Rights,
                                        InheritanceFlags.None,
                                        PropagationFlags.NoPropagateInherit,
                                        AccessControlType.Allow);

            DirectoryInfo Info = new DirectoryInfo(directory);
            DirectorySecurity Security = Info.GetAccessControl(AccessControlSections.Access);

            bool Result = false;
            Security.ModifyAccessRule(AccessControlModification.Set, AccessRule, out Result);

            if (!Result)
                return false;

            // *** Always allow objects to inherit on a directory
            InheritanceFlags iFlags = InheritanceFlags.ObjectInherit;
            iFlags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;

            // *** Add Access rule for the inheritance
            AccessRule = new FileSystemAccessRule("Users", Rights,
                                        iFlags,
                                        PropagationFlags.InheritOnly,
                                        AccessControlType.Allow);
            Result = false;
            Security.ModifyAccessRule(AccessControlModification.Add, AccessRule, out Result);

            if (!Result)
                return false;

            Info.SetAccessControl(Security);

            return true;
        }
    }

}