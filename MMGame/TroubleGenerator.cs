using System;
using System.Collections.Generic;

namespace MMGame
{
    public class TroubleGenerator
    {
        
        public List<ITrouble> ChairTree()
        {
            return new List<ITrouble>
            {
                new TroubleChair(Form1.width, 300),
                new TroubleChair(Form1.width, 400),
                new TroubleChair(Form1.width, 500),
                new TroubleChair(Form1.width + 150, 350),
                new TroubleChair(Form1.width + 150, 450),
                new TroubleChair(Form1.width + 300, 400)
            };
        }
        
        public List<ITrouble> ChairButterfly()
        {
            return new List<ITrouble>
            {
                new TroubleChair(Form1.width, 300),
                new TroubleChair(Form1.width, 400),
                new TroubleChair(Form1.width, 500),
                new TroubleChair(Form1.width + 150, 350),
                new TroubleChair(Form1.width + 150, 450),
                new TroubleChair(Form1.width + 300, 400),
                new TroubleChair(Form1.width + 450, 350),
                new TroubleChair(Form1.width + 450, 450),
                new TroubleChair(Form1.width + 600, 300),
                new TroubleChair(Form1.width + 600, 400),
                new TroubleChair(Form1.width + 600, 500),
            };
        }
        
        public List<ITrouble> LChar()
        {
            return new List<ITrouble>
            {
                new TroubleChair(Form1.width, 500),
                new TroubleChair(Form1.width + 150, 400),
                new TroubleChair(Form1.width + 300, 300),
                new TroubleChair(Form1.width + 450, 400),
                new TroubleChair(Form1.width + 600, 500)
            };
        }
        
        public List<ITrouble> OChar()
        {
            return new List<ITrouble>
            {
                new TroubleBench(Form1.width + 100, 300),
                new TroubleChair(Form1.width, 350),
                new TroubleChair(Form1.width, 420),
                new TroubleBench(Form1.width + 100, 550),
                new TroubleChair(Form1.width + 320, 350),
                new TroubleChair(Form1.width + 320, 420)
            };
        }
        
        public List<ITrouble> HChar()
        {
            return new List<ITrouble>
            {
                new TroubleChair(Form1.width, 500),
                new TroubleChair(Form1.width+150, 400),
                new TroubleChair(Form1.width, 300),
                new TroubleChair(Form1.width + 300, 300),
                new TroubleChair(Form1.width + 300, 500)
            };
        }

        public List<ITrouble> RandomTroubles()
        {
            var random = new Random();
            return random.Next(5) switch
            {
                0 => ChairTree(),
                1 => ChairButterfly(),
                2 => LChar(),
                3 => OChar(),
                4 => HChar(),
                _ => new List<ITrouble>()
            };
        }
    }
}