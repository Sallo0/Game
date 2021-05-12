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
        private List<Trouble> Troubles;
        Player player;
        public static Bitmap backgImage;
        public static Bitmap floorImage;
        public static Bitmap menuImage;
        public static Bitmap playerDeadImage;
        public int xFloorCord;
        public int floorSpeed;
        public static int width = 1500;
        public static int height = 630;
        public bool newGame = true;
        public Rectangle startButtonBox = new Rectangle(new Point(656,494),new Size(197,81));


        private void Init()
        {
            Troubles = new List<Trouble>();
            player = new Player(200,300);
            backgImage = new Bitmap(Tools.GetFullPath("Background.png"));
            floorImage = new Bitmap(Tools.GetFullPath("Floor.png"));
            menuImage = new Bitmap(Tools.GetFullPath("Start.png"));
            playerDeadImage = new Bitmap(Tools.GetFullPath("PlayerDead.png"));
            xFloorCord = 0;
            floorSpeed = 1;
            InitTroubles();
        }
        

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
                FormBorderStyle = FormBorderStyle.None;
                Init();
            };
            
            MouseClick += (sender, args) =>
            {
                if (!player.IsAlive)
                {
                    newGame = true;
                }
            };
            
            MouseClick += (sender, args) =>
            {
                if (startButtonBox.Contains(args.Location) && newGame)
                {
                    Init();
                    newGame = false;
                }
            };
            
            Paint += (sender, args) =>
            {
                var g = args.Graphics;
                
                if (newGame)
                {
                    g.DrawImage(menuImage,0,0);
                }
                else
                {
                    if (player.IsAlive)
                    {
                        DrawBackground(g);
                        DrawTroubles(g);
                        g.DrawImage(player.PlayerImage, player.x, player.y);
                        g.DrawRectangle(new Pen(Color.Purple), player.HitBox);
                    }
                    else
                    {
                        g.DrawImage(playerDeadImage, 0, 0);
                    }
                }
                foreach (var trouble in Troubles) player.Incident(trouble);
                Invalidate();
            };
            
            KeyDown += (sender, args) =>
            {
                if (player.IsAlive) player.Move(args.KeyCode.ToString(), width, height, floorImage.Height);
            };
        }
    }
}