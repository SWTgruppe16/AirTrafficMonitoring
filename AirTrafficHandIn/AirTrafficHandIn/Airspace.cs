using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficHandIn
{
    public class Airspace
    {
        public int X;
        public int Y;
        public int Z;
        public int width; //X akse
        public int height; //Z akse
        public int depth; //Y akse

        public bool IsInside(int x, int y, int z)
        {
            // Verify X
            if (x < X)
            {
                return false;
            }

            if (x > (width + X))
            {
                return false;
            }

            // Verify y
            if (y < Y)
            {
                return false;
            }

            if (y > (depth + Y))
            {
                return false;
            }

            // Verify Z
            if (z < Z)
            {
                return false;
            }

            if (z > (height + Z))
            {
                return false;
            }

            return true;
        }
    }

    
}
