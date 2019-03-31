using Game1.Entities;
using Game1.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game1.SimulationStateEnums;

namespace Game1.Creatures
{
    public class Cow : Interfaces.IDrawable, Interfaces.IMovable, Interfaces.IUpdateable, IHungry
    {
        public Cow(int xloc, int yloc)
        {
            this.xloc = xloc;
            this.yloc = yloc;
            GatheringEffectiveness = SimulationGlobals.AttributeValues[nameof(SimulationStateEnums.AttributeKeys.CowEffectivenessCap)] * (float)SimulationGlobals.random.NextDouble();
            var numOfNutrients = SimulationGlobals.random.Next(1, 3);
            targetNutrients = new NutrientTypes[numOfNutrients];

            for (int i = 0; i < numOfNutrients; i ++)
            {
                targetNutrients[i] = (NutrientTypes)SimulationGlobals.random.Next(1, 3);
            }
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle(new CircleF(new Point(xloc*(int)SimulationStateEnums.MapValues.TileSize - ((int)SimulationStateEnums.MapValues.TileSize / 2), 
                yloc* (int)SimulationStateEnums.MapValues.TileSize - ((int)SimulationStateEnums.MapValues.TileSize / 2)), (int)SimulationStateEnums.MapValues.TileSize * 0.5f), 
                8, Color.White, 4.0f);
        }

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

        public void Update()
        {
            switch (SimulationGlobals.random.Next(0, 2))
            {
                case 0:
                    MoveMe();
                    break;
                case 1:
                    Console.WriteLine("I would have eaten");
                    break;
            }
        }

        public void Eat(IEdible edible)
        {
             var nutrientHolder = edible.Eaten(new InfoTransfer.NutrientRequest(targetNutrients[SimulationGlobals.random.Next(0, targetNutrients.Length)], GatheringEffectiveness));
            Console.WriteLine($"Got {nutrientHolder.type.ToString()} amount: {nutrientHolder.amount}");
        }

        public int xloc { get; set; }
        public int yloc { get; set; }
        public float GatheringEffectiveness { get; set; }
        public SimulationStateEnums.NutrientTypes[] targetNutrients { get; set; }
    }
}
