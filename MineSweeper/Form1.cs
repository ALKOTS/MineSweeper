using System;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class Form1 : Form
    {
        int rows = 10;
        int cols = 10;
        int bombs_amount = 10;
        int free_tiles = 0;
        int flags = 0;
        Tile[] bombs;

        Field field;
        bool isGameOver = false;
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();
            

            
            timer1.Interval = 1000;
            timer1.Tick += new System.EventHandler(timeCountDown);
            

            resetBtn_Click(null, null);
        }

       
        private void timeCountDown(object? sender, EventArgs e)
        {
            timerLbl.Text = (int.Parse(timerLbl.Text) + 1).ToString();
        }


        private bool open_tile(Tile tile)
        {
            if (tile.is_open())
            {
                return true;
            }

            if (!tile.open_tile())
            {
                return false;
            }

            free_tiles ++;
            

            if (tile.get_surroundings() == 0)
            {
                int x = tile.get_coords()[1];
                int y = tile.get_coords()[0]; // I'm not sure why this is reversed, but it is.
                for (int a = -1; a < 2; a++)
                {
                    for (int b = -1; b < 2; b++)
                    {
                        if ((x + a > -1) && (y + b > -1) && (x + a < rows) && (y + b < cols))
                        {
                            open_tile(((Tile)field.GetControlFromPosition(x + a, y + b)));
                        }
                    }
                }
            }

            return true;
        }

        private void repeated_click(Tile tile) //Вся эта функция была написана копайлотом от начала и до конца
        {
            int x = tile.get_coords()[1];
            int y = tile.get_coords()[0]; // I'm not sure why this is reversed, but it is.
            int flags_around = 0;
            for (int a = -1; a < 2; a++)
            {
                for (int b = -1; b < 2; b++)
                {
                    if ((x + a > -1) && (y + b > -1) && (x + a < rows) && (y + b < cols))
                    {
                        if (((Tile)field.GetControlFromPosition(x + a, y + b)).is_flagged())
                        {
                            flags_around += 1;
                        }
                    }
                }
            }

            if (flags_around >= tile.get_surroundings())
            {
                for (int a = -1; a < 2; a++)
                {
                    for (int b = -1; b < 2; b++)
                    {
                        if ((x + a > -1) && (y + b > -1) && (x + a < rows) && (y + b < cols) && !((Tile)field.GetControlFromPosition(x + a, y + b)).is_flagged())
                        {
                            if (!open_tile(((Tile)field.GetControlFromPosition(x + a, y + b))))
                            {
                                end_game(false);
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void end_game(bool result)
        {
            for (int i = 0; i < bombs_amount; i++)
            {
                bombs[i].BackColor = Color.Black;
            }
            if (result)
            {
                resetBtn.BackColor = Color.Green;
            }
            else
            {
                resetBtn.Text = ":C";
            }
            isGameOver = true;
            timer1.Stop();
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

        private void resetBtn_Click(object? sender, EventArgs? e)
        {
            isGameOver = false;
            scoreLbl.Text = bombs_amount.ToString();
            flags = 0;
            free_tiles = 0;
            resetBtn.Text = ":)";
            timerLbl.Text = "0";
            resetBtn.UseVisualStyleBackColor = true;

            generate_field();
            timer1.Start();
        }

        private void tile_Click(Tile sender, MouseEventArgs e)
        {
            
            if (isGameOver)
            {
                return;
            }
            if (!field.has_bombs())
            {
                bombs = field.generate_bombs(rows, cols, sender, bombs_amount);
            }
            

            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (sender.is_flagged())
                    {
                        return;
                    }
                    
                    if (sender.is_open())
                    {
                        repeated_click(sender);
                    }
                    
                    if (!open_tile(sender))
                    {
                        end_game(false);
                        return;
                    }
                    
                    break;
                case MouseButtons.Right:
                    if (sender.is_open())
                    {
                        return;
                    }
                    if (sender.is_flagged())
                    {
                        flags --;
                    }
                    else
                    {
                        flags ++;
                    }
                    sender.flag();
                    
                    break;
                default:
                    Debug.WriteLine("Вася чорт");
                    break;
            }
            if ((flags == bombs_amount) && (free_tiles == rows * cols - bombs_amount))
            {
                end_game(true);
            }
            scoreLbl.Text = (bombs_amount - flags).ToString();
        }
    }
}
