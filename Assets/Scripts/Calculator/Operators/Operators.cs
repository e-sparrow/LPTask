using System;
using System.Collections.Generic;
using System.Linq;

namespace LPTask.Calculator.Operators
{
    public sealed class PlusOperator
        : IOperator
    {
        public char Symbol => '+';
        
        public int Apply(IEnumerable<int> input)
        {
            var result = input.Sum();
            return result;
        }
    }
    
    public sealed class MinusOperator
        : IOperator
    {
        public char Symbol => '-';
        
        public int Apply(IEnumerable<int> input)
        {
            int? total = null;
            foreach (var item in input)
            {
                if (!total.HasValue)
                {
                    total = item;
                }
                else
                {
                    total -= item;
                }
            }

            if (total.HasValue)
            {
                return total.Value;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
    
    public sealed class MultiplyOperator
        : IOperator
    {
        public char Symbol => '*';
        
        public int Apply(IEnumerable<int> input)
        {
            var total = 1;
            foreach (var item in input)
            {
                total *= item;
            }

            return total;
        }
    }
    
    public sealed class DivideOperator
        : IOperator
    {
        public char Symbol => '/';
        
        public int Apply(IEnumerable<int> input)
        {
            int? total = null;
            foreach (var item in input)
            {
                if (!total.HasValue)
                {
                    total = item;
                }
                else
                {
                    total /= item;
                }
            }

            if (total.HasValue)
            {
                return total.Value;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
    
    public sealed class PowerOperator
        : IOperator
    {
        public char Symbol => '^';
        
        public int Apply(IEnumerable<int> input)
        {
            int? total = null;
            foreach (var item in input)
            {
                if (!total.HasValue)
                {
                    total = item;
                }
                else
                {
                    var tempTotal = total.Value;
                    for (var i = 1; i < item; i++)
                    {
                        total *= tempTotal;
                    }
                }
            }

            if (total.HasValue)
            {
                return total.Value;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}