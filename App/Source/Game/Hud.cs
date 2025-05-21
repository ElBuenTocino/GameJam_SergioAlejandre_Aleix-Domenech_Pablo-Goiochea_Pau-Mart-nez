using SFML.Graphics;
using SFML.System;
using System;

namespace TcGame
{
  public class Hud : Actor
  {
        int persResc = 0, persCap = 0;
        private Text txt;
        public Hud() 
        {
            Layer = ELayer.Hud;
            Font f = new Font("Data/Fonts/LuckiestGuy.ttf");
            txt = new Text("", f);
            txt.Position = new Vector2f(10, 10);
            txt.FillColor = new Color(Color.Yellow);
            txt.DisplayedString = ("Map Refreshers: " + 3);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            SetText();
        }

        public void SetText()
        {
            txt.DisplayedString = ("Map Refreshers: " + Engine.Get.Scene.GetFirst<Player>().mapShowings);
        }

        public void IncreaseRescued()
        {
            persResc++;
        }

        public void IncreaseCaptured()
        {
            persCap++;
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(txt);
        }
    }
}

