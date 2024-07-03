using System;
using System.Collections.Generic;

namespace LPTask.Calculator.Operators
{
    public static class OperatorsHelper
    {
        // При миграции модуля в другие проекты операторы могут не понадобиться, так что лучше избежать лишних затрат памяти
        private static readonly Lazy<IDictionary<EOperator, IOperator>> Operators 
            = new Lazy<IDictionary<EOperator, IOperator>>(new Dictionary<EOperator, IOperator>()
        {
            { EOperator.Plus, new PlusOperator()},
            { EOperator.Minus, new MinusOperator()},
            { EOperator.Multiply, new MultiplyOperator()},
            { EOperator.Divide, new DivideOperator()},
            { EOperator.Power, new PowerOperator()}
        });

        public static IOperator GetOperator(EOperator @operator)
        {
            var result = Operators.Value[@operator];
            return result;
        }
    }
}