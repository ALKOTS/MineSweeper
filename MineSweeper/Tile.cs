using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Tile : Button
    {

        bool isBomb = false;
        bool isOpen = false;
        bool isFlagged = false;
        int bombsAround = 0;
        public int[] coords = new int[2];
        public Tile()
        {
            Location = new Point(0, 0);
            UseVisualStyleBackColor = true;
            Size = new Size(25, 25);
            Margin = new Padding(0, 0, 0, 0);
        }

        public int flag(int flags)
        {
            if (isFlagged)
            {
                isFlagged = false;
                //BackColor = Color.White;
                UseVisualStyleBackColor = true;
                flags--;
            }
            else
            {
                isFlagged = true;
                //this.Text = "F";
                BackColor = Color.Red;
                flags++;
            }
            return flags;
        }

        public bool is_flagged()
        {
            return isFlagged;
        }

        public void make_bomb()
        {
            isBomb = true;
            //this.BackColor = Color.Red;
        }

        public bool is_open()
        {
            return isOpen;
        }

        public bool open_tile()
        {
            if (is_flagged())
            {
                return true;
            }
            isOpen = true;
            if (is_bomb())
            {
                return false;
            }
            this.Text = bombsAround.ToString();
            BackColor = Color.White;
            return true;
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
            //this.BackColor = Color.Green;
            
            bombsAround++;
        }

        //public void set_coords(int x, int y)
        //{
        //    coords[0] = x;
        //    coords[1] = y;
        //}

        public int[] get_coords()
        {
            return coords;
        }
    }
}
