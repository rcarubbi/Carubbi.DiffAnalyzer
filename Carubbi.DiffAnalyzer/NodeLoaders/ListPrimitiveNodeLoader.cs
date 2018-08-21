using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Carubbi.DiffAnalyzer.NodeLoaders
{
    public class ListPrimitiveNodeLoader : NodeLoaderBase
    {
        public ListPrimitiveNodeLoader(Action<Type, object, object, List<DiffComparison>> loadTypeNodeHandler) :
            base(loadTypeNodeHandler) { }

        private const string COUNT_PROPERTY = "Count";

        public override void LoadNode(string propertyName, object oldInstance, object newInstance, List<DiffComparison> comparisons, int depth)
        {
            var countOldList = Convert.ToInt32(oldInstance.GetType().InvokeMember(COUNT_PROPERTY, BindingFlags.GetProperty, null, oldInstance, null));
            var countNewList = Convert.ToInt32(newInstance.GetType().InvokeMember(COUNT_PROPERTY, BindingFlags.GetProperty, null, newInstance, null));
            var smallerList = (countOldList < countNewList ? countOldList : countNewList);
            var biggerList = (countOldList > countNewList ? countOldList : countNewList);

            for (var j = 0; j < biggerList; j++)
            {
                var oldValue = ((IList) oldInstance).Count > j ? ((IList) oldInstance)?[j] : null;
                var newValue = ((IList) newInstance).Count > j ? ((IList) newInstance)[j] : null;

                comparisons.Add(new DiffComparison() { PropertyName = $"{propertyName}", OldValue = oldValue, NewValue = newValue, Depth = depth });
            }
        }

        public override void LoadNode(Type type, object oldInstance, object newInstance, List<DiffComparison> comparisons)
        {
            throw new NotSupportedException();
        }
    }
}
