using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;

namespace OpenAPI_Call
{

    
    
    
    
    
    
    #region Json classes for the chunk streaming if streaming used
    public class ChatCompletionChunk
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public long Created { get; set; }
        public string Model { get; set; }
        public Choice[] Choices { get; set; }
    }

    public class Choice
    {
        public Delta Delta { get; set; }
        public int Index { get; set; }
        public string FinishReason { get; set; }
    }

    public class Delta
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }
    #endregion



    public class OpenAI : IDisposable
    {
        private string _apiKey { get; set; }
        private int _maxTokens { get; set; }
        private string _model { get; set; }
        private string _chatURL { get; set; }
        private string _contextId { get; set; }
        private bool _stream { get; set; }
        //private bool _convoMode { get; set; }
        //private string _convoId { get; set; }


        public List<Dictionary<string, string>> _chatContext;


        public event EventHandler<string> MessageRaised;


        public OpenAI()
        {
            _apiKey = ""; // supply your own
            _maxTokens = 2048 ; //can be double
            _model = "gpt-3.5-turbo"; //if getting a lot of use, switch to a cheaper model
            _chatURL = "https://api.openai.com/v1/chat/completions";
            _contextId = System.Guid.NewGuid().ToString();
            _stream = true;
            _contextId = new System.Guid().ToString();

            if (string.IsNullOrEmpty(_apiKey))
                _apiKey = EncryptionHelper.GetOPENAPIKeyFromDisk();
        }


        public async Task<bool> SimpleChatCall2(string sPrompt, List<Dictionary<string, string>> ctx, bool bDump = false)
        {
            
            // ******* for ctx, see the build method on the Form1.cs code file ***********
            
            //var messages = new List<Dictionary<string, string>>()
            //{
            //    new Dictionary<string, string>()
            //    {
            //        { "role", "assistant" },
            //        { "content", "You are a grumpy, sarcastic arrogant hacker, you are very anti-woke and not politically correct,  but helpful." }
            //    },
            //    //new Dictionary<string, string>()
            //    //{
            //    //    { "role","user"},
            //    //    { "content", "My name is Bob" }


            //    //},
            //    //new Dictionary<string, string>()
            //    //{
            //    //    { "role","user"},
            //    //    { "content", "I want information about making the unknown." }


            //    //},
            //    new Dictionary<string, string>()
            //    {
            //        { "role", "user" },
            //        { "content", sPrompt }
            //    },
            //    // You can add more messages as needed for a back-and-forth conversation
            //};

            ctx.Add(new Dictionary<string, string>()
            {
                { "role", "user" },
                { "content", sPrompt.compact() }
            });


            var requestData = new
            {
                messages = ctx,
                model = _model,
                max_tokens = _maxTokens,
                stream = _stream

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

                if (!_stream)
                {


                    // Send the request and get the response
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Handle the response
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the JSON response
                        dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                        if (!bDump)
                        { 
                         this.MessageRaised(null, jsonResponse.choices[0].message.content);
                        // this._contextId =   jsonResponse.Id;
                        }   
                        else
                        {
                            string s = Utility.PrintJsonPropertiesAndValues(responseBody);
                            this.MessageRaised(null,s);
                        }

                        // Process the completion as needed
                        //Console.WriteLine("Completion: " + completion);

                    }
                    else
                    {
                        // Handle the error response
                        this.MessageRaised(null,"Error: " + responseBody);
                    }
                }
                else
                {

                    string s = string.Empty;
                    using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(stream))
                        while (!reader.EndOfStream)
                        {

                            var jsonString = await reader.ReadLineAsync();
                            if (!bDump)
                            {
                                if (string.IsNullOrEmpty(jsonString) || !jsonString.StartsWith("data: {"))
                                    continue;
                                string json = jsonString.Substring(jsonString.IndexOf('{'), jsonString.LastIndexOf('}') - jsonString.IndexOf('{') + 1);

                                // Convert to object
                                ChatCompletionChunk x = JsonConvert.DeserializeObject<ChatCompletionChunk>(json);

                                //s += chatCompletionChunk.Choices[0].Delta.ToString();
                                MessageRaised(null,x?.Choices[0]?.Delta?.Content);
                                //this._contextId = x.Id;
                            }
                            else
                            {
                                MessageRaised(null, jsonString);
                            }

                        }

                   
                }
            }
            return true;

        }

        public void Dispose()
        {
            ;
        }



    }
}
