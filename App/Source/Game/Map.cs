using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcGame;

namespace TcGame
{
    internal class Map : Actor
    {
        int tilesWidth, tilesHeight;
        char[,] map;

        View view = Engine.Get.Window.GetView();
        float timeBetweenRefreshers;


        public Map()
        { 
        //    tilesWidth = ((int)(Engine.Get.Window.Size.X / 100));
        //    tilesHeight = ((int)(Engine.Get.Window.Size.Y / 100));

            FloatRect viewRect = new FloatRect(
            view.Center.X - view.Size.X / 2,
            view.Center.Y - view.Size.Y / 2,
            view.Size.X,
            view.Size.Y
            );

            tilesWidth = (int)(viewRect.Width / 100);
            tilesHeight = (int)(viewRect.Height / 100);
            map = new char[tilesWidth, tilesHeight];

        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Player player = Engine.Get.Scene.GetFirst<Player>();

            //Console.WriteLine(player.Position.X/100);
            //Console.WriteLine(player.Position.Y/100);

            var view = Engine.Get.Window.GetView();
            FloatRect viewRect = new FloatRect(
                view.Center.X - view.Size.X / 2,
                view.Center.Y - view.Size.Y / 2,
                view.Size.X,
                view.Size.Y
            );

            //tilesWidth = (int)(viewRect.Width / 100);
            //tilesHeight = (int)(viewRect.Height / 100);

            tilesHeight = 10;
            tilesWidth = 10;

            map = new char[tilesWidth, tilesHeight];

            //Console.Clear();
            //Console.WriteLine(player.Position);

            BoardChange(viewRect, dt);
        }

        void DrawBoard()
        {
            for (int fil = 0; fil < map.GetLength(1); fil++)
            {
                DrawRowSeparator();
                for (int col = 0; col < map.GetLength(0); col++)
                {
                    Console.Write('|');
                    FillTile(col, fil); // Notice order: col is x, fil is y
                }
                Console.WriteLine('|');
            }
            DrawRowSeparator();
        }

        void DrawRowSeparator()
        {
            Console.Write("+");
            for (int j = 0; j < map.GetLength(1); j++)
                Console.Write("---+");
            Console.WriteLine();
        }
        void FillTile(int x, int y)
        {
            if (map[x, y] == 'P')
            {
                Console.Write(" P ");
            }
            else
            {
                Console.Write(" · ");
            }
        }

        void UpdateMap(FloatRect viewRect)
        {
            // Clear the map
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = ' ';
                }
            }

            // Set player position
            Player player = Engine.Get.Scene.GetFirst<Player>();
            int px = (int)((player.Position.X - viewRect.Left) / 100);
            int py = (int)((player.Position.Y - viewRect.Top) / 100);

            // Ensure it's in bounds
            if (px >= 0 && px < tilesWidth && py >= 0 && py < tilesHeight)
            {
                map[px, py] = 'P';
            }
        }

        void BoardChange(FloatRect viewRect, float dt)
        {
            Player player = Engine.Get.Scene.GetFirst<Player>();
            timeBetweenRefreshers += dt;

            if (Keyboard.IsKeyPressed(Keyboard.Key.R) && player.mapShowings > 0 && timeBetweenRefreshers >= 0.5f)
            {
                Console.SetCursorPosition(0, 0);
                UpdateMap(viewRect);
                DrawBoard();
                player.mapShowings--;
                timeBetweenRefreshers = 0;
            }
        }
    }
}
