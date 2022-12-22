using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Tile : Button
    {

        bool isBomb = false;
        int bombsAround = 0;
        int[] coords = new int[2];
        public Tile()
        {
        }

        public void make_bomb()
        {
            isBomb = true;
            this.BackColor = Color.Red;
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

        public void set_coords(int x, int y)
        {
            coords[0] = x;
            coords[1] = y;
        }

        public int[] get_coords()
        {
            return coords;
        }
    }
}
