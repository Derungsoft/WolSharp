# WolSharp
A simple .NET Standard library for sending WakeOnLan packets (Magic packet) over ethernet 

# Prerequisites

To wake up computers using this library they need to have WakeOnLan enabled and must be in the same local network

# Installation
Install the [NuGet package][1] of this library using
```
Install-Package Derungsoft.WolSharp
```

# ARM32 Wake on Lan Server for Docker (Raspberry Pi)

You can set up your own Wake On Lan Server running inside Docker on a Raspberry Pi!

Just install Docker on your Pi and then execute the following command:
```
sudo docker run --name WolSharp -d -it --restart unless-stopped --network=host derungsoft/wolsharp:0.1
 ```

 This command will download and run the WOL Server which runs on port 80.

 If you don't want the Server to automatically start on boot, you can remove "--restart unless-stopped"

 Then wake up other computers by calling this URL:

```
 http://192.168.1.123/api/wake/55-33-44-55-AA-BB
```

Just replace the IP and MAC address and you're good to go.

# Usage

## Basic usage
```C#
using Derungsoft.WolSharp;
// ...

// Create awakener instance with default PhysicalAddressParser
var awakener = new Awakener();

// Send WoL to "AA-00-BB-11-CC-22"
awakener.Wake("AA-00-BB-11-CC-22");
```

## Async using await

```C#
using Derungsoft.WolSharp;
// ...

// Create awakener instance with default PhysicalAddressParser
var awakener = new Awakener();

// Send WoL to "AA-00-BB-11-CC-22"
await awakener.WakeAsync("AA-00-BB-11-CC-22");
```

## Using custom PhysicalAddressParser
### Create custom PhysicalAddressParser class
```C#
public class MyPhysicalAddressParser : IPhysicalAddressParser
{
    //Implement Parse methods
    //...
}
```
### Pass parser to Awakener constructor
```C#
// Create custom PhysicalAddressParser
var physicalAddressParser = new MyPhysicalAddressParser();

// Pass custom parser to constructor
var awakener = new Awakener(physicalAddressParser);

await awakener.WakeAsync("AA-00-BB-11-CC-22");
```

## Dependency injection

1. Register ```IAwakener``` using ```Awakener```
2. Register ```IPhysicalAddressParser``` using ```DefaultPhysicalAddressParser``` or a custom implementation


[1]: https://www.nuget.org/packages/Derungsoft.WolSharp/
