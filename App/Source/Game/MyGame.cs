using App.Source.Game;
using SFML.Graphics;
using System;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace TcGame
{
    public class MyGame : Game // omedntar
    {
        public Hud hud { private set; get; }
        public Background background { get; private set; }
        private static MyGame instance;
        public Bars rightBar, leftBar, downBar, upBar;
        public float minDist = 1;
        public static MyGame Get
        {
            get
            {
                if (instance == null)
                {
                    instance = new MyGame();
                }

                return instance;
            }
        }
        private MyGame()
        {
        }
        public void Init()
        {
            background = Engine.Get.Scene.Create<Background>();
            Player player = Engine.Get.Scene.Create<Player>();
            CreatePersonSpawner();
            //CreateOvniSpawner();
            CreateGhostSpawner(new Vector2f(1000.0f, 0.0f), new Vector2f(0.0f, -200.0f));
            CreateGhostSpawner(new Vector2f(1000, Engine.Get.Window.Size.Y+100), new Vector2f(0, Engine.Get.Window.Size.Y));
            hud = Engine.Get.Scene.Create<Hud>();
            CreateBars();
            Engine.Get.Scene.Create<Map>();
            Engine.Get.Scene.Create<Cursor>();
        }
        private void CreatePersonSpawner()
        {
            ActorSpawner<Person> spawner;
            spawner = Engine.Get.Scene.Create<ActorSpawner<Person>>();
            spawner.MinPosition = new Vector2f(0.0f, -60.0f);
            spawner.MaxPosition = new Vector2f(1000.0f, -60.0f);
            spawner.MinTime = 2.0f;
            spawner.MinTime = 4.0f;
            spawner.Reset();
        }
/*        private void CreateOvniSpawner()
        {
            ActorSpawner<Ovni> spawner;
            spawner = Engine.Get.Scene.Create<ActorSpawner<Ovni>>();
            spawner.MinPosition = new Vector2f(0.0f, +1000.0f);
            spawner.MaxPosition = new Vector2f(900.0f, 1000.0f);
            spawner.MinTime = 8.0f;
            spawner.MinTime = 15.0f;
            spawner.Reset();
        }*/
        private void CreateGhostSpawner(Vector2f MaxPosition, Vector2f MinPosition)
        {
            ActorSpawner<EnemyGhost> spawner;
            spawner = Engine.Get.Scene.Create<ActorSpawner<EnemyGhost>>();
            spawner.MinPosition = MinPosition;
            spawner.MaxPosition = MaxPosition;
            spawner.MinTime = 1.0f;
            spawner.MaxTime = 3.0f;
            spawner.Reset();
        }
        private void CreateBars()
        {
            rightBar =Engine.Get.Scene.Create<Bars>();
            
            leftBar = Engine.Get.Scene.Create<Bars>();
            leftBar.Position = new Vector2f(0 - leftBar.GetLocalBounds().Width / 2, Engine.Get.Window.Size.Y / 2);
            leftBar.Forward = new Vector2f(1, 0);

            upBar = Engine.Get.Scene.Create<Bars>();
            upBar.Rotation = 90;
            upBar.Position = new Vector2f(Engine.Get.Window.Size.X / 2, 0-upBar.GetLocalBounds().Width/2);
            upBar.Forward = new Vector2f(0, 1);
            
            downBar = Engine.Get.Scene.Create<Bars>();
            downBar.Rotation = 90;
            downBar.Position = new Vector2f(Engine.Get.Window.Size.X / 2, Engine.Get.Window.Size.Y + downBar.GetLocalBounds().Width / 2);
            downBar.Forward = new Vector2f(0, -1);

        }
        public void DeInit()
        {
        }
        public void Update(float dt)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.K))
            {
                List<Actor> actores = Engine.Get.Scene.GetAll<Actor>();
                foreach (Actor actor in actores)
                {
                    Engine.Get.Scene.Destroy(actor);
                    if (Hud.HighScore < Hud.Score)
                    { Hud.HighScore = Hud.Score;   }
                    Hud.Score = 0;
                }
                Init();
                GameOver.dead = false;
            }
            int minmax = 2000;
            //Clamps for bars
            rightBar.Position = new Vector2f(Math.Clamp(rightBar.Position.X, (Engine.Get.Window.Size.X / 2) + rightBar.GetLocalBounds().Width / 2, minmax), rightBar.Position.Y);
            leftBar.Position = new Vector2f(Math.Clamp(leftBar.Position.X, -minmax , (Engine.Get.Window.Size.X / 2) - leftBar.GetLocalBounds().Width / 2), leftBar.Position.Y);
            upBar.Position = new Vector2f(upBar.Position.X, Math.Clamp(upBar.Position.Y, -minmax, (Engine.Get.Window.Size.Y / 2) - upBar.GetLocalBounds().Width/2));
            downBar.Position = new Vector2f(downBar.Position.X,  Math.Clamp(downBar.Position.Y, (Engine.Get.Window.Size.Y / 2) + downBar.GetLocalBounds().Width/2 , minmax));
        }
        private void DestroyAll<T>() where T : Actor
        {
            var actors = Engine.Get.Scene.GetAll<T>();
            actors.ForEach(x => x.Destroy());
        }
    }
}

