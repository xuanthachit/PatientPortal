﻿using System;
using System.IO;

namespace PatientPortal.SPA.CMS.Utility
{
    public class LogHelper
    {
        public static void WriteLog(string message, string type = "1")
        {
            if (message.Length < 1)
                return;

            string _path = ValueConstant.LOGS_PATH;
            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(_path)))
                Directory.CreateDirectory(_path);

            var filename = _path + DateTime.Today.ToString("dd-MM-yy") + ".txt";
            string path = System.Web.HttpContext.Current.Server.MapPath(filename);
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine("<<");
                    w.WriteLine("\r\nLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString());
                    string err = "Current path: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                                  ". Message:" + message;
                    w.WriteLine(err);
                    w.WriteLine(">>");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }
    }
}