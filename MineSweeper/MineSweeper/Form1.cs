using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class Form1 : Form
    {
        int rows = 10;
        int cols = 10;
        Field field = new Field(10, 10);

        Tile[][] field_arr= new Tile[10][];
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
            
            splitContainer1.Panel2.Controls.Clear();

            for (int i = 0; i < rows; i++)
            {
                field_arr[i] = new Tile[cols];
                for (int j = 0; j < cols; j++)
                {
                    
                    field_arr[i][j] = new Tile
                    {
                        Dock = DockStyle.Fill,
                        TabIndex = 0,
                        UseVisualStyleBackColor = true,  
                    };
                    field_arr[i][j].set_coords(i, j);
                    field_arr[i][j].MouseDown += (sender, e2) => tile_Click((Tile)sender, e2);
                };
                field.Controls.AddRange(field_arr[i]);
            }
            //________________________
            Random rn = new Random();
            int X, Y;

            for (int i = 0; i < 10; i++)
            {
                X = rn.Next(0, 10);
                Y = rn.Next(0, 10);
                while (field_arr[X][Y].is_bomb())
                {
                    X = rn.Next(0, 10);
                    Y = rn.Next(0, 10);
                }

                Debug.WriteLine(String.Format("{0}: [{1} {2}]", i, X, Y));

                field_arr[X][Y].make_bomb();

                for (int a = -1; a < 2; a++)
                {
                    for (int b = -1; b < 2; b++)
                    {
                        if ((X + a > -1) && (Y + b > -1) && (X + a < rows) && (Y + b < cols) && (!field_arr[X + a][Y + b].is_bomb()))
                        {
                            field_arr[X + a][Y + b].add_surrounding_info();
                        }
                    }
                }
            }
            //________________
            splitContainer1.Panel2.Controls.Add(field);
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            generate_field();
        }

        private void tile_Click(Tile sender, MouseEventArgs e)
        {
            
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
