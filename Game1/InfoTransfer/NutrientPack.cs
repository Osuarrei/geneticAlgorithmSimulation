using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game1.SimulationStateEnums;

namespace Game1.InfoTransfer
{
    public class NutrientPack
    {
        public NutrientPack(NutrientTypes type, float amount)
        {
            this.type = type;
            this.amount = amount;
        }

        public NutrientTypes type { get; private set; }
        public float amount { get; private set; }
    }
}
