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
        private List<Trouble> Troubles = new List<Trouble>();
        Player player = new Player(200,300);
        public static Bitmap backgImage = new Bitmap(Tools.GetFullPath("Background.png"));
        public static Bitmap floorImage = new Bitmap(Tools.GetFullPath("Floor.png"));
        public static int width = 1500;
        public static int height = floorImage.Height+backgImage.Height;
        public int xFloorCord;
        public int floorSpeed = 1;
        

        private void DrawBackground(Graphics graphics)
        {
            xFloorCord = xFloorCord % backgImage.Width-floorSpeed;
            for (var i = 0; i < width/backgImage.Width+1; i++)
            {
                graphics.DrawImage(backgImage,xFloorCord-backgImage.Width,0);
                graphics.DrawImage(backgImage,xFloorCord,0);
                graphics.DrawImage(floorImage,xFloorCord-backgImage.Width,backgImage.Height);
                graphics.DrawImage(floorImage,xFloorCord,backgImage.Height);
                xFloorCord += backgImage.Width;
            }
        }

        private void InitTroubles()
        {
            Troubles.Add(new Trouble(400,backgImage.Height, new Bitmap(Tools.GetFullPath("TroubleChair.png"))));
            Troubles.Add(new Trouble(800,height-200, new Bitmap(Tools.GetFullPath("TroubleChair.png"))));
            Troubles.Add(new Trouble(1200,backgImage.Height, new Bitmap(Tools.GetFullPath("TroubleChair.png"))));
            Troubles.Add(new Trouble(1600,height-200, new Bitmap(Tools.GetFullPath("TroubleChair.png"))));
        }

        private void DrawTroubles(Graphics graphics)
        {
            foreach (var trouble in Troubles)
            {
                trouble.x -= floorSpeed;
                if (trouble.x+trouble.TroubleImage.Width<=0)
                {
                    trouble.x = width;
                }
                trouble.HitBox.X = trouble.x;
                graphics.DrawRectangle(new Pen(Color.Brown),trouble.HitBox);
                graphics.DrawImage(trouble.TroubleImage,trouble.x,trouble.y);
            }
        }

        public Form1()
        {
            Load += (sender, args) =>
            {
                DoubleBuffered = true;
                ClientSize = new Size(width, height);   
                InitTroubles();
            };
            
            Paint += (sender, args) =>
            {
                var g = args.Graphics;
                DrawBackground(g);
                DrawTroubles(g);
                g.DrawImage(player.PlayerImage, player.x, player.y);
                g.DrawRectangle(new Pen(Color.Purple),player.HitBox);
                Invalidate();
            };

            KeyDown += (sender, args) =>
            {
                if (player.IsAlive) player.Move(args.KeyCode.ToString(), width, height, floorImage.Height);
                else
                {
                    floorSpeed = 0;
                }
                foreach (var trouble in Troubles) trouble.Incident(player);
            };
        }
    }
}