﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Interfaces
{
    public interface IHealthy
    {
        int MaxHealth { get; }
        int CurrentHealth { get; }
        float RestoreHealthRate { get; }
        bool KillMe { get; }
    }
}