using System;
using System.Collections.Generic;
using Xunit;

namespace Ephemera
{
	public class Tests {
	    [Fact]
	    public void GettingAnUnsetPropertyThrows()
	    {
	        var o = new Object();
	        Assert.Throws<KeyNotFoundException>(() => o.Fmrn().Get("Name"));
        }

	    [Fact]
	    public void SettingAndGettingAProperty()
	    {
	        var o = new Object();
	        o.Fmrn().Set("Name", "Nick");
	        Assert.Equal(o.Fmrn().Get("Name"), "Nick");
	        o.Fmrn().Set("Name", "Nicholas");
	        Assert.Equal(o.Fmrn().Get("Name"), "Nicholas");
	    }

	    [Fact]
	    public void SettingAndTryGettingAProperty()
	    {
	        var o = new Object();
	        Assert.False(o.Fmrn().TryGet("Name", out var name));

	        o.Fmrn().Set("Name", "Nick");
            Assert.True(o.Fmrn().TryGet("Name", out name));
            Assert.Equal(name, "Nick");

	        o.Fmrn().Set("Name", "Nicholas");
	        Assert.True(o.Fmrn().TryGet("Name", out name));
	        Assert.Equal(name, "Nicholas");
        }
    }
}
