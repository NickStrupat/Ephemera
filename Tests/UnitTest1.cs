using System;

using Ephemera;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests {
	[TestClass]
	public class UnitTest1 {
		[TestMethod]
		public void TestMethod1() {
			var o = new Object();
			o.Fmrn("Name", "Nick");
			Assert.AreEqual(o.Fmrn("Name"), "Nick");
		}

		[TestMethod]
		public void Performance() {
			
		}
	}
}
