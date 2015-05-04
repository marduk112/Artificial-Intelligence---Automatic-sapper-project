using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManipulation.DataManipulation;
using KnowledgeRepresentation;
using KnowledgeRepresentation.Fabrics;
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
            var bomb = BombFabric.CreateBomb(BombTypes.Ball);
            var tuple = Tuple.Create(bomb.FirstStageDisarming, bomb.SecondStageDisarming, bomb.ThirdStageDisarming);
            Assert.AreEqual(tuple, result);
        }

        [Test]
        public void NeuralNetworkTestForDemolitionBomb()
        {
            var result = _neural.GetDisarmingProcedure(2);
            var bomb = BombFabric.CreateBomb(BombTypes.Demolition);
            var tuple = Tuple.Create(bomb.FirstStageDisarming, bomb.SecondStageDisarming, bomb.ThirdStageDisarming);
            Assert.AreEqual(tuple, result);
        }
        [Test]
        public void NeuralNetworkTestForDemolitionExplosiveBomb()
        {
            var result = _neural.GetDisarmingProcedure(3);
            var bomb = BombFabric.CreateBomb(BombTypes.DemolitionExplosive);
            var tuple = Tuple.Create(bomb.FirstStageDisarming, bomb.SecondStageDisarming, bomb.ThirdStageDisarming);
            Assert.AreEqual(tuple, result);
        }
        [Test]
        public void NeuralNetworkTestForExplosiveBomb()
        {
            var result = _neural.GetDisarmingProcedure(4);
            var bomb = BombFabric.CreateBomb(BombTypes.Explosive);
            var tuple = Tuple.Create(bomb.FirstStageDisarming, bomb.SecondStageDisarming, bomb.ThirdStageDisarming);
            Assert.AreEqual(tuple, result);
        }
        [Test]
        public void NeuralNetworkTestForMine()
        {
            var result = _neural.GetDisarmingProcedure(5);
            var bomb = BombFabric.CreateBomb(BombTypes.Mine);
            var tuple = Tuple.Create(bomb.FirstStageDisarming, bomb.SecondStageDisarming, bomb.ThirdStageDisarming);
            Assert.AreEqual(tuple, result);
        }

        readonly NeuralNetwork _neural = new NeuralNetwork();
    }
}
