using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Carubbi.DiffAnalyzer
{
    public class DiffAnalyzer
    {
        private DiffModes _mode;

        private int _maxDepth;
        private int _depth = 0;
        private int _hash;
        public List<DiffComparison> Compare(object previousInstance, object currentInstance, DiffModes mode = DiffModes.All, int depth = 3)
        {
            _mode = mode;
            _maxDepth = depth;

            var comparisons = new List<DiffComparison>();

            var pendingPreviousNodes = new List<ComparisonNode>();
            var pendingCurrentNodes = new List<ComparisonNode>();

            _depth = 0;

           
            var previousNodes = GetStructure(previousInstance.GetType(), previousInstance);

            _hash = 0;
            _depth = 0;
            var currentNodes = GetStructure(currentInstance.GetType(), currentInstance);

            if (previousNodes.Count > currentNodes.Count)
            {
                for (var i = currentNodes.Count; i < previousNodes.Count; i++)
                {
                    currentNodes.Add(null);
                }
            }

            if (currentNodes.Count > previousNodes.Count)
            {
                for (var i = previousNodes.Count; i < currentNodes.Count; i++)
                {
                    previousNodes.Add(null);
                }
            }

            foreach (var node in previousNodes.Zip(currentNodes, (previous, current) => new { Previous = previous, Current = current }))
            {
                if (node.Previous == null && node.Current != null)
                {
                    var pendingPreviousNode = SearchNode(node.Current, pendingPreviousNodes, pendingCurrentNodes);

                    if (pendingPreviousNode == null) continue;

                    if (node.Current?.Hash != pendingPreviousNode.Hash)
                    {
                        comparisons.Add(new DiffComparison(node.Previous.Breadcrumb, null, node.Current?.Value));
                        comparisons.Add(new DiffComparison(node.Previous.Breadcrumb, pendingPreviousNode.Value, null));
                    }
                    else
                    {
                        comparisons.Add(new DiffComparison(node.Previous.Breadcrumb, pendingPreviousNode.Value, node.Current?.Value));
                    }
                }
                else if (node.Current == null && node.Previous != null)
                {
                    var pendingCurrentNode = SearchNode(node.Previous, pendingCurrentNodes, pendingPreviousNodes);

                    if (pendingCurrentNode == null) continue;

                    if (node.Previous?.Hash != pendingCurrentNode.Hash)
                    {
                        comparisons.Add(new DiffComparison(node.Previous.Breadcrumb, null,
                            pendingCurrentNode.Value));
                        comparisons.Add(new DiffComparison(node.Previous.Breadcrumb, node.Previous?.Value,
                            null));
                    }
                    else
                    {
                        comparisons.Add(new DiffComparison(node.Previous.Breadcrumb, node.Previous?.Value,
                            pendingCurrentNode.Value));
                    }
                }
                else if (node.Previous.Breadcrumb == node.Current.Breadcrumb)
                {
                    if (node.Previous.Hash != node.Current.Hash)
                    {
                        comparisons.Add(new DiffComparison(node.Previous.Breadcrumb, null,
                            node.Current?.Value));
                        comparisons.Add(new DiffComparison(node.Previous.Breadcrumb, node.Previous?.Value,
                            null));
                    }
                    else
                    {
                        comparisons.Add(new DiffComparison(node.Previous.Breadcrumb, node.Previous?.Value,
                            node.Current?.Value));
                    }

                }
                else
                {
                    var pendingCurrentNode = SearchNode(node.Previous, pendingCurrentNodes, pendingPreviousNodes);
                    if (pendingCurrentNode != null)
                    {
                        if (node.Previous?.Hash != pendingCurrentNode.Hash)
                        {
                            comparisons.Add(new DiffComparison(node.Previous.Breadcrumb, null,
                                pendingCurrentNode.Value));
                            comparisons.Add(new DiffComparison(node.Previous.Breadcrumb, node.Previous?.Value,
                                null));
                        }
                        else
                        {
                            comparisons.Add(new DiffComparison(node.Previous.Breadcrumb, node.Previous?.Value,
                                pendingCurrentNode.Value));
                        }
                    }


                    var pendingPreviousNode = SearchNode(node.Current, pendingPreviousNodes, pendingCurrentNodes);

                    if (pendingPreviousNode == null) continue;

                    if (pendingPreviousNode.Hash != node.Current?.Hash)
                    {
                        comparisons.Add(new DiffComparison(node.Current.Breadcrumb, null, node.Current?.Value));
                        comparisons.Add(new DiffComparison(node.Current.Breadcrumb, pendingPreviousNode.Value, null));
                    }
                    else { 
                        comparisons.Add(new DiffComparison(node.Current.Breadcrumb, pendingPreviousNode.Value, node.Current?.Value));
                    }
                }

            }

            foreach (var node in pendingPreviousNodes)
            {
                comparisons.Add(new DiffComparison(node.Breadcrumb, node.Value, null));
            }

            foreach (var node in pendingCurrentNodes)
            {
                comparisons.Add(new DiffComparison(node.Breadcrumb, null, node.Value));
            }

            return comparisons.OrderBy(c => c.BreadCrumb).ToList();
        }

        private ComparisonNode SearchNode(ComparisonNode node, ICollection<ComparisonNode> listToSearch, ICollection<ComparisonNode> listToAdd)
        {
            var pendingNodeFound = listToSearch.FirstOrDefault(pendingNode => node.Breadcrumb == pendingNode.Breadcrumb);

            if (pendingNodeFound != null)
            {
                listToSearch.Remove(pendingNodeFound);
                return pendingNodeFound;
            }

            listToAdd.Add(node);
            return null;
        }


        private IList<ComparisonNode> GetStructure(Type type, object instance, string breadcrumb = "Root")
        {
            _depth++;

            
            var nodes = new List<ComparisonNode>();

            if (type.IsValueType || type.IsPrimitive || instance is string)
            {
                nodes.Add(new ComparisonNode(breadcrumb, instance?.ToString(), _hash));
            }
            else switch (instance)
                {
                    case IDictionary dictionary:
                        foreach (var dictionaryKey in dictionary.Keys)
                        {
                            if (_depth > _maxDepth) continue;
                            nodes.AddRange(GetStructure(dictionary[dictionaryKey].GetType(), dictionary[dictionaryKey], $"{breadcrumb}[{dictionaryKey}]"));
                            _depth--;
                        }

                        break;
                    case IEnumerable enumerable:
                        {
                            var index = 0;
                            foreach (var item in enumerable)
                            {
                                if (_depth > _maxDepth) continue;

                                var listNodes = GetStructure(item.GetType(), item, $"{breadcrumb}[{index++}]");

                                foreach (var comparisonNode in listNodes)
                                {
                                    comparisonNode.Hash = _hash;
                                }

                                nodes.AddRange(listNodes);
                                _depth--;
                                _hash = 0;
                            }

                          
                            break;
                        }
                    default:
                        {
                            foreach (var property in type.GetProperties())
                            {
                                if (_depth > _maxDepth || instance == null) continue;

                                var indexedParameters = property.GetIndexParameters();
                                if (indexedParameters.Length > 0)
                                {
                                    var index = 0;
                                    while (true)
                                    {
                                        try
                                        {
                                            var indexedValue = property.GetValue(instance, new object[] { index });
                                            
                                            

                                            nodes.AddRange(GetStructure(property.PropertyType, indexedValue, $"{breadcrumb}.{property.Name}[{index++}]"));
                                            _depth--;
                                        }
                                        catch (TargetInvocationException)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    var propertyValue = property.GetValue(instance);

                                    var keyAttribute = property.GetCustomAttribute<DiffKeyAttribute>();

                                    if (_hash == 0)
                                    {
                                        _hash = keyAttribute != null && propertyValue != null
                                            ? propertyValue.GetHashCode()
                                            : 0;
                                    }

                                    nodes.AddRange(GetStructure(property.PropertyType, propertyValue , $"{breadcrumb}.{property.Name}"));

                                    _depth--;
                                }
                            }

                            break;
                        }
                }

            return nodes;
        }
    }
}
