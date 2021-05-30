using System.Drawing;

namespace MMGame
{
    public class Zet
    {
        public int x;
        public int y;

        public int X
        {
            get => x;
            set
            {
                x = value;
                ImageRect.X = value;
                HitBox.X = value + 30;
            }
        }

        public int Y
        {
            get => y;
            set
            {
                y = value;
                ImageRect.Y = value;
                HitBox.Y = value + ZetImage.Height - 25;
            }
        }

        public Image ZetImage = new Bitmap(Tools.GetFullPath("Zet.png"));
        public Rectangle HitBox;
        public Rectangle ImageRect;

        public Zet(int x, int y)
        {
            this.x = x;
            this.y = y;
            HitBox = new Rectangle(x, y + ZetImage.Height - 25, 30, 20);
            ImageRect = new Rectangle(x, y, ZetImage.Height, ZetImage.Width);
        }

        public bool Incident(Player player)
        {
            if (!HitBox.IntersectsWith(player.HitBox)) return false;
            player.ZetCount++;
            return true;
        }
    }
}