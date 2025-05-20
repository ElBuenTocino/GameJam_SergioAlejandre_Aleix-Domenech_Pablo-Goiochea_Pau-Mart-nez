using SFML.Graphics;
using SFML.System;
using SFML.Window;

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
            CreatePersonSpawner();
            CreateOvniSpawner();
            CreateTankSpawner();
            hud = Engine.Get.Scene.Create<Hud>();
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
        private void CreateOvniSpawner()
        {
            ActorSpawner<Ghost> spawner;
            spawner = Engine.Get.Scene.Create<ActorSpawner<Ghost>>();
            spawner.MinPosition = new Vector2f(0.0f, +1000.0f);
            spawner.MaxPosition = new Vector2f(900.0f, 1000.0f);
            spawner.MinTime = 8.0f;
            spawner.MinTime = 15.0f;
            spawner.Reset();
        }
        private void CreateTankSpawner()
        {
            ActorSpawner<Tank> spawner;
            spawner = Engine.Get.Scene.Create<ActorSpawner<Tank>>();
            spawner.MinPosition = new Vector2f(0.0f, -200.0f);
            spawner.MaxPosition = new Vector2f(1000.0f, 0.0f);
            spawner.MinTime = 8.0f;
            spawner.MinTime = 10.0f;
            spawner.Reset();
        }
        public void DeInit()
        {
        }
        public void Update(float dt)
        {
            
        }
        private void DestroyAll<T>() where T : Actor
        {
            var actors = Engine.Get.Scene.GetAll<T>();
            actors.ForEach(x => x.Destroy());
        }
    }
}

