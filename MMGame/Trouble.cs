using System.Drawing;
using System.Runtime.CompilerServices;

namespace MMGame
{
    public interface ITrouble
    {
        public Bitmap TroubleImage { get; set; }
        public Rectangle HitBox { get; set; }
        
        public int X { get; set; }

        public int Y { get; set; }
    }
    
    public class TroubleChair :  ITrouble
    {
        private int x;
        private int y;
        public Bitmap TroubleImage { get; set; }
        public Rectangle HitBox
        {
            get=> hitBox;
            set => hitBox = value;
        }
        private Rectangle hitBox;

        public TroubleChair(int x, int y)
        {
            this.x = x;
            this.y = y;
            TroubleImage =new Bitmap( Tools.GetFullPath("TroubleChair.png"));
            hitBox = new Rectangle(x, y + TroubleImage.Height - 23, TroubleImage.Width-17, 20);
        }
        
        public int X
        {
            get => x;
            set
            {
                x = value;
                hitBox.X = value+10;
            }
        }

        public int Y
        {
            get => y;
            set
            {
                y = value;
                hitBox.Y = value + TroubleImage.Height-10;
            }
        }
        
    }
    
    
    public class TroubleBench : ITrouble
    {
        
        private int x;
        private int y;
        public Bitmap TroubleImage { get; set; }
        public Rectangle HitBox
        {
            get=> hitBox;
            set => hitBox = value;
        }
        private Rectangle hitBox;
        
        public TroubleBench(int x, int y)
        {
            this.x = x;
            this.y = y;
            TroubleImage =new Bitmap( Tools.GetFullPath("TroubleBench.png"));
            hitBox = new Rectangle(x, y + TroubleImage.Height - 26, TroubleImage.Width-50, 23);
        }
        
        public int X
        {
            get => x;
            set
            {
                x = value;
                hitBox.X = value+20;
            }
        }

        public int Y
        {
            get => y;
            set
            {
                y = value;
                hitBox.Y = value + TroubleImage.Height-10;
            }
        }
    }
}