﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Framework.Providers
{
    public class DecimalToIntSearchExpressionProvider : DefaultSearchExpressionProvider
    {
        public override ConstantExpression GetValue(string input)
        {
            if (!decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out var dec))
                throw new ArgumentException("Invalid search value.");

            var places = BitConverter.GetBytes(decimal.GetBits(dec)[3])[2];
            if (places < 2) places = 2;
            var justDigits = (int)(dec * (decimal)Math.Pow(10, places));

            return Expression.Constant(justDigits);
        }

        public override Expression GetComparison(MemberExpression left, string op, ConstantExpression right)
        {
            switch (op.ToLower())
            {
                case "gt": return Expression.GreaterThan(left, right);
                case "gte": return Expression.GreaterThanOrEqual(left, right);
                case "lt": return Expression.LessThan(left, right);
                case "lte": return Expression.LessThanOrEqual(left, right);

                // If nothing matcher, fall back to base implementation
                default: return base.GetComparison(left, op, right);
            }
        }
    }
}
