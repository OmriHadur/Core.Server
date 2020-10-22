using Core.Server.Injection.Attributes;
using Core.Server.Tests.ResourceCreation.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Core.Server.Test.ResourceCreation
{
    [Inject]
    public class ObjectRandomizer : IObjectRandomizer
    {
        private readonly Random random;

        public ObjectRandomizer()
        {
            random = new Random();
        }

        public string GetRandomId()
        {
            var buffer = new byte[12];
            random.NextBytes(buffer);
            return string.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
        }
        public void AddRandomValues(object resource)
        {
            var properties = resource.GetType().GetProperties();
            foreach (var property in properties)
                SetRandomValue(resource, property);
        }

        public void SetRandomValue(object resource, PropertyInfo property)
        {
            property.SetValue(resource, GetRandomValue(property));
        }

        public string GetRandomString(int length)
        {
            if (length <= 0) return string.Empty;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

        }

        private object GetRandomValue(PropertyInfo property)
        {
            var type = property.PropertyType;

            if (type == typeof(bool))
                return random.Next(2) == 0;
            if (type == typeof(string))
                return GetStringValue(property);
            else if (type == typeof(int))
                return GetIntInRange(property);
            else if (type == typeof(long))
                return GetLongInRange(property);
            else if (type == typeof(double))
                return GetDoubleInRange(property);
            else if (type == typeof(decimal))
                return GetDecimalInRange(property);
            else if (type == typeof(DateTime))
                return DateTime.Now.AddHours(random.Next(240));
            else if (type.IsEnum)
                return random.Next(Enum.GetValues(type).Length);
            return null;
        }

        private int GetIntInRange(PropertyInfo property)
        {
            var range = GetRangeAttribute(property);
            return range == null ?
                random.Next(100) :
                random.Next((int)range.Maximum - (int)range.Minimum) + (int)range.Minimum;
        }

        private long GetLongInRange(PropertyInfo property)
        {
            var range = GetRangeAttribute(property);
            if (range == null)
                return random.Next(1000);

            var rangeValue = random.NextDouble() * ((double)range.Maximum - (double)range.Minimum);
            rangeValue += (double)range.Minimum;
            rangeValue = Math.Round(rangeValue, 0);
            return (long)rangeValue;
        }
        private double GetDoubleInRange(PropertyInfo property)
        {
            var range = GetRangeAttribute(property);
            var doubleValue = random.NextDouble();
            if (range != null)
            {
                doubleValue *= ((double)range.Maximum - (double)range.Minimum);
                doubleValue += (double)range.Minimum;
            }
            return Math.Round(doubleValue, 2);
        }

        private decimal GetDecimalInRange(PropertyInfo property)
        {
            return (decimal)GetDoubleInRange(property);
        }

        private static RangeAttribute GetRangeAttribute(PropertyInfo property)
        {
            return property.GetCustomAttributes().OfType<RangeAttribute>().FirstOrDefault();
        }

        private object GetStringValue(PropertyInfo property)
        {
            var CustomAttributes = property.GetCustomAttributes();
            if (CustomAttributes.OfType<EmailAddressAttribute>().Any())
                return GetRandomString(10) + "@gmail.com";

            SetMinAndMax(CustomAttributes, out int? minLength, out int? maxLength);

            var name = GetObjectName(property);
            if (maxLength != null && name.Length > maxLength)
                name = name.Substring(0, (int)maxLength);
            var stringValueLength = random.Next(Math.Max(name.Length+2, minLength ?? 10), maxLength ?? 20);
            var randomStringLength = stringValueLength - name.Length - 1;
            if (randomStringLength <= 0)
                return name;
            return $"{name}-{GetRandomString(randomStringLength)}";
        }

        private static void SetMinAndMax(IEnumerable<Attribute> CustomAttributes, out int? minLength, out int? maxLength)
        {
            var minLengthAttribute = GetAttribute<MinLengthAttribute>(CustomAttributes);
            var maxLengthAttribute = GetAttribute<MaxLengthAttribute>(CustomAttributes);
            var stringLengthAttribute = GetAttribute<StringLengthAttribute>(CustomAttributes);
            minLength = minLengthAttribute?.Length ?? stringLengthAttribute?.MinimumLength;
            maxLength = maxLengthAttribute?.Length ?? stringLengthAttribute?.MaximumLength;
        }

        private string GetObjectName(PropertyInfo property)
        {
            return $"{property.ReflectedType.Name.Substring(0, 2)}-{property.Name.Substring(0, 4)}";
        }

        private static T GetAttribute<T>(IEnumerable<Attribute> CustomAttributes)
        {
            return CustomAttributes.OfType<T>().FirstOrDefault();
        }
    }
}