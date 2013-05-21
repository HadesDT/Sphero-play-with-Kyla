namespace OSCmessageSender
{
    partial class OSCmsgSend
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.LTitre = new System.Windows.Forms.Label();
            this.TBIp = new System.Windows.Forms.TextBox();
            this.TBPort = new System.Windows.Forms.TextBox();
            this.LIp = new System.Windows.Forms.Label();
            this.LPort = new System.Windows.Forms.Label();
            this.LMsg = new System.Windows.Forms.Label();
            this.TBValue = new System.Windows.Forms.TextBox();
            this.TBMsg = new System.Windows.Forms.TextBox();
            this.LValue = new System.Windows.Forms.Label();
            this.BSend = new System.Windows.Forms.Button();
            this.CBString = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LTitre
            // 
            this.LTitre.AutoSize = true;
            this.LTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LTitre.Location = new System.Drawing.Point(143, 9);
            this.LTitre.Name = "LTitre";
            this.LTitre.Size = new System.Drawing.Size(176, 20);
            this.LTitre.TabIndex = 0;
            this.LTitre.Text = "OSCmessageSender";
            // 
            // TBIp
            // 
            this.TBIp.Location = new System.Drawing.Point(46, 53);
            this.TBIp.Name = "TBIp";
            this.TBIp.Size = new System.Drawing.Size(173, 20);
            this.TBIp.TabIndex = 1;
            // 
            // TBPort
            // 
            this.TBPort.Location = new System.Drawing.Point(353, 53);
            this.TBPort.Name = "TBPort";
            this.TBPort.Size = new System.Drawing.Size(100, 20);
            this.TBPort.TabIndex = 2;
            // 
            // LIp
            // 
            this.LIp.AutoSize = true;
            this.LIp.Location = new System.Drawing.Point(23, 56);
            this.LIp.Name = "LIp";
            this.LIp.Size = new System.Drawing.Size(17, 13);
            this.LIp.TabIndex = 3;
            this.LIp.Text = "IP";
            // 
            // LPort
            // 
            this.LPort.AutoSize = true;
            this.LPort.Location = new System.Drawing.Point(304, 56);
            this.LPort.Name = "LPort";
            this.LPort.Size = new System.Drawing.Size(26, 13);
            this.LPort.TabIndex = 4;
            this.LPort.Text = "Port";
            // 
            // LMsg
            // 
            this.LMsg.AutoSize = true;
            this.LMsg.Location = new System.Drawing.Point(12, 111);
            this.LMsg.Name = "LMsg";
            this.LMsg.Size = new System.Drawing.Size(31, 13);
            this.LMsg.TabIndex = 6;
            this.LMsg.Text = "MSG";
            // 
            // TBValue
            // 
            this.TBValue.Location = new System.Drawing.Point(50, 150);
            this.TBValue.Name = "TBValue";
            this.TBValue.Size = new System.Drawing.Size(239, 20);
            this.TBValue.TabIndex = 7;
            // 
            // TBMsg
            // 
            this.TBMsg.Location = new System.Drawing.Point(50, 108);
            this.TBMsg.Name = "TBMsg";
            this.TBMsg.Size = new System.Drawing.Size(403, 20);
            this.TBMsg.TabIndex = 8;
            // 
            // LValue
            // 
            this.LValue.AutoSize = true;
            this.LValue.Location = new System.Drawing.Point(12, 153);
            this.LValue.Name = "LValue";
            this.LValue.Size = new System.Drawing.Size(34, 13);
            this.LValue.TabIndex = 9;
            this.LValue.Text = "Value";
            // 
            // BSend
            // 
            this.BSend.Location = new System.Drawing.Point(407, 148);
            this.BSend.Name = "BSend";
            this.BSend.Size = new System.Drawing.Size(75, 23);
            this.BSend.TabIndex = 10;
            this.BSend.Text = "Send";
            this.BSend.UseVisualStyleBackColor = true;
            this.BSend.Click += new System.EventHandler(this.BSend_Click);
            // 
            // CBString
            // 
            this.CBString.AutoSize = true;
            this.CBString.Location = new System.Drawing.Point(328, 152);
            this.CBString.Name = "CBString";
            this.CBString.Size = new System.Drawing.Size(51, 17);
            this.CBString.TabIndex = 11;
            this.CBString.Text = "string";
            this.CBString.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(46, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Send Int";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(147, 218);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Send f i";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(244, 218);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "Send b";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(340, 218);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 15;
            this.button4.Text = "Send bundle";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(427, 218);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 16;
            this.button5.Text = "test old";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // OSCmsgSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 263);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CBString);
            this.Controls.Add(this.BSend);
            this.Controls.Add(this.LValue);
            this.Controls.Add(this.TBMsg);
            this.Controls.Add(this.TBValue);
            this.Controls.Add(this.LMsg);
            this.Controls.Add(this.LPort);
            this.Controls.Add(this.LIp);
            this.Controls.Add(this.TBPort);
            this.Controls.Add(this.TBIp);
            this.Controls.Add(this.LTitre);
            this.Name = "OSCmsgSend";
            this.Text = "OSCmsgSender";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LTitre;
        private System.Windows.Forms.TextBox TBIp;
        private System.Windows.Forms.TextBox TBPort;
        private System.Windows.Forms.Label LIp;
        private System.Windows.Forms.Label LPort;
        private System.Windows.Forms.Label LMsg;
        private System.Windows.Forms.TextBox TBValue;
        private System.Windows.Forms.TextBox TBMsg;
        private System.Windows.Forms.Label LValue;
        private System.Windows.Forms.Button BSend;
        private System.Windows.Forms.CheckBox CBString;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

