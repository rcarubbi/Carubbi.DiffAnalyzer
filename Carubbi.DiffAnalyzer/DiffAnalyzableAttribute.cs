using System;

namespace Carubbi.DiffAnalyzer
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class DiffAnalyzableAttribute : Attribute
    {
        public DiffAnalyzableAttribute()
            : this(DiffAnalyzableUsage.Mark, string.Empty) { }

        public DiffAnalyzableAttribute(string description)
            : this(DiffAnalyzableUsage.Mark, description) { }

        public DiffAnalyzableAttribute(DiffAnalyzableUsage usage)
            : this(usage, string.Empty) { }

        public DiffAnalyzableAttribute(DiffAnalyzableUsage usage, string description)
        {
            Usage = usage;
            Description = description;
        }

        public DiffAnalyzableUsage Usage { get; set; }

        public string Description { get; set; }
    }

    public enum DiffAnalyzableUsage
    {
        Mark,
        Ignore
    }
}
