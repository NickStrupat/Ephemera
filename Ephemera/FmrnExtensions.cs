using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;

using New.Instance;

namespace Ephemera {
	public static class FmrnExtensions {
		private static readonly ConditionalWeakTable<Object, ConcurrentDictionary<String, Object>> cwt = new ConditionalWeakTable<Object, ConcurrentDictionary<String, Object>>();

		private static ConcurrentDictionary<String, Object> FmrnInternal(this Object @object) => cwt.GetValue(@object, key => new ConcurrentDictionary<String, Object>());

		public static void Fmrn(this Object @object, String propertyName, Object value) {
			if (@object == null)
				throw new ArgumentNullException(nameof(@object));
			if (propertyName == null)
				throw new ArgumentNullException(nameof(propertyName));

			var dict = @object.FmrnInternal();
			lock (dict)
				dict.AddOrUpdate(propertyName, value, (s, o) => value);
		}

		public static Object Fmrn(this Object @object, String propertyName) {
			if (@object == null)
				throw new ArgumentNullException(nameof(@object));
			if (propertyName == null)
				throw new ArgumentNullException(nameof(propertyName));

			Object value;
			if (!@object.FmrnInternal().TryGetValue(propertyName, out value))
				throw new KeyNotFoundException($"A dynamic property named {propertyName} was not found on this object.");
			return value;
		}

		public static dynamic Fmrn(this Object @object) => @object.Fmrn<ExpandoObject>();

		private const String typeNamePrefix = "fmrnTypeName_";

		public static void Fmrn<T>(this Object @object, T value) => @object.Fmrn(typeNamePrefix + typeof(T).FullName, value);

		public static T Fmrn<T>(this Object @object) {
			if (@object == null)
				throw new ArgumentNullException(nameof(@object));

			var dict = @object.FmrnInternal();
			var key = typeNamePrefix + typeof (T).FullName;
            Object value;
			lock (dict)
				value = dict.GetOrAdd(key, x => New<T>.Instance());
			try {
				return (T)value;
			}
			catch (InvalidCastException ex) {
				throw new InvalidOperationException($"Requested property of type {typeof(T).FullName} exists as type {value.GetType().FullName} and could not be cast.", ex);
			}
		}
	}
}