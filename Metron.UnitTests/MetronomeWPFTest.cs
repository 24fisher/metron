using Metron.Core.Interfaces;
using Metron.Core.Models;
using MetronWPF.Models;
using NUnit.Framework;

namespace Metron.UnitTests
{


    [TestFixture]
    class MetronomeWPFTest
    {
        private MetronomeModel _metronModel;

        [SetUp]
        public void Init()
        {
            _metronModel = new MetronomeModel();

            IMetronomeBuilder builder = new MetronomeBuilder(_metronModel);
            IMetronomeDirector director = new WPFMetronomeDirector(builder);

            director.ConstructDefaultMetronomeModel();


            
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

            Assert.AreEqual(_metronModel.Tempo, 205);
   
        }
    }
}
