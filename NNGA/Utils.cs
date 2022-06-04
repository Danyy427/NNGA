using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNGA
{
    internal class Utils
    {
        private static Random random = new Random();

        public static int NextInt(int max)
        {
            return random.Next(max);
        }

        public static int NextInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public static double NextDouble()
        {
            return random.NextDouble();
        }

        public static double NextDouble(int max)
        {
            return random.NextDouble() * max;
        }

        public static double NextDouble(int min, int max)
        {
            return random.NextDouble() * (max - min) + min;
        }

        public static double Tanh(double value)
        {
            return Math.Tanh(value);
        }
    }
}
