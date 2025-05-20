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

        public Map()
        {
            tilesWidth = ((int)(Engine.Get.Window.Size.X / 100));
            tilesHeight = ((int)(Engine.Get.Window.Size.Y / 100));
            map = new char[tilesWidth, tilesHeight];
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            DrawBoard();
            Console.Clear();
        }

        void DrawBoard()
        {
            for (int fil = 0; fil < map.GetLength(1); fil++)
            {
                DrawRowSeparator();
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    Console.Write('|');
                    FillTile();
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
        void FillTile()
        {
            Console.Write(" · ");
        }
    }
}
