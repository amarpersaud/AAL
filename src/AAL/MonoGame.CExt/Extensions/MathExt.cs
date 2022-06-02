using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.Extensions
{
    public static class MathExt
    {

        /// <summary>
        /// PI constant
        /// </summary>
        public const float PI = 3.141592653589793238462643383279502884187169f;
        
        /// <summary>
        /// 2 PI, for float arithmetic
        /// </summary>
        public const float TWOPI = 2.0f * PI;

        /// <summary>
        /// Gives sign of a value
        /// </summary>
        /// <param name="x">input value</param>
        /// <returns>1 if positive or zero, -1 if negative.</returns>
        public static float Sign(this float x)
        {
            if(x >= 0)
            {
                return 1;
            }
            return -1;
        }

        /// <summary>
        /// Returns absolute value of a float
        /// </summary>
        /// <param name="x">input value</param>
        /// <returns>absolute value of x</returns>
        public static float Abs(this float x)
        {
            return x > 0 ? x : -x;
        }

        /// <summary>
        /// Returns absolute value of an integer
        /// </summary>
        /// <param name="x">input value</param>
        /// <returns>absolute value of x</returns>
        public static int Abs(this int x)
        {
            return x > 0 ? x : -x;
        }

        /// <summary>
        /// Returns minimum of two float values
        /// </summary>
        /// <param name="a">First value</param>
        /// <param name="b">Second Value</param>
        /// <returns>A if A<B, B if B<A</returns>
        public static float Min(float a, float b)
        {
            return a < b ? a : b;
        }
        /// <summary>
        /// Returns minimum of two integer values
        /// </summary>
        /// <param name="a">First value</param>
        /// <param name="b">Second Value</param>
        /// <returns>A if A<B, B otherwise</returns>
        public static int Min(int a, int b)
        {
            return a < b ? a : b;
        }

        /// <summary>
        /// Returns maximum of two float values
        /// </summary>
        /// <param name="a">First value</param>
        /// <param name="b">Second Value</param>
        /// <returns>A if A>B, B otherwise</returns>
        public static float Max(float a, float b)
        {
            return a > b ? a : b;
        }
        /// <summary>
        /// Returns maximum of two integer values
        /// </summary>
        /// <param name="a">First value</param>
        /// <param name="b">Second Value</param>
        /// <returns>A if A>B, B otherwise</returns>
        public static int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        /// <summary>
        /// Clamps input value between bounds
        /// </summary>
        /// <param name="x">Input value</param>
        /// <param name="min">Minimum</param>
        /// <param name="max">Maximum</param>
        /// <returns>min if x<min, max if x>max, x otherwise</returns>
        public static float Clamp(this float x, float min, float max)
        {
            if (x < min)
            {
                return min;
            }
            if (x > max)
            {
                return max;
            }
            return x;
        }
        /// <summary>
        /// Clamps input value between bounds
        /// </summary>
        /// <param name="x">Input value</param>
        /// <param name="min">Minimum</param>
        /// <param name="max">Maximum</param>
        /// <returns>min if x<min, max if x>max, x otherwise</returns>
        public static int Clamp(this int x, int min, int max)
        {
            return Min(Max(x, min), max);
        }
        
        /// <summary>
        /// Converts float angle in degrees to radians
        /// </summary>
        /// <param name="x">Angle in degrees</param>
        /// <returns>Angle in radians</returns>
        public static float ToRadians(this float x)
        {
            return (float)(Math.PI * x / 180.0);
        }
        /// <summary>
        /// Converts float angle in radians to degrees
        /// </summary>
        /// <param name="x">Angle in radians</param>
        /// <returns>Angle in degrees</returns>
        public static float ToDegrees(this float x)
        {
            return (float)(x * 180.0 / Math.PI);
        }

        /// <summary>
        /// Checks if a value is within a margin of error from the target value
        /// </summary>
        /// <param name="val">Observed value</param>
        /// <param name="target">Target value</param>
        /// <param name="error">Absolute maximum error</param>
        /// <returns>true if difference is within error bounds, false otherwise</returns>
        public static bool InTolerance(this float val, float target, float error)
        {
            float difference = Abs(val - target);   //find absolute value of difference
            return difference < error;              //if difference is less than error, return true. false otherwise.
        }


        /// <summary>
        /// Clamp a value within bounds by subtracting the range from the value until it is within bounds. Allows wrapping and angles kept in a certain range.
        /// </summary>
        /// <param name="x">Input value</param>
        /// <param name="min">Minimum Value</param>
        /// <param name="max">Maximum Value</param>
        /// <returns>(x % (max-min)) + min</returns>
        public static float ClampMod(this float x, float min, float max)
        {
            if(min == max)
            {
                throw new ArgumentException("Max and min cannot be equal");
            }
            if(x == min || x==max)
            {
                return x;
            }
            float dif = max - min;
            while(x > max)
            {
                x -= dif;
            }
            while (x < min)
            {
                x += dif;
            }
            return x;
        }

        /// <summary>
        /// Angle between two vectors in radians
        /// </summary>
        /// <param name="a">First Vector</param>
        /// <param name="b">Second Vector</param>
        /// <returns>Angle between the two vectors in radians</returns>
        public static float AngleTo(this Vector2 a, Vector2 b)
        {
            return (float)Math.Acos(Vector2.Dot(a,b) / (a.Length() * b.Length()));

        }

        /// <summary>
        /// Get normalized Vector without modifying original vector
        /// </summary>
        /// <param name="a">Input vector</param>
        /// <returns>Normalized input vector</returns>
        public static Vector2 Normalized(this Vector2 a)
        {
            return a / a.Length();
        }
    }
}
