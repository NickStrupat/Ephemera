using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;

using New.Instance;

namespace Ephemera {
	public static class FmrnExtensions {
		private static readonly ConditionalWeakTable<Object, ConcurrentDictionary<String, Object>> cwt = new ConditionalWeakTable<Object, ConcurrentDictionary<String, Object>>();

		private static ConcurrentDictionary<String, Object> FmrnInternal(this Object @object) {
			return cwt.GetValue(@object, key => new ConcurrentDictionary<String, Object>());
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

		public static void Fmrn(this Object @object, String propertyName, Object value) {
			if (@object == null)
				throw new ArgumentNullException(nameof(@object));
			if (propertyName == null)
				throw new ArgumentNullException(nameof(propertyName));

			@object.FmrnInternal().AddOrUpdate(propertyName, value, (s, o) => value);
		}

		private static class TypeKey<T> {
			public static readonly String Name = "fmrnTypeName_" + typeof (T).FullName;
		}

		public static T Fmrn<T>(this Object @object) {
			if (@object == null)
				throw new ArgumentNullException(nameof(@object));

			var value = @object.FmrnInternal().GetOrAdd(TypeKey<T>.Name, x => New<T>.Instance());
			try {
                return (T)value;
			}
			catch (InvalidCastException ex) {
				throw new InvalidOperationException($"Requested property of type {typeof(T).FullName} exists as type {value.GetType().FullName} and could not be cast.", ex);
			}
		}

		public static void Fmrn<T>(this Object @object, T value) {
			@object.Fmrn(TypeKey<T>.Name, value);
		}

		public static dynamic Fmrn(this Object @object) {
			return @object.Fmrn<ExpandoObject>();
		}
	}
}