namespace Carubbi.DiffAnalyzer
{
    public class DiffComparison
    {
        public string BreadCrumb { get; }

        public DiffState State { get; }

        public string PreviousValue { get; }

        public string CurrentValue { get; }

    
        internal DiffComparison(string breadCrumb, string previousValue, string currentValue)
        {
            PreviousValue = previousValue;
            CurrentValue = currentValue;
            BreadCrumb = breadCrumb;
            State = ParseState();
        }

        private DiffState ParseState()
        {
            if (PreviousValue == CurrentValue)
            {
                return DiffState.NotChanged;
            }

            if (PreviousValue == null && CurrentValue != null)
            {
                return DiffState.Added;
            }

            if (PreviousValue != null && CurrentValue == null)
            {
                return DiffState.Deleted;
            }

            return PreviousValue != CurrentValue ? DiffState.Modified : DiffState.Unknow;
        }
    }
}
