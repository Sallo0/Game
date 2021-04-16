using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMGame
{
    public partial class Form1 : Form
    {
        public static int width = 1200;
        public static int height = 650;
        Player player = new Player(200,300);
        public static Bitmap backgImage = new Bitmap(Tools.GetFullPath("Background.jpg"));
        public static Bitmap floorImage = new Bitmap(Tools.GetFullPath("Floor.jpg"));
        public int xFloorCord;

        private void DrawFloor(Graphics graphics)
        {
            xFloorCord = xFloorCord % backgImage.Width-1;
            for (var i = 0; i < width/backgImage.Width+1; i++)
            {
                graphics.DrawImage(backgImage,xFloorCord-backgImage.Width,0);
                graphics.DrawImage(backgImage,xFloorCord,0);
                graphics.DrawImage(floorImage,0,backgImage.Height);
                xFloorCord += backgImage.Width;
            }
        }

        public Form1()
        {
            DoubleBuffered = true;
            ClientSize = new Size(width, height);
            Paint += (sender, args) =>
            {
                var graphics = args.Graphics;
                DrawFloor(graphics);
                graphics.DrawImage(player.PlayerImage, player.x, player.y);
                Invalidate();
            };

            KeyDown += (sender, args) =>
            {
                player.Move(args.KeyCode.ToString(),width,height,floorImage.Height);
            };
        }
    }
}