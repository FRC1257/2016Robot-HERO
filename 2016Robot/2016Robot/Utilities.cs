using System;
using Microsoft.SPOT;

namespace _2016Robot
{
    static class Utilities
    {
        public static void Deadband(ref double value)
        {
            if (System.Math.Abs(value) < 0.1) value = 0;
        }

        public static double Deadband(double value)
        {
            if (System.Math.Abs(value) < 0.1) value = 0;
            return value;
        }

        public static void Limit(ref double value)
        {
            if (value < -1.0) value = -1.0;
            if (value > 1.0) value = 1.0;
        }

        public static double Limit(double value)
        {
            if (value < -1.0) value = -1.0;
            if (value > 1.0) value = 1.0;
            return value;
        }
    }
}
