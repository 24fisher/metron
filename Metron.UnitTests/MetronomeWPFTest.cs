using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MetronWPF;
using System.Threading;

namespace Metron.UnitTests
{


    [TestFixture]
    class MetronomeWPFTest
    {
        private MetronomeModel _metronModel;

        [SetUp]
        public void Init()
        {
            _metronModel = new MetronomeModel(new TimerWin32Adapted(), new WPFConsoleBeep(), new ColorWPF(), 0, 250);
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
            
            _metronModel.Tempo = 205;
            //_metronModel.StartTimer();

            //Thread.Sleep(500);
            Assert.AreEqual(_metronModel.Tempo, 205);
            Assert.AreEqual(_metronModel.Measure, "4/4");

            //Assert.AreEqual(_metronModel.TempoDescription, "prestissimo |");
        }
    }
}
