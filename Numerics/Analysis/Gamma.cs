using System;

namespace Orbifold.Numerics
{
    class Gamma
    {
        /// <summary>
        ///   Gamma function as computed by Stirling's formula.
        /// </summary>
        /// 
        public static double Stirling(double x)
        {
            double[] STIR =
            {
                 7.87311395793093628397E-4,
                -2.29549961613378126380E-4,
                -2.68132617805781232825E-3,
                 3.47222221605458667310E-3,
                 8.33333333333482257126E-2,
            };

            double MAXSTIR = 143.01608;

            double w = 1.0 / x;
            double y = Math.Exp(x);

            w = 1.0 + w * Functions.Polevl(w, STIR, 4);

            if (x > MAXSTIR)
            {
                double v = Math.Pow(x, 0.5 * x - 0.25);

                if (Double.IsPositiveInfinity(v) && Double.IsPositiveInfinity(y))
                {
                    // lim x -> inf { (x^(0.5*x - 0.25)) * (x^(0.5*x - 0.25) / exp(x))  }
                    y = Double.PositiveInfinity;
                }
                else
                {
                    y = v * (v / y);
                }
            }
            else
            {
                y = Math.Pow(x, x - 0.5) / y;
            }

            y = Constants.Sqrt2Pi * y * w;
            return y;
        }
        /// <summary>
        ///   Lower incomplete regularized gamma function P
        ///   (a.k.a. the incomplete Gamma function).
        /// </summary>
        /// 
        /// <remarks>
        ///   This function is equivalent to P(x) = γ(s, x) / Γ(s).
        /// </remarks>
        /// 
        public static double LowerIncomplete(double a, double x)
        {
            if (a <= 0)
                return 1.0;

            if (x <= 0)
                return 0.0;

            if (x > 1.0 && x > a)
                return 1.0 - UpperIncomplete(a, x);

            double ax = a * Math.Log(x) - x - Log(a);

            if (ax < -Constants.Lo