using Game1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Creatures
{
    public partial class Cow
    {
        public int MaxHealth
        {
            get => _maxHealth;
            private set => _maxHealth = value;
        }
        private int _maxHealth;

        public int CurrentHealth
        {
            get => _currentHealth;
            private set => _currentHealth = value;
        }
        private int _currentHealth;

        public float RestoreHealthRate
        {
            get => _resotoreHealthRate;
            private set => _resotoreHealthRate = value;
        }
        private float _resotoreHealthRate;

        public bool KillMe { get; private set; }
    }
}
