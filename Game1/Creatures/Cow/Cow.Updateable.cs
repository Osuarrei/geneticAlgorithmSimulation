using Game1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Creatures
{
    public partial class Cow
    {
        public void Update(Map map)
        {
            //do health updating here
            foreach (var target in TargetNutrients)
            {
                if (NutrientReserves[(int)target] < StarvingNutrientLevel)
                {
                    //reduce health
                    this.CurrentHealth--;
                    if (this.CurrentHealth <= 0)
                    {
                        KillMe = true;
                    }
                }
                else if (NutrientReserves[(int)target] > (2 * StarvingNutrientLevel))
                {
                    //add to health
                    var amountToAdd = this.MaxHealth * this.RestoreHealthRate;
                    if (this.CurrentHealth + amountToAdd < this.MaxHealth)
                    {
                        this.CurrentHealth += (int)amountToAdd;
                    }
                    else
                    {
                        this.CurrentHealth = this.MaxHealth;
                    }
                }
            }

            if (CurrentCooldownValue <= BreedingCooldown)
            {
                CurrentCooldownValue++;
            }

            switch (SimulationGlobals.random.Next(0, 11))
            {
                case 1:
                    MoveMe();
                    break;
                default:
                    Eat(map.GetTile(xloc, yloc));
                    break;
            }
        }
    }
}
