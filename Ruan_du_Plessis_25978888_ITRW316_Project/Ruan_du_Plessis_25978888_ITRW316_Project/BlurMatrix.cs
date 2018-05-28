using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruan_du_Plessis_25978888_ITRW316_Project
{
    class BlurMatrix
    {
        public static double[,] Average3x3
        {
            get
            {
                return new double[,]
                { { 1, 1, 1, },
                  { 1, 1, 1, },
                  { 1, 1, 1, }, };
            }
        }

        public static double[,] Average5x5
        {
            get
            {
                return new double[,]
                { { 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, }, };
            }
        }

        public static double[,] Average7x7
        {
            get
            {
                return new double[,]
                { { 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, }, };
            }
        }

        public static double[,] Average9x9
        {
            get
            {
                return new double[,]
                { { 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, }, };
            }
        }

        public static double[,] Average11x11
        {
            get
            {
                return new double[,]
                { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, }, };
            }
        }

    }
}
