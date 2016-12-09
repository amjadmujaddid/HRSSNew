using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HRSS.Utility
{
    public class UploadDataHelper
    {
        public static void Upload(HttpContext form)
        {
            form.Response.ContentType = "text/plain";
            var request = form.Request;
            var mode = request["mode"];
            var sessionName = request["sessionName"];
            if (sessionName == string.Empty) /*untuk menghandle upload image dalam 1 form lebih dari 1 button upload*/
            {
                sessionName = mode;
            }
            string strImage = string.Empty;

            foreach (string s in form.Request.Files)
            {
                HttpPostedFile file = form.Request.Files[s];
                string fileName = file.FileName;
                string fileExtension = file.ContentType;

                if (!string.IsNullOrEmpty(fileName))
                {
                    if (HttpContext.Current.Session[sessionName] != null)
                    {
                        File.Delete(Conf.TempFolder + mode + "/" + HttpContext.Current.Session[sessionName]);
                    }
                    strImage = fileName.ToString();
                    file.SaveAs(Conf.TempFolder + mode + "/" + strImage);

                    form.Session.Add(sessionName, strImage);
                }
            }

            form.Response.Write(strImage);
        }
    }
}
