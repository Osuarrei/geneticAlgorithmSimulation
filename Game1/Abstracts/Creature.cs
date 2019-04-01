using Game1.Entities;
using Game1.InfoTransfer;
using Game1.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game1.SimulationStateEnums;

namespace Game1.Abstracts
{
    public abstract class Creature : ICreature
    {
        protected int xloc { get; set; }
        protected int yloc { get; set; }

        protected int BreedingCooldown { get; set; }
        protected int CurrentBreedingCooldown { get; set; }
        public float BreedingChance { get; protected set; }

        protected float[] NutrientReserves { get; set; }
        protected NutrientTypes[] TargetNutrients { get; set; }

        public float gatheringDifficulty { get; private set; }

        protected float GatheringEffectiveness { get; set; }
        protected float MaxNutrients { get; set; }
        protected float StarvingNutrientLevel { get; set;}

        public bool CanBreed => BreedingCooldown >= CurrentBreedingCooldown;

        public bool IsDead => throw new NotImplementedException();

        public void BreedWith(IBreedable target, List<ICreature> breedables)
        {
            if (this.CanBreed && target.CanBreed)
            {
                if (this.GetType() == target.GetType())
                {
                    var chanceHolder = this.BreedingChance * target.BreedingChance;
                    if (chanceHolder > SimulationGlobals.random.NextDouble())
                    {
                        breedables.Add((ICreature)Activator.CreateInstance(this.GetType(), new object[] { this.xloc, this.yloc }));
                        CurrentBreedingCooldown = 0;
                        target.ResetBreedingCooldown();
                    }
                }
            }
        }

        public void ResetBreedingCooldown()
        {
            this.CurrentBreedingCooldown = 0;
        }

        public abstract void Draw(GraphicsDevice graphicsDevice);

        public abstract void Draw(SpriteBatch spriteBatch);

        public void Eat(IEdible edible)
        {
            var nutrientHolder = edible.Eaten(new InfoTransfer.NutrientRequest(TargetNutrients[SimulationGlobals.random.Next(0, TargetNutrients.Length)], GatheringEffectiveness));
            NutrientReserves[(int)nutrientHolder.type] += nutrientHolder.amount;
        }

        public NutrientPack Eaten(NutrientRequest request)
        {
            throw new NotImplementedException();
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

        public void Update(Map map)
        {
            throw new NotImplementedException();
        }

        public void UpdateHealth()
        {
            throw new NotImplementedException();
        }
    }
}
