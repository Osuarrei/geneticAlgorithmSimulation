using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public static class SimulationStateEnums
    {
        public enum MapValues
        {
            TileSize = 10,
            MapWidthTiles = 100,
            MapHeightTiles = 60
        }
        public enum WindowSize
        {
            Height=MapValues.TileSize * MapValues.MapHeightTiles,
            Width = MapValues.TileSize * MapValues.MapWidthTiles
        }

        public enum NutrientTypes
        {
            NutrientR,
            NutrientG,
            NutrientB
        }

        public enum AttributeKeys
        {
            CowEffectivenessCap
        }
    }
}
