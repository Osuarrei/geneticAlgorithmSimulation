﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Interfaces
{
    public interface IMovable
    {
        int xloc { get; set; }
        int yloc { get; set; }

        void MoveMe();
    }
}
