using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Interfaces
{
    public interface IBreedable
    {
        void BreedWith(IBreedable target, List<IBreedable> breedables);
        int BreedingCooldown { get; set; }
        float BreedingChance { get; set; }
        int CurrentCooldownValue { get; set; }
        bool CanBreed { get; }
    }
}
