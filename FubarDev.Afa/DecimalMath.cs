using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa
{
    static class DecimalMath
    {
        // http://www.daimi.au.dk/~ivan/FastExpproject.pdf
        // Left to Right Binary Exponentiation
        public static decimal Pow(decimal x, int y)
        {
            if (y == 0)
                return 1;

            if (y == 1)
                return x;

            decimal A = 1m;
            var e = new BitArray(BitConverter.GetBytes(y));
            int t = e.Length;

            for (int i = t - 1; i >= 0; --i)
            {
                A *= A;
                if (e[i])
                    A *= x;
            }
            return A;
        }

        /// <summary>
        /// Nth root
        /// </summary>
        /// <param name="value"></param>
        /// <param name="rootPower"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://www.vbforums.com/showthread.php?695303-%E2%88%9A%E2%88%9A%E2%88%9A-Root-of-a-number-as-a-Decimal-%E2%88%9A%E2%88%9A%E2%88%9A&p=4260359&viewfull=1#post4260359
        /// </remarks>
        public static decimal Root(decimal value, int rootPower)
        {
            var guess = (decimal)Math.Pow((double)value, 1.0 / rootPower);
            decimal oldGuess;
            decimal delta;
            var tolerance = 1.0e-25m;
            do
            {
                oldGuess = guess;
                guess = oldGuess - ((Pow(oldGuess, rootPower) - value) / (rootPower * Pow(oldGuess, rootPower - 1)));
                delta = guess - oldGuess;
            } while (Math.Abs(delta) >= tolerance);
            if (Math.Abs(delta) > 0)
                guess = NthRoot(value, rootPower, guess);
            return guess;
        }

        /// <summary>
        /// Nth root
        /// </summary>
        /// <param name="baseValue"></param>
        /// <param name="rootPower"></param>
        /// <param name="guess"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/3848640/n-th-root-algorithm
        /// </remarks>
        private static decimal NthRoot(decimal baseValue, int rootPower, decimal guess)
        {
            if (rootPower == 1)
                return baseValue;
            decimal deltaX;
            do
            {
                deltaX = (baseValue / Pow(guess, rootPower - 1) - guess) / rootPower;
                guess = guess + deltaX;
            } while (Math.Abs(deltaX) > 0);
            return guess;
        }

        //public static decimal NthRoot(decimal baseValue, uint rootPower)
        //{
        //    if (rootPower == 1)
        //        return baseValue;
        //    var guess = NthRoot(baseValue, rootPower, 0.1m);
        //    return guess;
        //}
    }
}
