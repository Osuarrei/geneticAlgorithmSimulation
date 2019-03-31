﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public static class SimulationGlobals
    {
        public static Random random = new Random();

        public static Dictionary<string, float> AttributeValues = new Dictionary<string, float>();

        static SimulationGlobals()
        {
            AttributeValues.Add(nameof(SimulationStateEnums.AttributeKeys.CowEffectivenessCap), 0.5f);
        }
    }

}
