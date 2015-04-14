using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManipulation.DataManipulation;
using KnowledgeRepresentation;
using NUnit.Framework;

namespace DataManipulationTests
{
    [TestFixture]
    public class NeuralNetworkTests
    {
        [Test]
        public void NeuralNetworkTestForBallBomb()
        {
            var result = _neural.GetDisarmingProcedure(1);
            var tuple = Tuple.Create(Disarming.CutControlWire, Disarming.CutYellowWire, Disarming.CutRedWire);
            Assert.AreEqual(tuple, result);
        }

        [Test]
        public void NeuralNetworkTestForDemolitionBomb()
        {
            var result = _neural.GetDisarmingProcedure(2);
            var tuple = Tuple.Create(Disarming.CutBlueWire, Disarming.CutGreenWire, Disarming.CutRedWire);
            Assert.AreEqual(tuple, result);
        }
        [Test]
        public void NeuralNetworkTestForDemolitionExplosiveBomb()
        {
            var result = _neural.GetDisarmingProcedure(3);
            var tuple = Tuple.Create(Disarming.CutGreenWire, Disarming.CutControlWire, Disarming.CutYellowWire);
            Assert.AreEqual(tuple, result);
        }
        [Test]
        public void NeuralNetworkTestForExplosiveBomb()
        {
            var result = _neural.GetDisarmingProcedure(4);
            var tuple = Tuple.Create(Disarming.CutRedWire, Disarming.CutGreenWire, Disarming.CutBlueWire);
            Assert.AreEqual(tuple, result);
        }
        [Test]
        public void NeuralNetworkTestForMine()
        {
            var result = _neural.GetDisarmingProcedure(5);
            var tuple = Tuple.Create(Disarming.CutControlWire, Disarming.CutBlueWire, Disarming.CutGreenWire);
            Assert.AreEqual(tuple, result);
        }

        readonly NeuralNetwork _neural = new NeuralNetwork();
    }
}
