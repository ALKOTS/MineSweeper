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
                    if (sender.is_bomb())
                    {
                        end_game(false);
                    }
                    else
                    {
                        sender.Text = sender.get_surroundings().ToString();
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
