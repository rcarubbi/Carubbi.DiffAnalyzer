using System;

namespace Carubbi.DiffAnalyzer
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class DiffKeyAttribute : Attribute
    {
      
    }
}
