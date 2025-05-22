using App.Source.Game;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcGame {
    public class Player : StaticActor 
    {
        private float deathCooldown = 0.06f, time = 0, bulletCooldown = 0.3f;
        public int mapShowings;
        Sound hurt, deadSound;
        public Player()
        {
            Layer = ELayer.Front;
            Sprite = new Sprite(new Texture("Data/Textures/Player/Player.png"));
            Position = (Vector2f)Engine.Get.Window.Size / 2;
            Center();
            Speed = 500;
            mapShowings = 10;
            SoundBuffer sb1 = new SoundBuffer("Data/Audio/hurt.wav");
            hurt = new Sound(sb1);
            SoundBuffer sb2 = new SoundBuffer("Data/Audio/dead.wav");
            deadSound = new Sound(sb2);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
        }

        public override void Update(float dt)
        {
            if (!GameOver.dead)
            {
                isInBar = false;
                time += dt;
                Vector2f maousePos = Engine.Get.MousePos - Position;
                Rotation = (float)Math.Atan2(maousePos.Y, maousePos.X) * MathUtil.RAD2DEG + 90;
                if (Keyboard.IsKeyPressed(Keyboard.Key.A) && (Position.X > 0))
                {
                    Forward += new Vector2f(-1, 0);
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.D) && (Position.X < Engine.Get.Window.Size.X))
                {
                    Forward += new Vector2f(1, 0);
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.W) && (Position.Y > 0))
                {
                    Forward += new Vector2f(0, -1);
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.S) && (Position.Y < Engine.Get.Window.Size.Y))
                {
                    Forward += new Vector2f(0, 1);
                }
                Forward = Forward.Normal();
                CheckCollision();
                CheckBars();

                if (Mouse.IsButtonPressed(Mouse.Button.Left) && time > bulletCooldown)
                {
                    Shoot();
                    time = 0;
                }

                base.Update(dt);
                Forward *= 0;
            }
            else
            {
                CheckCollision();
                deadSound.Play();
                time += dt;
                if (time > deathCooldown)
                {
                    Rotation = 0;
                    Sprite = new Sprite(new Texture("Data/Textures/Player/dead.png"));
                }
            }
        }

        void Shoot()
        {
            Bala b = Engine.Get.Scene.Create<Bala>();
            b.Position = new Vector2f(Position.X, Position.Y);
            b.blaster.Play();
        }
        private void CheckCollision()
        {
            List<EnemyGhost> ghostList = Engine.Get.Scene.GetAll<EnemyGhost>();
            EnemyGhost nearestGhost = null;
            float distMin = 25;

            foreach (EnemyGhost ghost in ghostList)
            {
                Vector2f distVector = (ghost.Position - Position);
                if (distVector.Size() <= distMin)
                {
                    nearestGhost = ghost;
                }
            }

            if (nearestGhost != null)
            {
                if(Hud.Lifes > 0) { 
                    hurt.Play();
                }
                
                Engine.Get.Scene.Destroy(nearestGhost); 
                time = 0;
                Hud.Lifes--;
                if (Hud.Lifes <= 0) { GameOver.Die(); }             }

            List<Battery> batteryList = Engine.Get.Scene.GetAll<Battery>();
            Battery nearestBattery = null;
            float distMinBattery = 40;

            foreach (Battery battery in batteryList)
            {
                Vector2f distVector = (battery.Position - Position);
                if (distVector.Size() <= distMinBattery)
                {
                    Engine.Get.Scene.Destroy(battery);
                    Engine.Get.Scene.GetFirst<Hud>().lightBattery.Size = new Vector2f(Engine.Get.Scene.GetFirst<Hud>().lightBattery.Size.X, Engine.Get.Scene.GetFirst<Hud>().lightBattery.Size.Y + 10);
                    if (Engine.Get.Scene.GetFirst<Hud>().lightBattery.Size.Y >= 150)
                    {
                        Engine.Get.Scene.GetFirst<Hud>().lightBattery.Size = new Vector2f(50, 150);
                    }
                }
            }

            List<Radar> radarList = Engine.Get.Scene.GetAll<Radar>();
            Battery nearestRadar = null;
            float distMinRadar = 40;

            foreach (Radar radar in radarList)
            {
                Vector2f distVector = (radar.Position - Position);
                if (distVector.Size() <= distMinRadar)
                {
                    Engine.Get.Scene.Destroy(radar);
                    Engine.Get.Scene.GetFirst<Player>().mapShowings++;
                }
            }
        }

        void CheckBars()
        {
            
        }
    }
}
