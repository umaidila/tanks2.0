﻿using Microsoft.VisualBasic.Devices;
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
        }

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
        bool redactorMode;
        tank player1;
        tank player2;
        bullet bullet1;
        bullet bullet2;
        List<tank> players;
        int speed = 4;
        int speedBullet = 16;
        int tickIndex = 0;

        public Form1()
        {
            InitializeComponent();
          
            listObstacles = new List<obstacle>(48);
            player1 = new tank();
            player2 = new tank();
            bullet1 = new bullet();
            bullet2 = new bullet();

            players = new List<tank> { player1, player2 };
            finishButton.Visible = false;
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
            picBitmap = new Bitmap(fieldPictureBox.Width, fieldPictureBox.Height);
            bullet1.active = false;
            bullet2.active = false;
            player1.hp = hpBar.Value;
            player2.hp = hpBar.Value;
            player1.move = false;
            player2.move = false;
            player1.direction = 1;
            player2.direction = 3;
            player1.x = 64;
            player1.y = 64;
            player2.x = 896;
            player2.y = 288;

            infolabel1.Visible = false;
            infolabel2.Visible = false;
            infolabel3.Visible = false;
            infolabel4.Visible = false;
            startButton.Visible = false;
            resultField.Visible = false;
            infolabel5.Visible = false;
            hpBar.Visible = false;
            hplabel.Visible = false;
            Player1Info.Visible = true;
            Player2Info.Visible = true;
            fieldPictureBox.Visible = true;
            finishButton.Visible = true;
            redactorButton.Visible = false;
            redactorMode = false;
            finishButton.Text = "Завершить игру";

            Player1Info.Text = "Игрок 1:  " + hpBar.Value;
            Player2Info.Text = "Игрок 2:  " + hpBar.Value;

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
                g.FillRectangle(b, getCoords(obst.x), getCoords(obst.y), 32, 32);
                g.DrawRectangle(p, getCoords(obst.x), getCoords(obst.y), 32, 32);
            }

            staticBitmap = (Bitmap)picBitmap.Clone();

            fieldPictureBox.Image = picBitmap;

            timer1.Enabled = true;
            render();           
        }

        private void finishGame()
        {
            timer1.Enabled = false;
            player1.move = false;
            player2.move = false;
            bullet1.active = false;
            bullet2.active = false;

            infolabel1.Visible = true;
            infolabel2.Visible = true;
            infolabel3.Visible = true;
            infolabel4.Visible = true;
            startButton.Visible = true;
            resultField.Visible = true;
            Player1Info.Visible = false;
            Player2Info.Visible = false;
            fieldPictureBox.Visible = false;
            finishButton.Visible = false;
            infolabel5.Visible = true;
            hpBar.Visible = true;
            hplabel.Visible = true;
            redactorButton.Visible = true;

            if (player1.hp == 0)
                resultField.Text = "Победил Игрок 2";
            else if (player2.hp == 0)
                resultField.Text = "Победил Игрок 1";
            else
                resultField.Text = "";

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

            if (bullet1.active)
            {
                g.FillEllipse(b1, bullet1.x, bullet1.y, 16, 16);
                g.DrawEllipse(p, bullet1.x, bullet1.y, 16, 16);
            }
            if (bullet2.active)
            {
                g.FillEllipse(b2, bullet2.x, bullet2.y, 16, 16);
                g.DrawEllipse(p, bullet2.x, bullet2.y, 16, 16);
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
                        if (y == getCoords(obst.y) + 32 && ((x >= getCoords(obst.x) && x < getCoords(obst.x) + 32) || (x + 31 >= getCoords(obst.x) && x+31 < getCoords(obst.x) + 32)))
                            return false;             
                    break;
                case 1:
                    if (x + 32 >= 992)
                        return false;
                    foreach (var obst in listObstacles)
                        if (x+32 == getCoords(obst.x) && ((y >= getCoords(obst.y) && y < getCoords(obst.y) + 32) || (y + 31 >= getCoords(obst.y) && y+31 < getCoords(obst.y) + 32)))
                            return false;
                    break;
                case 2:
                    if (y + 32 >= 544)
                        return false;
                    foreach (var obst in listObstacles)
                        if (y + 32 ==  getCoords(obst.y) && ((x >= getCoords(obst.x) && x < getCoords(obst.x) + 32) || (x + 31 >= getCoords(obst.x) && x + 31 < getCoords(obst.x) + 32)))
                            return false;
                    break;
                case 3:
                    if (x <= 32)
                        return false;
                    foreach (var obst in listObstacles)
                        if (x == getCoords(obst.x)+ 32 && ((y >= getCoords(obst.y) && y < getCoords(obst.y) + 32) || (y + 31 >= getCoords(obst.y) && y + 31 < getCoords(obst.y) + 32)))
                            return false;
                    break;
            }
            return true;
        }

        private bool checkBullet(int x,int y, int direction)
        {
            if (y <= 32 || x + 16 >= 992 || y + 16 >= 544 || x <= 32)
                return false;        

            foreach (var obst in listObstacles)
                if (x + 8 >= getCoords(obst.x) && x + 8 <= getCoords(obst.x)+32 && y + 8 >= getCoords(obst.y) && y + 8 <= getCoords(obst.y)+32)
                    return false;
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
            if (bullet1.active)
            {
                switch (bullet1.direction)
                {
                    case 0:
                        if (!checkBullet(bullet1.x, bullet1.y, bullet1.direction)) // снаряд попал в кирпич
                           bullet1.active = false;
                        else
                            bullet1.y -= speedBullet;
                        break;
                    case 1:
                        if (!checkBullet(bullet1.x, bullet1.y, bullet1.direction)) 
                            bullet1.active = false;
                        else
                            bullet1.x += speedBullet;
                        break;
                    case 2:
                        if (!checkBullet(bullet1.x, bullet1.y, bullet1.direction)) 
                            bullet1.active = false;
                        else
                            bullet1.y += speedBullet;
                        break;
                    case 3:
                        if (!checkBullet(bullet1.x, bullet1.y, bullet1.direction)) 
                            bullet1.active = false;
                        else
                            bullet1.x -= speedBullet;
                        break;
                }
                // проверка на попадание
                if (bullet1.x + 8 > player2.x && bullet1.x + 8 < player2.x + 32 && bullet1.y + 8 > player2.y && bullet1.y + 8 < player2.y + 32)
                {
                    bullet1.active = false;
                    player2.hp--;
                    Player2Info.Text = "Игрок 2:  " + player2.hp;
                    if (player2.hp == 0)
                        finishGame();
                }

            }
            if (bullet2.active)
            {
                switch (bullet2.direction)
                {
                    case 0:
                        if (!checkBullet(bullet2.x, bullet2.y, bullet2.direction)) // снаряд попал в кирпич
                            bullet2.active = false;
                        else
                            bullet2.y -= speedBullet;
                        break;
                    case 1:
                        if (!checkBullet(bullet2.x, bullet2.y, bullet2.direction))
                            bullet2.active = false;
                        else
                            bullet2.x += speedBullet;
                        break;
                    case 2:
                        if (!checkBullet(bullet2.x, bullet2.y, bullet2.direction))
                            bullet2.active = false;
                        else
                            bullet2.y += speedBullet;
                        break;
                    case 3:
                        if (!checkBullet(bullet2.x, bullet2.y, bullet2.direction))
                            bullet2.active = false;
                        else
                            bullet2.x -= speedBullet;
                        break;
                }
                // проверка на попадание
                if (bullet2.x + 8 > player1.x && bullet2.x + 8 < player1.x + 32 && bullet2.y + 8 > player1.y && bullet2.y + 8 < player1.y + 32)
                {
                    bullet2.active = false;
                    player1.hp--;
                    Player1Info.Text = "Игрок 1:  " + player1.hp;
                    if (player1.hp == 0)
                        finishGame();
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
            if (e.KeyCode == Keys.F && !bullet1.active)
            {
                bullet1.x = player1.x + 8;
                bullet1.y = player1.y + 8;
                bullet1.direction = player1.direction;
                bullet1.active = true;
            }
            
            if (e.KeyCode == Keys.I && player2.move == false)
            {
                player2.direction = 0;
                player2.move = true;
            }
            if (e.KeyCode == Keys.L && player2.move == false)
            {
                player2.direction = 1;
                player2.move = true;
            }
            if (e.KeyCode == Keys.K && player2.move == false)
            {
                player2.direction = 2;
                player2.move = true;
            }
            if (e.KeyCode == Keys.J && player2.move == false)
            {
                player2.direction = 3;
                player2.move = true;
            }
            if (e.KeyCode == Keys.H && !bullet2.active)
            {
                bullet2.x = player2.x + 8;
                bullet2.y = player2.y + 8;
                bullet2.direction = player2.direction;
                bullet2.active = true;
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
           

            if (e.KeyCode == Keys.I && player2.direction == 0)
            {
                player2.move = false;
            }
            if (e.KeyCode == Keys.L && player2.direction == 1)
            {
                player2.move = false;
            }
            if (e.KeyCode == Keys.K && player2.direction == 2)
            {
                player2.move = false;
            }
            if (e.KeyCode == Keys.J && player2.direction == 3)
            {
                player2.move = false;
            }
            
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            finishGame();
        }

        private void hpbar_ValueChanged(object sender, EventArgs e)
        {
            hplabel.Text = hpBar.Value.ToString();
        }

        private void redactorButton_Click(object sender, EventArgs e)
        {
            redactorMode = true;

            picBitmap = new Bitmap(fieldPictureBox.Width, fieldPictureBox.Height);
            
            infolabel1.Visible = false;
            infolabel2.Visible = false;
            infolabel3.Visible = false;
            infolabel4.Visible = false;
            startButton.Visible = false;
            resultField.Visible = false;
            infolabel5.Visible = false;
            hpBar.Visible = false;
            hplabel.Visible = false;
            Player1Info.Visible = false;
            Player2Info.Visible = false;
            fieldPictureBox.Visible = true;
            finishButton.Visible = true;
            finishButton.Text = "Завершить";
            redactorButton.Visible = false;
           
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
                g.FillRectangle(b, getCoords(obst.x), getCoords(obst.y), 32, 32);
                g.DrawRectangle(p, getCoords(obst.x), getCoords(obst.y), 32, 32);
            }

            staticBitmap = (Bitmap)picBitmap.Clone();

            fieldPictureBox.Image = picBitmap;

        }


        private void fieldPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (redactorMode)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (e.X >= 32 && e.X < 992 && e.Y >= 32 && e.Y < 544)
                    {
                        int indX = (e.X - 32) / 32;
                        int indY = (e.Y - 32) / 32;
                        // пытаемся найти кирпич по таком индексу
                        for (int i = 0; i < listObstacles.Count; i++)
                        {
                            if (listObstacles[i].x == indX && listObstacles[i].y == indY)
                            {
                                listObstacles.RemoveAt(i);
                                b = new SolidBrush(Color.FromArgb(166, 242, 150));
                                g = Graphics.FromImage(staticBitmap);
                                p = new Pen(Color.FromArgb(166, 242, 150), 2);
                                g.FillRectangle(b, getCoords(indX), getCoords(indY),32,32);
                                g.DrawRectangle(p, getCoords(indX), getCoords(indY),32, 32);
                                fieldPictureBox.Image = staticBitmap;

                                return;

                            }
                        }
                        // если не нашли, добавляем
                        listObstacles.Add(new obstacle(indX, indY));
                        b = new SolidBrush(Color.FromArgb(92, 33, 4));
                        g = Graphics.FromImage(staticBitmap);
                        p = new Pen(Color.Black, 2);

                        g.FillRectangle(b, getCoords(indX), getCoords(indY), 32, 32);
                        g.DrawRectangle(p, getCoords(indX), getCoords(indY), 32, 32);

                        fieldPictureBox.Image = staticBitmap;
                    }
                }
            }
        }
    }
}
