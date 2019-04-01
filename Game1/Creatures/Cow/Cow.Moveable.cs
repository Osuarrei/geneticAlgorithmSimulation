using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Creatures
{
    public partial class Cow
    {
        public int xloc { get; set; }
        public int yloc { get; set; }

        public void MoveMe()
        {
            switch (SimulationGlobals.random.Next(0, 4))
            {
                case 0:
                    if (yloc + 1 < (int)SimulationStateEnums.MapValues.MapHeightTiles)
                    {
                        yloc++;
                    }
                    break;
                case 1:
                    if (xloc + 1 < (int)SimulationStateEnums.MapValues.MapWidthTiles)
                    {
                        xloc++;
                    }
                    break;
                case 2:
                    if (yloc - 1 > -1)
                    {
                        yloc--;
                    }
                    break;
                case 3:
                    if (xloc - 1 > -1)
                    {
                        xloc--;
                    }
                    break;
            }
        }
    }
}
