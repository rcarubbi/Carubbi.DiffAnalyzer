using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Carubbi.DiffAnalyzer
{
    public static class Extensions
    {
        public static object CreateGenericTypeByReflection(Type genericType, Type type, Object[] constructorParams)
        {
            Type[] typeArgs = { type };
            var typedType = genericType.MakeGenericType(typeArgs);
            var typedtypeInstance = Activator.CreateInstance(typedType, constructorParams);
            return typedtypeInstance;
        }

        public static IEnumerable<KeyValuePair<Type, MethodInfo>> GetExtensionMethodsDefinedInType(this Type t)
        {
            if (!t.IsSealed || t.IsGenericType || t.IsNested)
                return Enumerable.Empty<KeyValuePair<Type, MethodInfo>>();

            var methods = t.GetMethods(BindingFlags.Public | BindingFlags.Static)
                           .Where(m => m.IsDefined(typeof(ExtensionAttribute), false));

            var pairs = new List<KeyValuePair<Type, MethodInfo>>();
            foreach (var m in methods)
            {
                var parameters = m.GetParameters();
                if (parameters.Length <= 0) continue;
                if (parameters[0].ParameterType.IsGenericParameter)
                {
                    if (!m.ContainsGenericParameters) continue;
                    var genericParameters = m.GetGenericArguments();
                    var genericParam = genericParameters[parameters[0].ParameterType.GenericParameterPosition];
                    pairs.AddRange(genericParam.GetGenericParameterConstraints().Select(constraint =>
                        new KeyValuePair<Type, MethodInfo>(parameters[0].ParameterType, m)));
                }
                else
                    pairs.Add(new KeyValuePair<Type, MethodInfo>(parameters[0].ParameterType, m));
            }

            return pairs;
        }

        public static bool IsGenericTypes(this Type source)
        {
            if (source.IsGenericType)
                return true;

            var baseType = source.BaseType;
            while (baseType != null)
            {
                if (baseType.IsGenericType)
                    return true;

                baseType = baseType.BaseType;
            }

            return false;
        }

        public static bool IsTypeByName(this Type source, string targetTypeName)
        {
            if (source.Name == targetTypeName)
                return true;

            var baseType = source.BaseType;
            while (baseType != null)
            {
                if (baseType.Name == targetTypeName)
                    return true;

                baseType = baseType.BaseType;
            }

            return false;
        }

        public static string PadLeft(this string source, int totalWidth, string padPattern)
        {
            var stb = new StringBuilder();
            for (var i = 0; i < totalWidth; i++)
                stb.Append(padPattern);

            stb.Append(source);
            return stb.ToString();
        }

        public static bool Has<T>(this System.Enum type, T value)
        {
            try
            {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            }
            catch
            {
                return false;
            }
        }

        public static bool Is<T>(this System.Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }

        public static T Add<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not append value from enumerated type '{typeof(T).Name}'.", ex);
            }
        }

        public static T Remove<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not remove value from enumerated type '{typeof(T).Name}'.", ex);
            }
        }
    }
}
