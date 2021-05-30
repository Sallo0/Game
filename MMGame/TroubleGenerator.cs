using System;
using System.Collections.Generic;

namespace MMGame
{
    public class TroubleGenerator
    {
        private const int StartPosition = Form1.width;
        private TroubleChair TChair = new TroubleChair(0, 0);
        private TroubleLava TLava = new TroubleLava(0, 0);
        private TroubleBench TBench = new TroubleBench(0, 0);
        private TroubleWires TWires = new TroubleWires(0, 0);
        
        public List<ITrouble> ChairButterfly()
        {
            return new List<ITrouble>
            {
                new TroubleChair(Form1.width, 300),
                new TroubleChair(Form1.width + 600, 300),
                new TroubleChair(Form1.width + 150, 350),
                new TroubleChair(Form1.width + 450, 350),
                new TroubleChair(Form1.width, 400),
                new TroubleChair(Form1.width + 300, 400),
                new TroubleChair(Form1.width + 600, 400),
                new TroubleChair(Form1.width + 150, 450),
                new TroubleChair(Form1.width + 450, 450),
                new TroubleChair(Form1.width, 500),
                new TroubleChair(Form1.width + 600, 500),
                new TroubleEmpty(StartPosition+600 + TChair.ImageRect.Width,0)
            };
        }
        
        public List<ITrouble> LChar()
        {
            return new List<ITrouble>
            {
                new TroubleChair(Form1.width + 300, 300),
                new TroubleChair(Form1.width + 150, 400),
                new TroubleChair(Form1.width + 450, 400),
                new TroubleChair(Form1.width, 500),
                new TroubleChair(Form1.width + 600, 500),
                new TroubleEmpty(StartPosition+600 + TChair.ImageRect.Width,0)
            };
        }
        
        public List<ITrouble> OChar()
        {
            return new List<ITrouble>
            {
                new TroubleBench(Form1.width + 100, 300),
                new TroubleChair(Form1.width, 350),
                new TroubleChair(Form1.width + 320, 350),
                new TroubleChair(Form1.width, 420),
                new TroubleChair(Form1.width + 320, 420),
                new TroubleBench(Form1.width + 100, 550),
                new TroubleEmpty(StartPosition+320 + TChair.ImageRect.Width,0)
            };
        }
        
        public List<ITrouble> HChar()
        {
            return new List<ITrouble>
            {
                new TroubleChair(StartPosition, 300),
                new TroubleChair(StartPosition + 300, 300),
                new TroubleChair(StartPosition+150, 400),
                new TroubleChair(StartPosition, 500),
                new TroubleChair(StartPosition + 300, 500),
                new TroubleEmpty(StartPosition+300 + TChair.ImageRect.Width,0)
            };
        }

        public List<ITrouble> Lava()
        {
            return new List<ITrouble>()
            {
                new TroubleLava(StartPosition, 330),
                new TroubleLava(StartPosition+600, 330),
                new TroubleLava(StartPosition+300, 500),
                new TroubleEmpty(StartPosition+600 + TLava.ImageRect.Width,0)
            };
        }

        public List<ITrouble> Wires()
        {
            return new List<ITrouble>()
            {
                new TroubleWires(StartPosition,340),
                new TroubleWires(StartPosition + 481,340),
                new TroubleChair(StartPosition+200,400),
                new TroubleChair(StartPosition+500,400),
                new TroubleChair(StartPosition+700,400),
                new TroubleWires(StartPosition, 550),
                new TroubleWires(StartPosition + 481,550),
                new TroubleEmpty(StartPosition + 481 + TWires.ImageRect.Width,0)
            };
        }


        public List<ITrouble> RandomTroubles()
        {
            var random = new Random();
            return random.Next(0,6) switch
            {
                0 => ChairButterfly(),
                1 => LChar(),
                2 => OChar(),
                3 => HChar(),
                4 => Lava(),
                5 => Wires(),
                _ => new List<ITrouble>()
            };
        }
    }
}