using System;
using NUnit.Framework;
using Retromono.Randomness;

namespace Tests {
    [TestFixture]
    public class AbstractRandomEngineTests {
        public static double[] TestFractions = {0.0, 0.1, 0.2, 0.3, 0.4, 0.499999, 0.5, 0.500001, 0.6, 0.7, 0.8, 0.9, 1.0 - 0.000000000000001};

        public static Type[] RandomClasses = {
            typeof(NativeRandom)
        };

        [Test]
        public void TestFractionsSanityCheck([ValueSource(nameof(TestFractions))] double fraction) {
            Assert.GreaterOrEqual(fraction, 0, "Test fraction is smaller than 0");
            Assert.Less(fraction, 1, "Test fraction is larger or equal to 1");
        }

        [Test]
        public void Int_Reasonable_ShouldBeInRange([ValueSource(nameof(TestFractions))] double fraction) {
            var value = MockRandom.Seed(fraction).Int(-100, 100);

            Assert.GreaterOrEqual(value, -100);
            Assert.Less(value, 100);
        }

        [Test]
        public void Int_Crazy_ShouldBeInRange([ValueSource(nameof(TestFractions))] double fraction) {
            var value = MockRandom.Seed(fraction).Int(int.MinValue / 2, int.MaxValue / 2);

            Assert.GreaterOrEqual(value, int.MinValue / 2);
            Assert.Less(value, int.MaxValue / 2);
        }

        [Test]
        public void Uint_Reasonable_ShouldBeInRange([ValueSource(nameof(TestFractions))] double fraction) {
            var value = MockRandom.Seed(fraction).Uint(0, 100);

            Assert.GreaterOrEqual(value, -100);
            Assert.Less(value, 100);
        }

        [Test]
        public void Uint_Crazy_ShouldBeInRange([ValueSource(nameof(TestFractions))] double fraction) {
            var value = MockRandom.Seed(fraction).Uint(uint.MinValue / 2, uint.MaxValue / 2);

            Assert.GreaterOrEqual(value, uint.MinValue / 2);
            Assert.Less(value, uint.MaxValue / 2);
        }

        [Test]
        public void Float_Reasonable_ShouldBeInRange([ValueSource(nameof(TestFractions))] double fraction) {
            var value = MockRandom.Seed(fraction).Float(-100, 100);

            Assert.GreaterOrEqual(value, -100);
            Assert.Less(value, 100);
        }

        [Test]
        public void Float_Crazy_ShouldBeInRange([ValueSource(nameof(TestFractions))] double fraction) {
            var value = MockRandom.Seed(fraction).Float(float.MinValue / 2, float.MaxValue / 2);

            Assert.GreaterOrEqual(value, float.MinValue / 2);
            Assert.Less(value, float.MaxValue / 2);
        }

        [Test]
        public void Double_Reasonable_ShouldBeInRange([ValueSource(nameof(TestFractions))] double fraction) {
            var value = MockRandom.Seed(fraction).Float(-100, 100);

            Assert.GreaterOrEqual(value, -100);
            Assert.Less(value, 100);
        }

        [Test]
        public void Double_Crazy_ShouldBeInRange([ValueSource(nameof(TestFractions))] double fraction) {
            var value = MockRandom.Seed(fraction).Double(double.MinValue / 2, double.MaxValue / 2);

            Assert.GreaterOrEqual(value, double.MinValue / 2);
            Assert.Less(value, double.MaxValue / 2);
        }

        [Test]
        public void Boolean_ChecksIfInputIsGreaterOrEqual(
            [ValueSource(nameof(TestFractions))] double fraction,
            [Values(0, 0.05, 0.1, 0.15, 0.5, 1)] double checks
        ) {
            var value = MockRandom.Seed(fraction).Boolean(checks);

            Assert.AreEqual(value, fraction >= checks);
        }

        [Test]
        public void Invertion_ChecksIfInputIsGreaterOrEqual(
            [ValueSource(nameof(TestFractions))] double fraction,
            [Values(0, 0.05, 0.1, 0.15, 0.5, 1)] double checks
        ) {
            var value = MockRandom.Seed(fraction).Invertion(checks);

            Assert.AreEqual(value, fraction >= checks ? -1 : 1);
        }
    }
}