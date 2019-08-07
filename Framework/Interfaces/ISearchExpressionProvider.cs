using System.Linq.Expressions;

namespace DotNetCoreWebApi.Framework.Interfaces
{
    public interface ISearchExpressionProvider
    {
        ConstantExpression GetValue(string input);
        Expression GetComparison(MemberExpression left, string op, ConstantExpression right);
    }
}
