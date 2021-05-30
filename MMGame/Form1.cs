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
        Player _player;
        public List<ITrouble> Troubles;
        public static Bitmap BackgImage;
        public static Bitmap FloorImage;
        public static Bitmap BackgImage2;
        public static Bitmap FloorImage2;
        public static Bitmap MenuImage;
        public static Bitmap PlayerDeadImage;
        public static Image PauseImage;
        public static Bitmap WinGameImage;
        public static Bitmap ZetCountImage;
        public int XFloorCord;
        public int FloorSpeed;
        public const int width = 1500;
        public const int height = 630;
        public bool NewGame = true;
        public bool Pause = false;
        public bool WinGame = false;
        
        public Rectangle EzButtonBox = new Rectangle(new Point(654,191),new Size(197,81));
        public Rectangle MediumButtonBox = new Rectangle(new Point(654,289),new Size(197,81));
        public Rectangle HardButtonBox = new Rectangle(new Point(654,384),new Size(197,81));
        public Rectangle ExitButtonBox = new Rectangle(new Point(629,515),new Size(246,91));
        
        public Rectangle PauseMenuButtonBox = new Rectangle(new Point(576, 370), new Size(162,64));
        public Rectangle PauseContinueButtonBox = new Rectangle(new Point(753, 369), new Size(159,65));
        public Zet Zet;
        public readonly Random Random = new Random();
        public readonly TroubleGenerator TroubleGen = new TroubleGenerator();
        public int WinZetAmount;

        private void Init()
        {
            Troubles = TroubleGen.RandomTroubles();
            _player = new Player(200,300);
            BackgImage = new Bitmap(Tools.GetFullPath("Background.png"));
            FloorImage = new Bitmap(Tools.GetFullPath("Floor.png"));
            BackgImage2 = new Bitmap(Tools.GetFullPath("Background2.png"));
            FloorImage2 = new Bitmap(Tools.GetFullPath("Floor2.png"));
            MenuImage = new Bitmap(Tools.GetFullPath("Start.png"));
            PlayerDeadImage = new Bitmap(Tools.GetFullPath("PlayerDead.png"));
            PauseImage = new Bitmap(Tools.GetFullPath("Pause.png"));
            WinGameImage = new Bitmap(Tools.GetFullPath("Win.png"));
            ZetCountImage = new Bitmap(Tools.GetFullPath("ZetCount.png"));
            Zet = new Zet(500,400);
            XFloorCord = 0;
            FloorSpeed = 3;
            WinGame = false;
        }

        private void DrawZetCount(Graphics graphics)
        {
            for (var i = 0; i < _player.ZetCount; i++)
            {
                ZetCountImage.MakeTransparent(Color.White);
                graphics.DrawImage(ZetCountImage,i*ZetCountImage.Width,0);
            }
        }
        
        private void DrawBackground(Graphics graphics)
        {
            XFloorCord = XFloorCord % BackgImage.Width-FloorSpeed;
            graphics.DrawImage(BackgImage,XFloorCord-BackgImage.Width,0);
            graphics.DrawImage(BackgImage2,XFloorCord,0);
            graphics.DrawImage(FloorImage,XFloorCord-BackgImage.Width,BackgImage.Height);
            graphics.DrawImage(FloorImage2,XFloorCord,BackgImage.Height);
            XFloorCord += BackgImage.Width;
        }

        private void GenerateTroubles()
        {
            if (Troubles[^1].ImageRect.Right < width-100) Troubles.AddRange(TroubleGen.RandomTroubles());
            if (Troubles.First().ImageRect.Right < -10) Troubles.RemoveAt(0);
        }

        private void GenerateZet()
        {
            Zet.X -= FloorSpeed;
            if (Zet.Incident(_player) || Zet.HitBox.Right < 0)
            {
                Zet.X = width + Random.Next(100,4000);
                Zet.Y = Random.Next(BackgImage.Height, height - Zet.ZetImage.Height);
            }

            foreach (var trouble in Troubles)
            {
                if (Zet.ImageRect.IntersectsWith(trouble.ImageRect))
                {
                    Zet.X = width;
                    Zet.Y = Random.Next(BackgImage.Height, height - Zet.ZetImage.Height);
                }
            }
        }
        
        private void DrawTroubles(Graphics graphics)
        {
            foreach (var trouble in Troubles)
            {
                trouble.X -= FloorSpeed;
                trouble.TroubleImage.MakeTransparent(Color.White);
                graphics.DrawImage(trouble.TroubleImage,trouble.X,trouble.Y);
                if (!_player.imageRect.IntersectsWith(trouble.ImageRect) ||
                    _player.HitBox.Top <= trouble.HitBox.Top) continue;
                graphics.DrawImage(trouble.TroubleImage,trouble.X,trouble.Y);
                graphics.DrawImage(_player.PlayerImage,_player.x,_player.y);
            }
        }

        private void DrawPause(Graphics graphics)
        {
            XFloorCord = XFloorCord % BackgImage.Width;
            graphics.DrawImage(BackgImage,XFloorCord-BackgImage.Width,0);
            graphics.DrawImage(BackgImage2,XFloorCord,0);
            graphics.DrawImage(FloorImage,XFloorCord-BackgImage.Width,BackgImage.Height);
            graphics.DrawImage(FloorImage2,XFloorCord,BackgImage.Height);
            XFloorCord += BackgImage.Width;
            DrawZetCount(graphics);
            graphics.DrawImage(Zet.ZetImage,Zet.x,Zet.y);
            graphics.DrawImage(_player.PlayerImage, _player.x, _player.y);
            foreach (var trouble in Troubles)
            {
                graphics.DrawImage(trouble.TroubleImage,trouble.X,trouble.Y);
                if (_player.imageRect.IntersectsWith(trouble.ImageRect) && _player.HitBox.Top > trouble.HitBox.Top)
                {
                    graphics.DrawImage(trouble.TroubleImage,trouble.X,trouble.Y);
                    graphics.DrawImage(_player.PlayerImage,_player.x,_player.y);
                }
            }
            graphics.DrawImage(PauseImage,0,0);
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
                if (EzButtonBox.Contains(args.Location) && NewGame)
                {
                    Init();
                    WinZetAmount = 2;
                    NewGame = false;
                }
                
                if (MediumButtonBox.Contains(args.Location) && NewGame)
                {
                    Init();
                    WinZetAmount = 5;
                    NewGame = false;
                }
                
                if (HardButtonBox.Contains(args.Location) && NewGame)
                {
                    Init();
                    WinZetAmount = 10;
                    NewGame = false;
                }
                
                if (ExitButtonBox.Contains(args.Location) && NewGame)
                {
                    Application.Exit();
                }
                
                if (Pause)
                {
                    if (PauseMenuButtonBox.Contains(args.Location))
                    {
                        NewGame = true;
                        Pause = false;
                    }

                    if (PauseContinueButtonBox.Contains(args.Location))
                    {
                        Pause = false;
                    }
                }
                
                if (!_player.IsAlive || WinGame)
                {
                    NewGame = true;
                }
            };
            
            Paint += (sender, args) =>
            {
                var g = args.Graphics;

                if (_player.ZetCount == WinZetAmount - 1 && WinZetAmount != 2) FloorSpeed = 7;
                else if (_player.ZetCount >= WinZetAmount / 2) FloorSpeed = 5;

                if (NewGame)
                {
                    g.DrawImage(MenuImage,0,0);
                }
                else if (_player.ZetCount == WinZetAmount)
                {
                    g.DrawImage(WinGameImage,0,0);
                    WinGame = true;
                }
                else
                {
                    if (_player.IsAlive)
                    {
                        if (Pause)
                        {
                            DrawPause(g);
                        }
                        else
                        {
                            DrawBackground(g);
                            DrawZetCount(g);
                            GenerateTroubles();
                            GenerateZet();
                            g.DrawImage(Zet.ZetImage,Zet.x,Zet.y);
                            g.DrawImage(_player.PlayerImage, _player.x, _player.y);
                            DrawTroubles(g);
                        }
                    }
                    else
                    {
                        g.DrawImage(PlayerDeadImage, 0, 0);
                    }
                }
                foreach (var trouble in Troubles) _player.Incident(trouble);
                Invalidate();
            };
            
            KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Escape)
                {
                    Pause = !Pause;
                }
                if (_player.IsAlive && !Pause) _player.Move(args.KeyCode.ToString(), width, height, FloorImage.Height);
            };
        }
    }
}