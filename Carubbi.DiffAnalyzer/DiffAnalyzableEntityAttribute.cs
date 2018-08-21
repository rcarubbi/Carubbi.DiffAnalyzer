using System;

namespace Carubbi.DiffAnalyzer
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class DiffAnalyzableEntityAttribute : Attribute
    {
        public DiffAnalyzableEntityAttribute()
            : this(string.Empty) { }

        public DiffAnalyzableEntityAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}
