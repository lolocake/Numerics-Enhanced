
﻿using System;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Numerics;
namespace Orbifold.Numerics
{
    /// <summary>
    /// Extension methods 
    /// </summary>
    public static class ComplexExtensions
    {
        /// <summary>
        /// The regex pattern recognizing 'a+bi' and all its variations.