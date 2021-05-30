using System;
using System.Collections.Generic;

namespace MMGame
{
    public class TroubleGenerator
    {
        private const int StartPosition = Form1.width;
        
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
                new TroubleChair(Form1.width + 600, 500)
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
                new TroubleChair(Form1.width + 600, 500)
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
                new TroubleBench(Form1.width + 100, 550)
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
                new TroubleChair(StartPosition + 300, 500)
            };
        }

        public List<ITrouble> Lava()
        {
            return new List<ITrouble>()
            {
                new TroubleLava(StartPosition, 330),
                new TroubleLava(StartPosition+600, 330),
                new TroubleLava(StartPosition+300, 500)
            };
        }

        public List<ITrouble> Wires()
        {
            return new List<ITrouble>()
            {
                new TroubleWires(StartPosition,330),
                new TroubleWires(StartPosition+300, 500)
            };
        }


        public List<ITrouble> RandomTroubles()
        {
            var random = new Random();
            return random.Next(0,4) switch
            {
                0 => ChairButterfly(),
                1 => LChar(),
                2 => OChar(),
                3 => HChar(),
                _ => new List<ITrouble>()
            };
        }
    }
}