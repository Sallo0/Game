using System.Drawing;

namespace MMGame
{
    public class Trouble
    {
        public int x;
        public int y;
        public Bitmap TroubleImage;
        public Rectangle HitBox;

        public Trouble(int x, int y, Bitmap troubleImage)
        {
            this.x = x;
            this.y = y;
            TroubleImage = troubleImage;
            HitBox = new Rectangle(x, y, TroubleImage.Width, TroubleImage.Height);
        }
        

        public void Incident(Player player)
        {
            if (HitBox.IntersectsWith(player.HitBox))
                player.IsAlive = false;
        }
    }
}