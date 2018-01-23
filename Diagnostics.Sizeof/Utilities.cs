using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Diagnostics.Sizeof
{
    public static class Utilities
    {
        /// <summary>
        ///     Calculate the optimistic size af any managed object.
        ///     Get the minimal memory footprint of <paramref name="someObject" />.
        ///     Counted are all <paramref /> fields, including auto-generated, private and protected.
        ///     Not counted: any static fields, any properties, functions, member methods.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static long SizeInBytes<T>(this T someObject)
        {
            var temp = new Size<T>(someObject);
            var tempSize = temp.GetSizeInBytes();
            return tempSize;
        }

        /// <summary>
        ///     A way to estimate the in-memory size af any menaged object
        /// </summary>
        /// <typeparam name="TT"></typeparam>
        private sealed class Size<TT>
        {
            private static readonly int PointerSize = Environment.Is64BitOperatingSystem
                ? sizeof(long)
                : sizeof(int);

            private readonly TT _obj;
            private readonly HashSet<object> _references;

            public Size(TT obj)
            {
                _obj = obj;
                _references = new HashSet<object> { _obj };
            }

            public long GetSizeInBytes()
            {
                return GetSizeInBytes(_obj);
            }

            private long GetSizeInBytes<T>(T obj)
            {
                if (obj == null) return sizeof(int);
                var type = obj.GetType();

                if (type.IsPrimitive)
                {
                    switch (Type.GetTypeCode(type))
                    {
                        case TypeCode.Boolean:
                        case TypeCode.Byte:
                        case TypeCode.SByte:
                            return sizeof(byte);
                        case TypeCode.Char:
                            return sizeof(char);
                        case TypeCode.Single:
                            return sizeof(float);
                        case TypeCode.Double:
                            return sizeof(double);
                        case TypeCode.Int16:
                        case TypeCode.UInt16:
                            return sizeof(short);
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                            return sizeof(int);
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        default:
                            return sizeof(long);
                    }
                }
                if (obj is decimal)
                {
                    return sizeof(decimal);
                }
                if (obj is string)
                {
                    return sizeof(char) * obj.ToString().Length;
                }
                if (type.IsEnum)
                {
                    return sizeof(int);
                }
                if (type.IsArray)
                {
                    long sizeTemp = PointerSize;
                    var casted = (IEnumerable)obj;
                    foreach (var item in casted)
                    {
                        sizeTemp += GetSizeInBytes(item);
                    }
                    return sizeTemp;
                }
                if (obj is Pointer)
                {
                    return PointerSize;
                }
                long size = 0;
                var t = type;
                while (t != null)
                {
                    size += PointerSize;
                    var fields =
                        t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                    BindingFlags.DeclaredOnly);
                    foreach (var field in fields)
                    {
                        var tempVal = field.GetValue(obj);
                        if (!_references.Contains(tempVal))
                        {
                            _references.Add(tempVal);
                            size += GetSizeInBytes(tempVal);
                        }
                    }
                    t = t.BaseType;
                }
                return size;
            }
        }
    }
}
