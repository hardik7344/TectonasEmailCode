using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TecMailAPI.BusinessLogic
{
    public class TecMailCommon
    {
        public static string logPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Tectona\\TecMailAPI\\";
        public static string MailUserName = "";
        public static string MailUserPassword = "";
        public static string strOSType = "";
        public static string LINUX_WWW_PATH = "";
        public static string LINUX_ROOT_PATH = "";
        public void WriteLog(string FileName, string Extension, string ProductName, string Message, bool HourWise)
        {
            try
            {
                if (HourWise)
                    FileName = FileName + "_" + System.DateTime.Now.ToString("yyyy-MM-dd HH") + "." + Extension;
                else
                    FileName = FileName + "." + Extension;
                    FileName = logPath + ProductName + "\\" + FileName;
                if (!Directory.Exists(logPath + ProductName))
                    Directory.CreateDirectory(logPath + ProductName);

                FileStream fs;
                fs = new FileStream(FileName, FileMode.Append);
                StreamWriter s = new StreamWriter(fs);
                s.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + Message);
                s.Close();
                fs.Close();
            }
            catch (Exception)
            {
            }
        }
        public string systemcheck()
        {
            string _systemPath = "";
            if (Is64BitSystem())
                _systemPath = Environment.GetFolderPath(Environment.SpecialFolder.System).ToUpper().Replace("SYSTEM32", "SysWOW64");
            else
                _systemPath = Environment.GetFolderPath(Environment.SpecialFolder.System).ToUpper().Replace("SysWOW64", "SYSTEM32");
            return _systemPath;
        }
        public bool Is64BitSystem()
        {
            return (Environment.GetEnvironmentVariable("ProgramFiles(x86)")) != null;
        }
        public string readLocalConfig(string FileName,string Item)
        {
            string xmlPath = Directory.GetCurrentDirectory() + "/wwwroot/xml/" + FileName;
            string retVal = "";
            DataSet ds = new DataSet("DataSet");
            try
            {
                ds.ReadXml(xmlPath);
                retVal = ds.Tables["Config"].Rows[0][Item].ToString();
            }
            catch (Exception ex)
            {
                retVal = "Linux";
                WriteLog("Exception", "log", "TecMailAPI", "Exception while read DB Configuration : " + ex.Message.ToString(), true);
            }
            finally
            {
                ds.Dispose();
            }
            return retVal;
        }
        public string readDBConfig(string Item, string FileName)
        {
            string _dbSettings = "";
            if (strOSType.ToUpper() == "LINUX")
            {
                _dbSettings = LINUX_WWW_PATH + "/xml/" + FileName;
            }
            else
            {
                _dbSettings = systemcheck() + "\\AssertYIT\\Configuration\\" + FileName;
            }
            string retVal = "";
            DataSet ds = new DataSet("DataSet");
            try
            {
                ds.ReadXml(_dbSettings);
                retVal = ds.Tables["Config"].Rows[0][Item].ToString();
            }
            catch (Exception ex)
            {
                WriteLog("Exception", "log", "TecMailAPI", "Exception while read DB Configuration : " + ex.Message.ToString(), true);
            }
            finally
            {
                ds.Dispose();
            }
            return retVal;
        }
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
