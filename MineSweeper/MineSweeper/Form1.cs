using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class Form1 : Form
    {
        int rows = 10;
        int cols = 10;
        Field field;

        public Form1()
        {
            InitializeComponent();
        }

        private bool open_tile(Tile tile)
        {
            if (tile.is_open())
            {
                return true;
            }

            if (!tile.open())
            {
                return false;
            }

            if (tile.get_surroundings() == 0)
            {
                int x = tile.get_coords()[0];
                int y = tile.get_coords()[1];
                for (int a = -1; a < 2; a++)
                {
                    for (int b = -1; b < 2; b++)
                    {
                        if ((x + a > -1) && (y + b > -1) && (x + a < rows) && (y + b < cols))
                        {
                            bool temp_val = open_tile(((Tile)field.GetControlFromPosition(x + a, y + b)));
                        }
                    }
                }


            }

            return true;
        }

        private void end_game(bool result)
        {
            if (result)
            {
                MessageBox.Show("You win!");
            }
            else
            {
                MessageBox.Show("You lose!");
            }
        }

        private void generate_field()
        {
            field = new Field(rows, cols);
            splitContainer1.Panel2.Controls.Clear();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Tile tile = new Tile
                    {
                        Dock = DockStyle.Fill,
                        TabIndex = 0,
                        UseVisualStyleBackColor = true,  
                    };
                    tile.set_coords(i, j);
                    tile.MouseDown += (sender, e2) => tile_Click((Tile)sender, e2);
                    field.Controls.Add(tile);
                };
            }

            splitContainer1.Panel2.Controls.Add(field);
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            generate_field();
        }

        private void tile_Click(Tile sender, MouseEventArgs e)
        {
            if (!field.has_bombs())
            {
                field.generate_bombs(rows, cols, sender);
            }
            

            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (!open_tile(sender))
                    {
                        end_game(false);
                    }

                    break;
                case MouseButtons.Right:
                    sender.Text = "Flag";
                    break;
                default:
                    Debug.WriteLine("Вася чорт");
                    break;
            }
        }
    }
}
