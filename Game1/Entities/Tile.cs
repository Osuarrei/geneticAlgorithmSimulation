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
            spriteBatch.FillRectangle(new Vector2(xloc * pixelwidth, yloc * pixelwidth), new Size2(pixelwidth, pixelwidth), NutrientsToColor());
        }

        public void AddNutrients(NutrientPack nutrientPack)
        {
            this.nutrients[(int)nutrientPack.type] += nutrientPack.amount;
        }

        public void AddNutrients(NutrientPack[] packs)
        {
            foreach (var item in packs)
            {
                this.AddNutrients(item);
            }
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

        private Color NutrientsToColor()
        {
            //get percentage of nutrients
            var saturationPercentage = nutrients.Sum() / (maxNutrient * nutrients.Length);
            Color colorHolder;
            if (saturationPercentage <= 0.1f)
            {
                colorHolder = new Color(0XE0, 0X8A, 0X3E);
            }
            else if (saturationPercentage <= 0.2f)
            {
                colorHolder = new Color(0XD4, 0X9F, 0X37);
            }
            else if (saturationPercentage <= 0.3f)
            {
                colorHolder = new Color(0XC9, 0XB2, 0X30);
            }
            else if (saturationPercentage <= 0.4f)
            {
                colorHolder = new Color(0XB8, 0XBE, 0X2A);
            }
            else if (saturationPercentage <= 0.5f)
            {
                colorHolder = new Color(0X92, 0XB3,0X25);
            }
            else if (saturationPercentage <= 0.6f)
            {
                colorHolder = new Color(0X6F, 0XA7, 0X20);
            }
            else if (saturationPercentage <= 0.7f)
            {
                colorHolder = new Color(0X4E, 0X9C, 0X1B);
            }
            else if (saturationPercentage <= 0.8f)
            {
                colorHolder = new Color(0X30, 0X91, 0X16);
            }
            else if (saturationPercentage <= 0.9f)
            {
                colorHolder = new Color(0X15, 0X86, 0X12);
            }
            else if (saturationPercentage <= 1.0f)
            {
                colorHolder = new Color(0X0F, 0X7B, 0X20);
            }
            else
            {
                colorHolder = new Color(0XE0, 0X8A, 0X3E);
            }

            return colorHolder;
        }

        public int xloc { get; set; }
        public int yloc { get; set; }
        private int pixelwidth = (int)SimulationStateEnums.MapValues.TileSize;
        private float[] nutrients = new float[3];
        public float gatheringDifficulty { get; set; }

        private float maxNutrient = 40.0f;
        public static Random tileRandom = new Random();
    }
}
