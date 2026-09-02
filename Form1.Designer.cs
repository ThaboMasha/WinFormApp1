namespace WinFormApp1
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
            pictureBox1 = new PictureBox();
            rtbConversation = new TextBox();
            txtUserInput = new TextBox();
            btnSend = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(152, 66);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // rtbConversation
            // 
            rtbConversation.BackColor = SystemColors.Window;
            rtbConversation.ForeColor = SystemColors.WindowText;
            rtbConversation.Location = new Point(180, 12);
            rtbConversation.Multiline = true;
            rtbConversation.Name = "rtbConversation";
            rtbConversation.ReadOnly = true;
            rtbConversation.ScrollBars = ScrollBars.Vertical;
            rtbConversation.Size = new Size(592, 380);
            rtbConversation.TabIndex = 4;
            // 
            // txtUserInput
            // 
            txtUserInput.Location = new Point(180, 405);
            txtUserInput.Name = "txtUserInput";
            txtUserInput.Size = new Size(500, 23);
            txtUserInput.TabIndex = 5;
            txtUserInput.Text = "hi";
            txtUserInput.KeyDown += txtUserInput_KeyDown;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(686, 404);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(86, 25);
            btnSend.TabIndex = 6;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSend);
            Controls.Add(txtUserInput);
            Controls.Add(rtbConversation);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "CyberSecureBot";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private TextBox rtbConversation;
        private TextBox txtUserInput;
        private Button btnSend;
    }
}