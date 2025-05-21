using App.Source.Game;
using SFML.Graphics;
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
            //CreatePersonSpawner();
            //CreateOvniSpawner();
            CreateGhostSpawner();
            CreateBatterySpawner();
            hud = Engine.Get.Scene.Create<Hud>();
            CreateBars();
            Engine.Get.Scene.Create<Map>();
            Engine.Get.Scene.Create<Cursor>();
        }
        //private void CreatePersonSpawner()
        //{
        //    ActorSpawner<Person> spawner;
        //    spawner = Engine.Get.Scene.Create<ActorSpawner<Person>>();
        //    spawner.MinPosition = new Vector2f(0.0f, -60.0f);
        //    spawner.MaxPosition = new Vector2f(1000.0f, -60.0f);
        //    spawner.MinTime = 2.0f;
        //    spawner.MinTime = 4.0f;
        //    spawner.Reset();
        //}
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
        private void CreateGhostSpawner()
        {
            ActorSpawner<EnemyGhost> spawner;
            spawner = Engine.Get.Scene.Create<ActorSpawner<EnemyGhost>>();
            spawner.MinPosition = new Vector2f(0.0f, -200.0f);
            spawner.MaxPosition = new Vector2f(1000.0f, 0.0f);
            spawner.MinTime = 1.0f;
            spawner.MaxTime = 3.0f;
            spawner.Reset();
        }

        private void CreateBatterySpawner()
        {
            ActorSpawner<Battery> spawner;
            spawner = Engine.Get.Scene.Create<ActorSpawner<Battery>>();
            spawner.MinPosition = new Vector2f(0.0f, -200.0f);
            spawner.MaxPosition = new Vector2f(1000.0f, 0.0f);
            spawner.MinTime = 3.0f;
            spawner.MaxTime = 5.0f;
            spawner.Reset();
        }

        private void CreateBars()
        {
            ActorSpawner<Bars> spawner;
            Bars rightBar, leftBar, downBar, upBar;
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
                }
                Init();
                GameOver.dead = false;
            }
        }
        private void DestroyAll<T>() where T : Actor
        {
            var actors = Engine.Get.Scene.GetAll<T>();
            actors.ForEach(x => x.Destroy());
        }
    }
}

