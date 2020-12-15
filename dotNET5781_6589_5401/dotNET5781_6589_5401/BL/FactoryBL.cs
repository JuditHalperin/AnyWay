using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public static class FactoryBL
    {
        public static IBL BlInstance
        {
            get => MyBL.getInstance();
        }
    }
}
