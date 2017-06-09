using System;
using System.Collections;
using NUnit.Framework;
using Retromono.Randomness;

namespace Tests {
    [TestFixture]
    public class AbstractRandomEngine_BooleanTests {
        [Test, TestCaseSource(typeof(AbstractRandomEngine_BooleanTests_Data), nameof(AbstractRandomEngine_BooleanTests_Data.TestCases))]
        public bool Boolean_ShouldReturnTrueIfFractionRollsUnderChance(double seed, double fractionChance) {
            return MockRandom.Seed(seed).Boolean(fractionChance);
        }
    }
}

public class AbstractRandomEngine_BooleanTests_Data {
    public static IEnumerable TestCases {
        get {
            yield return new TestCaseData(0.0, 0.5).Returns(true);
            yield return new TestCaseData(0.9, 0.5).Returns(false);
            yield return new TestCaseData(0.0, 0.0).Returns(false);
            yield return new TestCaseData(0.9, 0.0).Returns(false);
            yield return new TestCaseData(0.0, 1.0).Returns(true);
            yield return new TestCaseData(0.9, 1.0).Returns(true);
        }
    }
}