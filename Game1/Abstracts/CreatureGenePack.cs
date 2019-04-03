using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game1.SimulationStateEnums;

namespace Game1.Abstracts
{
    public abstract class CreatureGenePack : GenePack
    {
        protected override GenePack ProducePack(GenePack genePack)
        {
            throw new NotImplementedException();
        }

        public CreatureGenePack()
        {
            GatheringEffectiveness = SimulationGlobals.AttributeValues[nameof(SimulationStateEnums.AttributeKeys.CowEffectivenessCap)] * (float)SimulationGlobals.random.NextDouble();
            var numOfNutrients = SimulationGlobals.random.Next(1, 4);
            TargetNutrients = new NutrientTypes[numOfNutrients];
            MaxNutrients = ((float)SimulationGlobals.random.NextDouble() * 30.0f) + 20.0f;
            StarvingNutrientLevel = ((float)SimulationGlobals.random.NextDouble() * 15.0f) + 10.0f;

            List<NutrientTypes> typeHolder = Enum.GetValues(typeof(NutrientTypes)).Cast<NutrientTypes>().ToList();

            for (int i = 0; i < numOfNutrients; i++)
            {
                var chosenIndex = SimulationGlobals.random.Next(0, typeHolder.Count);
                TargetNutrients[i] = typeHolder[chosenIndex];
                typeHolder.RemoveAt(chosenIndex);
            }

            

            BreedingCooldown = SimulationGlobals.random.Next((int)SimulationGlobals.AttributeValues[nameof(SimulationStateEnums.AttributeKeys.CowBreedCooldownMin)], 1500);
            BreedingChance = (float)SimulationGlobals.random.NextDouble() * SimulationGlobals.AttributeValues[nameof(AttributeKeys.CowBreedabilityCap)];

            MaxHealth = SimulationGlobals.random.Next(50, 101);
            RestoreHealthRate = (float)SimulationGlobals.random.NextDouble() * 0.1f;
        }

        public int BreedingCooldown { get; protected set; }
        public float BreedingChance { get; protected set; }


        public float gatheringDifficulty { get; protected set; }
        public NutrientTypes[] TargetNutrients { get; protected set; }

        public float GatheringEffectiveness { get; protected set; }
        public float MaxNutrients { get; protected set; }
        public float StarvingNutrientLevel { get; protected set; }

        public int MaxHealth { get; protected set; }
        public float RestoreHealthRate { get; protected set; }
    }
}
