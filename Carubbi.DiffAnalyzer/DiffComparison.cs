
namespace Carubbi.DiffAnalyzer
{
    public class DiffComparison
    {
        public string PropertyName { get; set; }

        private object _oldValue;
        public object OldValue
        {
            get => _oldValue ?? string.Empty;
            set => _oldValue = value;
        }

        private object _newValue;
        public object NewValue
        {
            get => _newValue ?? string.Empty;
            set => _newValue = value;
        }

        public virtual DiffState State
        {
            get
            {
                if (OldValue == null && NewValue == null)
                {
                    return DiffState.Unknow;
                }

                if (OldValue != null && OldValue.GetHashCode() == NewValue.GetHashCode())
                {
                    return DiffState.NotChanged;
                }

                if ((string) OldValue == string.Empty && (string) NewValue != string.Empty)
                {
                    return DiffState.Added;
                }

                if ((string) NewValue == string.Empty && (string) OldValue != string.Empty)
                {
                    return DiffState.Deleted;
                }

                if (OldValue != null && NewValue.GetHashCode() != OldValue.GetHashCode())
                {
                    return DiffState.Modified;
                }

                return DiffState.Unknow;
            }
        }

        public int Depth { get; set; }

        public bool LastProperty { get; set; }
    }
}
