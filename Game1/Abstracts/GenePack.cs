using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Abstracts
{
    public abstract class GenePack
    {
        public GenePack Combine(GenePack genePack)
        {
            if (this.GetType() == genePack.GetType())
            {
                return ProducePack(genePack);
            }

            throw new ArgumentException("Cannot combine different typpes of genes.");
        }

        protected abstract GenePack ProducePack(GenePack genePack);
    }
}
