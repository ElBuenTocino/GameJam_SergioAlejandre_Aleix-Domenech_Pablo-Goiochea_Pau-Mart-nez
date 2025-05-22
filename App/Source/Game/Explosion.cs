using SFML.Window;
using SFML.System;
using SFML.Audio;
using SFML.Graphics;

namespace TcGame {
    public class Explosion : AnimatedActor {
        private float LifeTime;
        Sound explosion;
        public Explosion()
        {
            AnimatedSprite = new AnimatedSprite(new Texture("Data/Textures/FX/Explosion2.png"), 4, 1);
            AnimatedSprite.Loop = false;
            AnimatedSprite.FrameTime = 0.2f;
            LifeTime = AnimatedSprite.FrameTime * 3.0f;
            Center();
            SoundBuffer sb = new SoundBuffer("Data/Audio/explosion.wav");
            explosion = new Sound(sb);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            explosion.Play();
            Position += new Vector2f(0.0f, 30.0f * dt);
            LifeTime -= dt;
            if (LifeTime < 0.0f)
            {
                Destroy();
            }
        }
    }
}

