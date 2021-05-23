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
                HitBox.X = value;
            }
        }

        public int Y
        {
            get => y;
            set
            {
                y = value;
                HitBox.Y = value + ZetImage.Height - 10;
            }
        }

        public Image ZetImage = new Bitmap(Tools.GetFullPath("Zet.png"));
        public Rectangle HitBox;

        public Zet(int x, int y)
        {
            this.x = x;
            this.y = y;
            HitBox = new Rectangle(x, y + ZetImage.Height - 10, ZetImage.Width, 10);
        }

        public bool Incident(Player player)
        {
            if (!HitBox.IntersectsWith(player.HitBox)) return false;
            player.ZetCount++;
            return true;
        }
    }
}