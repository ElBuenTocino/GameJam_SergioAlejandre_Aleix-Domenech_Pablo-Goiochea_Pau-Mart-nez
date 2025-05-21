using SFML.Graphics;
using SFML.System;
using System;

namespace TcGame
{
  public class EnemyGhost : Enemy
  {
        public bool beingCaptured;
        public bool beingReached;

        public EnemyGhost()
        {
            Layer = ELayer.Back;
            Random alea = new Random();
            Speed = 30;
            Position = new Vector2f(100,100);
            Center();
            switch (alea.Next(1, 4))
            {
                case 1:
                    Sprite = new Sprite(new Texture("Data\\Textures\\Enemies\\ghost3.png"));
                    Speed = 100;
                    break;
                case 2:
                    Sprite = new Sprite(new Texture("Data\\Textures\\Enemies\\ghost2.png"));
                    Speed = 200;
                    break;
                case 3:
                    Sprite = new Sprite(new Texture("Data\\Textures\\Enemies\\ghost1.png"));
                    Speed = 300;
                    break;
            }
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Forward = (Engine.Get.Scene.GetFirst<Player>().Position - Position).Normal();
            Center();

            //Speed = Math.Clamp(Speed, 30, 90);
            //Speed += Speed * dt;
            CheckCollisions();
        }

        public void CheckCollisions()
        {
            Player player = Engine.Get.Scene.GetFirst<Player>();

            if (GetGlobalBounds().Intersects(player.GetGlobalBounds()))
            {
                GameOver.dead = true;
            }

            foreach(Bala bala in Engine.Get.Scene.GetAll<Bala>())
            {
                if (GetGlobalBounds().Intersects(bala.GetGlobalBounds()))
                {
                    Console.WriteLine(Speed);
                    Engine.Get.Scene.Destroy(this);
                    Engine.Get.Scene.Destroy(bala);

                }
            }
        }
  }
}
