using System;
using System.Collections.Generic;
using System.Threading;
using SFML.Graphics;
using SFML.System;
using static System.Net.Mime.MediaTypeNames;

namespace TcGame
{
    public class Ghost : Enemy 
    {
        Random rnd = new Random();
        public Ghost()
        {
            Layer = ELayer.Front;
            Speed = 75;
            Center();
            Forward = new Vector2f(1, 0);

            switch (rnd.Next(1,4))
            {
                case 1:
                    Sprite = new Sprite(new Texture("Data\\Textures\\Enemies\\ghost3.png"));
                    break;
                case 2:
                    Sprite = new Sprite(new Texture("Data\\Textures\\Enemies\\ghost2.png"));
                    break;
                case 3:
                    Sprite = new Sprite(new Texture("Data\\Textures\\Enemies\\ghost1.png"));
                    break;
            }
        }

        public override void Update(float dt)
        {
            base.Update(dt);

        }
    }
}
