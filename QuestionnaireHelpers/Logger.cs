using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace QuestionHelpers
{
    public class Logger
    {
        private const string _savePath = "C:\\log\\log.log";
        public static void WriteLog(string modelName, Exception ex)
        {
            string content =
$@"-----
{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} 
     {modelName}
     {ex.ToString()}
-----

";
            File.AppendAllText(Logger._savePath, content);
        }
    }
}