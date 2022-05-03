using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace tanks2._0
{
    public partial class Form1 : Form
    {
        public struct obstacle {
            public int x;
            public int y;
            public obstacle(int X, int Y)
            {
                x = X;
                y = Y;
            }
        };

        public struct tank
        {
            public int x;
            public int y;
            public int hp;
            public int direction; // 0 - up, 1 - right, 2 - down, 3 - left
            public bool move;

        }

        public struct bullet
        {
            public int x;
            public int y;
            public int direction;
            public bool active;
        }

        List<obstacle> listObstacles;

        private Bitmap picBitmap;
        private Bitmap staticBitmap;
        private Pen p;
        private SolidBrush b;
        private SolidBrush b1;
        private SolidBrush b2;
        private Graphics g;
        tank player1;
        tank player2;
        bullet bullet1;
        bullet bullet2;
        int setHp = 3;
        List<tank> players;
        int speed = 4;
        int tickIndex = 0;


        public Form1()
        {
            InitializeComponent();
            picBitmap = new Bitmap(fieldPictureBox.Width, fieldPictureBox.Height);
            listObstacles = new List<obstacle>(48);
            player1 = new tank();
            player2 = new tank();
            bullet1 = new bullet();
            bullet2 = new bullet();
            bullet1.active = false;
            bullet2.active = false;
            player1.hp = setHp;
            player2.hp = setHp;
            player1.move = false;
            player2.move = false;
            player1.direction = 1;
            player2.direction = 3;
            player1.x = 64;
            player1.y = 64;
            player2.x = 896;
            player2.y = 288;
            players = new List<tank> { player1, player2 };
            {                
                listObstacles.Add(new obstacle(9, 0));
                listObstacles.Add(new obstacle(9, 1));
                listObstacles.Add(new obstacle(9, 2));
                listObstacles.Add(new obstacle(9, 3));
                listObstacles.Add(new obstacle(9, 4));
                listObstacles.Add(new obstacle(8, 4));
                listObstacles.Add(new obstacle(7, 4));
                listObstacles.Add(new obstacle(6, 4));
                listObstacles.Add(new obstacle(13, 2));
                listObstacles.Add(new obstacle(14, 2));
                listObstacles.Add(new obstacle(15, 2));
                listObstacles.Add(new obstacle(16, 2));
                listObstacles.Add(new obstacle(17, 2));
                listObstacles.Add(new obstacle(18, 2));
                listObstacles.Add(new obstacle(26, 2));
                listObstacles.Add(new obstacle(26, 3));
                listObstacles.Add(new obstacle(3, 10));
                listObstacles.Add(new obstacle(3, 11));
                listObstacles.Add(new obstacle(4, 10));
                listObstacles.Add(new obstacle(4, 11));
                listObstacles.Add(new obstacle(8, 11));
                listObstacles.Add(new obstacle(8, 12));
                listObstacles.Add(new obstacle(8, 13));
                listObstacles.Add(new obstacle(9, 13));
                listObstacles.Add(new obstacle(12, 10));
                listObstacles.Add(new obstacle(13, 10));
                listObstacles.Add(new obstacle(13, 9));
                listObstacles.Add(new obstacle(13, 8));
                listObstacles.Add(new obstacle(13, 7));
                listObstacles.Add(new obstacle(14, 7));
                listObstacles.Add(new obstacle(14, 6));
                listObstacles.Add(new obstacle(17, 12));
                listObstacles.Add(new obstacle(17, 13));
                listObstacles.Add(new obstacle(20, 6));
                listObstacles.Add(new obstacle(21, 6));
                listObstacles.Add(new obstacle(20, 7));
                listObstacles.Add(new obstacle(20, 8));
                listObstacles.Add(new obstacle(20, 9));
                listObstacles.Add(new obstacle(21, 9));
                listObstacles.Add(new obstacle(23, 13));
                listObstacles.Add(new obstacle(26, 13));
                listObstacles.Add(new obstacle(26, 12));
                listObstacles.Add(new obstacle(23, 14));
                listObstacles.Add(new obstacle(24, 14));
                listObstacles.Add(new obstacle(25, 14));
                listObstacles.Add(new obstacle(26, 14));
                listObstacles.Add(new obstacle(23, 15));
                listObstacles.Add(new obstacle(24, 15));
                listObstacles.Add(new obstacle(25, 15));
                listObstacles.Add(new obstacle(26, 15));
            }
        }


        private void startButton_Click(object sender, EventArgs e)
        {   
            infolabel1.Visible = false;
            infolabel2.Visible = false;
            infolabel3.Visible = false;
            infolabel4.Visible = false;
            startButton.Visible = false;
            player1Info.Visible = true;
            Player2Info.Visible = true;
            fieldPictureBox.Visible = true;

            player1Info.Text = "Игрок 1:  " + setHp;
            Player2Info.Text = "Игрок 2:  " + setHp;

            p = new Pen(Color.Black, 2);
            b = new SolidBrush(Color.FromArgb(166, 242, 150));
            g = Graphics.FromImage(picBitmap);

            g.FillRectangle(b, 0, 0, fieldPictureBox.Width, fieldPictureBox.Height);

            b.Color = Color.FromArgb(92, 33, 4);

            for (int i = 0; i < 32; i++) // верхняя и нижняя граница
            {
                g.FillRectangle(b, i * 32, 0, 32, 32);
                g.DrawRectangle(p, i * 32, 0, 32, 32);
                g.FillRectangle(b, i * 32, 544, 32, 32);
                g.DrawRectangle(p, i * 32, 544, 32, 32);           
            }
            for (int i = 1; i < 17; i++) // левая и правая граница
            {
                g.FillRectangle(b, 0, i * 32, 32, 32);
                g.DrawRectangle(p, 0, i * 32, 32, 32);
                g.FillRectangle(b, 992, i * 32, 32, 32);
                g.DrawRectangle(p, 992, i * 32, 32, 32);               
            }

            foreach (var obst in listObstacles)
            {
                g.FillRectangle(b, 32 + obst.x * 32, 32 + obst.y * 32, 32, 32);
                g.DrawRectangle(p, 32 + obst.x * 32, 32 + obst.y * 32, 32, 32);
            }

            staticBitmap = (Bitmap)picBitmap.Clone();

            fieldPictureBox.Image = picBitmap;

            timer1.Enabled = true;
            render();

            
        }

        private void render()
        {
            //picBitmap.Dispose();
            picBitmap = (Bitmap)staticBitmap.Clone();
            g = Graphics.FromImage(picBitmap);
            p = new Pen(Color.Black, 2);
            b1 = new SolidBrush(Color.FromArgb(255, 0,0));
            b2 = new SolidBrush(Color.FromArgb(0,0,255));

            g.FillRectangle(b1, player1.x, player1.y, 32, 32);
            g.DrawRectangle(p, player1.x, player1.y, 32, 32);
            g.FillRectangle(b2, player2.x, player2.y, 32, 32);
            g.DrawRectangle(p, player2.x, player2.y, 32, 32);

            switch (player1.direction) 
            {
                case 0:
                    g.DrawLine(p, player1.x + 16, player1.y, player1.x + 16, player1.y - 16);
                    break;
                case 1:
                    g.DrawLine(p, player1.x + 32, player1.y + 16, player1.x + 48, player1.y + 16);
                    break;
                case 2:
                    g.DrawLine(p, player1.x + 16, player1.y + 32, player1.x + 16, player1.y + 48);
                    break;
                case 3:
                    g.DrawLine(p, player1.x, player1.y + 16, player1.x - 16, player1.y + 16);
                    break;
            }

            switch (player2.direction)
            {
                case 0:
                    g.DrawLine(p, player2.x + 16, player2.y, player2.x + 16, player2.y - 16);
                    break;
                case 1:
                    g.DrawLine(p, player2.x + 32, player2.y + 16, player2.x + 48, player2.y + 16);
                    break;
                case 2:
                    g.DrawLine(p, player2.x + 16, player2.y + 32, player2.x + 16, player2.y + 48);
                    break;
                case 3:
                    g.DrawLine(p, player2.x, player2.y + 16, player2.x - 16, player2.y + 16);
                    break;
            }

            fieldPictureBox.Image = picBitmap;
            tickIndex++;
            if (tickIndex > 50)
            {
                tickIndex = 0;
                GC.Collect();
            }
        }

        int getCoords(int t)
        {
            return 32 + t * 32;
        }

        private bool checkBorders(int x,int y, int direction)
        {
            switch (direction)
            {
                case 0:
                    if (y <= 32)
                        return false;
                    foreach (var obst in listObstacles)
                        if (y <= getCoords(obst.y)+32 && ((x + 32 >= getCoords(obst.x) && x+ 32 <= getCoords(obst.x) + 32)||(x >= getCoords(obst.x) && x <= getCoords(obst.x) + 32)))
                            return false;
                    break;
                case 1:
                    if (x + 32 >= 992)
                        return false;
                    foreach (var obst in listObstacles)
                        if (x + 32 >= getCoords(obst.x) && ((y + 32 >= getCoords(obst.y) && y + 32 <= getCoords(obst.y) + 32) || (y >= getCoords(obst.y) && y <= getCoords(obst.y) + 32)))
                            return false;
                    break;
                case 2:
                    if (y + 32 >= 544)
                        return false;
                    foreach (var obst in listObstacles)
                        if (y + 32 >=  getCoords(y) && ((x + 32 >= getCoords(obst.x) && x + 32 <= getCoords(obst.x) + 32) || (x >= getCoords(obst.x) && x <= getCoords(obst.x) + 32)))
                            return false;
                    break;
                case 3:
                    if (x <= 32)
                        return false;
                    foreach (var obst in listObstacles)
                        if (x <= getCoords(obst.x)+ 32 && ((x + 32 >= getCoords(obst.x) && x + 32 <= getCoords(obst.x) + 32) || (x >= getCoords(obst.x) && x <= getCoords(obst.x) + 32)))
                            return false;
                    break;
            }
            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (player1.move)
            {
                switch (player1.direction)
                {
                    case 0:
                        if (checkBorders(player1.x,player1.y,player1.direction))
                            player1.y -= speed;
                        break;
                    case 1:
                        if (checkBorders(player1.x, player1.y, player1.direction))                          
                            player1.x += speed;
                        break;
                    case 2:
                        if (checkBorders(player1.x, player1.y, player1.direction))                            
                            player1.y += speed;
                        break;
                    case 3:
                        if (checkBorders(player1.x, player1.y, player1.direction))                          
                            player1.x -= speed;
                        break;
                }
                
            }
            if (player2.move)
            {
                switch (player2.direction)
                {
                    case 0:
                        if (checkBorders(player2.x, player2.y, player2.direction))                           
                            player2.y -= speed;
                        break;
                    case 1:
                        if (checkBorders(player2.x, player2.y, player2.direction))                            
                            player2.x += speed;
                        break;
                    case 2:
                        if (checkBorders(player2.x, player2.y, player2.direction))
                            player2.y += speed;
                        break;
                    case 3:
                        if (checkBorders(player2.x, player2.y, player2.direction))
                            player2.x -= speed;
                        break;
                }
            }
            render();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {           
            if (e.KeyCode == Keys.W && player1.move == false)
            {
                player1.direction = 0;
                player1.move = true;
            }
            if (e.KeyCode == Keys.D && player1.move == false)
            {
                player1.direction = 1;
                player1.move = true;
            }
            if (e.KeyCode == Keys.S && player1.move == false)
            {
                player1.direction = 2;
                player1.move = true;
            }
            if (e.KeyCode == Keys.A && player1.move == false)
            {
                player1.direction = 3;
                player1.move = true;
            }

            if (e.KeyCode == Keys.Up && player2.move == false)
            {
                player2.direction = 0;
                player2.move = true;
            }
            if (e.KeyCode == Keys.Right && player2.move == false)
            {
                player2.direction = 1;
                player2.move = true;
            }
            if (e.KeyCode == Keys.Down && player2.move == false)
            {
                player2.direction = 2;
                player2.move = true;
            }
            if (e.KeyCode == Keys.Left && player2.move == false)
            {
                player2.direction = 3;
                player2.move = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W && player1.direction == 0)
            {
                player1.move = false;
            }
            if (e.KeyCode == Keys.D && player1.direction == 1)
            {
                player1.move = false;
            }
            if (e.KeyCode == Keys.S && player1.direction == 2)
            {
                player1.move = false;
            }
            if (e.KeyCode == Keys.A && player1.direction == 3)
            {
                player1.move = false;
            }

            if (e.KeyCode == Keys.Up && player2.direction == 0)
            {
                player2.move = false;
            }
            if (e.KeyCode == Keys.Right && player2.direction == 1)
            {
                player2.move = false;
            }
            if (e.KeyCode == Keys.Down && player2.direction == 2)
            {
                player2.move = false;
            }
            if (e.KeyCode == Keys.Left && player2.direction == 3)
            {
                player2.move = false;
            }
        }
    }
}
