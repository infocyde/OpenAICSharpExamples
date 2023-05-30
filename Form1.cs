using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenAPI_Call
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        List<Dictionary<string, string>> chatContext = new List<Dictionary<string, string>>();

        void BuildContext(string prompt, string sresponse)
        {
            if (chatContext.Count() == 0)
            {

                chatContext.Add(new Dictionary<string, string>()
                {
                    { "role", "assistant" },
                    { "content", ("You are a grumpy, sarcastic arrogant hacker, you are very anti-woke and not politically correct,  but helpful.").compact() }
                });
            }

            while (chatContext.Count > 13) // x -1 / 2 = number of convos held in context.  More > tolkens
            {
                chatContext.RemoveAt(1);
            }
            if (!string.IsNullOrEmpty(prompt))
                chatContext.Add(new Dictionary<string, string>()
                {
                    {"role","user" },
                    {"content",prompt.compact() }
                });
            if (!string.IsNullOrEmpty(sresponse))
                chatContext.Add(new Dictionary<string, string>()
                {
                    {"role","assistant" },
                    {"content",sresponse.compact() }
                });

            
        }


        async void ResetConversation()
        {
            this.txtPrompt.Text = string.Empty;
            this.txtPrompt.Update();
            this.txtResult2.Text = string.Empty;
            this.txtResult2.Update();
            this.chatContext.Clear();
            BuildContext("Hello",""); 
            InitialGreeting();

        }

        async void InitialGreeting()
        {
            using (OpenAI oa = new OpenAI())
            {
                Logger.AppendChatLog("New Conversation " + DateTime.Now.ToString() + "\\n----\\n\\nn ", "");
                oa.MessageRaised += (sender, message) => { this.txtResult2.Text += message; this.txtResult2.Update(); };
                await oa.SimpleChatCall2("Give me a short, random, sarcastic, arrogant, greeting.", chatContext, this.chkDumpClass.Checked);
                if (chatContext.Count() > 1)
                    chatContext.RemoveAt(1); // nuke the greeting
            }
        }


        async void MakeChatCall()
        {
            btnSend.Text = "Sending...";
            this.txtResult2.Text = "";
            btnSend.Update();
            txtResult2.Update();
            using (OpenAI oa = new OpenAI())
            {
                oa.MessageRaised += (sender, message) => { this.txtResult2.Text += message; this.txtResult2.Update(); };
                await oa.SimpleChatCall2(this.txtPrompt.Text, chatContext, this.chkDumpClass.Checked);
            }
            BuildContext(txtPrompt.Text, txtResult2.Text);
            btnSend.Text = "Sent!";
            btnSend.Update();

        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            MakeChatCall();
        }

        private void txtPrompt_TextChanged(object sender, EventArgs e)
        {
            ;
        }

        private void txtPrompt_Enter(object sender, EventArgs e)
        {
            if (this.txtPrompt.Text == "Your text here...")
            {
                this.txtPrompt.Text = string.Empty;
                txtPrompt.Update();
            }
        }





        private void Form1_Load(object sender, EventArgs e)
        {
            BuildContext("", "");
            InitialGreeting();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            ResetConversation();
        }
    }
}
