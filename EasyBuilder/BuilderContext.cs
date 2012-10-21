using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EasyBuilder
{
    public class BuilderContext<TInstance> where TInstance : new()
    {
        private Dictionary<Expression, LambdaExpression> _setters;

        public BuilderContext()
        {
            _setters = new Dictionary<Expression, LambdaExpression>();
        }

        public static implicit operator TInstance(BuilderContext<TInstance> builderContext)
        {
            return builderContext.Build();
        }

        public TInstance Build()
        {
            var instance = new TInstance();
            foreach (var setter in _setters)
            {
                var setterLambdaExpression = setter.Key as LambdaExpression;
                var parameterExpression = GetParameterExpression(setterLambdaExpression);
                var valueExpression = setter.Value;
                var assignExpression = Expression.Assign(setterLambdaExpression.Body, valueExpression.Body);
                Expression<Action<TInstance>> assignmentExpression = Expression.Lambda<Action<TInstance>>(assignExpression, parameterExpression);
                assignmentExpression.Compile()(instance);
            }
            return instance;
        }

        private ParameterExpression GetParameterExpression(LambdaExpression lambdaExpression)
        {
            var memberExpression = lambdaExpression.Body as MemberExpression;
            var parameterExpression = memberExpression.Expression as ParameterExpression;
            return parameterExpression;
        }

        public BuilderContext<TInstance> SetProperty<TMember>(Expression<Func<TInstance, TMember>> memberFunc, TMember value)
        {
            return SetProperty(memberFunc, () => value);
        }

        public BuilderContext<TInstance> SetProperty<TMember>(Expression<Func<TInstance, TMember>> memberFunc, Expression<Func<TMember>> valueFunc)
        {
            _setters.Add(memberFunc, valueFunc);
            return this;
        }
    }
}