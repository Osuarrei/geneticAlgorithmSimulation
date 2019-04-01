using Game1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Creatures
{
    public partial class Cow
    {
        public float[] NutrientReserves { get; private set; }
        public float GatheringEffectiveness { get; set; }
        public SimulationStateEnums.NutrientTypes[] TargetNutrients { get; set; }
        public float MaxNutrients { get; private set; }
        public float StarvingNutrientLevel { get; private set; }

        public void Eat(IEdible edible)
        {
            var nutrientHolder = edible.Eaten(new InfoTransfer.NutrientRequest(TargetNutrients[SimulationGlobals.random.Next(0, TargetNutrients.Length)], GatheringEffectiveness));
            NutrientReserves[(int)nutrientHolder.type] += nutrientHolder.amount;
        }
    }
}