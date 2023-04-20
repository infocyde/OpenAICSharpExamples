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

        
        async void MakeChatCall()
        {
            btnSend.Text = "Sending...";
            this.txtResult.Text = "";
            btnSend.Update();
            txtResult.Update();
            using(OpenAI oa = new OpenAI())
            {
                this.txtResult.Text = await oa.SimpleChatCall2(this.txtPrompt.Text, this.chkDumpClass.Checked);
            }
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
    }
}
