using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muziek_Game
{
    public class Level
    {
        public float Tempo { get; set; } // BPM (beats per minute)
        public int Difficulty { get; set; } // Moeilijkheidsgraad (bijv. 1-5)
        public int Timing { get; set; } // Timing in milliseconden
        public int[] BlockRows { get; set; } // Array voor het spawnen van blokken


        /// <summary>
        /// Het Level object zorgt ervoor dat je levels kan opslaan
        /// </summary>
        /// <param name="tempo"></param>
        /// <param name="difficulty"></param>
        /// <param name="timing"></param>
        /// <param name="blockRows"></param>
        public Level(float tempo, int difficulty, int timing, int[] blockRows)
        {
            Tempo = tempo;
            Difficulty = difficulty;
            Timing = timing;
            BlockRows = blockRows;
        }
    }
}
