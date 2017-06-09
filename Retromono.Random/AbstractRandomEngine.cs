using System;
using System.Diagnostics.Contracts;

namespace Retromono.Randomness {
    public abstract class AbstractRandomEngine : IRandomEngine {
        private static readonly Random _resetSeedSource = new Random();

        public bool SupportsSpecificSeed { get; protected set; }

        public abstract void SetUniversalSeed(ulong seed);
        public abstract double Fraction();
        protected abstract string GetSpecificSeed();
        protected abstract void SetSpecificSeed(string seed);

        public void RandomizeSeed() {
            SetUniversalSeed((ulong) (_resetSeedSource.NextDouble() * ulong.MaxValue));
        }

        public int Int(int from, int to) {
            Contract.Requires(from >= int.MinValue / 2);
            Contract.Requires(from <= int.MaxValue / 2);
            Contract.Requires(to >= int.MinValue / 2);
            Contract.Requires(to <= int.MaxValue / 2);

            if (from > to) {
                var temp = from;
                from = to;
                to = temp;
            }

            long fromLong = from;
            long toLong = to;

            return (int) (fromLong + Math.Floor((toLong - fromLong) * Fraction()));
        }

        public uint Uint(uint from, uint to) {
            Contract.Requires(from <= uint.MaxValue / 2);
            Contract.Requires(to <= uint.MaxValue / 2);

            if (from > to) {
                var temp = from;
                from = to;
                to = temp;
            }

            ulong fromLong = from;
            ulong toLong = to;

            return (uint) (fromLong + Math.Floor((toLong - fromLong) * Fraction()));
        }

        public float Float(float from, float to) {
            Contract.Requires(from >= float.MinValue / 2);
            Contract.Requires(from <= float.MaxValue / 2);
            Contract.Requires(to >= float.MinValue / 2);
            Contract.Requires(to <= float.MaxValue / 2);

            if (from > to) {
                var temp = from;
                from = to;
                to = temp;
            }

            var delta = to - from;

            return from + (float) (delta * Fraction()) % delta;
        }

        public double Double(double from, double to) {
            Contract.Requires(from >= double.MinValue / 2);
            Contract.Requires(from <= double.MaxValue / 2);
            Contract.Requires(to >= double.MinValue / 2);
            Contract.Requires(to <= double.MaxValue / 2);

            if (from > to) {
                var temp = from;
                from = to;
                to = temp;
            }

            var delta = to - from;

            return from + delta * Fraction() % delta;
        }

        public bool Boolean(double trueChance = 0.5) {
            return Fraction() < trueChance;
        }

        public int Invertion(double invertChance = 0.5) {
            return Fraction() < invertChance ? -1 : 1;
        }

        public string SpecificSeed {
            get { return GetSpecificSeed(); }
            set { SetSpecificSeed(value); }
        }

        protected double UlongToFraction(ulong value) {
            return (double) value / ulong.MaxValue;
        }

        protected double UlongToBidirectionalFraction(ulong value) {
            return UlongToFraction(value) * 2 - 1;
        }

        protected int UlongToIntFullSpread(ulong value) {
            return (int) (int.MaxValue * UlongToBidirectionalFraction(value));
        }
    }
}