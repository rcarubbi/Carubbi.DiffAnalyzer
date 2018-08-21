using System;

namespace Carubbi.DiffAnalyzer
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class DiffAnalyzableListItemKeyAttribute : Attribute
    {
    }
}
