using System.Drawing;
using NUnit.Framework;


namespace MMGame
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void IsSizeInitializationCorrect()
        {
            var gaming = new Form1();
            Assert.AreEqual(1500, gaming.Width);
            Assert.AreEqual(630, gaming.Height);
        }

        [Test]
        public void IsHitboxesCorrect()
        {
            var player = new Player(0,0);
            var trouble = new Trouble(0, 0, new Bitmap(Tools.GetFullPath("TroubleChair.png")));
            Assert.AreEqual(true, player.HitBox.IntersectsWith(trouble.HitBox));
        }
    }
}