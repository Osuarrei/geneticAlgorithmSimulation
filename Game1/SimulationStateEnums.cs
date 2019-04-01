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
            CowEffectivenessCap,
            CowBreedabilityCap,
            CowBreedCooldownMin
        }

        public enum TileColors
        {
            nutrient100 = 0X0F7B20,
            nutrient90  = 0X158612,
            nutrient80  = 0X309116,
            nutrient70  = 0X4E9C1B,
            nutrient60  = 0X6FA720,
            nutrient50  = 0X92B325,
            nutrient40  = 0XB8BE2A,
            nutrient30  = 0XC9B230,
            nutrient20  = 0XD49F37,
            nutrient10  = 0XE08A3E
        }
    }
}
