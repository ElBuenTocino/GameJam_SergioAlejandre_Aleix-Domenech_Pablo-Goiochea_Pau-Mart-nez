using App.Source.Game;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;

namespace TcGame
{
  public class EnemyGhost : Enemy
  {
        public bool beingCaptured;
        public bool beingReached;
        public Sprite defaultSprite;

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
                    Speed = 60;
                    Sprite.Scale = new Vector2f(1.5f, 1.5f);
                    break;
                case 2:
                    Sprite = new Sprite(new Texture("Data\\Textures\\Enemies\\ghost2.png"));
                    Speed = 120;
                    break;
                case 3:
                    Sprite = new Sprite(new Texture("Data\\Textures\\Enemies\\ghost1.png"));
                    Speed = 180;
                    Sprite.Scale = new Vector2f(0.8f, 0.8f);
                    break;
            }

            defaultSprite = Sprite;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Forward = (Engine.Get.Scene.GetFirst<Player>().Position - Position).Normal();
            Center();

            //Speed = Math.Clamp(Speed, 30, 90);
            //Speed += Speed * dt;
            isInBar = false;
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
                    Engine.Get.Scene.Destroy(this);
                    Engine.Get.Scene.Destroy(bala);
                    if (Speed == 50)
                    {
                        Hud.Score += 10;
                    }
                    if (Speed == 100)
                    {
                        Hud.Score += 15;
                    }
                    else { Hud.Score += 20; }
                    List<Bars> list = Engine.Get.Scene.GetAll<Bars>();
                    foreach (Bars bar in list)
                    {
                        if (bar.Position.X < Engine.Get.Window.Size.X / 2)
                        {
                            bar.Position -= new Vector2f(10, 0);
                        }
                        else if (bar.Position.X > Engine.Get.Window.Size.X / 2)
                        {
                            bar.Position -= new Vector2f(-10, 0);
                        }
                        else if (bar.Position.Y < Engine.Get.Window.Size.Y / 2)
                        {
                            bar.Position -= new Vector2f(0, 10);
                        }
                        else if (bar.Position.Y > Engine.Get.Window.Size.Y / 2)
                        {
                            bar.Position -= new Vector2f(0, -10);
                        }
                    }

                }
            }
        }

        public void CheckBarCollision()
        {
            /*foreach (Bars bar in Engine.Get.Scene.GetAll<Bars>())
            {
                if (GetGlobalBounds().Intersects(bar.GetGlobalBounds()))
                {
                    //Sprite = new Sprite(new Texture("Data\\Textures\\Enemies\\ghostNull.png"));
                    //Sprite.Rotation += 30;
                }
                else
                {
                    Sprite = defaultSprite;
                }
            }*/
        }
    }
}
