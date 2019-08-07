using DotNetCoreWebApi.Framework.Interfaces;
using DotNetCoreWebApi.Framework.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SearchableAttribute : Attribute
    {
        public ISearchExpressionProvider ExpressionProvider { get; set; } = new DefaultSearchExpressionProvider();
    }
}
