using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Interfaces
{
    public interface ICreature : IEdible, IDrawable, IHungry, IMovable, IUpdateable, IBreedable, IHealthy
    {
        
    }
}
