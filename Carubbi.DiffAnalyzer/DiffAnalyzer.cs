using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Carubbi.Extensions;

namespace Carubbi.DiffAnalyzer
{
    /// <summary>
    /// Classe responsável por analizar e apontar as diferenças entre os valores de dois objetos da mesma classe
    /// </summary>
    public class DiffAnalyzer
    {
        private readonly AnalyzerBehavior _behavior;
        private readonly int _depthToAnalyze;
        private int _depth = 0;
        private Type _comparingType;

        /// <summary>
        /// Cria um analisador de diferenças de objetos até o 3o. nível de profundidade
        /// </summary>
        public DiffAnalyzer()
            : this(3, AnalyzerBehavior.CompareAll) { }

        public DiffAnalyzer(int depth)
            : this(depth, AnalyzerBehavior.CompareAll) { }

        public DiffAnalyzer(AnalyzerBehavior behavior)
            : this(3, behavior) { }

        /// <summary>
        /// Cria um analisador de diferenças de objetos definindo a profundidade
        /// </summary>
        /// <param name="depth">Profundidade das agregações que devem ser analisadas</param>
        public DiffAnalyzer(int depth, AnalyzerBehavior behavior)
        {
            _depthToAnalyze = depth;
            _behavior = behavior;
        }

        /// <summary>
        /// Compara duas instâncias da mesma classe
        /// </summary>
        /// <typeparam name="T">Tipo da classe a ser comparada</typeparam>
        /// <param name="oldInstance">Primeira instância</param>
        /// <param name="newInstance">Segunda instância</param>
        /// <returns>Lista de comparações com o nome da propriedade, valor antigo e novo valor bem como o status da comparação (Adicionado, Removido, Alterado, Não modificado, e desconhecido</returns>
        public List<DiffComparison> Compare(object oldInstance, object newInstance)
        {
            _depth = 0;

            Type type = null;
            Type newTtype = null;

            if (oldInstance != null)
                type = oldInstance.GetType();
            if (newInstance != null)
                newTtype = newInstance.GetType();

            if (type != null)
                _comparingType = type;
            else if (newTtype != null)
                _comparingType = newTtype;

            // Não é possível passar uma Interface caso esta validação seja aqui por enqto
            if (type != null && type.IsInterface)
                throw new Exception("Não é possível comparar interfaces.");
            else if (newTtype != null && newTtype.IsInterface)
                throw new Exception("Não é possível comparar com interfaces.");
            else if (type != newTtype)
                throw new Exception("Os tipos comparados são incompatíveis.");

            var comparisons = LoadAggregationTypesRecursive(_comparingType, oldInstance, newInstance);
            return comparisons;
        }

        /// <summary>
        /// Compara duas instâncias da mesma classe
        /// </summary>
        /// <typeparam name="T">Tipo da classe a ser comparada</typeparam>
        /// <param name="oldInstance">Primeira instância</param>
        /// <param name="newInstance">Segunda instância</param>
        /// <param name="predicate">predicate para ser usado como filtro na expressão linq Where</param>
        /// <returns>Lista de comparações com o nome da propriedade, valor antigo e novo valor bem como o status da comparação (Adicionado, Removido, Alterado, Não modificado, e desconhecido</returns>
        public List<DiffComparison> Compare<T>(T oldInstance, T newInstance, Func<DiffComparison, bool> predicate)
        {
            return Compare(oldInstance, newInstance).Where(predicate).ToList();
        }

        /// <summary>
        /// Ponto de partida para a recursão que realiza a análise dos objetos
        /// </summary>
        /// <param name="root">Tipo principal (raiz da recursão)</param>
        /// <param name="oldInstance">Primeira instância</param>
        /// <param name="newInstance">Segunda instância</param>
        /// <returns>Lista de comparações com o nome da propriedade, valor antigo e novo valor bem como o status da comparação (Adicionado, Removido, Alterado, Não modificado, e desconhecido</returns>
        private List<DiffComparison> LoadAggregationTypesRecursive(Type root, object oldInstance, object newInstance)
        {
            var comparisons = new List<DiffComparison>();
            LoadTypeNode(root, oldInstance, newInstance, comparisons);
            return comparisons;
        }

