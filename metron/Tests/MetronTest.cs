using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Metron
{
    [TestFixture]
    class MetronTest
    {

        [Test]
        public void PatternBasicTest()
        {
            Pattern pattern = new Pattern(0, "1000");

            Assert.AreEqual(pattern.PatternString, "1000");
        }
        [Test] public void PaternIndxerTest()
        {
            Pattern pattern = new Pattern(0, "1000");

            Assert.AreEqual(pattern.PatternString, "1000");
        }
        [Test]
        public void PatternEmptyStringTest()
        {
            Pattern pattern = new Pattern(999, "");

            Assert.AreEqual(pattern.PatternString, "1");
            Assert.AreEqual(pattern.CurrentTickIndex, 0);
        }
        [Test]
        public void PatternPreMaxValuesTest()
        {
            Pattern pattern = new Pattern(18, "1010101010101010101");

            Assert.AreEqual(pattern.CurrentTickIndex, 0);

            Assert.AreEqual(pattern.PatternString.Length, 19);
        }
        [Test]
        public void PatternMaxValuesTest()
        {
            Pattern pattern = new Pattern(19, "10101010101010101010");

            Assert.AreEqual(pattern.CurrentTickIndex, 0);

            Assert.AreEqual(pattern.PatternString.Length, 20);
        }
        
        [Test]
        public void PatternOverMaxValuesTest()
        {
            Pattern pattern = new Pattern(30, "10101010101010101010101010101010101010101010101010101");

            Assert.AreEqual(pattern.CurrentTickIndex, 0);
            Assert.AreEqual(pattern.PatternString, "10101010101010101010");
            Assert.AreEqual(pattern.PatternString.Length, 20);
        }

        [Test]
        public void PatternNextTickTest()
        {
            Pattern pattern = new Pattern(0, "10101001100");

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
            Pattern pattern = new Pattern(0, "1000");

            Assert.AreEqual(pattern.Measure, "4/4");
        }

        [Test]
        public void PatternLargeMeasureTest()
        {
            Pattern pattern = new Pattern(0, "100010001001");

            Assert.AreEqual(pattern.Measure, "12/4");
        }
        [Test]
        public void PatternEmptyMeasureTest()
        {
            Pattern pattern = new Pattern(0, "");

            Assert.AreEqual(pattern.Measure, "1/4");
        }
    }

    [TestFixture]
    class MetronomeModelTest
    {
        private MetronomeModel _metronModel;

        [SetUp]
        public void Init()
        {
            _metronModel = new MetronomeModel(new ConcreteTimerClr(), new MetronomeConsoleBeep());
        }
        [Test]
        public void ModelStartStopTest()
        {
            Assert.AreEqual(_metronModel.IsRunning, false);

            _metronModel.StartTimer();

            Assert.AreEqual(_metronModel.IsRunning, true);

            _metronModel.StopTimer();
            Assert.AreEqual(_metronModel.IsRunning, false);
        }
        [Test]
        public void ModelTest()
        {
  
            _metronModel.StartTimer();

            _metronModel.Tempo = 250;

            Assert.AreEqual(_metronModel.TempoDescription, "prestissimo");
        }


    }
}
