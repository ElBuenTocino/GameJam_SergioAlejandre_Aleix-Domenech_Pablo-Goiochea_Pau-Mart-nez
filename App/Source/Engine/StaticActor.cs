using App.Source.Game;
using SFML.Graphics;
using System;

namespace TcGame
{
    public class StaticActor : Actor
    {
        public Sprite Sprite { get; set; }
        public bool isInBar = false;

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (Sprite != null)
            {
  /*              foreach (Bars bars in Engine.Get.Scene.GetAll<Bars>())
                {
                    if (Sprite.GetGlobalBounds().Intersects(bars.GetGlobalBounds()) && this is not Bars && this is not Background)
                    {
                        isInBar = true;
                    }
                    if (Sprite.GetGlobalBounds().Intersects(Engine.Get.Scene.GetFirst<Background>().Sprite.GetGlobalBounds()) && )
                }*/

                if(((MyGame.Get.downBar.Sprite.Position - Sprite.Position).Size() < MyGame.Get.minDist || (MyGame.Get.leftBar.Sprite.Position - Sprite.Position).Size() < MyGame.Get.minDist || (MyGame.Get.rightBar.Sprite.Position - Sprite.Position).Size() < MyGame.Get.minDist || (MyGame.Get.upBar.Sprite.Position - Sprite.Position).Size() < MyGame.Get.minDist) && this is not Bars && this is not Background)
                {
                    Console.WriteLine((MyGame.Get.downBar.Sprite.Position - Sprite.Position).Size());
                }
                else
                {
                    isInBar = false;
                }

                if (!isInBar)
                {
                    base.Draw(target, states);
                    states.Transform *= Transform;
                    target.Draw(Sprite, states);
                }
            }
        }
        public override FloatRect GetLocalBounds()
        {
            return (Sprite != null) ? Sprite.GetLocalBounds() : new FloatRect();
        }
    }
}