        /// <summary>
        /// Analiza a estrutura de um tipo bem como os valores de duas instancias
        /// </summary>
        /// <param name="type">Tipo a ser analisado</param>
        /// <param name="oldInstance">Primeira instância</param>
        /// <param name="newInstance">Segunda instância</param>
        /// <param name="comparisons">Lista de comparações com o nome da propriedade, valor antigo e novo valor bem como o status da comparação (Adicionado, Removido, Alterado, Não modificado, e desconhecido</param>
        private void LoadTypeNode(Type type, object oldInstance, object newInstance, List<DiffComparison> comparisons)
        {
            _depth++;
            if (_depth > _depthToAnalyze)
            {
                _depth--;
                return;
            }

            try
            {
                var oldInstanceAux = oldInstance;
                var newInstanceAux = newInstance;

                foreach (var property in type.GetProperties())
                {
                    if (HasAttribute(property, DiffAnalyzableUsage.Ignore))
                        continue;

                    if (_behavior == AnalyzerBehavior.CompareMarked && !HasAttribute(property, DiffAnalyzableUsage.Mark))
                        continue;

                    if (!property.PropertyType.IsPrimitive
                        && property.PropertyType != typeof(string)
                        && !property.PropertyType.IsEnum
                        && property.PropertyType != typeof(Decimal)
                        && property.PropertyType != typeof(DateTime))
                    {
                        LoadComplexNode(type, property, oldInstanceAux, newInstanceAux, comparisons);
                    }
                    else
                    {
                        var oldValue = GetValue(property, oldInstanceAux);
                        var newValue = GetValue(property, newInstanceAux);
                        AddComparison(type, GetDescription(property), oldValue, newValue, comparisons);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao tentar carregar tipo {type.Name}, profundidade {_depth}.", ex);
            }
            _depth--;
        }

        private void LoadComplexNode(Type parentType, PropertyInfo property, object oldInstance, object newInstance, List<DiffComparison> comparisons)
        {
            try
            {
                if (property.PropertyType.IsGeneric())
                {
                    LoadGenericTypeProperty(parentType, property, oldInstance, newInstance, comparisons);
                }
                else if (property.PropertyType.IsArray)
                {
                    LoadArrayItemProperty(property, oldInstance, newInstance, comparisons);
                }
                else if (property.GetIndexParameters().Length > 0)
                {
                    LoadIndexedProperty(property, oldInstance, newInstance, comparisons);
                }
                else
                {
                    LoadDefaultProperty(property, oldInstance, newInstance, comparisons);
                }
            }
            catch (TargetInvocationException) { }
            catch (TargetException) { }
            catch (Exception)
            {
                // ignored
            }
        }

        private void LoadDefaultProperty(PropertyInfo property, object oldInstance, object newInstance, List<DiffComparison> comparisons)
        {
            var oldValue = GetValue(property, oldInstance);
            var newValue = GetValue(property, newInstance);

            var typeName = string.Empty;
            if (oldValue != null)
                typeName = oldValue.GetType().Name;
            else if (newValue != null)
                typeName = newValue.GetType().Name;

            if (string.IsNullOrEmpty(typeName)) return;
            try
            {
                var nodeLoader = NodeLoaders.NodeLoaderFactory.GetInstance().GetLoader(typeName, LoadTypeNode);
                nodeLoader.LoadNode(property.PropertyType, oldValue, newValue, comparisons);
            }
            catch (TargetException) { }
        }

        private object GetValue(PropertyInfo property, object instance)
        {
            object value = string.Empty;
            try
            {
                value = instance == null || (string) instance == "" ? null : property.GetValue(instance, null);
            }
            catch (NullReferenceException) { }
            catch (TargetInvocationException) { }
            catch (TargetException) { }
            return value;
        }

        private void LoadGenericTypeProperty(Type parentType, PropertyInfo property, object oldInstance, object newInstance, List<DiffComparison> comparisons)
        {
            foreach (var genericType in property.PropertyType.GetGenericArguments())
            {
                object oldValue = string.Empty, newValue = string.Empty;

                if (oldInstance != null && (string) oldInstance != string.Empty)
                    oldValue = property.GetValue(oldInstance, null);

                if (newInstance != null && (string) newInstance != string.Empty)
                    newValue = property.GetValue(newInstance, null);

                if (!genericType.IsPrimitive && genericType != typeof(string) && !genericType.IsEnum && genericType != typeof(Decimal) && genericType != typeof(DateTime))
                {
                    NodeLoaders.NodeLoaderBase nodeLoader = null;
                    if (oldValue != null && (string) oldValue != "")
                    {
                        nodeLoader = NodeLoaders.NodeLoaderFactory.GetInstance().GetLoader(oldValue.GetType().Name, LoadTypeNode);
                    }
                    else if (newValue != null && (string) newValue != "")
                    {
                        nodeLoader = NodeLoaders.NodeLoaderFactory.GetInstance().GetLoader(newValue.GetType().Name, LoadTypeNode);
                    }

                    if (nodeLoader != null)
                    {
                        AddListComparison(parentType, property.Name, comparisons);
                        nodeLoader.LoadNode(genericType, oldValue, newValue, comparisons);
                    }
                    else
                        AddComparison(parentType, GetDescription(property), oldValue, newValue, comparisons);
                }
                else
                {
                    NodeLoaders.NodeLoaderBase nodeLoader = NodeLoaders.NodeLoaderFactory.GetInstance().GetPrimitiveNodeLoader(property.PropertyType.Name, LoadTypeNode);
                    nodeLoader.LoadNode(GetPropertyName(oldInstance?.GetType(), property), oldValue, newValue, comparisons, _depth);
                }
            }
        }

        private void AddComparison(Type parentType, string propertyName, object oldValue, object newValue, List<DiffComparison> comparisons)
        {
            comparisons.Add(new DiffComparison() { PropertyName = GetPropertyName(parentType, propertyName), OldValue = oldValue, NewValue = newValue, Depth = _depth });
        }

        private void AddListComparison(Type parentType, string propertyName, List<DiffComparison> comparisons)
        {
            if (_depth <= 1)
                comparisons.Add(new DiffListComparison() { PropertyName = GetPropertyName(parentType, propertyName), Depth = _depth, State = DiffState.Unknow });
        }

        private void LoadDiffComparison(object valueToAnalyze, object oldValue, object newValue, string propertyName, List<DiffComparison> comparisons, NodeLoaders.NodeLoaderBase nodeLoader)
        {
            if (valueToAnalyze == null) return;
            if (valueToAnalyze.GetType().IsPrimitive ||
                valueToAnalyze is string ||
                valueToAnalyze is decimal ||
                valueToAnalyze is DateTime ||
                valueToAnalyze.GetType().IsEnum)
            {
                AddComparison(null, propertyName, oldValue, newValue, comparisons);
            }
            else
            {
                nodeLoader.LoadNode(valueToAnalyze.GetType(), oldValue, newValue, comparisons);
            }
        }

        private void LoadArrayItemProperty(PropertyInfo property, object oldInstance, object newInstance, List<DiffComparison> comparisons)
        {
            try
            {
                var oldValue = property.GetValue(oldInstance, null);
                var newValue = property.GetValue(newInstance, null);
                var oldArrayLength = ((Array)oldValue).Length;
                var newArrayLength = ((Array)newValue).Length;
                var smallerList = (oldArrayLength < newArrayLength ? oldArrayLength : newArrayLength);
                var biggerList = (oldArrayLength > newArrayLength ? oldArrayLength : newArrayLength);
                var isNewListBiggerThanOld = (oldArrayLength < newArrayLength);

                var nodeLoader = NodeLoaders.NodeLoaderFactory.GetInstance().GetLoader(property.PropertyType.Name, LoadTypeNode);
                for (var i = 0; i < smallerList; i++)
                {
                    var oldItem = ((Array)oldValue).GetValue(i);
                    var newItem = ((Array)newValue).GetValue(i);
                    var item = oldItem ?? newItem;

                    LoadDiffComparison(item, oldItem, newItem, GetPropertyName(property.DeclaringType, property), comparisons, nodeLoader);
                }

                if (biggerList == smallerList) return;
                {
                    object oldItem = string.Empty;
                    object newItem = string.Empty;
                    if (isNewListBiggerThanOld)
                    {
                        for (var i = smallerList; i < biggerList; i++)
                        {
                            newItem = ((Array)newValue).GetValue(i);
                            LoadDiffComparison(newItem, oldItem, newItem, property.Name, comparisons, nodeLoader);
                        }
                    }
                    else
                    {
                        for (var i = smallerList; i < biggerList; i++)
                        {
                            oldItem = ((Array)oldValue).GetValue(i);
                            LoadDiffComparison(oldItem, oldItem, newItem, property.Name, comparisons, nodeLoader);
                        }
                    }
                }
            }
            catch (TargetInvocationException) { }
            catch (TargetException) { }
            catch (Exception)
            {
                //throw new ApplicationException(string.Format("Erro ao tentar carregar indexador: propriedade {0} ", property.Name), ex);
            }
        }

        private void LoadIndexedProperty(PropertyInfo property, object oldInstance, object newInstance, List<DiffComparison> comparisons)
        {
            try
            {
                var countOldList = Convert.ToInt32(property.DeclaringType.GetProperty("Count").GetValue(oldInstance, null));
                var countNewList = Convert.ToInt32(property.DeclaringType.GetProperty("Count").GetValue(newInstance, null));
                var smallerList = (countOldList < countNewList ? countOldList : countNewList);
                var biggerList = (countOldList > countNewList ? countOldList : countNewList);
                var isNewListBiggerThanOld = (countOldList < countNewList);

                var nodeLoader = NodeLoaders.NodeLoaderFactory.GetInstance().GetLoader(property.PropertyType.Name, LoadTypeNode);
                for (var i = 0; i < smallerList; i++)
                {
                    var oldValue = property.GetValue(oldInstance, new object[] { i });
                    var newValue = property.GetValue(newInstance, new object[] { i });
                    nodeLoader.LoadNode(property.PropertyType, oldValue, newValue, comparisons);
                }

                if (biggerList == smallerList) return;
                {
                    object oldValue = string.Empty;
                    object newValue = string.Empty;
                    if (isNewListBiggerThanOld)
                    {
                        for (var i = smallerList; i < biggerList; i++)
                        {
                            newValue = property.GetValue(oldInstance, new object[] { i });
                            nodeLoader.LoadNode(property.PropertyType, oldValue, newValue, comparisons);
                        }
                    }
                    else
                    {
                        for (var i = smallerList; i < biggerList; i++)
                        {
                            oldValue = property.GetValue(oldInstance, new object[] { i });
                            nodeLoader.LoadNode(property.PropertyType, oldValue, newValue, comparisons);
                        }
                    }
                }
            }
            catch (TargetInvocationException) { }
            catch (TargetException) { }
            catch (Exception)
            {
                // throw new ApplicationException(string.Format("Erro ao tentar carregar indexador: propriedade {0} ", property.Name), ex);
            }
        }

        private string GetPropertyName(Type parentType, PropertyInfo property)
        {
            return GetPropertyName(parentType, GetDescription(property));
        }

        private string GetPropertyName(Type parentType, string propertyName)
        {
            string property = string.Empty;

            if (propertyName != null)
            {
                if (parentType != null)
                {
                    if (_depth > 1 && parentType != _comparingType)
                        property = $"{GetDescription(parentType)} > {propertyName}";
                    else
                        property = $"{propertyName}";
                }
                else
                {
                    property = propertyName;
                }
            }
            else
            {
                if (parentType != null)
                {
                    property = parentType.Name;
                }
            }
            return property;
        }

        private string GetDescription(PropertyInfo property)
        {
            var customAttributes = property.GetCustomAttributes(typeof(DiffAnalyzableAttribute), true);
            var description = string.Empty;
            description = customAttributes.Length > 0 
                ? ((DiffAnalyzableAttribute)customAttributes[0]).Description
                : property.Name;
            return description;
        }

        private string GetDescription(Type type)
        {
            var customAttributes = type.GetCustomAttributes(typeof(DiffAnalyzableEntityAttribute), true);
            var description = string.Empty;
            description = customAttributes.Length > 0 
                ? ((DiffAnalyzableEntityAttribute)customAttributes[0]).Description 
                : type.Name;

            return description;
        }

        private bool HasAttribute(PropertyInfo property, DiffAnalyzableUsage usage)
        {
            return (property.GetCustomAttributes(typeof(DiffAnalyzableAttribute), true).Any(daa => ((DiffAnalyzableAttribute)daa).Usage == usage));
        }
    }
}
