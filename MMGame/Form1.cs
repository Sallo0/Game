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
        Player player;
        public List<ITrouble> Troubles;
        public static Bitmap backgImage;
        public static Bitmap floorImage;
        public static Bitmap backgImage2;
        public static Bitmap floorImage2;
        public static Bitmap menuImage;
        public static Bitmap playerDeadImage;
        public static Image pauseImage;
        public static Bitmap winGameImage;
        public int xFloorCord;
        public int floorSpeed;
        public const int width = 1500;
        public const int height = 630;
        public bool newGame = true;
        public bool pause = false;
        public bool winGame = false;
        public Rectangle startButtonBox = new Rectangle(new Point(656,397),new Size(197,81));
        public Rectangle exitButtonBox = new Rectangle(new Point(656,494),new Size(197,81));
        public Rectangle pauseMenuButtonBox = new Rectangle(new Point(576, 370), new Size(162,64));
        public Rectangle pauseContinueButtonBox = new Rectangle(new Point(753, 369), new Size(159,65));
        public Zet zet;
        public Random Random = new Random();
        public TroubleGenerator TGen = new TroubleGenerator();
        public int winZetAmount;

        private void Init()
        {
            Troubles = TGen.Lava();
            player = new Player(200,300);
            backgImage = new Bitmap(Tools.GetFullPath("Background.png"));
            floorImage = new Bitmap(Tools.GetFullPath("Floor.png"));
            backgImage2 = new Bitmap(Tools.GetFullPath("Background2.png"));
            floorImage2 = new Bitmap(Tools.GetFullPath("Floor2.png"));
            menuImage = new Bitmap(Tools.GetFullPath("Start.png"));
            playerDeadImage = new Bitmap(Tools.GetFullPath("PlayerDead.png"));
            pauseImage = new Bitmap(Tools.GetFullPath("Pause.png"));
            winGameImage = new Bitmap(Tools.GetFullPath("Win.png"));
            zet = new Zet(500,400);
            xFloorCord = 0;
            floorSpeed = 3;
            winGame = false;
            winZetAmount = 5;
        }
        
        private void DrawBackground(Graphics graphics)
        {
            xFloorCord = xFloorCord % backgImage.Width-floorSpeed;
            graphics.DrawImage(backgImage,xFloorCord-backgImage.Width,0);
            graphics.DrawImage(backgImage2,xFloorCord,0);
            graphics.DrawImage(floorImage,xFloorCord-backgImage.Width,backgImage.Height);
            graphics.DrawImage(floorImage2,xFloorCord,backgImage.Height);
            xFloorCord += backgImage.Width;
        }

        private void GenerateTroubles()
        {
            if (Troubles[^1].ImageRect.Right < width-100) Troubles.AddRange(TGen.RandomTroubles());
            if (Troubles.First().ImageRect.Right < -10) Troubles.RemoveAt(0);
        }

        private void GenerateZet()
        {
            zet.X -= floorSpeed;
            if (zet.Incident(player) || zet.HitBox.Right < 0)
            {
                zet.X = width + Random.Next(100,4000);
                zet.Y = Random.Next(backgImage.Height, height - zet.ZetImage.Height);
            }

            foreach (var trouble in Troubles)
            {
                if (zet.ImageRect.IntersectsWith(trouble.ImageRect))
                {
                    zet.X = width;
                    zet.Y = Random.Next(backgImage.Height, height - zet.ZetImage.Height);
                }
            }
        }
        
        private void DrawTroubles(Graphics graphics)
        {
            foreach (var trouble in Troubles)
            {
                trouble.X -= floorSpeed;
                trouble.TroubleImage.MakeTransparent(Color.White);
                graphics.DrawImage(trouble.TroubleImage,trouble.X,trouble.Y);
                //graphics.DrawRectangle(new Pen(Color.Brown),trouble.HitBox);
                if (!player.imageRect.IntersectsWith(trouble.ImageRect) ||
                    player.HitBox.Top <= trouble.HitBox.Top) continue;
                graphics.DrawImage(trouble.TroubleImage,trouble.X,trouble.Y);
                graphics.DrawImage(player.PlayerImage,player.x,player.y);
            }
        }

        private void DrawPause(Graphics graphics)
        {
            xFloorCord = xFloorCord % backgImage.Width;
            graphics.DrawImage(backgImage,xFloorCord-backgImage.Width,0);
            graphics.DrawImage(backgImage2,xFloorCord,0);
            graphics.DrawImage(floorImage,xFloorCord-backgImage.Width,backgImage.Height);
            graphics.DrawImage(floorImage2,xFloorCord,backgImage.Height);
            xFloorCord += backgImage.Width;
            
            graphics.DrawImage(zet.ZetImage,zet.x,zet.y);
            graphics.DrawImage(player.PlayerImage, player.x, player.y);
            foreach (var trouble in Troubles)
            {
                graphics.DrawImage(trouble.TroubleImage,trouble.X,trouble.Y);
                if (player.imageRect.IntersectsWith(trouble.ImageRect) && player.HitBox.Top > trouble.HitBox.Top)
                {
                    graphics.DrawImage(trouble.TroubleImage,trouble.X,trouble.Y);
                    graphics.DrawImage(player.PlayerImage,player.x,player.y);
                }
            }
            graphics.DrawImage(pauseImage,0,0);
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
                if (startButtonBox.Contains(args.Location) && newGame)
                {
                    Init();
                    newGame = false;
                }
                
                if (exitButtonBox.Contains(args.Location) && newGame)
                {
                    Application.Exit();
                }
                
                if (pause)
                {
                    if (pauseMenuButtonBox.Contains(args.Location))
                    {
                        newGame = true;
                        pause = false;
                    }

                    if (pauseContinueButtonBox.Contains(args.Location))
                    {
                        pause = false;
                    }
                }
                
                if (!player.IsAlive || winGame)
                {
                    newGame = true;
                }
            };
            
            Paint += (sender, args) =>
            {
                var g = args.Graphics;

                if (player.ZetCount == winZetAmount - 1) floorSpeed = 7;
                else if (player.ZetCount >= winZetAmount / 2) floorSpeed = 5;

                if (newGame)
                {
                    g.DrawImage(menuImage,0,0);
                }
                else if (player.ZetCount == winZetAmount)
                {
                    g.DrawImage(winGameImage,0,0);
                    winGame = true;
                }
                else
                {
                    if (player.IsAlive)
                    {
                        if (pause)
                        {
                            DrawPause(g);
                        }
                        else
                        {
                            DrawBackground(g);
                            GenerateTroubles();
                            GenerateZet();
                            g.DrawImage(zet.ZetImage,zet.x,zet.y);
                            g.DrawImage(player.PlayerImage, player.x, player.y);
                            DrawTroubles(g);
                        }
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
                if (args.KeyCode == Keys.Escape)
                {
                    pause = !pause;
                }
                if (player.IsAlive && !pause) player.Move(args.KeyCode.ToString(), width, height, floorImage.Height);
            };
        }
    }
}