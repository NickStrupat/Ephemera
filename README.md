# Ephemera

#### .NET ephemerons

Attach and use properties to objects at run-time which are automatically collected after the host object is collected.

This is accomplished with weak-references, implemented in the [ConditionalWeakTable](https://msdn.microsoft.com/en-us/library/dd287757(v=vs.110).aspx) class  

| .NET support                                  | NuGet package                                                                                                                                              |
|:----------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| >= Framework 4.0 &#124;&#124; >= Standard 2.0 | [![NuGet Status](http://img.shields.io/nuget/v/Ephemera.svg?style=flat)](https://www.nuget.org/packages/Ephemera/) |

### Usage

```csharp
var foo = new Foo();

// Attach some things to `foo`
foo.Fmrn().Set("Name", "Value");
foo.Fmrn().Set("Bar", new Bar());

// Retrieve them
var bar = foo.Fmrn().Get<Bar>("Bar");
var name = foo.Fmrn().Get<String>("Name");

// Retrieve with `TryGet`
var didGetUnsetProperty = foo.Fmrn().TryGet("Nope", out var unset);
```
