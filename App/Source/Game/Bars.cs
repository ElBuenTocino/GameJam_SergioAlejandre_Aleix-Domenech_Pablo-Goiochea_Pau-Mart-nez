using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcGame;

namespace App.Source.Game
{
    public class Bars : StaticActor
    {
        public Bars()
        {
            Layer = ELayer.Background;
            Speed = 7.5f;
            Sprite = new Sprite(new Texture($"Data/Textures/Bars.jpg"));
            Position = new Vector2f(Engine.Get.Window.Size.X + Sprite.GetLocalBounds().Width/2, Engine.Get.Window.Size.Y/2);
            Center();
            Forward = new Vector2f(-1, 0); 
            Sprite.Color = new Color(255,255, 255, 200);
        }
        public override void Update(float dt)
        {
            base.Update(dt);
        }

    }
}
