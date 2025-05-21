using App.Source.Game;
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
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("   ");
            }
            else if (map[x,y] == 'G')
            {
                Console.BackgroundColor = ConsoleColor.Red;

                Console.Write("   ");
            }
            else if (map[x, y] == 'B')
            {
                Console.BackgroundColor = ConsoleColor.Yellow;

                Console.Write("   ");
            }
            else if (map[x, y] == 'R')
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;

                Console.Write("   ");
            }
            else
            {
                Console.Write(" · ");
            }
            Console.ResetColor();
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

            List<EnemyGhost> enemyGhosts = Engine.Get.Scene.GetAll<EnemyGhost>();

            if (enemyGhosts != null && enemyGhosts.Count > 0)
            {
                for (int i = 0; i <= enemyGhosts.Count - 1; i++)
                {
                    if (enemyGhosts[i] != null)
                    {
                        int gx = (int)((enemyGhosts[i].Position.X - viewRect.Left) / 100);
                        int gy = (int)((enemyGhosts[i].Position.Y - viewRect.Top) / 100);


                        if (gx >= 0 && gx < tilesWidth && gy >= 0 && gy < tilesHeight)
                        {
                            map[gx, gy] = 'G';
                        }
                    }
                }
            }

            List<Battery> batteries = Engine.Get.Scene.GetAll<Battery>();

            if (batteries != null && batteries.Count > 0)
            {
                for (int i = 0; i <= batteries.Count - 1; i++)
                {
                    if (batteries[i] != null)
                    {
                        int bx = (int)((batteries[i].Position.X - viewRect.Left) / 100);
                        int by = (int)((batteries[i].Position.Y - viewRect.Top) / 100);


                        if (bx >= 0 && bx < tilesWidth && by >= 0 && by < tilesHeight)
                        {
                            map[bx, by] = 'B';
                        }
                    }
                }
            }

            List<Radar> radars = Engine.Get.Scene.GetAll<Radar>();

            if (radars != null && radars.Count > 0)
            {
                for (int i = 0; i <= radars.Count - 1; i++)
                {
                    if (radars[i] != null)
                    {
                        int rx = (int)((radars[i].Position.X - viewRect.Left) / 100);
                        int ry = (int)((radars[i].Position.Y - viewRect.Top) / 100);


                        if (rx >= 0 && rx < tilesWidth && ry >= 0 && ry < tilesHeight)
                        {
                            map[rx, ry] = 'R';
                        }
                    }
                }
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
