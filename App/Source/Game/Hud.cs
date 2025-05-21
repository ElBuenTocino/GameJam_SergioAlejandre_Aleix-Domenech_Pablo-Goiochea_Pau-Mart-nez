using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace TcGame
{
  public class Hud : Actor
  {
        int persResc = 0, persCap = 0;
        private Text txt,scoreText;
        public SFML.Graphics.RectangleShape lightBattery;
        public static int Score, HighScore;
        public Hud() 
        {
            
            Layer = ELayer.Hud;
            Font f = new Font("Data/Fonts/LuckiestGuy.ttf");
            txt = new Text("", f);
            scoreText = new Text("",f);
            scoreText.Position = new Vector2f(10,40);
            txt.Position = new Vector2f(10, 10);
            txt.FillColor = new Color(Color.White);
            txt.DisplayedString = ("Map Refreshers: " + 3);
            scoreText.DisplayedString = ($"SCORE: {Score}");
            

            lightBattery = new RectangleShape(new Vector2f(50, 150))
            {
                FillColor = new Color(Color.Yellow),
                Origin = new Vector2f(50 / 2, 0),
                Position = new Vector2f(Engine.Get.Window.Size.X - 50, 200),
                Rotation = 180

            };
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            SetText();
            UpdateBattery();
        }

        public void SetText()
        {
            txt.DisplayedString = ("Map Refreshers: " + Engine.Get.Scene.GetFirst<Player>().mapShowings);
            scoreText.DisplayedString = ($"SCORE: {Score}");
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
            target.Draw(scoreText);
            target.Draw(lightBattery);
        }

        void UpdateBattery()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Right) && lightBattery.Size.Y > 0)
            {
                lightBattery.Size = new Vector2f(lightBattery.Size.X, lightBattery.Size.Y - 0.5f);
            }
        }
    }
}

