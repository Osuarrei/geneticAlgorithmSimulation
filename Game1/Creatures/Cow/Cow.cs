using Game1.Abstracts;
using Game1.Entities;
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
using static Game1.SimulationStateEnums;

namespace Game1.Creatures
{
    public partial class Cow : Creature
    {
        public Cow(int xloc, int yloc)
        {
            this.xloc = xloc;
            this.yloc = yloc;
            GatheringEffectiveness = SimulationGlobals.AttributeValues[nameof(SimulationStateEnums.AttributeKeys.CowEffectivenessCap)] * (float)SimulationGlobals.random.NextDouble();
            var numOfNutrients = SimulationGlobals.random.Next(1, 4);
            TargetNutrients = new NutrientTypes[numOfNutrients];
            NutrientReserves = new float[3];
            MaxNutrients = ((float)SimulationGlobals.random.NextDouble()*30.0f) + 20.0f;
            StarvingNutrientLevel = ((float)SimulationGlobals.random.NextDouble()*15.0f) + 10.0f;

            List<NutrientTypes> typeHolder = Enum.GetValues(typeof(NutrientTypes)).Cast<NutrientTypes>().ToList();

            for (int i = 0; i < numOfNutrients; i++)
            {
                var chosenIndex = SimulationGlobals.random.Next(0,typeHolder.Count);
                TargetNutrients[i] = typeHolder[chosenIndex];
                typeHolder.RemoveAt(chosenIndex);
            }

            BreedingCooldown = SimulationGlobals.random.Next((int)SimulationGlobals.AttributeValues[nameof(SimulationStateEnums.AttributeKeys.CowBreedCooldownMin)], 1500);
            BreedingChance = (float)SimulationGlobals.random.NextDouble() * SimulationGlobals.AttributeValues[nameof(AttributeKeys.CowBreedabilityCap)];
            this.CurrentBreedingCooldown = 0;

            cowColor = Color.Black;

            MaxHealth = SimulationGlobals.random.Next(50, 101);
            CurrentHealth = this.MaxHealth;
            RestoreHealthRate = (float)SimulationGlobals.random.NextDouble() * 0.1f;
        }

        private Color cowColor { get; set; }
    }
}
