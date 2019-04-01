using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game1.SimulationStateEnums;

namespace Game1
{
    public static class SimulationGlobals
    {
        public static Random random = new Random();

        public static Dictionary<string, float> AttributeValues = new Dictionary<string, float>();

        static SimulationGlobals()
        {
            AttributeValues.Add(nameof(AttributeKeys.CowEffectivenessCap), 0.5f);
            AttributeValues.Add(nameof(AttributeKeys.CowBreedabilityCap), 0.1f);
            AttributeValues.Add(nameof(AttributeKeys.CowBreedCooldownMin), 200f);

        }
    }

}
