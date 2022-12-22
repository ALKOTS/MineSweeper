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
        bool hasBomb = false;
        
        public Field(int rows, int cols)
        {
            this.ColumnCount = cols;
            this.RowCount = rows;
            for (int i = 0; i < rows; i++)
            {
                this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                this.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            }
            this.Dock = DockStyle.Fill;
            this.Location = new Point(0, 0);
            this.Size = new Size(800, 364);
            this.TabIndex = 0;
        }

        public bool has_bombs()
        {
            return hasBomb;
        }

        public void generate_bombs(int rows, int cols, Tile clicked_tile)
        {
            Random rn = new Random();
            int X, Y;



            for (int i = 0; i < 10; i++)
            {
                X = rn.Next(0, 10);
                Y = rn.Next(0, 10);
                Tile tile = (Tile)GetControlFromPosition(X, Y);

                while (tile.is_bomb() || (Y == clicked_tile.get_coords()[0] && X == clicked_tile.get_coords()[1])) //????
                {
                    X = rn.Next(0, 10);
                    Y = rn.Next(0, 10);
                    tile = (Tile)GetControlFromPosition(X, Y);
                }

                Debug.WriteLine(String.Format("{0}: [{1} {2}]", i, X, Y));

                tile.make_bomb();

                for (int a = -1; a < 2; a++)
                {
                    for (int b = -1; b < 2; b++)
                    {
                        if ((X + a > -1) && (Y + b > -1) && (X + a < rows) && (Y + b < cols))
                        {
                            ((Tile)GetControlFromPosition(X + a, Y + b)).add_surrounding_info();
                        }
                    }
                }
            }

            hasBomb = true;
        }
    }
            
}

