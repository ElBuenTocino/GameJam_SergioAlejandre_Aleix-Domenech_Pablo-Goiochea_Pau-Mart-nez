using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcGame;

namespace TcGame
{
    public class Bars : StaticActor
    {
        public Bars()
        {
            Layer = ELayer.Background;
            Speed = 15f;
            //Speed = 7.5f;
            Sprite = new Sprite(new Texture($"Data/Textures/Bars.jpg"));
            Position = new Vector2f(Engine.Get.Window.Size.X + Sprite.GetLocalBounds().Width / 2, Engine.Get.Window.Size.Y / 2);
            Center();
            Forward = new Vector2f(-1, 0);
            Sprite.Color = new Color(255, 255, 255, 200);
        }
        public override void Update(float dt)
        {
            base.Update(dt);
            CheckPlayerColision();
            CheckEnemyCollision();
        }

        public void CheckPlayerColision()
        {
            /*            Player player = Engine.Get.Scene.GetFirst<Player>();
                        if (GetGlobalBounds().Intersects(player.GetGlobalBounds()))
                        {
                            GameOver.dead = true;
                        }*/
            Player player = Engine.Get.Scene.GetFirst<Player>();

            //if (GetGlobalBounds().Intersects(ghost.GetGlobalBounds()))
            if (player.GetGlobalBounds().Intersects(GetGlobalBounds()) && !GameOver.dead)
            {
                player.Sprite.Color = new Color(255, 255, 255, 0);
                player.isInBar = true;
            }
            else if (!player.isInBar)
            {
                player.Sprite.Color = new Color(255, 255, 255, 255);
            }

        }

        public void CheckEnemyCollision()
        {
            foreach (EnemyGhost ghost in Engine.Get.Scene.GetAll<EnemyGhost>())
            {
                //if (GetGlobalBounds().Intersects(ghost.GetGlobalBounds()))
                if (ghost.GetGlobalBounds().Intersects(GetGlobalBounds()))
                {
                    ghost.Sprite.Color = new Color(255, 255, 255, 0);
                    ghost.isInBar = true;
                }
                else if (!ghost.isInBar)
                {
                    ghost.Sprite.Color = new Color(255, 255, 255, 255);
                }
            }
        }
    }
}
