﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLAPI
{
    public static class BlFactory
    {
        public static IBL GetBl() => new BL.BLIMP();
    }
}
