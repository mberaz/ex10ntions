using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RepoProj.BLL
{
    public class Trees
    {
        //demo
        //var allUsers = Getusers();
        //    List<Expression<Func<UserModel, bool>>> andList = new List<Expression<Func<UserModel, bool>>>();

        //    andList.Add((o) => o.Last != "berezin");
        //    andList.Add((o) => o.Age>5);
        //    andList.Add((o) => o.isTemp);
        //    andList.Add((o) => o.Rank==1);
         
        //    var test = allUsers.Where(Trees.CreateCompoundANDQuery<UserModel>(andList).Compile());

        public static int EnumeratePropery<t>(Expression<Func<t, Boolean>> expression) where t : class
        {
            Type type;
            var body = expression.Body as System.Linq.Expressions.BinaryExpression;
            if (body == null)
            {
                type = (expression.Body as System.Linq.Expressions.Expression).Type;
            }
            else
            {
                type = body.Left.Type;
            }


            if (type == typeof(bool))
            {
                return 1;
            }

            if (type == typeof(int) || type == typeof(long) || type == typeof(uint) || type == typeof(ulong))
            {
                return 2;
            }

            if (type == typeof(float) || type == typeof(double))
            {
                return 3;
            }

            if (type == typeof(decimal))
            {
                return 4;
            }

            if (type == typeof(string) || type == typeof(Guid))
            {
                return 5;
            }

            return 6;
        }

        public static Expression<Func<t, Boolean>> CreateCompoundANDQuery<t>(List<Expression<Func<t, Boolean>>> expressionList, bool useAlso = true, bool sortPropertys = false) where t : class
        {
            if (sortPropertys)
            {
                expressionList = expressionList.OrderBy(o => EnumeratePropery<t>(o)).ToList();
            }

            var compoundQuery = expressionList[0];

            foreach (var item in expressionList.Skip(1))
            {
                compoundQuery = compoundQuery.And(item, useAlso);
            }

            return compoundQuery;
        }

        public static Expression<Func<t, Boolean>> CreateCompoundORQuery<t>(List<Expression<Func<t, Boolean>>> expressionList, bool useElse = true, bool sortPropertys = false) where t : class
        {
            if (sortPropertys)
            {
                expressionList = expressionList.OrderBy(o => EnumeratePropery<t>(o)).ToList();
            }

            var compoundQuery = expressionList[0];

            foreach (var item in expressionList.Skip(1))
            {
                compoundQuery = compoundQuery.Or(item, useElse);
            }

            return compoundQuery;
        }

        public static Expression<Func<t, Boolean>> CreateCompoundQuery<t>(List<Expression<Func<t, Boolean>>> expressionANDList, List<Expression<Func<t, Boolean>>> expressionORList
                                                                                                , bool useAlso = true, bool useElse = true, bool sortPropertys = false) where t : class
        {
            if (sortPropertys)
            {
                expressionANDList = expressionANDList.OrderBy(o => EnumeratePropery<t>(o)).ToList();
                expressionORList = expressionORList.OrderBy(o => EnumeratePropery<t>(o)).ToList();
            }

            var compoundQuery = expressionANDList[0];

            foreach (var item in expressionANDList.Skip(1))
            {
                compoundQuery = compoundQuery.And(item, useAlso);
            }

            foreach (var item in expressionORList)
            {
                compoundQuery = compoundQuery.Or(item, useElse);
            }

            return compoundQuery;
        }

        public static IQueryable<t> CreateCompoundOrderASCQuery<t>(IQueryable<t> query, List<Expression<Func<t, dynamic>>> list) where t : class
        {
            var compoud = query.OrderBy(list[0].Compile());

            foreach (var nextExpression in list.Skip(1))
            {
                compoud = compoud.ThenBy(nextExpression.Compile());
            }

            return compoud.AsQueryable();
        }

        public static IQueryable<t> CreateCompoundOrderDescQuery<t>(IQueryable<t> query, List<Expression<Func<t, dynamic>>> list) where t : class
        {
            var compoud = query.OrderByDescending(list[0].Compile());

            foreach (var nextExpression in list.Skip(1))
            {
                compoud = compoud.ThenByDescending(nextExpression.Compile());
            }

            return compoud.AsQueryable();
        }

        public static IQueryable<t> CreateCompoundOrderQuery<t>(IQueryable<t> query, List<Expression<Func<t, dynamic>>> listAsc, List<Expression<Func<t, dynamic>>> listDesc, bool startAsc) where t : class
        {
            var compoud = startAsc ? query.OrderBy(listAsc[0].Compile()) : query.OrderByDescending(listAsc[0].Compile());

            foreach (var nextExpression in listAsc.Skip(1))
            {
                compoud = startAsc ? compoud.ThenBy(nextExpression.Compile()) : compoud.ThenByDescending(nextExpression.Compile());
            }

            foreach (var nextExpression in listDesc)
            {
                compoud = startAsc ? compoud.ThenByDescending(nextExpression.Compile()) : compoud.ThenBy(nextExpression.Compile());
            }

            return compoud.AsQueryable();
        }


    }
    //http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }

    public static class ExpressionUtility
    {
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second, bool useAlso = true)
        {
            return useAlso ? first.Compose(second, Expression.AndAlso) : first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second, bool useElse = true)
        {
            return useElse ? first.Compose(second, Expression.OrElse) : first.Compose(second, Expression.Or);
        }
    }
}
