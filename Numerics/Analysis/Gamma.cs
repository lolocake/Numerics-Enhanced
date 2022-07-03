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

            if (ax < -Constants.LogMax)
                return 0.0;

            ax = Math.Exp(ax);

            double r = a;
            double c = 1.0;
            double ans = 1.0;

            do
            {
                r += 1.0;
                c *= x / r;
                ans += c;
            } while (c / ans > Constants.DoubleEpsilon);

            return ans * ax / a;
        }
        /// <summary>
        ///   Random Gamma-distribution number generation 
        ///   based on Marsaglia's Simple Method (2000).
        /// </summary>
        /// 
        public static double Random(double d, double c)
        {
            var g = new GaussianDistribution(0,1);

            // References:
            //
            // - Marsaglia, G. A Simple Method for Generating Gamma Variables, 2000
            //

            while (true)
            {
                // 2. Generate v = (1+cx)^3 with x normal
                double x, t, v;

                do
                {
                    x = g.NextDouble();
                    t = (1.0 + c * x);
                    v = t * t * t;
                } while (v <= 0);


                // 3. Generate uniform U
                double U = Numerics.Random.NextDouble();

                // 4. If U < 1-0.0331*x^4 return d*v.
                double x2 = x * x;
                if (U < 1 - 0.0331 * x2 * x2)
                    return d * v;

                // 5. If log(U) < 0.5*x^2 + d*(1-v+log(v)) return d*v.
                if (Math.Log(U) < 0.5 * x2 + d * (1.0 - v + Math.Log(v)))
                    return d * v;

                // 6. Goto step 2
            }
        }
        /// <summary>
        ///   Natural logarithm of the gamma function.
        /// </summary>
        /// 
        public static double Log(double x)
        {
            if (x == 0)
                return Double.PositiveInfinity;

            double p, q, w, z;

            double[] A =
            {
                 8.11614167470508450300E-4,
                -5.95061904284301438324E-4,
                 7.93650340457716943945E-4,
                -2.77777777730099687205E-3,
                 8.33333333333331927722E-2
            };

            double[] B =
            {
                -1.37825152569120859100E3,
                -3.88016315134637840924E4,
                -3.31612992738871184744E5,
                -1.16237097492762307383E6,
                -1.7