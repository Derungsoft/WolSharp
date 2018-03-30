# WolSharp
A simple .NET Standard library for sending WakeOnLan packets (Magic packet) over ethernet 

# Prerequisites

To wake up computers using this library they need to have WakeOnLan enabled and must be in the same local network

# Installation
Install the [NuGet package][1] of this library using
```
Install-Package Derungsoft.WolSharp
```

# Usage

## Basic usage
```C#
using Derungsoft.WolSharp;
// ...

// Create awakener instance with default MacAddressParser
var awakener = new Awakener();

// Send WoL to "AA-00-BB-11-CC-22"
awakener.Wake("AA-00-BB-11-CC-22");
```

## Async using await

```C#
using Derungsoft.WolSharp;
// ...

// Create awakener instance with default MacAddressParser
var awakener = new Awakener();

// Send WoL to "AA-00-BB-11-CC-22"
await awakener.WakeAsync("AA-00-BB-11-CC-22");
```

## Using custom MacAddressParser
### Create custom MacAddressParser class
```C#
public class MyMacAddressParser : IMacAddressPaser
{
    //Implement Parse methods
    //...
}
```
### Pass parser to Awakener constructor
```C#
// Create custom MacAddressParser
var macAddressParser = new MyMacAddressParser();

// Pass custom parser to constructor
var awakener = new Awakener(macAddressParser);

await awakener.WakeAsync("AA-00-BB-11-CC-22");
```

## Dependency injection

1. Register ```IAwakener``` using ```Awakener```
2. Register ```IMacAddressParser``` using ```DefaultMacAddressParser``` or a custom implementation


[1]: https://www.nuget.org/packages/Derungsoft.WolSharp/
