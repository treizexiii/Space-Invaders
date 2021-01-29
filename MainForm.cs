using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            new Enemies().CreateSprites(this);
            InsertAlien();
        }

        List<PictureBox> aliens = new List<PictureBox>();
        List<PictureBox> delay = new List<PictureBox>();

        const int x = 360, y = 650;
        const int limit = 730;

        int top = 0;
        int left = -1;
        int speed = -1;
        int count = 0;
        int pts = 0;

        bool game = true;
        bool moveLeft;
        bool moveRight;
        bool fired;

        private void Pressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q || e.KeyCode == Keys.Left)
            {
                moveLeft = true;
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                moveRight = true;
            }
            if (e.KeyCode == Keys.Space && game && !fired)
            {
                fired = true;
                Missile();
            }
        }

        private void Released(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q || e.KeyCode == Keys.Left)
            {
                moveLeft = false;
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                moveRight = false;
            }
            else if (e.KeyCode == Keys.Space)
            {
                fired = false;
            }
        }

        private void PlayerMove(object sender, EventArgs e)
        {
            if (moveLeft && Player.Location.X >= 0)
            {
                Player.Left--;
            }
            if (moveRight && Player.Location.X <= limit)
            {
                Player.Left++;
            }
        }

        //Create a player's missile in controls
        private void Missile()
        {
            PictureBox bullet = new PictureBox();
            bullet.Location = new Point(Player.Location.X + Player.Width / 2, Player.Location.Y - 20);
            bullet.Size = new Size(5, 20);
            bullet.BackgroundImage = Properties.Resources.bullet;
            bullet.BackgroundImageLayout = ImageLayout.Stretch;
            bullet.Name = "Bullet";
            Controls.Add(bullet);
        }

        //Add ogject alien to a list
        private void InsertAlien()
        {
            foreach (Control control in Controls)
            {
                if (control is PictureBox && control.Name == "Alien")
                {
                    PictureBox alien = (PictureBox)control;
                    aliens.Add(alien);
                }
            }
        }

        //Verify if alien touch the border
        private bool Touched(PictureBox alien)
        {
            return alien.Location.X <= 0 || alien.Location.X >= limit;
        }

        //Set the alien's direction, depends on Touched() value
        private void SetDirection(PictureBox alien)
        {
            int size = alien.Height;

            if (Touched(alien))
            {
                top = 1;
                left = 0;
                count++;

                if (count == size)
                {
                    top = 0;
                    left = speed * -1;
                    Observer.Start();
                }
                else if (count == size * 2)
                {
                    top = 0;
                    left = speed;
                    count = 0;
                    Observer.Start();
                }
            }
        }

        //Check collision with player
        private void Colllided(PictureBox alien)
        {
            if (alien.Bounds.IntersectsWith(Player.Bounds))
            {
                GameOver();
            }
        }

        private void AlienMove()
        {
            foreach (PictureBox alien in aliens)
            {
                alien.Location = new Point(alien.Location.X + left, alien.Location.Y + top);
                SetDirection(alien);
                Colllided(alien);
            }
        }

        private void MoveAliens(object sender, EventArgs e)
        {
            AlienMove();
        }

        //Create an alien's laser in controls
        private void Beam(PictureBox alien)
        {
            PictureBox laser = new PictureBox();
            laser.Location = new Point(alien.Location.X + alien.Width / 3, alien.Location.Y + 20);
            laser.Size = new Size(5, 20);
            laser.BackgroundImage = Properties.Resources.laser;
            laser.BackgroundImageLayout = ImageLayout.Stretch;
            laser.Name = "Laser";
            Controls.Add(laser);
        }

        //Select randomly an alien for Beam()
        private void StrikeSpan(object sender, EventArgs e)
        {
            Random r = new Random();
            int pick;
            if (aliens.Count > 0)
            {
                pick = r.Next(aliens.Count);
                Beam(aliens[pick]);
            }
        }

        private void DetectLaser(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is PictureBox && control.Name == "Laser")
                {
                    PictureBox laser = (PictureBox)control;
                    laser.Top += 5;
                    if (laser.Location.Y >= limit)
                    {
                        Controls.Remove(laser);
                    }
                    if (laser.Bounds.IntersectsWith(Player.Bounds))
                    {
                        Controls.Remove(laser);
                        LoseLife();
                    }
                }
            }
        }

        private void FireBullet(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is PictureBox && control.Name == "Bullet")
                {
                    PictureBox bullet = (PictureBox)control;
                    bullet.Top -= 5;

                    if (bullet.Location.Y <= 0)
                    {
                        Controls.Remove(bullet);
                    }
                    foreach (Control ct in Controls)
                    {
                        if (control is PictureBox && control.Name == "Laser")
                        {
                            PictureBox laser = (PictureBox)ct;
                            if (bullet.Bounds.IntersectsWith(laser.Bounds))
                            {
                                Controls.Remove(bullet);
                                Controls.Remove(laser);
                                pts++;
                                Score(pts);
                            }
                        }
                    }
                    foreach (Control crtl in Controls)
                    {
                        if (control is PictureBox && control.Name == "Alien")
                        {
                            PictureBox alien = (PictureBox)crtl;

                            if (bullet.Bounds.IntersectsWith(alien.Bounds) && !Touched(alien))
                            {
                                Controls.Remove(alien);
                                Controls.Remove(bullet);
                                aliens.Remove(alien);
                                pts += 5;
                                Score(pts);
                                CheckForWinner();
                            }
                            else if (bullet.Bounds.IntersectsWith(alien.Bounds) && Touched(alien))
                            {
                                Controls.Remove(alien);
                                Controls.Remove(bullet);
                                delay.Add(alien);
                                pts += 5;
                                Score(pts);
                                CheckForWinner();
                            }
                        }
                    }
                }
            }
        }

        private void Observe(object sender, EventArgs e)
        {
            Observer.Stop();

            foreach (PictureBox delayed in delay)
            {
                aliens.Remove(delayed);
            }
            delay.Clear();
        }

        private void Score(int pts)
        {
            label2.Text = "Score: " + pts.ToString();
        }

        private void CheckForWinner()
        {
            int count = 0;

            foreach (Control alien in Controls)
            {
                if (alien is PictureBox && alien.Name == "Alien")
                {
                    count++;
                }
                if (count == 0)
                {
                    YouWon();
                }
            }
        }

        private void YouWon()
        {
            game = false;
            foreach (Control finish in Controls)
            {
                if (finish is Label && finish.Name == "Finish")
                {
                    Label lbl = (Label)finish;
                    lbl.Text = "You won" + "\n" + "Score: " + pts.ToString();
                }
                else
                {
                    finish.Visible = false;
                }
            }
        }

        private void LoseLife()
        {
            Player.Location = new Point(x, y);
            
            foreach (Control life in Controls)
            {
                if (life is PictureBox && life.Name.Contains("Life") && life.Visible == true)
                {
                    PictureBox player = (PictureBox)life;
                    player.Visible = false;
                    return;
                }
            }
            GameOver();
        }

        private void GameOver()
        {
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            timer4.Stop();
            timer5.Stop();
            Observer.Stop();

            foreach (Control finish in Controls)
            {
                if (finish is Label && finish.Name == "Finish")
                {
                    Label lbl = (Label)finish;
                    lbl.Text = "Game Over";
                    game = false;
                }
                else
                {
                    finish.Visible = false;
                }
            }
        }
    }
}
