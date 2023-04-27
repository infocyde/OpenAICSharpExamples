
namespace OpenAPI_Call
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtPrompt = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.chkDumpClass = new System.Windows.Forms.CheckBox();
            this.txtResult2 = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPrompt
            // 
            this.txtPrompt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPrompt.Location = new System.Drawing.Point(97, 30);
            this.txtPrompt.Multiline = true;
            this.txtPrompt.Name = "txtPrompt";
            this.txtPrompt.Size = new System.Drawing.Size(578, 63);
            this.txtPrompt.TabIndex = 1;
            this.txtPrompt.Text = "Your text here...";
            this.txtPrompt.TextChanged += new System.EventHandler(this.txtPrompt_TextChanged);
            this.txtPrompt.Enter += new System.EventHandler(this.txtPrompt_Enter);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.Tomato;
            this.btnSend.Location = new System.Drawing.Point(694, 30);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 37);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send!";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // chkDumpClass
            // 
            this.chkDumpClass.AutoSize = true;
            this.chkDumpClass.ForeColor = System.Drawing.Color.White;
            this.chkDumpClass.Location = new System.Drawing.Point(694, 75);
            this.chkDumpClass.Name = "chkDumpClass";
            this.chkDumpClass.Size = new System.Drawing.Size(58, 17);
            this.chkDumpClass.TabIndex = 3;
            this.chkDumpClass.Text = "Debug";
            this.chkDumpClass.UseVisualStyleBackColor = true;
            // 
            // txtResult2
            // 
            this.txtResult2.Location = new System.Drawing.Point(21, 114);
            this.txtResult2.Name = "txtResult2";
            this.txtResult2.ReadOnly = true;
            this.txtResult2.Size = new System.Drawing.Size(748, 451);
            this.txtResult2.TabIndex = 4;
            this.txtResult2.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::OpenAPI_Call.Properties.Resources.infocyde_cyberpunk_hacker_of_the_future_illustrated_drawing_vec_4445c8f4_7446_4133_8fa6_7c3145d2c16a;
            this.pictureBox1.Location = new System.Drawing.Point(21, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(63, 62);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(800, 585);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtResult2);
            this.Controls.Add(this.chkDumpClass);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtPrompt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Open API Demo v .03";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtPrompt;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.CheckBox chkDumpClass;
        private System.Windows.Forms.RichTextBox txtResult2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

