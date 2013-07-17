using System;
using System.Globalization;
using System.Reflection;
using CashFlowManager.Common.Attributes;

namespace CashFlowManager.Common.BaseClass
{
    public class EntityBase<TInfoType> where TInfoType : EntityBase<TInfoType>, new()
    {

        /// <summary>
        /// Build up an Info class from the data contained in an EF entity class
        /// </summary>
        /// <param name="entityFrameworkClass">The EF entity class to copy the data from</param>
        /// <param name="validationEntityFields">An indicator of whether validation must be enabled for this entity</param>
        /// <returns>An Info class with all the properties set from the EF entity class</returns>
        public static TInfoType FromEntity(Object entityFrameworkClass, bool validationEntityFields = false)
        {
            var newObject = new TInfoType();
            PropertyInfo[] propertyInfos = entityFrameworkClass.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in propertyInfos)
            {
                PropertyInfo destinationPropertyInfo = newObject.GetType().GetProperty(propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                if (destinationPropertyInfo != null)
                {

                    var ignore = destinationPropertyInfo.GetCustomAttribute(typeof(IgnoreProperty));
                    if (ignore != null)
                        continue;

                    object value = propertyInfo.GetValue(entityFrameworkClass, null);

                    // TFS 29547 (believe it or not....)
                    // Accommodate the situation where we are transferring an int field into an enum field (this may still need some bullet proofing, we'll wait and see)
                    if (destinationPropertyInfo.PropertyType.IsEnum)
                    {
                        value = Enum.ToObject(destinationPropertyInfo.PropertyType, value);
                    }
                    if (destinationPropertyInfo.PropertyType.IsAssignableFrom(propertyInfo.PropertyType))
                    {
                        if (destinationPropertyInfo.CanWrite && propertyInfo.CanRead &&
                            (destinationPropertyInfo.PropertyType == propertyInfo.PropertyType || destinationPropertyInfo.PropertyType.IsEnum))
                        {
                            destinationPropertyInfo.SetValue(newObject, value, null);
                        }
                    }
                    else
                    {

                        try
                        {
                            value = ConvertValue(destinationPropertyInfo.PropertyType, value, true);
                        }
                        catch (Exception exception)
                        {
                            string message =
                                string.Format("The field type for field {3}.{0} does not match.  EntityType: {1}, InfoType {2}.  The Info classes may need to be regenerated",
                                destinationPropertyInfo.Name, propertyInfo.PropertyType, destinationPropertyInfo.PropertyType, entityFrameworkClass.GetType());


                            throw new InvalidOperationException(message, exception);
                        }

                        destinationPropertyInfo.SetValue(newObject, value, null);
                    }
                }
            }
            return newObject;
        }

        /// <summary>
        /// Build up an Entity class from the data contained in this Info class
        /// </summary>
        /// <typeparam name="TEntity">The type of the Entity class to map to, for example BankLink.Practice.SystemDomain.User</typeparam>
        /// <returns>A fully populated Entity class where the Entity class's properties matched the Info classes properties</returns>
        public TEntity ToEntity<TEntity>() where TEntity : new()
        {
            var newObject = new TEntity();
            PropertyInfo[] propertyInfos = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in propertyInfos)
            {
                var ignore = propertyInfo.GetCustomAttribute(typeof(IgnoreProperty));
                if (ignore != null)
                    continue;

                PropertyInfo destinationPropertyInfo = newObject.GetType().GetProperty(propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (destinationPropertyInfo != null)
                {
                    if (propertyInfo.CanRead)
                    {
                        object value = propertyInfo.GetValue(this, null);

                        if (destinationPropertyInfo.PropertyType.IsAssignableFrom(propertyInfo.PropertyType))
                        {
                            if (destinationPropertyInfo.CanWrite && (destinationPropertyInfo.PropertyType == propertyInfo.PropertyType))
                            {
                                destinationPropertyInfo.SetValue(newObject, value, null);
                            }
                        }
                        else
                        {
                            try
                            {
                                value = ConvertValue(destinationPropertyInfo.PropertyType, value, true);
                                destinationPropertyInfo.SetValue(newObject, value, null);
                            }
                            catch (Exception exception)
                            {
                                string message = string.Format("The field type for field {3}.{0} does not match.  EntityType: {1}, InfoType {2}.  The Info classes may need to be regenerated",
                                destinationPropertyInfo.Name, propertyInfo.PropertyType, destinationPropertyInfo.PropertyType, this.GetType());
                                throw new InvalidOperationException(message, exception);
                            }
                        }
                    }
                }
            }
            return newObject;
        }

        /// <summary>
        /// Converts the value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <returns></returns>
        public static object ConvertValue(Type type, object value, bool throwException)
        {
            try
            {
                if ((value == null) || type.IsAssignableFrom(value.GetType()))
                {
                    return value;
                }
                if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    value = NullableConvertValue(type, value);
                    return value;
                }
                value = Convert.ChangeType(value, type, CultureInfo.CurrentCulture);
            }
            catch
            {
                if (throwException)
                {
                    throw;
                }
            }
            return value;
        }

        private static object NullableConvertValue(Type type, object value)
        {
            Type underlyingType = Nullable.GetUnderlyingType(type);
            value = Convert.ChangeType(value, underlyingType, CultureInfo.CurrentCulture);
            switch (Type.GetTypeCode(underlyingType))
            {
                case TypeCode.Empty:
                    throw new InvalidCastException("InvalidCast_Empty");

                case TypeCode.Object:
                    return value;

                case TypeCode.DBNull:
                    throw new InvalidCastException("InvalidCast_DBNull");

                case TypeCode.Boolean:
                    return new bool?((bool)value);

                case TypeCode.Char:
                    return new char?((char)value);

                case TypeCode.SByte:
                    return new sbyte?((sbyte)value);

                case TypeCode.Byte:
                    return new byte?((byte)value);

                case TypeCode.Int16:
                    return new short?((short)value);

                case TypeCode.UInt16:
                    return new ushort?((ushort)value);

                case TypeCode.Int32:
                    return new int?((int)value);

                case TypeCode.UInt32:
                    return new uint?((uint)value);

                case TypeCode.Int64:
                    return new long?((long)value);

                case TypeCode.UInt64:
                    return new ulong?((ulong)value);

                case TypeCode.Single:
                    return new float?((float)value);

                case TypeCode.Double:
                    return new double?((double)value);

                case TypeCode.Decimal:
                    return new decimal?((decimal)value);

                case TypeCode.DateTime:
                    return new DateTime?((DateTime)value);
            }
            throw new ArgumentException("Arg_UnknownTypeCode");
        }

    }
}
