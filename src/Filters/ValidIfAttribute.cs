
using System;
using System.ComponentModel.DataAnnotations;

namespace Gestaoaju.Filters
{
    public class ValidIfAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is bool)
            {
                return (bool)value;
            }

            throw new ArgumentException("The property must return a Boolean.");
        }
    }
}
