using System;
using System.Collections.Generic;
using NUnit.Framework;
using Retromono.Randomness;

namespace Tests {
    [TestFixture]
    public class CommonRandomTests {
        public const int FractionChecksCount = 100000;
        public const int ResetSeedChecksCount = 10000;
        public const int SpecificSeedTests = 20;
        public const int SpecificSeedLookaheads = 1000;
        public const int UniversalSeedRepeats = 20;
        public const int UniversalSeedLookaheads = 1000;

        public static readonly ulong[] UniversalSeedValues = new ulong[] {ulong.MinValue, ulong.MaxValue, ulong.MaxValue / 2, ulong.MaxValue / 3, ulong.MaxValue / 4, ulong.MaxValue / 5};

        public static Type[] RandomEngines = {
            typeof(NativeRandom),
            typeof(XorShift128Random)
        };

        [Test]
        public void FractionShouldBeInRange(
            [ValueSource(nameof(RandomEngines))] Type engineType
        ) {
            var random = Instantiate(engineType);

            for (var i = 0; i < FractionChecksCount; i++) {
                IsValidFraction(random.Fraction());
            }
        }

        [Test]
        public void ResetSeedShouldNotCrash(
            [ValueSource(nameof(RandomEngines))] Type engineType
        ) {
            var random = Instantiate(engineType);

            for (var i = 0; i < ResetSeedChecksCount; i++) {
                random.RandomizeSeed();

                IsValidFraction(random.Fraction());
            }
        }

        [Test]
        public void SettingUniversalSeedShouldNotCrash(
            [ValueSource(nameof(RandomEngines))] Type engineType,
            [ValueSource(nameof(UniversalSeedValues))] ulong universalSeed
        ) {
            var random = (IRandomEngine) Activator.CreateInstance(engineType);

            random.SetUniversalSeed(universalSeed);
            IsValidFraction(random.Fraction());
        }

        [Test]
        public void SpecificSeedWorksAndIsRepeatable(
            [ValueSource(nameof(RandomEngines))] Type engineType
        ) {
            var random = Instantiate(engineType);

            if (!random.SupportsSpecificSeed) {
                Assert.True(true);
                return;
            }

            for (var i = 0; i < SpecificSeedTests; i++) {
                random.RandomizeSeed();
                var seed = random.SpecificSeed;

                var results = GetRolls(random, SpecificSeedLookaheads);
                
                var checkedRandom = Instantiate(engineType);
                checkedRandom.SpecificSeed = seed;

                var checkedResults = GetRolls(checkedRandom, SpecificSeedLookaheads);
                CollectionAssert.AreEqual(results, checkedResults);
            }
        }

        [Test]
        public void SettingUniversalSeedBringsYouToTheSameState(
            [ValueSource(nameof(RandomEngines))] Type engineType,
            [ValueSource(nameof(UniversalSeedValues))] ulong universalSeed,
            [Values(true, false)] bool recreateInstanceEachStep
        ) {
            var random = Instantiate(engineType);
            random.SetUniversalSeed(universalSeed);

            var results = GetRolls(random, UniversalSeedLookaheads);
            var checkedRandom = Instantiate(engineType);
            for (var i = 0; i < UniversalSeedRepeats; i++) {
                if (recreateInstanceEachStep)
                    checkedRandom = Instantiate(engineType);

                checkedRandom.SetUniversalSeed(universalSeed);

                CollectionAssert.AreEqual(results, GetRolls(checkedRandom, UniversalSeedLookaheads));
            }
        }

        private IRandomEngine Instantiate(Type engineType) {
            var random = (IRandomEngine) Activator.CreateInstance(engineType);
            Assert.NotNull(random, "Random engine failed to instantiate");

            return random;
        }

        private void IsValidFraction(double fraction) {
            Assert.GreaterOrEqual(fraction, 0, "Test fraction is smaller than 0");
            Assert.Less(fraction, 1, "Test fraction is larger or equal to 1");
        }

        private List<double> GetRolls(IRandomEngine random, int count) {
            var list = new List<double>(count);
            for (var j = 0; j < UniversalSeedLookaheads; j++) {
                list.Add(random.Fraction());
            }

            return list;
        }
    }
}