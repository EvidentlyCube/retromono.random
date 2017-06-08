using System;

namespace Retromono.Randomness {
    /// <summary>
    /// This random engine uses native <see cref="System.Random"/> as the source of randomness.
    /// Does not support specific seeds.
    /// </summary>
    public class NativeRandom : AbstractRandomEngine {
        private Random _random;

        public NativeRandom() {
            SupportsSpecificSeed = false;
            _random = new Random();
            RandomizeSeed();
        }

        public override void SetUniversalSeed(ulong seed) {
            _random = new Random(UlongToIntFullSpread(seed));
        }

        public override double Fraction() {
            return _random.NextDouble();
        }

        protected override string GetSpecificSeed() {
            throw new NotImplementedException("Native random does not support setting specific seed");
        }

        protected override void SetSpecificSeed(string seed) {
            throw new NotImplementedException("Native random does not support setting specific seed");
        }
    }
}