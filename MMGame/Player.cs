using System.Diagnostics.Contracts;
using System.Drawing;

namespace MMGame
{
    public class Player
    {
        public int x;
        public int y;
        public Image PlayerImage;
        public Rectangle HitBox;
        public Rectangle imageRect;
        public int movementSpeed = 10;
        public bool IsAlive = true;
        public int ZetCount;

        public Player(int x, int y)
        {
            PlayerImage = new Bitmap(Tools.GetFullPath("Player.png"));
            this.x = x;
            this.y = y;
            HitBox = new Rectangle(x+20, y+ PlayerImage.Height - 5, PlayerImage.Width/2, 5);
            imageRect = new Rectangle(x, y, PlayerImage.Width, PlayerImage.Height);
        }

        public void Move(string dir,int width, int height, int floorImgHeight)
        {
            switch (dir)
            {
                case "Right":
                    if (x + PlayerImage.Width < width) x += movementSpeed;
                    else  x = width - PlayerImage.Width;
                    break;
                case "Left":
                    if (x > 0) x -= movementSpeed; 
                    else  x = 0;
                    break;
                case "Up":
                    if (y+PlayerImage.Height > height-floorImgHeight + PlayerImage.Height/5) y -= movementSpeed; 
                    else  y = height-floorImgHeight + PlayerImage.Height/5 - PlayerImage.Height;
                    break;
                case "Down":
                    if (y + PlayerImage.Height < height - 10) y += movementSpeed;
                    else y = height - 10 - PlayerImage.Height;
                    break;
            }
            HitBox.X = x+20;
            HitBox.Y = y + PlayerImage.Height - 5;
            imageRect.X = x;
            imageRect.Y = y;
        }
        
        public void Incident(ITrouble trouble)
        {
            if (HitBox.IntersectsWith(trouble.HitBox)) IsAlive = false;
        }
    }
}