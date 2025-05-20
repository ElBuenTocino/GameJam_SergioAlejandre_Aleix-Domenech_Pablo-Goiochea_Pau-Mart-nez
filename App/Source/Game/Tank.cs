using SFML.Graphics;
using SFML.System;
using System;

namespace TcGame
{
  public class Tank : Enemy
  {
        public bool beingCaptured;
        public bool beingReached;

        public Tank()
        {
            Layer = ELayer.Back;
            Random alea = new Random();
            Speed = 30;
            Sprite = new Sprite (new Texture($"Data/Textures/Enemies/Tank0{alea.Next(1, 3)}.png"));
            Position = new Vector2f(alea.Next(0, (int)Engine.Get.Window.Size.X), 0);
            Center();
        }

        public override void Update(float dt)
        {
            //Console.WriteLine("Tank position" + Position);

            base.Update(dt);

            if (!beingCaptured)
            {
                Forward = new Vector2f(0, 1);

                if (Position.Y >= Engine.Get.Window.Size.Y)
                {
                    Destroy();
                }
            }
            else
            {
                Forward = new Vector2f(0, -1);
                Speed = 10;
            }
            Position = Position + (Speed * Forward * dt);

        }
  }
}
