using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TcGame.Actor;

namespace TcGame
{
    internal class Radar : Enemy
    {
        public Radar() 
        {
            Layer = ELayer.Back;
            Random alea = new Random();
            Speed = 30;
            Position = new Vector2f(alea.Next(0, ((int)Engine.Get.Window.Size.X)), alea.Next(0, ((int)Engine.Get.Window.Size.Y)));
            Center();
            Sprite = new Sprite(new Texture("Data\\Textures\\Radar4.png"));
            Forward = new Vector2f(0, 0);
        }
    }
}
