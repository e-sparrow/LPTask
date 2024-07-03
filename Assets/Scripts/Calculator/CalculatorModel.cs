using System.Linq;
using LPTask.Calculator.Operators;

namespace LPTask.Calculator
{
    public sealed class CalculatorModel
    {
        public CalculatorModel(IOperator @operator)
        {
            _operator = @operator;
        }

        private readonly IOperator _operator;
        
        public bool Calculate(string equation, out int result)
        {
            result = default;

            // Специфика InputField'ов в Unity - добавлять этот знак в конец строки
            equation = equation.Trim((char) 8203); 
            
            if (!ValidateEquation(equation))
            {
                return false;
            }

            result = _operator.Apply(equation
                .Split(_operator.Symbol)
                .Select(int.Parse));

            return true;
        }

        private bool ValidateEquation(string equation)
        {
            var hasNoPlus = !equation.Contains(_operator.Symbol);
            if (hasNoPlus)
            {
                return false;
            }

            var isValid = equation.All(value => char.IsDigit(value) || value == _operator.Symbol);
            if (!isValid)
            {
                return false;
            }

            var isPlusIsolated = equation.First() != _operator.Symbol && equation.Last() != _operator.Symbol;
            if (!isPlusIsolated)
            {
                return false;
            }

            // Проверка нет ли в строке повторяющихся друг за другом знаков '+'
            for (var i = 1; i < equation.Length; i++)
            {
                if (equation[i] == _operator.Symbol && equation[i - 1] == _operator.Symbol)
                {
                    return false;
                }
            }

            return true;
        }
    }
}