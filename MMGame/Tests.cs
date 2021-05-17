using System.Drawing;
using System.Windows.Forms;
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
            Assert.AreEqual(1500, Form1.width);
            Assert.AreEqual(630, Form1.height);
        }

        [Test]
        public void IsHitboxesCorrect()
        {
            var player = new Player(0,0);
            var trouble = new Trouble(0, 0, new Bitmap(Tools.GetFullPath("TroubleChair.png")));
            Assert.AreEqual(false, player.HitBox.IntersectsWith(trouble.HitBox));
        }
    }
}