using App.Source.Game;
using SFML.Graphics;
using System;

namespace TcGame
{
    public class StaticActor : Actor
    {
        public Sprite Sprite { get; set; }
        public bool isInBar;

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (Sprite != null)
            {
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
