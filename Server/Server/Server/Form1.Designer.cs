namespace Server
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
            this.rtbMessages = new System.Windows.Forms.RichTextBox();
            this.btnPing = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCurrentSpeaker = new System.Windows.Forms.Label();
            this.btnTurnOffCurrentSpeaker = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbMessages
            // 
            this.rtbMessages.HideSelection = false;
            this.rtbMessages.Location = new System.Drawing.Point(13, 13);
            this.rtbMessages.Name = "rtbMessages";
            this.rtbMessages.Size = new System.Drawing.Size(359, 132);
            this.rtbMessages.TabIndex = 0;
            this.rtbMessages.Text = "";
            // 
            // btnPing
            // 
            this.btnPing.Location = new System.Drawing.Point(13, 152);
            this.btnPing.Name = "btnPing";
            this.btnPing.Size = new System.Drawing.Size(75, 39);
            this.btnPing.TabIndex = 1;
            this.btnPing.Text = "Ping Clients";
            this.btnPing.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Current speaker:";
            // 
            // lblCurrentSpeaker
            // 
            this.lblCurrentSpeaker.AutoSize = true;
            this.lblCurrentSpeaker.Location = new System.Drawing.Point(188, 165);
            this.lblCurrentSpeaker.Name = "lblCurrentSpeaker";
            this.lblCurrentSpeaker.Size = new System.Drawing.Size(33, 13);
            this.lblCurrentSpeaker.TabIndex = 3;
            this.lblCurrentSpeaker.Text = "None";
            // 
            // btnTurnOffCurrentSpeaker
            // 
            this.btnTurnOffCurrentSpeaker.Location = new System.Drawing.Point(108, 182);
            this.btnTurnOffCurrentSpeaker.Name = "btnTurnOffCurrentSpeaker";
            this.btnTurnOffCurrentSpeaker.Size = new System.Drawing.Size(136, 23);
            this.btnTurnOffCurrentSpeaker.TabIndex = 4;
            this.btnTurnOffCurrentSpeaker.Text = "Turn off current speaker";
            this.btnTurnOffCurrentSpeaker.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(464, 401);
            this.Controls.Add(this.btnTurnOffCurrentSpeaker);
            this.Controls.Add(this.lblCurrentSpeaker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPing);
            this.Controls.Add(this.rtbMessages);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbMessages;
        private System.Windows.Forms.Button btnPing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurrentSpeaker;
        private System.Windows.Forms.Button btnTurnOffCurrentSpeaker;
    }
}

