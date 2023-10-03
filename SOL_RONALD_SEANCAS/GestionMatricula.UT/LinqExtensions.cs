using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GestionMatricula.UT
{
    public static class LinqExtensions
    {
        private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortName, string sortOrder)
        {
            var propInfo = typeof(T).GetProperty(sortName);
            var expr = GetOrderExpression(typeof(T), propInfo);

            string sortOrderFunctionName = "OrderBy";
            if ((sortOrder ?? "").ToLower() == "desc")
            {
                sortOrderFunctionName = "OrderByDescending";
            }

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == sortOrderFunctionName && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { source, expr });
        }
    }
}
