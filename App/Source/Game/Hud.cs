using SFML.Graphics;
using SFML.System;
using System;

namespace TcGame
{
  public class Hud : Actor
  {
        public int Score;

        int persResc = 0, persCap = 0;
        private Text txt, txt2;

        public Hud() 
        {
            Layer = ELayer.Hud;
            Font f = new Font("Data/Fonts/LuckiestGuy.ttf");
            txt = new Text("", f);
            txt2 = new Text("", f);
            txt.Position = new Vector2f(10, 10);
            txt.FillColor = new Color(Color.White);
            txt.DisplayedString = ($"Score: {Score}");
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            SetText();
        }

        public void SetText()
        {
            txt.DisplayedString = ($"Score: {Score}");
            
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

