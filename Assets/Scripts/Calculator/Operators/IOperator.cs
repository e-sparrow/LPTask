using System.Collections.Generic;

namespace LPTask.Calculator.Operators
{
    public interface IOperator
    {
        char Symbol
        {
            get;
        }
        
        int Apply(IEnumerable<int> input);
    }
}