using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Carubbi.Extensions;
using Carubbi.Utils.Data;

namespace Carubbi.DiffAnalyzer.NodeLoaders
{
    public class ListNodeLoader : NodeLoaderBase
    {
        public ListNodeLoader(Action<Type, object, object, List<DiffComparison>> loadTypeNodeHandler)
            : base(loadTypeNodeHandler) { }

        private const string COUNT_PROPERTY = "Count";

        public override void LoadNode(Type type, object oldInstance, object newInstance, List<DiffComparison> comparisons)
        {
            var countOldList = (oldInstance != null && (string) oldInstance != string.Empty)
                ? oldInstance.GetProperty<int>(COUNT_PROPERTY)
                : 0;

            var countNewList = newInstance != null && (string) newInstance != string.Empty
                ? Convert.ToInt32(newInstance.GetType()
                    .InvokeMember(COUNT_PROPERTY, BindingFlags.GetProperty, null, newInstance, null))
                : 0;

            var smallerList = (countOldList < countNewList ? countOldList : countNewList);
            var biggerList = (countOldList > countNewList ? countOldList : countNewList);

            var propertyKeyNames = GetPropertyKeyNames(type);
            var keyNames = propertyKeyNames as string[] ?? propertyKeyNames.ToArray();
            var sortClasses = keyNames.Select(propertyKeyName => new SortClass(propertyKeyName, SortDirection.Ascending)).ToList();

            if (sortClasses.Count > 0)
            {
                var typedMultipleComparer = typeof(GenericMultipleComparer<>).Of(type).New(sortClasses);

                var listType = oldInstance?.GetType();

                listType?.InvokeMember("Sort",
                    BindingFlags.InvokeMethod,
                    null, oldInstance,
                    new[] { typedMultipleComparer });

                listType?.InvokeMember("Sort",
                    BindingFlags.InvokeMethod,
                   null, newInstance,
                   new[] { typedMultipleComparer });

                for (var i = 0; i < smallerList; i++)
                {
                    if (countOldList <= countNewList)
                    {
                        SyncronizeLists(type, oldInstance, newInstance, keyNames, listType, i);
                    }
                    else
                    {
                        SyncronizeLists(type, newInstance, oldInstance, keyNames, listType, i);
                    }
                }

                for (var i = 0; i < biggerList; i++)
                {
                    if (countOldList <= countNewList)
                    {
                        SyncronizeLists(type, newInstance, oldInstance, keyNames, listType, i);
                    }
                    else
                    {
                        SyncronizeLists(type, oldInstance, newInstance, keyNames, listType, i);
                    }
                }

                var countSyncronizedList = ((string) oldInstance != string.Empty) 
                    ? Convert.ToInt32(oldInstance?.GetType().InvokeMember(COUNT_PROPERTY, BindingFlags.GetProperty, null, oldInstance, null)) 
                    : 0;

                for (var i = 0; i < countSyncronizedList; i++)
                {
                    LoadTypeNodeHandler?.Invoke(type, ((IList) oldInstance)?[i], ((IList) newInstance)?[i], comparisons);
                    comparisons.Last().LastProperty = true;
                }
            }
            else
            {
                for (var i = 0; i < smallerList; i++)
                {
                    LoadTypeNodeHandler(type, (oldInstance as IList)?[i], (newInstance as IList)?[i], comparisons);
                    comparisons.Last().LastProperty = true;
                }

                for (var j = smallerList; j < biggerList; j++)
                {
                    object oldValue = "";
                    object newValue = "";

                    if (oldInstance != null && (string) oldInstance != "")
                    {
                        oldValue = ((IList) oldInstance).Count > j ? (oldInstance as IList)?[j] : null;
                    }

                    if (newInstance != null && (string) newInstance != "")
                    {
                        newValue = ((IList) newInstance).Count > j ? (newInstance as IList)?[j] : null;
                    }
                    LoadTypeNodeHandler(type, oldValue, newValue, comparisons);
                    comparisons.Last().LastProperty = true;
                }
            }
        }

        private static void SyncronizeLists(Type type, object oldInstance, object newInstance, IEnumerable<string> propertyKeyNames, Type listType, int i)
        {
            var constructorParams = propertyKeyNames.ToArray();
            var typedEqualityComparer = typeof(GenericEqualityComparer<>).Of(type).New(constructorParams);

            var exists = newInstance.CallGeneric<bool>(type, "Contains", newInstance, (oldInstance as IList)?[i], typedEqualityComparer);

            if (!exists)
            {
                listType.InvokeMember("Insert", BindingFlags.InvokeMethod, null, newInstance, new object[] { i, null });
            }
        }

        private static IEnumerable<string> GetPropertyKeyNames(Type type)
        {
            return from p in type.GetProperties()
                let attributes = p.GetCustomAttributes(typeof(DiffAnalyzableListItemKeyAttribute), true)
                where attributes.Length > 0
                select p.Name;
        }
    }
}
