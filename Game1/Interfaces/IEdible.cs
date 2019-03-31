using Game1.InfoTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Interfaces
{
    public interface IEdible
    {
        NutrientPack Eaten(NutrientRequest request);
        float gatheringDifficulty { get; set; }
    }
}
