using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace TcGame
{
  public class Hud : Actor
  {
        int persResc = 0, persCap = 0;
        private Text txt,scoreText, highScoreText;
        private Text gameOvertxt, pressRtxt, pressEsctxt;
        public SFML.Graphics.RectangleShape lightBattery;
        public float Score, timerScore, highScore = 0;
        //List<float> highScore = new List<float>();
        public static int Lifes = 3;
        public Sprite Hearth1, Hearth2, Hearth3;
        public Hud() 
        {
            Layer = ELayer.Hud;
            Hearth1 = new Sprite(new Texture("Data/Textures/Player/Undertale.png"));
            Hearth1.Position = new Vector2f(640,50);
            Hearth1.Scale = new Vector2f(0.4f, 0.4f);

            Hearth2 = new Sprite(new Texture("Data/Textures/Player/Undertale.png"));
            Hearth2.Position = new Vector2f(Hearth1.Position.X+60, 50);
            Hearth2.Scale = new Vector2f(0.4f, 0.4f);

            Hearth3 = new Sprite(new Texture("Data/Textures/Player/Undertale.png"));
            Hearth3.Position = new Vector2f(Hearth2.Position.X + 60, 50);
            Hearth3.Scale = new Vector2f(0.4f, 0.4f);
            Font f = new Font("Data/Fonts/LuckiestGuy.ttf");
            txt = new Text("", f);
            scoreText = new Text("",f);
            scoreText.Position = new Vector2f(10,40);
            txt.Position = new Vector2f(10, 10);
            txt.FillColor = new Color(Color.White);
            txt.DisplayedString = ("Map Refreshers: " + 3);
            scoreText.DisplayedString = ($"SCORE: {Score}");
            highScoreText = new Text ("",f);
            highScoreText.Position = new Vector2f(Engine.Get.Window.Size.X / 2.4f, Engine.Get.Window.Size.Y / 1.95f);
            highScoreText.FillColor = new Color(Color.White);
            highScoreText.OutlineThickness = 2.5f;
            highScoreText.OutlineColor = new Color(Color.Black);
            highScoreText.DisplayedString = ($"HIGH SCORE: {highScore}");

            gameOvertxt = new Text("", f);
            pressRtxt = new Text("", f);
            pressEsctxt = new Text("", f);
            gameOvertxt.Position = new Vector2f(Engine.Get.Window.Size.X / 3.7f, Engine.Get.Window.Size.Y / 3f);
            pressRtxt.Position = new Vector2f(Engine.Get.Window.Size.X / 2.7f, Engine.Get.Window.Size.Y / 2.1f);
            pressEsctxt.Position = new Vector2f(Engine.Get.Window.Size.X / 2.65f, Engine.Get.Window.Size.Y / 2.3f);
            gameOvertxt.Scale = new Vector2f(3, 3);
            gameOvertxt.FillColor = new Color(Color.Red);
            pressRtxt.FillColor = new Color(Color.White);
            pressEsctxt.FillColor = new Color(Color.White);
            pressRtxt.OutlineThickness = 2.5f;
            pressEsctxt.OutlineThickness = 2.5f;
            gameOvertxt.OutlineThickness = 2.5f;
            pressRtxt.OutlineColor = new Color(Color.Black);
            pressEsctxt.OutlineColor = new Color(Color.Black);
            gameOvertxt.DisplayedString = ("GAME OVER");
            pressRtxt.DisplayedString = ("Press R to restart");
            pressEsctxt.DisplayedString = ("Press ESC to exit");


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
            timerScore += dt;
            if(timerScore > 1 && !GameOver.dead)
            {
                Score++;
                timerScore = 0;
            }
            
        }


        public void SetText()
        {
            txt.DisplayedString = ("Map Refreshers: " + Engine.Get.Scene.GetFirst<Player>().mapShowings);
            scoreText.DisplayedString = ($"SCORE: {Score}");
            if (highScore < Score)
            { 
                highScore = Score;
            }
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
            if (Hud.Lifes == 3)
            {
                target.Draw(Hearth1);
                target.Draw(Hearth2);
                target.Draw(Hearth3);
            }
            if (Hud.Lifes == 2)
            {
                target.Draw(Hearth1);
                target.Draw(Hearth2);
            }
            if (Hud.Lifes == 1) { target.Draw(Hearth1); }
            else { }

            if (GameOver.dead)
            {

                target.Draw(gameOvertxt);
                target.Draw(pressRtxt);
                target.Draw(pressEsctxt);
                target.Draw(scoreText);
                target.Draw(highScoreText);
            }
        }

        void UpdateBattery()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Right) && lightBattery.Size.Y > 0)
            {
                lightBattery.Size = new Vector2f(lightBattery.Size.X, lightBattery.Size.Y - 0.2f);
            }
        }
    }
}

