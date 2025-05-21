using SFML.Graphics;
using SFML.System;
using System;
using System.Threading.Tasks.Sources;

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
            Forward = (Engine.Get.Scene.GetFirst<Player>().Position - Position).Normal();
            Center();

            Speed = Math.Clamp(Speed, 30, 90);
            Speed += Speed * dt;
            CheckCollisions();
        }

        public void CheckCollisions()
        {
            Player player = Engine.Get.Scene.GetFirst<Player>();

            if (GetGlobalBounds().Intersects(player.GetGlobalBounds()))
            {
                //Player.Die();
            }

            foreach(Bala bala in Engine.Get.Scene.GetAll<Bala>())
            {
                if (GetGlobalBounds().Intersects(bala.GetGlobalBounds()))
                {
                    Hud hud = Engine.Get.Scene.GetFirst<Hud>();
                    Engine.Get.Scene.Destroy(this);
                    if (Speed = ))
                    {

                    }
                    hud.score += 1;
                }
            }
        }
  }
}
