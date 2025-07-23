namespace UTS_SpaceForce
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panelLobby = new Panel();
            btnExit = new Button();
            btnTutorial = new Button();
            btnStart = new Button();
            panelPause = new Panel();
            btnBuyRepair = new Button();
            pictureBox4 = new PictureBox();
            FinalCoinTx = new Label();
            FinalScoreTx = new Label();
            pictureBox6 = new PictureBox();
            pictureBox5 = new PictureBox();
            btnResume = new Button();
            btnBackToLobby = new Button();
            panelGame = new Panel();
            CoinTx = new Label();
            pictureBox3 = new PictureBox();
            ScoreTx = new Label();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            btnPause = new Button();
            gameTimer = new System.Windows.Forms.Timer(components);
            panelLobby.SuspendLayout();
            panelPause.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            panelGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panelLobby
            // 
            panelLobby.BackgroundImage = (Image)resources.GetObject("panelLobby.BackgroundImage");
            panelLobby.Controls.Add(btnExit);
            panelLobby.Controls.Add(btnTutorial);
            panelLobby.Controls.Add(btnStart);
            panelLobby.Location = new Point(3, 2);
            panelLobby.Name = "panelLobby";
            panelLobby.Size = new Size(1177, 750);
            panelLobby.TabIndex = 0;
            // 
            // btnExit
            // 
            btnExit.Image = (Image)resources.GetObject("btnExit.Image");
            btnExit.Location = new Point(560, 644);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(143, 48);
            btnExit.TabIndex = 2;
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnTutorial
            // 
            btnTutorial.Image = (Image)resources.GetObject("btnTutorial.Image");
            btnTutorial.Location = new Point(560, 590);
            btnTutorial.Name = "btnTutorial";
            btnTutorial.Size = new Size(143, 48);
            btnTutorial.TabIndex = 1;
            btnTutorial.UseVisualStyleBackColor = true;
            btnTutorial.Click += btnTutorial_Click;
            // 
            // btnStart
            // 
            btnStart.Image = (Image)resources.GetObject("btnStart.Image");
            btnStart.Location = new Point(560, 536);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(143, 48);
            btnStart.TabIndex = 0;
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // panelPause
            // 
            panelPause.BackgroundImage = (Image)resources.GetObject("panelPause.BackgroundImage");
            panelPause.BackgroundImageLayout = ImageLayout.Stretch;
            panelPause.Controls.Add(btnBuyRepair);
            panelPause.Controls.Add(pictureBox4);
            panelPause.Controls.Add(FinalCoinTx);
            panelPause.Controls.Add(FinalScoreTx);
            panelPause.Controls.Add(pictureBox6);
            panelPause.Controls.Add(pictureBox5);
            panelPause.Controls.Add(btnResume);
            panelPause.Controls.Add(btnBackToLobby);
            panelPause.Location = new Point(3, 2);
            panelPause.Name = "panelPause";
            panelPause.Size = new Size(1177, 750);
            panelPause.TabIndex = 3;
            panelPause.Visible = false;
            // 
            // btnBuyRepair
            // 
            btnBuyRepair.BackColor = Color.Transparent;
            btnBuyRepair.Image = (Image)resources.GetObject("btnBuyRepair.Image");
            btnBuyRepair.Location = new Point(686, 485);
            btnBuyRepair.Name = "btnBuyRepair";
            btnBuyRepair.Size = new Size(213, 99);
            btnBuyRepair.TabIndex = 7;
            btnBuyRepair.UseVisualStyleBackColor = false;
            btnBuyRepair.Click += BtnBuyRepair_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(253, 462);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(360, 142);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 6;
            pictureBox4.TabStop = false;
            // 
            // FinalCoinTx
            // 
            FinalCoinTx.AutoSize = true;
            FinalCoinTx.BackColor = Color.Transparent;
            FinalCoinTx.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point);
            FinalCoinTx.ForeColor = Color.WhiteSmoke;
            FinalCoinTx.Location = new Point(639, 93);
            FinalCoinTx.Name = "FinalCoinTx";
            FinalCoinTx.Size = new Size(70, 81);
            FinalCoinTx.TabIndex = 5;
            FinalCoinTx.Text = "0";
            // 
            // FinalScoreTx
            // 
            FinalScoreTx.AutoSize = true;
            FinalScoreTx.BackColor = Color.Transparent;
            FinalScoreTx.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point);
            FinalScoreTx.ForeColor = Color.WhiteSmoke;
            FinalScoreTx.Location = new Point(639, 25);
            FinalScoreTx.Name = "FinalScoreTx";
            FinalScoreTx.Size = new Size(70, 81);
            FinalScoreTx.TabIndex = 4;
            FinalScoreTx.Text = "0";
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.Transparent;
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(437, 49);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(196, 44);
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.TabIndex = 3;
            pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(437, 114);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(152, 44);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 2;
            pictureBox5.TabStop = false;
            // 
            // btnResume
            // 
            btnResume.Image = (Image)resources.GetObject("btnResume.Image");
            btnResume.Location = new Point(656, 675);
            btnResume.Name = "btnResume";
            btnResume.Size = new Size(257, 48);
            btnResume.TabIndex = 1;
            btnResume.UseVisualStyleBackColor = true;
            btnResume.Click += btnResume_Click;
            // 
            // btnBackToLobby
            // 
            btnBackToLobby.Image = (Image)resources.GetObject("btnBackToLobby.Image");
            btnBackToLobby.Location = new Point(274, 675);
            btnBackToLobby.Name = "btnBackToLobby";
            btnBackToLobby.Size = new Size(257, 48);
            btnBackToLobby.TabIndex = 0;
            btnBackToLobby.UseVisualStyleBackColor = true;
            btnBackToLobby.Click += btnBackToLobby_Click;
            // 
            // panelGame
            // 
            panelGame.BackColor = Color.Black;
            panelGame.BackgroundImageLayout = ImageLayout.Stretch;
            panelGame.Controls.Add(CoinTx);
            panelGame.Controls.Add(pictureBox3);
            panelGame.Controls.Add(ScoreTx);
            panelGame.Controls.Add(pictureBox2);
            panelGame.Controls.Add(pictureBox1);
            panelGame.Controls.Add(btnPause);
            panelGame.Dock = DockStyle.Fill;
            panelGame.Location = new Point(0, 0);
            panelGame.Name = "panelGame";
            panelGame.Size = new Size(1182, 753);
            panelGame.TabIndex = 4;
            panelGame.Visible = false;
            // 
            // CoinTx
            // 
            CoinTx.AutoSize = true;
            CoinTx.BackColor = Color.Transparent;
            CoinTx.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            CoinTx.ForeColor = Color.WhiteSmoke;
            CoinTx.Location = new Point(120, 88);
            CoinTx.Name = "CoinTx";
            CoinTx.Size = new Size(43, 50);
            CoinTx.TabIndex = 7;
            CoinTx.Text = "0";
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(12, 101);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(94, 28);
            pictureBox3.TabIndex = 6;
            pictureBox3.TabStop = false;
            // 
            // ScoreTx
            // 
            ScoreTx.AutoSize = true;
            ScoreTx.BackColor = Color.Transparent;
            ScoreTx.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            ScoreTx.ForeColor = Color.WhiteSmoke;
            ScoreTx.Location = new Point(120, 45);
            ScoreTx.Name = "ScoreTx";
            ScoreTx.Size = new Size(43, 50);
            ScoreTx.TabIndex = 5;
            ScoreTx.Text = "0";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(12, 57);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(124, 28);
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(48, 28);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // btnPause
            // 
            btnPause.Image = (Image)resources.GetObject("btnPause.Image");
            btnPause.Location = new Point(1055, 12);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(115, 48);
            btnPause.TabIndex = 1;
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // gameTimer
            // 
            gameTimer.Interval = 20;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1182, 753);
            Controls.Add(panelPause);
            Controls.Add(panelLobby);
            Controls.Add(panelGame);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            panelLobby.ResumeLayout(false);
            panelPause.ResumeLayout(false);
            panelPause.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            panelGame.ResumeLayout(false);
            panelGame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLobby;
        private Button btnExit;
        private Button btnTutorial;
        private Button btnStart;
        private Panel panelPause;
        private Button btnBackToLobby;
        private Panel panelGame;
        private System.Windows.Forms.Timer gameTimer;
        private Button btnPause;
        private PictureBox pictureBox1;
        private Button btnResume;
        private Label ScoreTx;
        private PictureBox pictureBox2;
        private Label CoinTx;
        private PictureBox pictureBox3;
        private PictureBox pictureBox6;
        private PictureBox pictureBox5;
        private Label FinalScoreTx;
        private Label FinalCoinTx;
        private PictureBox pictureBox4;
        private Button btnBuyRepair;
    }
}