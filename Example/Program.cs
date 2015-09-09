using System;
using System.Collections.Generic;

using Ephemera;

namespace Example {
	class DynamicProperties {
		public String Id { get; set; }
		public Int32 IdNum => Int32.Parse(Id);
	}

	enum What { Who, Where }

	class Program {
		static void Main(String[] args) {
			var list = new List<String>();
			list.Fmrn<DynamicProperties>().Id = "42";
			Console.WriteLine(list.Fmrn<DynamicProperties>().Id);

			list.Fmrn(false);
			Console.WriteLine(list.Fmrn<Boolean>());

			list.Fmrn<String>();
			Console.WriteLine(list.Fmrn<String>());

			list.Fmrn("Name", "Nick");
			Console.WriteLine(list.Fmrn("Name"));

			list.Fmrn("GetCount", new Func<List<String>, Int32>(x => x.Count));
			var getCount = (Func<List<String>, Int32>)list.Fmrn("GetCount");
			Console.WriteLine(getCount(list));

			list.Fmrn().What = "yea";
			Console.WriteLine(list.Fmrn().What);

			list.Fmrn().Foo = new Func<Int32>(() => list.Count);
			Console.WriteLine(list.Fmrn().Foo());

			var asdf = list.Fmrn<String[]>();
			Console.WriteLine(asdf.Length);

			list.Fmrn(new String[12]);
			var wjat = list.Fmrn<String[]>();
			Console.WriteLine(wjat.Length);

			list.Fmrn(What.Where);
			Console.WriteLine(list.Fmrn<What>());
		}
	}
}
