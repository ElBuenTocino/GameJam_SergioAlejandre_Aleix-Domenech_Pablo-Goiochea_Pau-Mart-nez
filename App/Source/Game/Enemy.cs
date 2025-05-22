using SFML.Graphics;
using SFML.System;

namespace TcGame {
    public abstract class Enemy : StaticActor {
        protected Enemy()
        {
            if(this.GetType() == typeof(EnemyGhost))
            {
                OnDestroy += Explode;
            }
        }

        private void Explode(Actor actor)
        {
            Actor a = Engine.Get.Scene.Create<Explosion>();
            a.Position = new Vector2f(Position.X, Position.Y);
        }
    }
}

