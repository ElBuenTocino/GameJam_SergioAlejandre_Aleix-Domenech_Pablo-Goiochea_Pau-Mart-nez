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
            txt.DisplayedString = ($"Persones Rescatades: {persResc}\nPersones Capturades: {persCap}");
        }

        public override void Update(float dt)
        {
            //Console.Clear();

            //Console.WriteLine(Position);
            //Console.WriteLine(txt);

            base.Update(dt);
            SetText();
        }

        public void SetText()
        {
            txt.DisplayedString = ($"Persones Rescatades: {persResc}\nPersones Capturades: {persCap}");
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

