using System;
using System.Collections;
using NUnit.Framework;
using Retromono.Randomness;

namespace Tests {
    [TestFixture]
    public class AbstractRandomEngine_InvertionTests {
        [Test, TestCaseSource(typeof(AbstractRandomEngine_InvertionTests_Data), nameof(AbstractRandomEngine_InvertionTests_Data.TestCases))]
        public int Invertion_ShouldReturnTrueIfFractionRollsUnderChance(double seed, double fractionChance) {
            return MockRandom.Seed(seed).Invertion(fractionChance);
        }
    }
}

public class AbstractRandomEngine_InvertionTests_Data {
    public static IEnumerable TestCases {
        get {
            yield return new TestCaseData(0.0, 0.5).Returns(-1);
            yield return new TestCaseData(0.9, 0.5).Returns(1);
            yield return new TestCaseData(0.0, 0.0).Returns(1);
            yield return new TestCaseData(0.9, 0.0).Returns(1);
            yield return new TestCaseData(0.0, 1.0).Returns(-1);
            yield return new TestCaseData(0.9, 1.0).Returns(-1);
        }
    }
}