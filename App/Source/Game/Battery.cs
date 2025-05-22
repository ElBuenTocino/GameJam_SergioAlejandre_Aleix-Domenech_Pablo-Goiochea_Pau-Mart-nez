using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcGame;
using static TcGame.Actor;

namespace App.Source.Game
{
    internal class Battery : Enemy
    {
        public Battery() 
        {
            Layer = ELayer.Back;
            Random alea = new Random();
            Speed = 15;
            Position = new Vector2f(alea.Next(0, ((int)Engine.Get.Window.Size.X)), alea.Next(0, ((int)Engine.Get.Window.Size.Y)));
            Center();
            Sprite = new Sprite(new Texture("Data\\Textures\\Battery4.png"));
            Forward = new Vector2f(0,0);
        }
    }
}
