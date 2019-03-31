using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game1.SimulationStateEnums;

namespace Game1.InfoTransfer
{
    public class NutrientRequest
    {
        public NutrientTypes targetNutrient { get; set; }
        public float gatheringEffectiveness;

        public NutrientRequest(NutrientTypes target, float effectiveness)
        {
            targetNutrient = target;
            gatheringEffectiveness = effectiveness;
        }
    }
}
