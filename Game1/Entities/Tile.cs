using Game1.InfoTransfer;
using Game1.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Entities
{
    public class Tile : Interfaces.IDrawable, IEdible
    {
        public Tile(int x, int y)
        {
            xloc = x;
            yloc = y;
            nutrients[(int)SimulationStateEnums.NutrientTypes.NutrientR] = (float)Tile.tileRandom.NextDouble() * maxNutrient;
            nutrients[(int)SimulationStateEnums.NutrientTypes.NutrientG] = (float)Tile.tileRandom.NextDouble() * maxNutrient;
            nutrients[(int)SimulationStateEnums.NutrientTypes.NutrientB] = (float)Tile.tileRandom.NextDouble() * maxNutrient;
            gatheringDifficulty = (float)SimulationGlobals.random.NextDouble();
            if (gatheringDifficulty > 0.3f)
            {
                gatheringDifficulty = 0.3f;
            }
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(new Vector2(xloc * pixelwidth, yloc * pixelwidth), new Size2(pixelwidth, pixelwidth), new Color(
                    nutrients[(int)SimulationStateEnums.NutrientTypes.NutrientR] / maxNutrient,
                    nutrients[(int)SimulationStateEnums.NutrientTypes.NutrientG] / maxNutrient,
                    nutrients[(int)SimulationStateEnums.NutrientTypes.NutrientB] / maxNutrient
                    ));
        }

        public NutrientPack Eaten(NutrientRequest request)
        {
            if (request.gatheringEffectiveness >= gatheringDifficulty)
            {
                var nutrientPercentage = request.gatheringEffectiveness - gatheringDifficulty;
                var extractedNutrient = nutrients[(int)request.targetNutrient] * nutrientPercentage;
                nutrients[(int)request.targetNutrient] -= extractedNutrient;

                return new NutrientPack(request.targetNutrient, extractedNutrient);
            }
            else
            {
                return new NutrientPack(request.targetNutrient, 0.0f);
            }
        }

        public int xloc { get; set; }
        public int yloc { get; set; }
        private int pixelwidth = (int)SimulationStateEnums.MapValues.TileSize;
        private float[] nutrients = new float[3];
        public float gatheringDifficulty { get; set; }

        private float maxNutrient = 10.0f;
        public static Random tileRandom = new Random();
    }
}
