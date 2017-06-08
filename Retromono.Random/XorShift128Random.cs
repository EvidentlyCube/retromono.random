using System.Diagnostics.Contracts;

namespace Retromono.Randomness {
    /// <summary>
    /// This random engine uses XOR Shift 128. <a href="https://en.wikipedia.org/wiki/Xorshift">See Wikipedia</a> for more information.
    /// </summary>
    public class XorShift128Random : AbstractRandomEngine {
        private const byte ShiftUlongToInt = 33;
        private const double IntToDoubleMultiplier = 1.0 / (int.MaxValue + 1.0);
        private const ulong SeedX = 521288629U << 32;
        private const ulong SeedY = 4101842887655102017UL;

        private ulong _x;
        private ulong _y;
        private bool _bytesAvailable;

        public XorShift128Random() {
            SupportsSpecificSeed = true;
            RandomizeSeed();
        }

        public override void SetUniversalSeed(ulong seed) {
            _x = (ulong) (SeedX + UlongToFraction(seed) * ulong.MaxValue);
            _y = SeedY;
            _bytesAvailable = false;
        }

        public override double Fraction() {
            if (_bytesAvailable) {
                _bytesAvailable = false;
                return (int) ((_x + _y) << ShiftUlongToInt >> ShiftUlongToInt) * IntToDoubleMultiplier;
            }

            var tx = _x;
            var ty = _y;
            _x = ty;
            tx ^= tx << 23;
            tx ^= tx >> 17;
            tx ^= ty ^ (ty >> 26);
            _y = tx;
            _bytesAvailable = true;

            var result = (int) ((tx + ty) >> ShiftUlongToInt) * IntToDoubleMultiplier;

            Contract.Assert(result >= 0.0 && result < 1.0);
            return result;
        }

        protected override string GetSpecificSeed() {
            var bytes = _bytesAvailable ? 1 : 0;
            return $"{_x}:{_y}:{bytes}";
        }

        protected override void SetSpecificSeed(string seed) {
            Contract.Assert(seed != null);
            var parts = seed.Split(':');

            Contract.Assert(parts.Length == 3);

            _x = ulong.Parse(parts[0]);
            _y = ulong.Parse(parts[1]);
            _bytesAvailable = parts[2] == "1";

            Contract.Assert(_x > 0);
            Contract.Assert(_y > 0);
        }
    }
}