﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Interfaces
{
    public interface IBreedable
    {
        void BreedWith(IBreedable target, List<ICreature> breedables);
        float BreedingChance { get; }
        bool CanBreed { get; }
        void ResetBreedingCooldown();
    }
}
