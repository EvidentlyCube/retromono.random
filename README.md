# Retromono.Random
A library containing pseudo-random number generators for use with [Monogame](http://www.monogame.net/).

Available on [NuGet](https://www.nuget.org/packages/Retromono.Random/).

## How to use
Just create an instance of one of the classes implements `IRandomEngine` and use one of the many methods available:

```csharp
var random = new XorShift128Random();
random.Int(0, 16);
```

## API

**Important!** `Int()`, `Uint()`, `Float()` and `Double()` are guaranteed to only work in about half of the space available to them, basically from `MinValue / 2` to `MaxValue / 2`. Anything beyond that might give incorrect results due to overflows.
It **should** work fine with cases like `0` to `MaxValue` but it has not been tested.

### Rolls

 - `double Fraction()` - Returns a double between 0 (inclusive) and 1 (exclusive). This is the basis by which all other methods are rolled
 - `int Int(int from, int to)` - Returns a signed integer between `from` (inclusive) and `to` (exclusive)
 - `uint Uint(uint from, uint to)` - Returns an unsignedinteger between `from` (inclusive) and `to` (exclusive)
 - `float Float(float from, float to)` - Returns a float between `from` (inclusive) and `to` (exclusive)
 - `double Double(double from, double to)` - Returns a double between `from` (inclusive) and `to` (exclusive)
 - `bool Boolean(double trueChance)` - Returns true or false, where `trueChance` for values `0`, `0.5` and `1` have respecively 0%, 50% and 100% chance to return true.
 - `int Invertion(double invertChance)` - Returns `-1` or `1`, where `invertChance` for values `0`, `0.5` and `1` have respecively 0%, 50% and 100% chance to return `-1`.

### Other
 - `bool SupportsSpecificSeed()` - Does this engine support getting and setting a specific seed (which allows retrieving and restoring a specific state of the RNG)
 - `bool SpecificSeed{get; set;}` - Set and retrieve specific seed
 - `void RandomizeSeed()` - Randomizes the seed using `System.Random` as the source of randomness
 - `void SetUniversalSeed(ulong seed)` - Sets the universal seed to a specific value. Setting the seed to a specific value always brings the RNG to the same state.