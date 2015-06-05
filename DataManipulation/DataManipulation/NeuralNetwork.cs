using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DataManipulation.Interfaces;
using KnowledgeRepresentation;
using KnowledgeRepresentation.Fabrics;
using KnowledgeRepresentation.Interfaces;
using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;

namespace DataManipulation.DataManipulation
{
    public class NeuralNetwork : IDataManipulation
    {
        public NeuralNetwork()
        {
            _inputHiddenBackpropagationConnector = new BackpropagationConnector(_inputLayer, _hiddenLayer);
            _hiddenOutputBackpropagationConnector = new BackpropagationConnector(_hiddenLayer, _outputLayer);
            _learnNetworkTask = LearnNetworkAsync();
        }

        public async Task<Tuple<Disarming, Disarming, Disarming>> GetDisarmingProcedure(int beepsLevel)
        {
            await _learnNetworkTask;
            _network.StopLearning();
            var procedure = _network.Run(new double[] {beepsLevel});
            var result = new Disarming[3];
            var mins = Enumerable.Repeat(100.0, 3).ToArray();
            foreach (var bomb in _bombTypeses)
            {
                var abs1 = Math.Abs((int)bomb.FirstStageDisarming - procedure[0]);
                var abs2 = Math.Abs((int)bomb.SecondStageDisarming - procedure[1]);
                var abs3 = Math.Abs((int)bomb.ThirdStageDisarming - procedure[2]);
                if (mins[0] > abs1)
                {
                    mins[0] = abs1;
                    result[0] = bomb.FirstStageDisarming;
                }
                if (mins[1] > abs2)
                {
                    mins[1] = abs2;
                    result[1] = bomb.SecondStageDisarming;
                }
                if (mins[2] > abs3)
                {
                    mins[2] = abs3;
                    result[2] = bomb.ThirdStageDisarming;
                }
            }
            return Tuple.Create(result[0], result[1], result[2]);
        }

        async Task LearnNetworkAsync()
        {
            _network = new BackpropagationNetwork(_inputLayer, _outputLayer);
            _network.Initialize();
            var trainingSet = new TrainingSet(1, 3);
            foreach (var b in from bomb in Enum.GetValues(typeof(BombTypes)).Cast<BombTypes>() where bomb != BombTypes.Mine select BombFabric.CreateBomb(bomb) into b where b != null select b)
            {
                trainingSet.Add(new TrainingSample(new double[] { b.BeepsLevel },
                    new double[]{
                        (int)b.FirstStageDisarming, 
                        (int)b.SecondStageDisarming, 
                        (int)b.ThirdStageDisarming
                    }));
            }
            _network.Learn(trainingSet, 100000);
        }

        private readonly Task _learnNetworkTask;
        private readonly LinearLayer _inputLayer = new LinearLayer(1);
        private readonly SigmoidLayer _hiddenLayer = new SigmoidLayer(30);
        private readonly LogarithmLayer _outputLayer = new LogarithmLayer(3);
        private readonly BackpropagationConnector _inputHiddenBackpropagationConnector;
        private readonly BackpropagationConnector _hiddenOutputBackpropagationConnector;
        private BackpropagationNetwork _network;
        private readonly List<IBomb> _bombTypeses = Enum.GetValues(typeof(BombTypes)).Cast<BombTypes>().Select(BombFabric.CreateBomb).Where(tempObject => tempObject != null).ToList();
    }
}
