
using NUnit.Framework;


namespace Metron
{
    [TestFixture]
    class MetronomePatternTest
    {
        Pattern pattern;
        
        [Test]
        public void PatternBasicTest()
        {
            pattern = new Pattern(0, "1000");
            
            Assert.AreEqual(pattern.PatternString, "1000");
        }
        [Test] public void PaternIndxerTest()
        {
            pattern = new Pattern(0, "1000");

            Assert.AreEqual(pattern.PatternString, "1000");
        }
        [Test]
        public void PatternEmptyStringTest()
        {
            pattern = new Pattern(999, "");

            Assert.AreEqual(pattern.PatternString, "1");
            Assert.AreEqual(pattern.CurrentTickIndex, 0);
        }
        [Test]
        public void PatternPreMaxValuesTest()
        {
            pattern = new Pattern(18, "1010101010101010101");

            Assert.AreEqual(pattern.CurrentTickIndex, 0);

            Assert.AreEqual(pattern.PatternString.Length, 19);
        }
        [Test]
        public void PatternMaxValuesTest()
        {
            pattern = new Pattern(19, "10101010101010101010");

            Assert.AreEqual(pattern.CurrentTickIndex, 0);

            Assert.AreEqual(pattern.PatternString.Length, 20);
        }
        
        [Test]
        public void PatternOverMaxValuesTest()
        {
            pattern = new Pattern(30, "10101010101010101010101010101010101010101010101010101");

            Assert.AreEqual(pattern.CurrentTickIndex, 0);
            Assert.AreEqual(pattern.PatternString, "10101010101010101010");
            Assert.AreEqual(pattern.PatternString.Length, 20);
        }

        [Test]
        public void PatternNextTickTest()
        {
            pattern = new Pattern(0, "10101001100");

            pattern.NextTick();
            pattern.NextTick();
            pattern.NextTick();
            pattern.NextTick();
            pattern.NextTick();
            pattern.NextTick();
            pattern.NextTick();

            Assert.AreEqual(pattern.CurrentTickIndex, 7);
            Assert.AreEqual(pattern.CurrentTick, '1');
        }

        [Test]
        public void PatternMeasureTest()
        {
            pattern = new Pattern(0, "1000");

            Assert.AreEqual(pattern.Measure, "4/4");
        }

        [Test]
        public void PatternLargeMeasureTest()
        {
            pattern = new Pattern(0, "100010001001");

            Assert.AreEqual(pattern.Measure, "12/4");
        }
        [Test]
        public void PatternEmptyMeasureTest()
        {
            pattern = new Pattern(0, "");

            Assert.AreEqual(pattern.Measure, "1/4");
        }
    }



}

