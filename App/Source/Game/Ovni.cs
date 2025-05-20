using System;
using System.Collections.Generic;
using System.Threading;
using SFML.Graphics;
using SFML.System;
using static System.Net.Mime.MediaTypeNames;

namespace TcGame
{
    public class Ovni : Enemy //FA FALTA CANVIAR QUE ENLLOC D'ANAR CAP ALS TANCS VAGI CAP A LES PERSONES (QUE ENCARA NO ESTAN EN EL MEU BRANCH I TAL)
    {
        //Person
        private Tank targetPerson;


        private float dtSum;
        private bool isCapturing;

        Random alea = new Random();


        public enum State
        {
            Patrolling,
            ReachingPerson,
            CapturingPerson
        }

        public State currentState;

        public Ovni()
        {
            Layer = ELayer.Front;
            Speed = 75;
            Sprite = new Sprite(new Texture($"Data/Textures/Enemies/Ovni0{alea.Next(1, 4)}.png"));
            Position = new Vector2f(alea.Next(0, (int)Engine.Get.Window.Size.X), 400);
            Center();
            Forward = new Vector2f(1, 0);
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            List<Tank> persons = Engine.Get.Scene.GetAll<Tank>();


            switch (currentState)
            {
                case State.Patrolling:
                    if (Position.X >= Engine.Get.Window.Size.X)
                    {
                        Forward = new Vector2f(-1, 0);
                    }
                    if (Position.X <= 0)
                    {
                        Forward = new Vector2f(1, 0);
                    }

                    if (targetPerson == null && persons != null && persons.Count > 0)
                    {
                        targetPerson = persons[alea.Next(0, persons.Count)];
                        if (targetPerson.beingReached)
                        {
                            targetPerson = null;
                        }
                    }
                    if (targetPerson != null)
                    {
                        currentState = State.ReachingPerson;
                    }

                    break;

                case State.ReachingPerson:

                    Forward = targetPerson.Position - Position;
                    Forward = Forward.Normal();
                    targetPerson.beingReached = true;

                    if ((targetPerson.Position - Position).Size() <= 1f)
                    {
                        currentState = State.CapturingPerson;
                    }
                    break;

                case State.CapturingPerson:
                    if (targetPerson != null && targetPerson.beingReached && Engine.Get.Scene.GetAll<Tank>().Contains(targetPerson))
                    {
                        Forward = new Vector2f(0, 0);
                        targetPerson.beingCaptured = true;
                        dtSum += dt;
                        if (dtSum >= 3)
                        {
                            targetPerson.Destroy();
                            targetPerson.beingReached = false;
                            targetPerson = null;
                            dtSum = 0;
                            Engine.Get.Scene.GetFirst<Hud>().IncreaseCaptured();
                        }
                    }
                    else
                    {
                        Forward = new Vector2f(1, 0);
                        currentState = State.Patrolling;
                    }
                    break;

            }
        }
    }
}
