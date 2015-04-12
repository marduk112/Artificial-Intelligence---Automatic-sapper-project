using System;
using System.Collections.Generic;
using System.Linq;
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
            LearnNetwork();
        }

        public Tuple<Disarming, Disarming, Disarming> GetDisarmingProcedure(int beepsLevel)
        {
            var procedure = _network.Run(new double[] {beepsLevel});
            return Tuple.Create((Disarming)((int)Math.Round(procedure[0], 0)), (Disarming)((int)Math.Round(procedure[1], 0)), (Disarming)((int)Math.Round(procedure[2], 0)));
        }

        private void LearnNetwork()
        {
            _network = new BackpropagationNetwork(_inputLayer, _outputLayer);
            _network.Initialize();
            var trainingSet = new TrainingSet(1, 3);
            foreach (var bomb in _bombTypeses)
            {
                trainingSet.Add(new TrainingSample(new double[] { bomb.BeepsLevel },
                                                    new double[]{
                                                        (int)bomb.FirstStageDisarming, 
                                                        (int)bomb.SecondStageDisarming, 
                                                        (int)bomb.ThirdStageDisarming
                                                    }));
            }
            _network.Learn(trainingSet, 1000);
            //_network.StopLearning();
        }

        private readonly SigmoidLayer _inputLayer = new SigmoidLayer(1);
        private readonly SigmoidLayer _hiddenLayer = new SigmoidLayer(6);
        private readonly LinearLayer _outputLayer = new LinearLayer(3);
        private readonly BackpropagationConnector _inputHiddenBackpropagationConnector;
        private readonly BackpropagationConnector _hiddenOutputBackpropagationConnector;
        private BackpropagationNetwork _network;
        private readonly List<IBomb> _bombTypeses = Enum.GetValues(typeof(BombTypes)).Cast<BombTypes>().Select(BombFabric.CreateBomb).Where(tempObject => tempObject != null).ToList();
    }
}
