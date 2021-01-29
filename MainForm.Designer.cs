using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvader
{
    partial class MainForm
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
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            timer1 = new Timer(components);
            timer2 = new Timer(components);
            Player = new PictureBox();
            timer3 = new Timer(components);
            timer4 = new Timer(components);
            timer5 = new Timer(components);
            Observer = new Timer(components);
            label1 = new Label();
            life1 = new PictureBox();
            life2 = new PictureBox();
            label2 = new Label();
            finish = new Label();
            ((ISupportInitialize)(Player)).BeginInit();
            ((ISupportInitialize)(life1)).BeginInit();
            ((ISupportInitialize)(life2)).BeginInit();
            SuspendLayout();

            //timer1
            timer1.Enabled = true;
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(PlayerMove);

            //timer2
            timer2.Enabled = true;
            timer2.Interval = 10;
            timer2.Tick += new EventHandler(FireBullet);

            //Player
            Player.BackgroundImage = global::SpaceInvader.Properties.Resources.tank;
            Player.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            Player.Location = new Point(360, 650);
            Player.Name = "Player";
            Player.Size = new Size(50, 50);
            Player.TabIndex = 0;
            Player.TabStop = false;

            // timer3
            timer3.Enabled = true;
            timer3.Interval = 10;
            timer3.Tick += new EventHandler(MoveAliens);

            //timer4
            timer4.Enabled = true;
            timer4.Interval = 1500;
            timer4.Tick += new EventHandler(StrikeSpan);

            //timer5
            timer5.Enabled = true;
            timer5.Interval = 1;
            timer5.Tick += new EventHandler(DetectLaser);

            //Observer
            Observer.Interval = 1;
            Observer.Tick += new EventHandler(Observe);

            //label1
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(238)));
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 710);
            label1.Name = "label1";
            label1.Size = new Size(55, 20);
            label1.TabIndex = 1;
            label1.Text = "Lives: ";

            //life1
            life1.BackgroundImage = (Image)(resources.GetObject("Life1.BackgroundImage"));
            life1.BackgroundImageLayout = ImageLayout.Stretch;
            life1.Location = new Point(85, 710);
            life1.Name = "Life1";
            life1.Size = new Size(30, 30);
            life1.TabIndex = 2;
            life1.TabStop = false;

            //life2
            life2.BackgroundImage = (Image)(resources.GetObject("Life1.BackgroundImage"));
            life2.BackgroundImageLayout = ImageLayout.Stretch;
            life2.Location = new Point(121, 710);
            life2.Name = "Life2";
            life2.Size = new Size(30, 30);
            life2.TabIndex = 2;
            life2.TabStop = false;

            //label2
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            label2.ForeColor = Color.White;
            label2.Location = new Point(670, 710);
            label2.Name = "label2";
            label2.TabIndex = 6;
            label2.Text = "Score: 0";

            //Finish
            finish.AutoSize = true;
            finish.Font = new Font("Sitka Small", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            finish.ForeColor = Color.White;
            finish.Location = new Point(286, 285);
            finish.Name = "Finish";
            finish.Size = new Size(0, 52);
            finish.TabIndex = 7;

            //MainForm
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new System.Drawing.Size(784, 761);
            Controls.Add(finish);
            Controls.Add(label2);
            Controls.Add(Player);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(life1);
            Controls.Add(life2);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Space Invaders";
            KeyDown += new KeyEventHandler(Pressed);
            KeyUp += new KeyEventHandler(Released);
            ((ISupportInitialize)(Player)).EndInit();
            ((ISupportInitialize)(life1)).EndInit();
            ((ISupportInitialize)(life2)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox Player;
        private Timer timer1;
        private Timer timer2;
        private Timer timer3;
        private Timer timer4;
        private Timer timer5;
        private Timer Observer;
        private PictureBox life1;
        private PictureBox life2;
        private Label label1;
        private Label label2;
        private Label finish;
    }
}

