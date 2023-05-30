using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAPI_Call
{
    class ContextEngine
    {

        //*********** This holds the request logs so context is maintained **************

        // 
        private List<Dictionary<string, string>> _chatContext;
        
        // these are kept in context, at the beginning, for each request
        private List<Dictionary<string, string>> _defaults; 



        public bool addContext(string sPrompt, string sResponse)
        {
            // check to see if there is enough room

            // remove oldest items that are not the defaults if needed

            // add new item

            // return new context
            
            return true;
        }
    }
}
