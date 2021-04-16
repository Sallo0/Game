using System.Drawing;

namespace MMGame
{
    public class Player
    {
        public int x;
        public int y;
        public Image PlayerImage;

        public Player(int x, int y)
        {
            PlayerImage = new Bitmap(Tools.GetFullPath("Player.png"));
            this.x = x;
            this.y = y;
        }

        public void Move(string dir,int width, int height, int floorImgHeight)
        {
            var movementSpeed = 20;
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
        }
    }
}