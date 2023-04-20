using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace OpenAPI_Call
{
    public class OpenAI : IDisposable
    {
        private string _apiKey { get; set; }
        private int _maxTokens { get; set; }
        private string _model { get; set;  }
        private string _chatURL { get; set; }
        private string _contextId { get; set; }


        public OpenAI()
        {
            _apiKey = ""; // supply your own
            _maxTokens = 2048; //can be double
            _model = "gpt-3.5-turbo"; //if getting a lot of use, switch to a cheaper model
            _chatURL = "https://api.openai.com/v1/chat/completions";
            _contextId = string.Empty;
        }


        public async Task<string> SimpleChatCall2(string sPrompt, bool bDump = false)
        {
            var messages = new List<Dictionary<string, string>>()
            {
                new Dictionary<string, string>()
                {
                    { "role", "system" },
                    { "content", "You are a helpful assistant." }
                },
                new Dictionary<string, string>()
                {
                    { "role", "user" },
                    { "content", sPrompt }
                },
                // You can add more messages as needed for a back-and-forth conversation
            };

            var requestData = new
            {
                messages = messages,
                model =_model
            };

            string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);

            // Set up the HTTP client and request
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
                var request = new HttpRequestMessage(HttpMethod.Post, _chatURL)
                {
                    Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
                };

                // Send the request and get the response
                HttpResponseMessage response = await httpClient.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();

                // Handle the response
                if (response.IsSuccessStatusCode)
                {
                    // Parse the JSON response
                    dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                    if (!bDump)
                        return jsonResponse.choices[0].message.content;
                    else
                    {
                        string s = Utility.PrintJsonPropertiesAndValues(responseBody);
                        return s;
                    }
                       
                    // Process the completion as needed
                    //Console.WriteLine("Completion: " + completion);
                   
                }
                else
                {
                    // Handle the error response
                    return "Error: " + responseBody;
                }
            }

        }

        public void Dispose()
        {
            ;
        }



    }
}
