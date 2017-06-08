using Retromono.Randomness;

namespace Tests {
    public class MockRandom : AbstractRandomEngine {
        private static readonly MockRandom _instance;

        public static MockRandom Seed(double returnFraction) {
            _instance._returnFraction = returnFraction;

            return _instance;
        }

        static MockRandom() {
            _instance = new MockRandom();
        }


        private double _returnFraction;

        public override double Fraction() {
            return _returnFraction;
        }

        protected override string GetSpecificSeed() {
            return null;
        }

        public override void SetUniversalSeed(ulong seed) { }
        protected override void SetSpecificSeed(string seed) { }
    }
}