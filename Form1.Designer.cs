using System.Windows.Forms;

namespace tanks2._0
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.infolabel1 = new System.Windows.Forms.Label();
            this.infolabel2 = new System.Windows.Forms.Label();
            this.infolabel3 = new System.Windows.Forms.Label();
            this.infolabel4 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.fieldPictureBox = new System.Windows.Forms.PictureBox();
            this.Player2Info = new System.Windows.Forms.Label();
            this.player1Info = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.fieldPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // infolabel1
            // 
            this.infolabel1.AutoSize = true;
            this.infolabel1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.infolabel1.Location = new System.Drawing.Point(155, 145);
            this.infolabel1.Name = "infolabel1";
            this.infolabel1.Size = new System.Drawing.Size(86, 25);
            this.infolabel1.TabIndex = 0;
            this.infolabel1.Text = "Игрок 1";
            // 
            // infolabel2
            // 
            this.infolabel2.AutoSize = true;
            this.infolabel2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.infolabel2.Location = new System.Drawing.Point(656, 145);
            this.infolabel2.Name = "infolabel2";
            this.infolabel2.Size = new System.Drawing.Size(86, 25);
            this.infolabel2.TabIndex = 1;
            this.infolabel2.Text = "Игрок 2";
            // 
            // infolabel3
            // 
            this.infolabel3.AutoSize = true;
            this.infolabel3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.infolabel3.Location = new System.Drawing.Point(133, 214);
            this.infolabel3.Name = "infolabel3";
            this.infolabel3.Size = new System.Drawing.Size(136, 40);
            this.infolabel3.TabIndex = 2;
            this.infolabel3.Text = "w,a,s,d - движение\r\n\"space\" - огонь";
            // 
            // infolabel4
            // 
            this.infolabel4.AutoSize = true;
            this.infolabel4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.infolabel4.Location = new System.Drawing.Point(627, 214);
            this.infolabel4.Name = "infolabel4";
            this.infolabel4.Size = new System.Drawing.Size(147, 40);
            this.infolabel4.TabIndex = 3;
            this.infolabel4.Text = "стрелки - движение\r\n\"enter\" - огонь\r\n";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(406, 318);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(93, 23);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Начать игру";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // fieldPictureBox
            // 
            this.fieldPictureBox.Location = new System.Drawing.Point(8, 41);
            this.fieldPictureBox.Name = "fieldPictureBox";
            this.fieldPictureBox.Size = new System.Drawing.Size(1024, 576);
            this.fieldPictureBox.TabIndex = 5;
            this.fieldPictureBox.TabStop = false;
            this.fieldPictureBox.Visible = false;
            // 
            // Player2Info
            // 
            this.Player2Info.AutoSize = true;
            this.Player2Info.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Player2Info.ForeColor = System.Drawing.Color.Blue;
            this.Player2Info.Location = new System.Drawing.Point(844, 9);
            this.Player2Info.Name = "Player2Info";
            this.Player2Info.Size = new System.Drawing.Size(96, 25);
            this.Player2Info.TabIndex = 6;
            this.Player2Info.Text = "Игрок 2: ";
            this.Player2Info.Visible = false;
            // 
            // player1Info
            // 
            this.player1Info.AutoSize = true;
            this.player1Info.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.player1Info.ForeColor = System.Drawing.Color.Red;
            this.player1Info.Location = new System.Drawing.Point(51, 9);
            this.player1Info.Name = "player1Info";
            this.player1Info.Size = new System.Drawing.Size(96, 25);
            this.player1Info.TabIndex = 6;
            this.player1Info.Text = "Игрок 1: ";
            this.player1Info.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 629);
            this.Controls.Add(this.player1Info);
            this.Controls.Add(this.Player2Info);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.infolabel4);
            this.Controls.Add(this.infolabel3);
            this.Controls.Add(this.infolabel2);
            this.Controls.Add(this.infolabel1);
            this.Controls.Add(this.fieldPictureBox);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.fieldPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label infolabel1;
        private System.Windows.Forms.Label infolabel2;
        private System.Windows.Forms.Label infolabel3;
        private System.Windows.Forms.Label infolabel4;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.PictureBox fieldPictureBox;
        private System.Windows.Forms.Label Player2Info;
        private System.Windows.Forms.Label player1Info;
        private System.Windows.Forms.Timer timer1;
    }
}

