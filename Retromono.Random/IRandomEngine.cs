using System;

namespace Retromono.Randomness {
    public interface IRandomEngine {
        /// <summary>
        /// Randomizes the seed using <see cref="Random"/> as the source of randomness.
        /// </summary>
        void RandomizeSeed();

        /// <summary>
        /// Sets the seed using an unsigned long - all engines must support this gracefully.
        /// You can never retrieve universal seed because many random engines have more states than can fit in a ulong.
        /// For retrieving seed see <see cref="get_SpecificSeed"/> and <see cref="set_SpecificSeed"/>
        /// </summary>
        /// <param name="seed">Seed as an ulong</param>
        void SetUniversalSeed(ulong seed);

        /// <summary>
        /// Generates a new fraction that is guaranteed to be between 0 (inclusive) and 1 (exclusive)
        /// </summary>
        /// <returns>Double in range 0 (inclusive) and 1 (exclusive)</returns>
        double Fraction();

        /// <summary>
        /// Returns a signed integer in the specified range, <paramref name="from"/> is inclusive, <paramref name="to"/> is exclusive.
        /// To avoid heavy edge-case checking, this function only supports middle half of the int space.
        /// </summary>
        /// <param name="from">Inclusive from range (supported down to <c>int.MinValue / 2</c>)</param>
        /// <param name="to">Exclusive to range (supported up to <c>int.MaxValue / 2</c>)</param>
        /// <returns>Random signed integer</returns>
        int Int(int from = int.MinValue / 2, int to = int.MaxValue / 2);

        /// <summary>
        /// Returns an unsigned integer in the specified range, <paramref name="from"/> is inclusive, <paramref name="to"/> is exclusive.
        /// To avoid heavy edge-case checking, this function only supports lower half of the uint space.
        /// </summary>
        /// <param name="from">Inclusive from range (supported down to <c>0</c>)</param>
        /// <param name="to">Exclusive to range (supported up to <c>uint.MaxValue / 2</c>)</param>
        /// <returns>Random unsigned integer</returns>
        uint Uint(uint from = 0, uint to = uint.MaxValue / 2);

        /// <summary>
        /// Returns a float in the specified range, <paramref name="from"/> is inclusive, <paramref name="to"/> is exclusive.
        /// To avoid heavy edge-case checking, this function only supports middle half of the float space.
        /// </summary>
        /// <param name="from">Inclusive from range (supported down to <c>float.MinValue / 2</c>)</param>
        /// <param name="to">Exclusive to range (supported up to <c>float.MaxValue / 2</c>)</param>
        /// <returns>Random float</returns>
        float Float(float from = float.MinValue / 2, float to = float.MaxValue / 2);

        /// <summary>
        /// Returns a double in the specified range, <paramref name="from"/> is inclusive, <paramref name="to"/> is exclusive.
        /// To avoid heavy edge-case checking, this function only supports middle half of the double space.
        /// </summary>
        /// <param name="from">Inclusive from range (supported down to <c>float.double.MaxValue / 2</c>)</param>
        /// <param name="to">Exclusive to range (supported up to <c>double.MaxValue / 2</c>)</param>
        /// <returns>Random double</returns>
        double Double(double from = double.MinValue / 2, double to = double.MaxValue / 2);

        /// <summary>
        /// Returns <c>true</c> or <c>false</c> with the specified chance.
        /// </summary>
        /// <param name="trueChance">Chance that the value will be <c>true</c>, where 1 means 100% chance, 0.5 means 50% chance and 0 means 0% chance</param>
        /// <returns>A random boolean</returns>
        bool Boolean(double trueChance = 0.5);

        /// <summary>
        /// Returns <c>-1</c> or <c>1</c> with the specified chance.
        /// </summary>
        /// <param name="invertChance">Chance that the value will be <c>-1</c>, where 1 means 100% chance, 0.5 means 50% chance and 0 means 0% chance</param>
        /// <returns><c>-1</c> or <c>1</c></returns>
        int Invertion(double invertChance = 0.5);

        /// <summary>
        /// Returns if the current random engine supports specific seeds
        /// </summary>
        bool SupportsSpecificSeed { get; }

        /// <summary>
        /// Sets or retrieves the specific seed - each random engine can support specific seed (see <see cref="SupportsSpecificSeed"/>)
        /// and the output it generates can be fed back to the same type of engine. Specific seeds of one engine type
        /// generally never will work with another type.
        /// </summary>
        string SpecificSeed { get; set; }
    }
}