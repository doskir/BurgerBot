namespace BurgerBot
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
            this.axShockwaveFlash1 = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.RunBotButton = new System.Windows.Forms.Button();
            this.StopBotButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.LogButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).BeginInit();
            this.SuspendLayout();
            // 
            // axShockwaveFlash1
            // 
            this.axShockwaveFlash1.Enabled = true;
            this.axShockwaveFlash1.Location = new System.Drawing.Point(13, 13);
            this.axShockwaveFlash1.Name = "axShockwaveFlash1";
            this.axShockwaveFlash1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash1.OcxState")));
            this.axShockwaveFlash1.Size = new System.Drawing.Size(640, 480);
            this.axShockwaveFlash1.TabIndex = 5;
            // 
            // RunBotButton
            // 
            this.RunBotButton.Location = new System.Drawing.Point(660, 13);
            this.RunBotButton.Name = "RunBotButton";
            this.RunBotButton.Size = new System.Drawing.Size(116, 125);
            this.RunBotButton.TabIndex = 6;
            this.RunBotButton.Text = "Run Bot";
            this.RunBotButton.UseVisualStyleBackColor = true;
            this.RunBotButton.Click += new System.EventHandler(this.RunBotButton_Click);
            // 
            // StopBotButton
            // 
            this.StopBotButton.Location = new System.Drawing.Point(660, 395);
            this.StopBotButton.Name = "StopBotButton";
            this.StopBotButton.Size = new System.Drawing.Size(116, 98);
            this.StopBotButton.TabIndex = 10;
            this.StopBotButton.Text = "Abort Bot";
            this.StopBotButton.UseVisualStyleBackColor = true;
            this.StopBotButton.Click += new System.EventHandler(this.StopBotButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(660, 294);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(183, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Continue Playing after day is over";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // LogButton
            // 
            this.LogButton.Location = new System.Drawing.Point(660, 341);
            this.LogButton.Name = "LogButton";
            this.LogButton.Size = new System.Drawing.Size(114, 48);
            this.LogButton.TabIndex = 13;
            this.LogButton.Text = "Log";
            this.LogButton.UseVisualStyleBackColor = true;
            this.LogButton.Visible = false;
            this.LogButton.Click += new System.EventHandler(this.LogButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(660, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Select your save manually.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(660, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Hit Run Bot";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(660, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Don\'t click anywhere in the game";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(660, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "or in the Bot window.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(660, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "You need to finish the tutorial level";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(660, 260);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(167, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "(Day1) manually, it breaks the bot.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 499);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LogButton);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.StopBotButton);
            this.Controls.Add(this.RunBotButton);
            this.Controls.Add(this.axShockwaveFlash1);
            this.Name = "Form1";
            this.Text = "BurgerBot";
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash1;
        private System.Windows.Forms.Button RunBotButton;
        private System.Windows.Forms.Button StopBotButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button LogButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
    }
}

