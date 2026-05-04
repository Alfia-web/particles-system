namespace particle_system
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.picDisplay = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tbDirection = new System.Windows.Forms.TrackBar();
            this.lbDirection = new System.Windows.Forms.Label();
            this.tbGravition = new System.Windows.Forms.TrackBar();
            this.tbGravition2 = new System.Windows.Forms.TrackBar();
            this.score = new System.Windows.Forms.Label();
            this.txbScore = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDirection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGravition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGravition2)).BeginInit();
            this.SuspendLayout();
            // 
            // picDisplay
            // 
            this.picDisplay.BackgroundImage = global::particle_system.Properties.Resources.b137314d29b249a7d78a2c1243c4063c;
            this.picDisplay.Location = new System.Drawing.Point(3, 2);
            this.picDisplay.Name = "picDisplay";
            this.picDisplay.Size = new System.Drawing.Size(661, 746);
            this.picDisplay.TabIndex = 0;
            this.picDisplay.TabStop = false;
            this.picDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picDisplay_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tbDirection
            // 
            this.tbDirection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tbDirection.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.tbDirection.Location = new System.Drawing.Point(670, 141);
            this.tbDirection.Maximum = 400;
            this.tbDirection.Name = "tbDirection";
            this.tbDirection.Size = new System.Drawing.Size(104, 56);
            this.tbDirection.TabIndex = 1;
            this.tbDirection.TabStop = false;
            this.tbDirection.Scroll += new System.EventHandler(this.tbDirection_Scroll);
            // 
            // lbDirection
            // 
            this.lbDirection.AutoSize = true;
            this.lbDirection.Location = new System.Drawing.Point(780, 141);
            this.lbDirection.Name = "lbDirection";
            this.lbDirection.Size = new System.Drawing.Size(44, 16);
            this.lbDirection.TabIndex = 2;
            this.lbDirection.Text = "label1";
            // 
            // tbGravition
            // 
            this.tbGravition.Location = new System.Drawing.Point(670, 203);
            this.tbGravition.Maximum = 100;
            this.tbGravition.Name = "tbGravition";
            this.tbGravition.Size = new System.Drawing.Size(130, 56);
            this.tbGravition.TabIndex = 3;
            // 
            // tbGravition2
            // 
            this.tbGravition2.Location = new System.Drawing.Point(670, 265);
            this.tbGravition2.Maximum = 575;
            this.tbGravition2.Name = "tbGravition2";
            this.tbGravition2.Size = new System.Drawing.Size(116, 56);
            this.tbGravition2.TabIndex = 4;
            // 
            // score
            // 
            this.score.AutoSize = true;
            this.score.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.score.ForeColor = System.Drawing.Color.Navy;
            this.score.Location = new System.Drawing.Point(670, 18);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(133, 16);
            this.score.TabIndex = 7;
            this.score.Text = "Твой счёт, капитан";
            // 
            // txbScore
            // 
            this.txbScore.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txbScore.Location = new System.Drawing.Point(670, 37);
            this.txbScore.Name = "txbScore";
            this.txbScore.Size = new System.Drawing.Size(130, 22);
            this.txbScore.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 756);
            this.Controls.Add(this.txbScore);
            this.Controls.Add(this.score);
            this.Controls.Add(this.tbGravition2);
            this.Controls.Add(this.tbGravition);
            this.Controls.Add(this.lbDirection);
            this.Controls.Add(this.tbDirection);
            this.Controls.Add(this.picDisplay);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDirection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGravition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGravition2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picDisplay;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar tbDirection;
        private System.Windows.Forms.Label lbDirection;
        private System.Windows.Forms.TrackBar tbGravition;
        private System.Windows.Forms.TrackBar tbGravition2;
        private System.Windows.Forms.Label score;
        private System.Windows.Forms.TextBox txbScore;
    }
}

