using SFML.Graphics;
using SFML.System;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcGame {
    internal class Bala : StaticActor {
        public Sound blaster;
        public Bala()
        {
            Sprite = new Sprite(new Texture("Data/Textures/Bullets/bala.png"));
            Speed = 700;
            Scale = new Vector2f(0.05f, 0.05f);
            Center();
            Forward = Engine.Get.MousePos - Engine.Get.Scene.GetFirst<Player>().Position;
            Forward = Forward.Normal();
            SoundBuffer sb = new SoundBuffer("Data/Audio/blaster.wav");
            blaster = new Sound(sb);
        }
        public override void Update(float dt)
        {
            Position += Forward * Speed * dt;
        }
        Enemy EnemyMesProper()
        {
            Enemy ret = null;
            float distMin = 999999;
            List<Enemy> Enemys = Engine.Get.Scene.GetAll<Enemy>();
            foreach (Enemy e in Enemys)
            {
                Vector2f resta = (e.Position - Position);
                float dist = resta.Size();
                if (dist < distMin)
                {
                    ret = e;
                    distMin = dist;
                }
            }

            return ret;
        }
    }
}
