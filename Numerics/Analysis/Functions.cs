using System;
using System.Globalization;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Special mathematical functions and polynomials.
	/// </summary>
	/// <seealso cref="EngineeringFunctions"/>
	/// <seealso cref="FinancialFunctions"/>
	/// <seealso cref="Trigonometry"/>
	public static class Functions
	{
		/// <summary>
		/// The factorial ln cache size.
		/// </summary>
		private const int FactorialLnCacheSize = 2 * FactorialPrecompSize;

		/// <summary>
		/// The factorial precomp size.
		/// </summary>
		private const int FactorialPrecompSize = 100;

		/// <summary>
		/// The factorial precomp.
		/// </summary>
		private static readonly double[] FactorialPrecomp = new[] {
			/*0*/1d,
			/*1*/1d,
			/*2*/2d,
			/*3*/6d,
			/*4*/24d,
			/*5*/120d,
			/*6*/720d,
			/*7*/5040d,
			/*8*/40320d,
			/*9*/362880d,
			/*10*/3628800d,
			/*11*/39916800d,
			/*12*/479001600d,
			/*13*/6227020800d,
			/*14*/87178291200d,
			/*15*/1307674368000d,
			/*16*/20922789888000d,
			/*17*/355687428096000d,
			/*18*/6402373705728000d,
			/*19*/121645100408832000d,
			/*20*/2432902008176640000d,
			/*21*/51090942171709440000d,
			/*22*/1124000727777607680000d,
			/*23*/25852016738884976640000d,
			/*24*/620448401733239439360000d,
			/*25*/15511210043330985984000000d,
			/*26*/403291461126605635584000000d,
			/*27*/10888869450418352160768000000d,
			/*28*/304888344611713860501504000000d,
			/*29*/8841761993739701954543616000000d,
			/*30*/265252859812191058636308480000000d,
			/*31*/8222838654177922817725562880000000d,
			/*32*/263130836933693530167218012160000000d,
			/*33*/8683317618811886495518194401280000000d,
			/*34*/295232799039604140847618609643520000000d,
			/*35*/10333147966386144929666651337523200000000d,
			/*36*/371993326789901217467999448150835200000000d,
			/*37*/13763753091226345046315979581580902400000000d,
			/*38*/523022617466601111760007224100074291200000000d,
			/*39*/20397882081197443358640281739902897356800000000d,
			/*40*/815915283247897734345611269596115894272000000000d,
			/*41*/33452526613163807108170062053440751665152000000000d,
			/*42*/1405006117752879898543142606244511569936384000000000d,
			/*43*/60415263063373835637355132068513997507264512000000000d,
			/*44*/2658271574788448768043625811014615890319638528000000000d,
			/*45*/119622220865480194561963161495657715064383733760000000000d,
			/*46*/5502622159812088949850305428800254892961651752960000000000d,
			/*47*/258623241511168180642964355153611979969197632389120000000000d,
			/*48*/12413915592536072670862289047373375038521486354677760000000000d,
			/*49*/608281864034267560872252163321295376887552831379210240000000000d,
			/*50*/30414093201713378043612608166064768844377641568960512000000000000d,
			/*51*/1551118753287382280224243016469303211063259720016986112000000000000d,
			/*52*/80658175170943878571660636856403766975289505440883277824000000000000d,
			/*53*/4274883284060025564298013753389399649690343788366813724672000000000000d,
			/*54*/230843697339241380472092742683027581083278564571807941132288000000000000d,
			/*55*/12696403353658275925965100847566516959580321051449436762275840000000000000d,
			/*56*/710998587804863451854045647463724949736497978881168458687447040000000000000d,
			/*57*/40526919504877216755680601905432322134980384796226602145184481280000000000000d,
			/*58*/2350561331282878571829474910515074683828862318181142924420699914240000000000000d,
			/*59*/138683118545689835737939019720389406345902876772687432540821294940160000000000000d,
			/*60*/8320987112741390144276341183223364380754172606361245952449277696409600000000000000d,
			/*61*/507580213877224798800856812176625227226004528988036003099405939480985600000000000000d,
			/*62*/31469973260387937525653122354950764088012280797258232192163168247821107200000000000000d,
			/*63*/1982608315404440064116146708361898137544773690227268628106279599612729753600000000000000d,
			/*64*/126886932185884164103433389335161480802865516174545192198801894375214704230400000000000000d,
			/*65*/8247650592082470666723170306785496252186258551345437492922123134388955774976000000000000000d,
			/*66*/544344939077443064003729240247842752644293064388798874532860126869671081148416000000000000000d,
			/*67*/36471110918188685288249859096605464427167635314049524593701628500267962436943872000000000000000d,
			/*68*/2480035542436830599600990418569171581047399201355367672371710738018221445712183296000000000000000d,
			/*69*/171122452428141311372468338881272839092270544893520369393648040923257279754140647424000000000000000d,
			/*70*/11978571669969891796072783721689098736458938142546425857555362864628009582789845319680000000000000000d,
			/*71*/850478588567862317521167644239926010288584608120796235886430763388588680378079017697280000000000000000d,
			/*72*/61234458376886086861524070385274672740778091784697328983823014963978384987221689274204160000000000000000d,
			/*73*/4470115461512684340891257138125051110076800700282905015819080092370422104067183317016903680000000000000000d,
			/*74*/330788544151938641225953028221253782145683251820934971170611926835411235700971565459250872320000000000000000d,
			/*75*/24809140811395398091946477116594033660926243886570122837795894512655842677572867409443815424000000000000000000d,
			/*76*/1885494701666050254987932260861146558230394535379329335672487982961844043495537923117729972224000000000000000000d,
			/*77*/145183092028285869634070784086308284983740379224208358846781574688061991349156420080065207861248000000000000000000d,
			/*78*/11324281178206297831457521158732046228731749579488251990048962825668835325234200766245086213177344000000000000000000d,
			/*79*/894618213078297528685144171539831652069808216779571907213868063227837990693501860533361810841010176000000000000000000d,
			/*80*/71569457046263802294811533723186532165584657342365752577109445058227039255480148842668944867280814080000000000000000000d,
			/*81*/5797126020747367985879734231578109105412357244731625958745865049716390179693892056256184534249745940480000000000000000000d,
			/*82*/475364333701284174842138206989404946643813294067993328617160934076743994734899148613007131808479167119360000000000000000000d,
			/*83*/39455239697206586511897471180120610571436503407643446275224357528369751562996629334879591940103770870906880000000000000000000d,
			/*84*/3314240134565353266999387579130131288000666286242049487118846032383059131291716864129885722968716753156177920000000000000000000d,
			/*85*/281710411438055027694947944226061159480056634330574206405101912752560026159795933451040286452340924018275123200000000000000000000d,
			/*86*/24227095383672732381765523203441259715284870552429381750838764496720162249742450276789464634901319465571660595200000000000000000000d,
			/*87*/2107757298379527717213600518699389595229783738061356212322972511214654115727593174080683423236414793504734471782400000000000000000000d,
			/*88*/185482642257398439114796845645546284380220968949399346684421580986889562184028199319100141244804501828416633516851200000000000000000000d,
			/*89*/16507955160908461081216919262453619309839666236496541854913520707833171034378509739399912570787600662729080382999756800000000000000000000d,
			/*90*/1485715964481761497309522733620825737885569961284688766942216863704985393094065876545992131370884059645617234469978112000000000000000000000d,
			/*91*/135200152767840296255166568759495142147586866476906677791741734597153670771559994765685283954750449427751168336768008192000000000000000000000d,
			/*92*/12438414054641307255475324325873553077577991715875414356840239582938137710983519518443046123837041347353107486982656753664000000000000000000000d,
			/*93*/1156772507081641574759205162306240436214753229576413535186142281213246807121467315215203289516844845303838996289387078090752000000000000000000000d,
			/*94*/108736615665674308027365285256786601004186803580182872307497374434045199869417927630229109214583415458560865651202385340530688000000000000000000000d,
			/*95*/10329978488239059262599702099394727095397746340117372869212250571234293987594703124871765375385424468563282236864226607350415360000000000000000000000d,
			/*96*/991677934870949689209571401541893801158183648651267795444376054838492222809091499987689476037000748982075094738965754305639874560000000000000000000000d,
			/*97*/96192759682482119853328425949563698712343813919172976158104477319333745612481875498805879175589072651261284189679678167647067832320000000000000000000000d,
			/*98*/9426890448883247745626185743057242473809693764078951663494238777294707070023223798882976159207729119823605850588608460429412647567360000000000000000000000d,
			/*99*/933262154439441526816992388562667004907159682643816214685929638952175999932299156089414639761565182862536979208272237582511852109168640000000000000000000000d,
			/*100*/93326215443944152681699238856266700490715968264381621468592963895217599993229915608941463976156518286253697920827223758251185210916864000000000000000000000000d,
		};
		/// <summary>
		/// The <see cref="FactorialLn"/> cache.
		/// </summary>
		private static double[] factorialLnCache;

		/// <summary>
		/// Returns the binomial coefficient of two integers as a double precision number.
		/// </summary>
		/// <param name="n">
		/// A number.
		/// </param>
		/// <param name="k">
		/// A number.
		/// </param>
		/// <remarks>http://en.wikipedia.org/wiki/Binomial_coefficient</remarks>
		/// <seealso cref="BinomialCoefficientLn"/>
		/// <seealso cref="BinomialDistribution"/>
		public static double BinomialCoefficient(int n, int k)
		{
			return k < 0 || n < 0 || k > n ? 0d : Math.Floor(0.5 + Math.Exp(FactorialLn(n) - FactorialLn(k) - FactorialLn(n - k)));
		}

		/// <summary>
		/// Returns the regularized lower incomplete beta function
		/// The regularized incomplete beta function (or regularized beta function for short) is defined in terms of the incomplete beta function and the complete beta function. 
		/// </summary>
		/// <remarks>http://en.wikipedia.org/wiki/Regularized_Beta_function</remarks>
		public static double BetaRegularized(double a, double b, double x)
		{
			const int MaxIterations = 100;
			if(a < 0.0)
				throw new ArgumentOutOfRangeException("a");
			if(b < 0.0)
				throw new ArgumentOutOfRangeException("b");
			if(x < 0.0 || x > 1.0)
				throw new ArgumentOutOfRangeException("x", "0d");
			var bt = (Math.Abs(x) < Constants.Epsilon || Math.Abs(x - 1d) < Constants.Epsilon) ? 0.0 : Math.Exp(GammaLn(a + b) - GammaLn(a) - GammaLn(b) + a * Math.Log(x) + b * Math.Log(1.0 - x));
			var symmetryTransformation = x >= (a + 1.0) / (a + b + 2.0);
			var eps = Constants.RelativeAccuracy;
			var fpmin = Constants.SmallestNumberGreaterThanZero / eps;
			if(symmetryTransformation) {
				x = 1.0 - x;
				var swap = a;
				a = b;
				b = swap;
			}

			var qab = a + b;
			var qap = a + 1.0;
			var qam = a - 1.0;
			var c = 1.0;
			var d = 1.0 - qab * x / qap;
			if(Math.Abs(d) < fpmin)
				d = fpmin;
			d = 1.0 / d;
			var h = d;

			for(int m = 1, m2 = 2; m <= MaxIterations; m++, m2 += 2) {
				var aa = m * (b - m) * x / ((qam + m2) * (a + m2));
				d = 1.0 + aa * d;

				if(Math.Abs(d) < fpmin)
					d = fpmin;

				c = 1.0 + aa / c;
				if(Math.Abs(c) < fpmin)
					c = fpmin;

				d = 1.0 / d;
				h *= d * c;
				aa = -(a + m) * (qab + m) * x / ((a + m2) * (qap + m2));
				d = 1.0 + aa * d;

				if(Math.Abs(d) < fpmin)
					d = fpmin;

				c = 1.0 + aa / c;

				if(Math.Abs(c) < fpmin)
					c = fpmin;

				d = 1.0 / d;
				var del = d * c;
				h *= del;

				if(Math.Abs(del - 1.0) <= eps)
					return symmetryTransformation ? 1.0 - bt * h / a : bt * h / a;
			}
			throw new ArgumentException("a,b");
		}

		/// <summary>
		/// Returns the natural logarithm of the binomial coefficient of n and k.
		/// </summary>
		/// <param name="n">
		/// A number
		/// </param>
		/// <param name="k">
		/// A number
		/// </param>
		public static double BinomialCoefficientLn(int n, int k)
		{
			if(k < 0 || n < 0 || k > n)
				return 1.0;
			return FactorialLn(n) - FactorialLn(k) - FactorialLn(n - k);
		}

		/// <summary>
		/// Evaluates the minimum distance to the next distinguishable number near the argument value.
		/// </summary>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <returns>
		/// Relative Epsilon (positive double or NaN).
		/// </returns>
		/// <remarks>
		/// Evaluates the <b>Negative</b> epsilon. The more common positive epsilon is equal to two times this negative epsilon.
		/// </remarks>
		public static double EpsilonOf(double value)
		{
			if(double.IsInfinity(value) || double.IsNaN(value))
				return double.NaN;

			var signed64 = BitConverter.DoubleToInt64Bits(value);
			if(signed64 == 0) {
				signed64++;
				return BitConverter.Int64BitsToDouble(signed64) - value;
			}
			if(signed64-- < 0)
				return BitConverter.Int64BitsToDouble(signed64) - value;
			return value - BitConverter.Int64BitsToDouble(signed64);
		}

		/// <summary>
		/// Returns the factorial (n!) of an integer number &gt; 0. Consider using <see cref="FactorialLn"/> instead.
		/// </summary>
		/// <param name="n">
		/// The argument.
		/// </param>
		/// <returns>
		/// A value value! for value &gt; 0.
		/// </returns>
		/// <remarks>
		/// http://en.wikipedia.org/wiki/Factorial
		/// </remarks>
		/// <seealso cref="FactorialLn"/>
		public static double Factorial(int n)
		{
			if(n < 0)
				throw new ArgumentOutOfRangeException("n");
			if(n == 0)
				return 1d;
			if(n == 1)
				return 1d;
			return n >= FactorialPrecompSize ? Math.Exp(GammaLn(n + 1.0)) : FactorialPrecomp[n];
		}

		/// <summary>
		/// Returns the natural logarithm of the factorial (n!) for an integer value &gt; 0.
		/// </summary>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <returns>
		/// A value ln(value!) for value &gt; 0.
		/// </returns>
		public static double FactorialLn(int value)
		{
			if(value < 0)
				throw new ArgumentOutOfRangeException("value");
			if(value <= 1)
				return 0d;
			if(value >= FactorialLnCacheSize)
				return GammaLn(value + 1d);
			if(factorialLnCache == null)
				factorialLnCache = new double[FactorialLnCacheSize];
			return Math.Abs(factorialLnCache[value] - 0.0) > Constants.Epsilon ? factorialLnCache[value] : (factorialLnCache[value] = GammaLn(value + 1.0));
		}

		/// <summary>
		/// Returns the natural logarithm of Gamma for a real value &gt; 0.
		/// </summary>
		/// <param name="x">
		/// The value.
		/// </param>
		/// <returns>
		/// A value ln|Gamma(value))| for value &gt; 0.
		/// </returns>
		public static double GammaLn(double x)
		{
			// if (x <= 0) throw new Exception("Input value must be > 0");

			var coef = new[] {57.1562356658629235,
				-59.5979603554754912,
				14.1360979747417471,
				-0.491913816097620199,
				0.339946499848118887E-4,
				0.465236289270485756E-4,
				-0.983744753048795646E-4,
				0.158088703224912494E-3,
				-0.210264441724104883E-3,
				0.217439618115212643E-3,
				-0.164318106536763890E-3,
				0.844182239838527433E-4,
				-0.261908384015814087E-4,
				0.368991826595316234E-5
			};

			var denominator = x;
			var series = 0.999999999999997092;
			var temp = x + 5.24218750000000000;
			temp = (x + 0.5) * Math.Log(temp) - temp;
			for(var j = 0; j < 14; j++)
				series += coef[j] / ++denominator;
			return (temp + Math.Log(2.5066282746310005 * series / x));
		}
        /// <summary>
        ///   Evaluates polynomial of degree N
        /// </summary>
        /// 
        public static double Polevl(double x, double[] coef, int n)
        {
            double ans;

            ans = coef[0];

            for (int i = 1; i <= n; i++)
                ans = ans * x + coef[i];

            return ans;
        }

        /// <summary>
        ///   Evaluates polynomial of degree N with assumption that coef[N] = 1.0
        /// </summary>
        /// 
        public static double P1evl(double x, double[] coef, int n)
        {
            double ans;

            ans = x + coef[0];

            for (int i = 1; i < n; i++)
                ans = ans * x + coef[i];

            return ans;
        }

		/// <summary>
		/// Returns the gamma function.
		/// </summary>
		/// <remarks>http://en.wikipedia.org/wiki/Gamma_function</remarks>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double Gamma(double x)
		{
			if(Math.Abs(x - 1) < Constants.Epsilon)
				return 1d;
			if(x < 0)
				throw new Exception("The Gamma function implementation does not negative arguments.");
			int n;
			if(int.TryParse(x.ToString(CultureInfo.InvariantCulture), out n) && n > 0) {
				return Factorial(n - 1);
			}
			return Math.Exp(GammaLn(x));
		}

		/// <summary>
		/// Returns the regularized lower incomplete gamma function
		/// P(a,x) = 1/Gamma(a) * int(exp(-t)t^(a-1),t=0..x) for real a &gt; 0, x &gt; 0.
		/// </summary>
		/// <param name="a">
		/// The a.
		/// </param>
		/// <param name="x">
		/// The x.
		/// </param>
		/// <remarks>Note that some packages like Mathematica define the regularized gamma function differently.</remarks>
		public static double GammaRegularized(double a, double x)
		{
			const int MaxIterations = 100;
			var eps = Constants.RelativeAccuracy;
			var fpmin = Constants.SmallestNumberGreaterThanZero / eps;

			if(a < 0.0 || x < 0.0)
				throw new ArgumentOutOfRangeException("a");

			var gln = GammaLn(a);
			if(x < a + 1.0) {
				if(x <= 0.0)
					return 0.0;
				var ap = a;
				double del, sum = del = 1.0 / a;

				for(var n = 0; n < MaxIterations; n++) {
					++ap;
					del *= x / ap;
					sum += del;
					if(Math.Abs(del) < Math.Abs(sum) * eps)
						return sum * Math.Exp(-x + a * Math.Log(x) - gln);
				}
			} else {
				// Continued fraction representation
				var b = x + 1.0 - a;
				var c = 1.0 / fpmin;
				var d = 1.0 / b;
				var h = d;

				for(var i = 1; i <= MaxIterations; i++) {
					var an = -i * (i - a);
					b += 2.0;
					d = an * d + b;
					if(Math.Abs(d) < fpmin)
						d = fpmin;

					c = b + an / c;
					if(Math.Abs(c) < fpmin)
						c = fpmin;
					d = 1.0 / d;
					var del = d * c;
					h *= del;

					if(Math.Abs(del - 1.0) <= eps)
						return 1.0 - Math.Exp(-x + a * Math.Log(x) - gln) * h;
				}
			}

			throw new ArgumentException("a");
		}

		/// <summary>
		/// Returns the inverse P^(-1) of the regularized lower incomplete gamma function
		/// P(a,x) = 1/Gamma(a) * int(exp(-t)t^(a-1),t=0..x) for real a &gt; 0, x &gt; 0,
		/// such that P^(-1)(a,P(a,x)) == x.
		/// </summary>
		public static double InverseGammaRegularized(double a, double y0)
		{
			const double Epsilon = 0.000000000000001;
			const double BigNumber = 4503599627370496.0;
			const double Threshold = 5 * Epsilon;

			// TODO: Consider to throw an out-of-range exception instead of NaN
			if(a < 0 || a.IsZero() || y0 < 0 || y0 > 1) {
				return Double.NaN;
			}

			if(y0.IsZero()) {
				return 0d;
			}

			if(y0.IsEqualTo(1)) {
				return Double.PositiveInfinity;
			}

			y0 = 1 - y0;

			var xUpper = BigNumber;
			double xLower = 0;
			double yUpper = 1;
			double yLower = 0;

			// Initial Guess
			double d = 1 / (9 * a);
			double y = 1 - d - (0.98 * Constants.Sqrt2 * ErfInverse((2.0 * y0) - 1.0) * Math.Sqrt(d));
			double x = a * y * y * y;
			double lgm = GammaLn(a);

			for(var i = 0; i < 10; i++) {
				if(x < xLower || x > xUpper) {
					d = 0.0625;
					break;
				}

				y = 1 - GammaRegularized(a, x);
				if(y < yLower || y > yUpper) {
					d = 0.0625;
					break;
				}

				if(y < y0) {
					xUpper = x;
					yLower = y;
				} else {
					xLower = x;
					yUpper = y;
				}

				d = ((a - 1) * Math.Log(x)) - x - lgm;
				if(d < -709.78271289338399) {
					d = 0.0625;
					break;
				}

				d = -Math.Exp(d);
				d = (y - y0) / d;
				if(Math.Abs(d / x) < Epsilon) {
					return x;
				}

				if((d > (x / 4)) && (y0 < 0.05)) {
					// Naive heuristics for cases near the singularity
					d = x / 10;
				}

				x -= d;
			}

			if(xUpper == BigNumber) {
				if(x <= 0) {
					x = 1;
				}

				while(xUpper == BigNumber) {
					x = (1 + d) * x;
					y = 1 - GammaRegularized(a, x);
					if(y < y0) {
						xUpper = x;
						yLower = y;
						break;
					}

					d = d + d;
				}
			}

			int dir = 0;
			d = 0.5;
			for(var i = 0; i < 400; i++) {
				x = xLower + (d * (xUpper - xLower));
				y = 1 - GammaRegularized(a, x);
				lgm = (xUpper - xLower) / (xLower + xUpper);
				if(Math.Abs(lgm) < Threshold) {
					return x;
				}

				lgm = (y - y0) / y0;
				if(Math.Abs(lgm) < Threshold) {
					return x;
				}

				if(x <= 0d) {
					return 0d;
				}

				if(y >= y0) {
					xLower = x;
					yUpper = y;
					if(dir < 0) {
						dir = 0;
						d = 0.5;
					} else {
						if(dir > 1) {
							d = (0.5 * d) + 0.5;
						} else {
							d = (y0 - yLower) / (yUpper - yLower);
						}
					}

					dir = dir + 1;
				} else {
					xUpper = x;
					yLower = y;
					if(dir > 0) {
						dir = 0;
						d = 0.5;
					} else {
						if(dir < -1) {
							d = 0.5 * d;
						} else {
							d = (y0 - yLower) / (yUpper - yLower);
						}
					}

					dir = dir - 1;
				}
			}

			return x;
		}

		/// <summary>
		/// Returns the square of the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static double Squared(double value)
		{
			return value * value;
		}

		/// <summary>
		/// Returns the square of the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static double Sqr(double value)
		{
			return value * value;
		}

		/// <summary>
		/// The sine integral function.
		/// </summary>
		/// <param name="x">A number.</param>
		public static double Si(double x)
		{
			var sum = 0.0;
			double t;

			var n = 0;
			do {
				t = Math.Pow(-1, n) * Math.Pow(x, 2 * n + 1) / (2 * n + 1) / Gamma(2 * n + 2);
				sum += t;
				n++;
			} while (Math.Abs(t) > Constants.Epsilon);
			return sum;
		}

		/// <summary>
		/// The cosine integral function.
		/// </summary>
		/// <param name="x">A number.</param>
		public static double Ci(double x)
		{
			var sum = 0.0;
			double t;
			var n = 1;
			do {
				t = Math.Pow(-1, n) * Math.Pow(x, 2 * n) / (2 * n) / Gamma(2 * n + 1);
				sum += t;
				n++;
			} while (Math.Abs(t) > Constants.Epsilon);
			return 0.57721566490153286060 + Math.Log(x) + sum;
		}

		/// <summary>
		/// Returns the error function.
		/// </summary>
		public static double Erf(double x)
		{
			return x >= 0 ? 1.0 - ErfcCheb(x) : ErfcCheb(-x) - 1.0;
		}

		/// <summary>
		/// Returns the complementary or inverse error function.
		/// </summary>
		public static double ErfC(double x)
		{
			return x >= 0 ? ErfcCheb(x) : 2.0 - ErfcCheb(-x);
		}

		/// <summary>
		/// Returns the inverse error function erf^-1(x).
		/// </summary>
		/// <remarks>
		/// <para>
		/// The algorithm uses a minimax approximation by rational functions
		/// and the result has a relative error whose absolute value is less
		/// than 1.15e-9.
		/// </para>
		/// <para>
		/// See the page <see href="http://home.online.no/~pjacklam/notes/invnorm/"/>
		/// for more details.
		/// </para>
		/// </remarks>
		public static double ErfInverse(double x)
		{
			//if(x < -1.0 || x > 1.0)
			//{
			//    throw new ArgumentOutOfRangeException("x", x, Properties.LocalStrings.ArgumentInIntervalXYInclusive(-1, 1));
			//}

			x = 0.5 * (x + 1.0);

			// Define break-points.
			const double Plow = 0.02425;
			const double Phigh = 1 - Plow;

			double q;

			// Rational approximation for lower region:
			if(x < Plow) {
				q = Math.Sqrt(-2 * Math.Log(x));
				return (((((ErfInvC[0] * q + ErfInvC[1]) * q + ErfInvC[2]) * q + ErfInvC[3]) * q + ErfInvC[4]) * q + ErfInvC[5]) /((((ErfInvD[0] * q + ErfInvD[1]) * q + ErfInvD[2]) * q + ErfInvD[3]) * q + 1) * Constants.Sqrt1Over2;
			}

			// Rational approximation for upper region:
			if(Phigh < x) {
				q = Math.Sqrt(-2 * Math.Log(1 - x));
				return -(((((ErfInvC[0] * q + ErfInvC[1]) * q + ErfInvC[2]) * q + ErfInvC[3]) * q + ErfInvC[4]) * q + ErfInvC[5]) / ((((ErfInvD[0] * q + ErfInvD[1]) * q + ErfInvD[2]) * q + ErfInvD[3]) * q + 1) * Constants.Sqrt1Over2;
			}

			// Rational approximation for central region:
			q = x - 0.5;
			var r = q * q;
			return (((((ErfInvA[0] * r + ErfInvA[1]) * r + ErfInvA[2]) * r + ErfInvA[3]) * r + ErfInvA[4]) * r + ErfInvA[5]) * q / (((((ErfInvB[0] * r + ErfInvB[1]) * r + ErfInvB[2]) * r + ErfInvB[3]) * r + ErfInvB[4]) * r + 1) * Constants.Sqrt1Over2;
		}

		private static readonly double[] ErfInvA = {
			-3.969683028665376e+01, 2.209460984245205e+02,
			-2.759285104469687e+02, 1.383577518672690e+02,
			-3.066479806614716e+01, 2.506628277459239e+00
		};

		private static readonly double[] ErfInvB = {
			-5.447609879822406e+01, 1.615858368580409e+02,
			-1.556989798598866e+02, 6.680131188771972e+01,
			-1.328068155288572e+01
		};

		private static readonly double[] ErfInvC = {
			-7.784894002430293e-03, -3.223964580411365e-01,
			-2.400758277161838e+00, -2.549732539343734e+00,
			4.374664141464968e+00, 2.938163982698783e+00
		};

		private static readonly double[] ErfInvD = {
			7.784695709041462e-03, 3.224671290700398e-01,
			2.445134137142996e+00, 3.754408661907416e+00
		};

		private static double ErfcCheb(double x)
		{
			int j;
			var d = 0.0;
			var dd = 0.0;

			if(x < 0.0)
				throw new Exception("ErfcCheb requires nonnegative argument");

		