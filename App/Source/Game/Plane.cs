using SFML.Graphics;
using System.Collections.Generic;

namespace TcGame
{
    public class Plane : AnimatedActor
    {


        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
        }

        private void CheckCollision()
        {
            List<Person> lPerson = Engine.Get.Scene.GetAll<Person>();

            foreach (Person person in lPerson) 
            {
                if (person.GetGlobalBounds().Intersects(this.GetGlobalBounds()))
                {
                    Engine.Get.Scene.Destroy(person);
                    Hud hud = Engine.Get.Scene.GetFirst<Hud>();
                    hud.IncreaseRescued();
                }
            }
        }
    }
}
