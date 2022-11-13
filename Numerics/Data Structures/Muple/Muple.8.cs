namespace Orbifold.Numerics
{
	/// <summary>
	/// A mutable tuple of dimension eight.
	/// </summary>
	/// <typeparam name="T1">The data type of the first item.</typeparam>
	/// <typeparam name="T2">The data type of the second item.</typeparam>
	/// <typeparam name="T3">The data type of the third item.</typeparam>
	/// <typeparam name="T4">The data type of the fourth item.</typeparam>
	/// <typeparam name="T5">The data type of the fifth item.</typeparam>
	/// <typeparam name="T6">The data type of the sixth item.</typeparam>
	/// <typeparam name="T7">The data type of the seventh item.</typeparam>
	/// <typeparam name="T8">The data type of the eighth item.</typeparam>
	public class Muple<T1, T2, T3, T4, T5, T6, T7, T8> : Muple<T1, T2, T3, T4, T5, T6, T7>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Muple&lt;T1, T2, T3, T4, T5, T6, T7, T8&gt;"/> class.
		/// </summary>
		/// <param name="item1">The ite