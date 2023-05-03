using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OpenAPI_Call
{
    
    /* *********** Simple Logging *************** */
    /*                                            */
    /*    Really simple logger for now.  Will     */
    /*    expand out when do the context engine   */
    /*                                            */
    /**********************************************/
    
   
    
    // Note assuming you have rights to crud files on your local system.
    // If not God bless you.
    public static class Logger
    {
        
        //private static string _filepath {get;set;}

        public static bool AppendChatLog(string sPrompt, string sResponse)
        {

            if (sPrompt == "Give me a short, random, sarcastic, arrogant, greeting.")
                sPrompt = "Hello";
            
            string sDirectory = @"c:\chatGPTLogs";
            string sFileName = sDirectory + @"\chat_" + DateTime.Now.ToString("yyMMdd") + ".txt";


            // check directory, create if not there
            if (!Directory.Exists(sDirectory))
            {
                Directory.CreateDirectory(sDirectory);
            }

            // Check if the file exists
            if (!File.Exists(sFileName))
            {
                // If the file does not exist, create it
                File.Create(sFileName).Dispose();
            }

            // Append the content to the file
            using (StreamWriter sw = File.AppendText(sFileName))
            {
                sw.WriteLine("-------");
                sw.WriteLine("[usr] " + sPrompt);
                sw.WriteLine("[bot] " + sResponse);
                //sw.WriteLine(Environment.NewLine);
            }

            return true;
        }
    }
}
