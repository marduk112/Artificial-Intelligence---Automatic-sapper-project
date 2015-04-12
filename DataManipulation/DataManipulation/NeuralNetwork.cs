using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManipulation.Interfaces;
using DataManipulation.Structs;
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
            throw new NotImplementedException();
            int beep;
            _bombTypeses.Zip(_bombTypeses, (p, q) => p.BeepsLevel - q.BeepsLevel).Min(b => beep = b);

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
