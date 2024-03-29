﻿using System;
using Metron.Core.Interfaces;
using Metron.Core.Services;
using MetronWPF.Models;
using NUnit.Framework;

namespace Metron.UnitTests
{

    [TestFixture]
    class TempoDescriptionTest
    {
        ITempoDescriptionService tempoDescrition;

        [SetUp]
        public void Init()
        {
            tempoDescrition = new TempoDescriptionServiceService(new WpfPlatformSpecificXmlDoc());
        }

        [Test]
        public void Tempo_205_Should_return_prestissimo_TempoDescription()
        {
           Assert.AreEqual(tempoDescrition.GetTempoDescription(205), "prestissimo | ");
        }

        [Test]
        public void Tempo_69_Should_return_andantino_TempoDescription()
        {
            Assert.AreEqual(tempoDescrition.GetTempoDescription(86), "andantino | moderato assai | moderato | con moto | ");
        }

        [Test]
        public void Tempo_minus10_Should_return_String_Empty_TempoDescription()
        {
            Assert.AreEqual(tempoDescrition.GetTempoDescription(-10), String.Empty);
        }

        [Test]
        public void Tempo_1000_Should_return_String_Empty_TempoDescription()
        {
            Assert.AreEqual(tempoDescrition.GetTempoDescription(1000), String.Empty);
        }


    }
}
