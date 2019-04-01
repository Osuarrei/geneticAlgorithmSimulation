using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game1.SimulationStateEnums;

namespace Game1.Interfaces
{
    public interface IHungry
    {
        void Eat(IEdible edible);
        float GatheringEffectiveness { get; }
    }
}
