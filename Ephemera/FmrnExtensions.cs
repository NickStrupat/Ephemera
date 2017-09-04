using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Ephemera {
    public sealed class Ephemeron
    {
        private static readonly ConditionalWeakTable<Object, Ephemeron> Ephemerons = new ConditionalWeakTable<Object, Ephemeron>();

        internal static Ephemeron GetOrCreate(Object @object) => Ephemerons.GetOrCreateValue(@object);

        private readonly ConcurrentDictionary<String, Object> properties = new ConcurrentDictionary<String, Object>();

        public void Set(String name, Object value) => properties.AddOrUpdate(name, value, (s, o) => value);

        public Boolean TryGet(String name, out Object value) => properties.TryGetValue(name, out value);

        public Object Get(String name) => TryGet(name, out var value) ? value : throw new KeyNotFoundException();

        public T Get<T>(String name) => TryGet(name, out var value) ? (T) value : throw new KeyNotFoundException();
    }

	public static class FmrnExtensions
	{
	    public static Ephemeron Fmrn<T>(this T @object) where T : class => Ephemeron.GetOrCreate(@object ?? throw new ArgumentNullException(nameof(@object)));
    }
}