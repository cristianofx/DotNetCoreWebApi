using DotNetCoreWebApi.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Framework.Providers
{
    public class SearchTerm
    {
        public string Name { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }

        public bool ValidSyntax { get; set; }
        public ISearchExpressionProvider ExpressionProvider { get; set; }
    }
}
