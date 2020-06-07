using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MovieApp.Model
{
    public class ErrorFiler
    {
        readonly string path = HttpContext.Current.Server.MapPath(@"~\Content\ErrorLog.txt");



        public void WriteError(string error, string errorText)
        {
            if (File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.AppendText(path))
                {    
                    sw.WriteLine(DateTime.Now+"\t"+error+ " -> "+errorText);  
                }

            }
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }


    }
}
