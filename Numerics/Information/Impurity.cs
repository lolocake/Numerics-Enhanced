#region Copyright

// Copyright (c) 2007-2013, Orbifold bvba.
// 
// For the complete license agreement see http://orbifold.net/EULA or contact us at sales@orbifold.net.

#endregion

using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
    /// <summary>
    /// Abstract base class for calculating impurities when creating decision trees.
    /// </summary>
    /// <seealso cref="http://en.wikipedia.org/wiki/Decision_tree_learning"/>
    /// <seealso cref="DecisionTreeMachineLearningModelCreator"/>
    public abstract class Impurity
    {
        public Range<double>[] Segments { get; set; }

        public bool Discrete { get; set; }

        /// <summary>
        /// Calculates the impurity of the given vector.
        /// </summary>
        public ab