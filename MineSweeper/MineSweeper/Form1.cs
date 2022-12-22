using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MineSweeper
{

    public class Tile : Button
    {

        bool isBomb = false;
        int bombsAround = 0;
        public Tile() { 
        }

        public void make_bomb()
        {
            isBomb = true;
            this.BackColor= Color.Red;
        }

        public bool is_bomb()
        {
            return isBomb;
        }

        public int get_surroundings()
        {
            return bombsAround;
        }

        public void add_surrounding_info()
        {
            this.BackColor= Color.Green;
            bombsAround++;
        }
    }
    public partial class Form1 : Form
    {
        int rows = 10;
        int cols = 10;

        Tile[][] field_arr= new Tile[10][];
        public Form1()
        {
            InitializeComponent();
        }

        private void generate_field()
        {
            TableLayoutPanel field = new TableLayoutPanel();
            splitContainer1.Panel2.Controls.Clear();

            field.ColumnCount = cols;
            field.RowCount = rows;
            for (int i = 0; i < rows; i++)
            {
                field.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                field.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            }
            field.Dock = DockStyle.Fill;
            field.Location = new Point(0, 0);
            field.Size = new Size(800, 364);
            field.TabIndex = 0;

            for (int i = 0; i < rows; i++)
            {
                field_arr[i] = new Tile[cols];
                for (int j = 0; j < cols; j++)
                {
                    
                    field_arr[i][j] = new Tile
                    {
                        Dock = DockStyle.Fill,
                        TabIndex = 0,
                        //Text = String.Format("{0}, {1}", i, j),
                        UseVisualStyleBackColor = true,
                    };
                    field_arr[i][j].Click += (sender, e2) => tile_Click((Tile)sender, e2);
                };
                field.Controls.AddRange(field_arr[i]);
            }

            Random rn = new Random();
            int X, Y;

            for (int i = 0; i < 10; i++)
            {
                X = rn.Next(0, 10);
                Y = rn.Next(0, 10);
                while(field_arr[X][Y].is_bomb())
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
                        if((X+a > -1)&&(Y+b > -1)&&(X+a < rows)&&(Y+b < cols)&&(!field_arr[X+a][Y+b].is_bomb()))
                        {
                            field_arr[X+a][Y+b].add_surrounding_info();
                        }
                    }
                }
            }

            splitContainer1.Panel2.Controls.Add(field);
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            generate_field();
        }

        private void tile_Click(Tile sender, EventArgs e)
        {
            sender.Text = sender.is_bomb() ? "Bomb" : sender.get_surroundings().ToString();
            Debug.WriteLine(e.ToString());
        }
    }
}