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
			/*36*/37199332678990121746