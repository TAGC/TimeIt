## TimeIt

[![NuGet](https://img.shields.io/nuget/v/TimeIt.svg)](https://www.nuget.org/packages/TimeIt)
[![Build status](https://ci.appveyor.com/api/projects/status/9a057rcq6n00p38t?svg=true)](https://ci.appveyor.com/project/TAGC/timeit)

TimeIt is a tool that can be used to measure the time it takes for regions of code to execute and perform various actions based on that, such as throwing exceptions if the elapsed time exceeds some specified timeout.

The idea is that you wrap up the code region you want to profile in a `using` block and configure the sequence of actions to perform after the block has finished running:

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
        using (TimeIt.Then.Log(_logger, "Code took {Elapsed} time").And.ThrowIfLongerThan(500))
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

### Performance

Although you can use TimeIt to help measure and optimise parts of your code, it will inevitably have a small impact on performance itself. Each general use case of the tool has been profiled using BenchmarkDotNet, and for each case we measure:

 - The time it takes for an expensive operation to be performed once, without profiling ("One off control").
 - The time it takes for an expensive operation to be performed, wrapped in a TimeIt block ("One off SUT").
 - The time it takes for a simple operation to be performed thousands of times repeatedly ("Looping control").
 - The time it takes for a simple operation to be performed thousands of times repeatedly, with a TimeIt block being created and destroyed on each loop ("Looping SUT"). This practice is *not recommended* in real code.

 These are one obtained set of results:

 #### TimeIt.Then.DoNothing

``` ini

BenchmarkDotNet=v0.10.14, OS=macOS High Sierra 10.13.4 (17E199) [Darwin 17.5.0]
Intel Core i5-4308U CPU 2.80GHz (Haswell), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (CoreCLR 4.6.0.0, CoreFX 4.6.26018.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (CoreCLR 4.6.0.0, CoreFX 4.6.26018.01), 64bit RyuJIT


```
|         Method |      Mean |     Error |    StdDev |
|--------------- |----------:|----------:|----------:|
|  OneOffControl | 103.18 ms | 0.6265 ms | 0.5860 ms |
|      OneOffSut | 102.70 ms | 0.7494 ms | 0.7010 ms |
| LoopingControl |  26.43 ms | 0.2262 ms | 0.1889 ms |
|     LoopingSut |  33.15 ms | 0.1524 ms | 0.1426 ms |

#### TimeIt.Then.Log

``` ini

BenchmarkDotNet=v0.10.14, OS=macOS High Sierra 10.13.4 (17E199) [Darwin 17.5.0]
Intel Core i5-4308U CPU 2.80GHz (Haswell), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (CoreCLR 4.6.0.0, CoreFX 4.6.26018.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (CoreCLR 4.6.0.0, CoreFX 4.6.26018.01), 64bit RyuJIT


```
|         Method |      Mean |     Error |    StdDev |
|--------------- |----------:|----------:|----------:|
|  OneOffControl | 103.01 ms | 0.7194 ms | 0.6729 ms |
|      OneOffSut | 103.47 ms | 0.5551 ms | 0.5193 ms |
| LoopingControl |  26.40 ms | 0.1219 ms | 0.1140 ms |
|     LoopingSut | 113.92 ms | 0.8666 ms | 0.8107 ms |


#### TimeIt.Then.ThrowIfLongerThan

``` ini

BenchmarkDotNet=v0.10.14, OS=macOS High Sierra 10.13.4 (17E199) [Darwin 17.5.0]
Intel Core i5-4308U CPU 2.80GHz (Haswell), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (CoreCLR 4.6.0.0, CoreFX 4.6.26018.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (CoreCLR 4.6.0.0, CoreFX 4.6.26018.01), 64bit RyuJIT


```
|         Method |      Mean |     Error |    StdDev |
|--------------- |----------:|----------:|----------:|
|  OneOffControl | 106.26 ms | 1.1729 ms | 1.0971 ms |
|      OneOffSut | 105.75 ms | 1.9630 ms | 1.8361 ms |
| LoopingControl |  29.13 ms | 0.4260 ms | 0.3985 ms |
|     LoopingSut |  57.76 ms | 0.2895 ms | 0.2566 ms |
