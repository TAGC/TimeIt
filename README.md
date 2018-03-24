## TimeIt

[![NuGet](https://img.shields.io/nuget/v/TimeIt.svg)](https://www.nuget.org/packages/TimeIt)
[![Build status](https://ci.appveyor.com/api/projects/status/9a057rcq6n00p38t?svg=true)](https://ci.appveyor.com/project/TAGC/timeit)

TimeIt is a tool that can be used to measure the time it takes for regions of code to execute and perform various actions based on that, such as throwing exceptions if the elapsed time exceeds some specified timeout.

The idea is that you wrap up the code region you want to profile in a `using` block and configure the sequence of actions to perform based on the time it takes for the region to execute after it's finished running:

```cs
using (TimeIt.Then.Do(elapsed => { /* something */ }).And.Do(elapsed => { /* something else */ }))
{
    // Profiled code goes here.    
}
```

Importing the `TimeItCore` namespace also allows you to use a core set of extension methods for configuring common actions:

```cs
using Microsoft.Extensions.Logging;
using TimeItCore;

public class Foo
{
    private readonly ILogger _logger;

    public Foo(ILogger<Foo> logger)
    {
        _logger = logger;
    }

    public void Bar()
    {
        using (TimeIt.Then.Log(_logger, "Code took {Elapsed} time").And.ThrowIfLongerThan(500)))
        {
            // Profiled code goes here.    
        }
    }
}
```

### Installation

TimeIt targets the .NET Standard and can be used within .NET Core and .NET Framework applications. It is available on the standard NuGet feed and can be installed from there. For example, using the dotnet CLI:

```
dotnet add package TimeIt
```