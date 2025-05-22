using App.Source.Game;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcGame {
    internal static class GameOver {
        
        public static bool dead = false;
        public static void Die()
        {
            dead = true;
        }
    }
    
}
