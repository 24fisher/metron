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
            WpfAppBuilder wpfAppBuilder = new WpfAppBuilder()
            {
                ColorImplementor = new ColorWPF(),
                SoundImplementor = new WPFAudioFileBeep(),
                TimerImplementor = new TimerWin32Adapted(),
                XmlDocImplementor = new WpfPlatformSpecificXmlDoc(),
                metronomeHighLimit = 300,
                metronomeLowLimit = 10

            };
            

            _metronModel = new MetronomeModel(wpfAppBuilder);
        }
        [Test]
        public void ModelStartStopTest()
        {
            Assert.AreEqual(_metronModel.IsRunning, false);

            _metronModel.Run();


            Assert.AreEqual(_metronModel.IsRunning, true);

            _metronModel.Stop();
            Assert.AreEqual(_metronModel.IsRunning, false);
        }
        [Test]
        public void ModelTest()
        {
            
            _metronModel.Tempo = 205;
            //_metronModel.Run();

            //Thread.Sleep(500);
            Assert.AreEqual(_metronModel.Tempo, 205);
   
        }
    }
}
