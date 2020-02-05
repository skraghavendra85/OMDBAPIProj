using System;
using System.IO;
using System.Text;
using System.Configuration;
namespace ResponseAppender
{
    public class FileAppender : IAppender
    {
        string path = "";

        public FileAppender()
        {
            
        }
        public void AddtoFile(Type t, string Message,string path)
        {
            try { 
                File.WriteAllText(
                Path.Combine(path , @"Logs\", $"{t.Namespace + DateTime.Now.Date.ToString("yyyyMMdd")+".log"}"),
                Message,
                Encoding.UTF8);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
