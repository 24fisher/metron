
using Metron.Core.Models;
using NUnit.Framework;


namespace Metron
{
    [TestFixture]
    class MetronomePatternTest
    {
        BeatPattern _beatPattern;
        
        [Test]
        public void PatternBasicTest()
        {
            _beatPattern = new BeatPattern(0, "1000");
            
            Assert.AreEqual(_beatPattern.PatternString, "1000");
        }

        [Test]
        public void PatternEmptyStringTest()
        {
            _beatPattern = new BeatPattern(999, "");

            Assert.AreEqual(_beatPattern.PatternString, "1");
            Assert.AreEqual(_beatPattern.CurrentTickIndex, 0);
        }
        [Test]
        public void PatternPreMaxValuesTest()
        {
            _beatPattern = new BeatPattern(18, "1010101010101010101");

            Assert.AreEqual(_beatPattern.CurrentTickIndex, 0);

            Assert.AreEqual(_beatPattern.PatternString.Length, 19);
        }
        [Test]
        public void PatternMaxValuesTest()
        {
            _beatPattern = new BeatPattern(19, "10101010101010101010");

            Assert.AreEqual(_beatPattern.CurrentTickIndex, 0);

            Assert.AreEqual(_beatPattern.PatternString.Length, 20);
        }
        
        [Test]
        public void PatternOverMaxValuesTest()
        {
            _beatPattern = new BeatPattern(30, "10101010101010101010101010101010101010101010101010101");

            Assert.AreEqual(_beatPattern.CurrentTickIndex, 0);
            Assert.AreEqual(_beatPattern.PatternString, "10101010101010101010");
            Assert.AreEqual(_beatPattern.PatternString.Length, 20);
        }

        [Test]
        public void PatternNextTickTest()
        {
            _beatPattern = new BeatPattern(0, "10101001100");

            _beatPattern.NextTick();
            _beatPattern.NextTick();
            _beatPattern.NextTick();
            _beatPattern.NextTick();
            _beatPattern.NextTick();
            _beatPattern.NextTick();
            _beatPattern.NextTick();

            Assert.AreEqual(_beatPattern.CurrentTickIndex, 7);
            Assert.AreEqual(_beatPattern.CurrentTick, '1');
        }

        [Test]
        public void PatternMeasureTest()
        {
            _beatPattern = new BeatPattern(0, "1000");

            Assert.AreEqual(_beatPattern.Measure, "4/4");
        }

        [Test]
        public void PatternLargeMeasureTest()
        {
            _beatPattern = new BeatPattern(0, "100010001001");

            Assert.AreEqual(_beatPattern.Measure, "12/4");
        }
        [Test]
        public void PatternEmptyMeasureTest()
        {
            _beatPattern = new BeatPattern(0, "");

            Assert.AreEqual(_beatPattern.Measure, "1/4");
        }
    }



}

