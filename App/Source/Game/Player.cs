using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcGame {
    public class Player : StaticActor {
        private float coolDown = 0.3f, time = 0;
        public Player()
        {
            Layer = ELayer.Front;
            Sprite = new Sprite(new Texture("Data/Textures/Player/Player.png"));
            Position = (Vector2f)Engine.Get.Window.Size / 2;
            Center();
            Speed = 500;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
        }

        public override void Update(float dt)
        {
            time += dt;

            Vector2f maousePos = Engine.Get.MousePos - Position;
            Rotation = (float)Math.Atan2(maousePos.Y, maousePos.X) * MathUtil.RAD2DEG + 90;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                Forward += new Vector2f(-1, 0);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                Forward += new Vector2f(1, 0);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                Forward += new Vector2f(0, -1);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                Forward += new Vector2f(0, 1);
            }
            Forward = Forward.Normal();

            CheckCollision();
            CheckBorders();

            if (Mouse.IsButtonPressed(Mouse.Button.Left) && time > coolDown)
            {
                Shoot();
                time = 0;
            }

            base.Update(dt);
            Forward *= 0;
        }

        void Shoot()
        {
            Bala b = Engine.Get.Scene.Create<Bala>();
            b.Position = new Vector2f(Position.X, Position.Y);
        }
        private bool CheckCollision()
        {
            List<Person> personList = Engine.Get.Scene.GetAll<Person>();
            Person nearestPerson = null;
            float distMin = 50;

            foreach (Person person in personList)
            {
                Vector2f distVector = (person.Position - Position);
                if (distVector.Size() <= distMin)
                {
                    nearestPerson = person;
                }
            }

            if (nearestPerson != null)
            {
                return true;//End game
            }

            return false;
        }

        void CheckBorders()
        {
            
        }
    }
}
