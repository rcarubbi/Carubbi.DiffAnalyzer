namespace Carubbi.DiffAnalyzer
{
    internal class ComparisonNode
    {
        internal string Breadcrumb { get;  }

        internal string Value { get;  }

        internal int Hash { get; set; }

        internal ComparisonNode(string breadcrumb, string value, int hash)
        {
            Breadcrumb = breadcrumb;
            Value = value;
            Hash = hash;
        }
    }
}
