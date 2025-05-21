using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcGame
{
    internal class Cursor : Actor
    {
        public RectangleShape rectangleShape;
        public Cursor() 
        { 
            CreateLight();
            Layer = ELayer.Front;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            MoveLight();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Right) && Engine.Get.Scene.GetFirst<Hud>().lightBattery.Size.Y > 0)
            {
                base.Draw(target, states);
                target.Draw(rectangleShape, states);
            }
        }

        Vector2f GetCursorPosition()
        {
            Vector2i mousePosition = SFML.Window.Mouse.GetPosition(Engine.Get.Window);
            return new Vector2f(mousePosition.X, mousePosition.Y);
        }

        void CreateLight()
        {
            rectangleShape = new RectangleShape(new Vector2f(150, 150))
            {
                FillColor = new Color(255, 255, 255, 100),
                Origin = new Vector2f(150 / 2, 150 / 2)
            };
        }

        void MoveLight()
        {
            rectangleShape.Position = GetCursorPosition();
        }

        public void CheckEnemyCollision()
        {
            foreach (EnemyGhost ghost in Engine.Get.Scene.GetAll<EnemyGhost>())
            {
                //if (GetGlobalBounds().Intersects(ghost.GetGlobalBounds()))
                if (ghost.GetGlobalBounds().Intersects(rectangleShape.GetGlobalBounds()))
                {
                    ghost.Sprite.Color = new Color(100, 100, 100, 100);
                    ghost.Sprite = new Sprite(new Texture("Data\\Textures\\Player\\player.png"));
                    //ghost.Sprite = ghost.defaultSprite;
                }

            }
        }
    }
}
