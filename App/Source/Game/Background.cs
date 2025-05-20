using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace TcGame {
    public class Background : StaticActor {
        public float Speed = 0;
        public Background()
        {
            Layer = ELayer.Background;
            Sprite = new Sprite(new Texture("Data/Textures/Background.jpg"));
            Sprite.Scale = new Vector2f(2, 2);
        }
        public override void Update(float dt)
        {
        }
    }
}

