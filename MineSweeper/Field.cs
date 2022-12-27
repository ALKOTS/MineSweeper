using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Field : TableLayoutPanel
    {
        bool hasBombs = false;
        
        public Field(int rows, int cols)
        {
            Dock = DockStyle.Fill;
            Anchor = AnchorStyles.None;
            Location = new Point(0, 0);
            Size = new Size(25 * cols, 25 * rows);

            ColumnCount = cols;
            RowCount = rows;
            for (int i = 0; i < rows; i++)
            {    
                RowStyles.Add(new RowStyle(SizeType.Percent));
            }
            for (int i = 0; i < cols; i++)
            {
                ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
            }

            Dock = DockStyle.Fill;
            Location = new Point(0, 0);
            //Size = new Size(800, 364);
            TabIndex = 0;
        }

        public bool has_bombs()
        {
            return hasBombs;
        }

        public Tile[] generate_bombs(int rows, int cols, Tile clicked_tile, int bombs_amount)
        {
            Random rn = new Random();
            int X, Y;

            Tile[] bombs = new Tile[bombs_amount];
            



            for (int i = 0; i < bombs_amount; i++)
            {
                X = rn.Next(0, cols);
                Y = rn.Next(0, rows);
                Tile tile = (Tile)GetControlFromPosition(X, Y);

                while (tile.is_bomb() || (X == clicked_tile.get_coords()[0] && Y == clicked_tile.get_coords()[1])) 
                {
                    X = rn.Next(0, cols);
                    Y = rn.Next(0, rows);
                    tile = (Tile)GetControlFromPosition(X, Y);
                }

                Debug.WriteLine(String.Format("{0}: [{1} {2}]", i, X, Y));

                tile.make_bomb();

                for (int a = -1; a < 2; a++)
                {
                    for (int b = -1; b < 2; b++)
                    {
                        if ((Y + a > -1) && (X + b > -1) && (Y + a < rows) && (X + b < cols))
                        {
                            ((Tile)GetControlFromPosition(X + b, Y + a)).add_surrounding_info();
                        }
                    }
                }
                bombs[i] = tile;
            }

            hasBombs = true;
            return bombs;
        }
    }
            
}

