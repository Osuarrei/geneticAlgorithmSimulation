using Game1.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Creatures
{
    public partial class Cow
    {
        public int BreedingCooldown { get; set; }
        public float BreedingChance { get; set; }
        public int CurrentCooldownValue { get; set; }

        public bool CanBreed => CurrentCooldownValue > BreedingCooldown;

        public void BreedWith(IBreedable target, List<ICreature> breedables)
        {
            var totalChance = this.BreedingChance * target.BreedingChance;
            if (totalChance > SimulationGlobals.random.NextDouble())
            {
                breedables.Add(new Cow(xloc, yloc) { BreedingCooldown = -10000, cowColor = Color.HotPink });
            }
        }
    }
}
