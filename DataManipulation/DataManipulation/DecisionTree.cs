using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using numl;
using KnowledgeRepresentation;
using numl.Model;
using numl.Supervised.DecisionTree;
using DataManipulation.Interfaces;

namespace DataManipulation.DataManipulation
{

    public class BombDecisionTree
    {
        [Feature]
        public BombTypes bomb { get; set; }
        [Feature]
        public Disarming FirstStageDisarming { get; set; }
        [Feature]
        public Disarming SecondStageDisarming { get; set; }
        [Feature]
        public Disarming ThirdStageDisarming { get; set; }
        [Label]
        public bool Successfull { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Successfull);
        }

        public static BombDecisionTree[] GetData()
        {
            return new BombDecisionTree[]  {
            new BombDecisionTree { Successfull = true, bomb=BombTypes.Ball, FirstStageDisarming = Disarming.CutControlWire, 
                SecondStageDisarming = Disarming.CutYellowWire, ThirdStageDisarming = Disarming.CutRedWire},
            new BombDecisionTree { Successfull = true, bomb=BombTypes.Demolition, FirstStageDisarming = Disarming.CutBlueWire, 
                SecondStageDisarming = Disarming.CutGreenWire, ThirdStageDisarming = Disarming.CutRedWire},
            new BombDecisionTree { Successfull = true, bomb=BombTypes.DemolitionExplosive, FirstStageDisarming = Disarming.CutGreenWire, 
                SecondStageDisarming = Disarming.CutControlWire, ThirdStageDisarming = Disarming.CutYellowWire},
            new BombDecisionTree { Successfull = true, bomb=BombTypes.Explosive, FirstStageDisarming = Disarming.CutRedWire, 
                SecondStageDisarming = Disarming.CutGreenWire,ThirdStageDisarming = Disarming.CutBlueWire},
            new BombDecisionTree { Successfull = true, bomb=BombTypes.Mine, FirstStageDisarming = Disarming.CutControlWire, 
                SecondStageDisarming = Disarming.CutBlueWire, ThirdStageDisarming = Disarming.CutGreenWire},

            new BombDecisionTree { Successfull = false, bomb=BombTypes.Explosive, FirstStageDisarming = Disarming.CutControlWire, 
                SecondStageDisarming = Disarming.CutYellowWire, ThirdStageDisarming = Disarming.CutRedWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.DemolitionExplosive, FirstStageDisarming = Disarming.CutBlueWire, 
                SecondStageDisarming = Disarming.CutGreenWire, ThirdStageDisarming = Disarming.CutRedWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.Demolition, FirstStageDisarming = Disarming.CutGreenWire, 
                SecondStageDisarming = Disarming.CutControlWire, ThirdStageDisarming = Disarming.CutYellowWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.Mine, FirstStageDisarming = Disarming.CutRedWire, 
                SecondStageDisarming = Disarming.CutGreenWire,ThirdStageDisarming = Disarming.CutBlueWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.Ball, FirstStageDisarming = Disarming.CutControlWire, 
                SecondStageDisarming = Disarming.CutBlueWire, ThirdStageDisarming = Disarming.CutGreenWire},

            new BombDecisionTree { Successfull = false, bomb=BombTypes.Demolition, FirstStageDisarming = Disarming.CutControlWire, 
                SecondStageDisarming = Disarming.CutYellowWire, ThirdStageDisarming = Disarming.CutRedWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.Ball, FirstStageDisarming = Disarming.CutBlueWire, 
                SecondStageDisarming = Disarming.CutGreenWire, ThirdStageDisarming = Disarming.CutRedWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.Mine, FirstStageDisarming = Disarming.CutGreenWire, 
                SecondStageDisarming = Disarming.CutControlWire, ThirdStageDisarming = Disarming.CutYellowWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.DemolitionExplosive, FirstStageDisarming = Disarming.CutRedWire, 
                SecondStageDisarming = Disarming.CutGreenWire,ThirdStageDisarming = Disarming.CutBlueWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.Explosive, FirstStageDisarming = Disarming.CutControlWire, 
                SecondStageDisarming = Disarming.CutBlueWire, ThirdStageDisarming = Disarming.CutGreenWire},

            new BombDecisionTree { Successfull = false, bomb=BombTypes.Mine, FirstStageDisarming = Disarming.CutControlWire, 
                SecondStageDisarming = Disarming.CutYellowWire, ThirdStageDisarming = Disarming.CutRedWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.Explosive, FirstStageDisarming = Disarming.CutBlueWire, 
                SecondStageDisarming = Disarming.CutGreenWire, ThirdStageDisarming = Disarming.CutRedWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.Ball, FirstStageDisarming = Disarming.CutGreenWire, 
                SecondStageDisarming = Disarming.CutControlWire, ThirdStageDisarming = Disarming.CutYellowWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.Demolition, FirstStageDisarming = Disarming.CutRedWire, 
                SecondStageDisarming = Disarming.CutGreenWire,ThirdStageDisarming = Disarming.CutBlueWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.DemolitionExplosive, FirstStageDisarming = Disarming.CutControlWire, 
                SecondStageDisarming = Disarming.CutBlueWire, ThirdStageDisarming = Disarming.CutGreenWire},

            new BombDecisionTree { Successfull = false, bomb=BombTypes.DemolitionExplosive, FirstStageDisarming = Disarming.CutControlWire, 
                SecondStageDisarming = Disarming.CutYellowWire, ThirdStageDisarming = Disarming.CutRedWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.Mine, FirstStageDisarming = Disarming.CutBlueWire, 
                SecondStageDisarming = Disarming.CutGreenWire, ThirdStageDisarming = Disarming.CutRedWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.Explosive, FirstStageDisarming = Disarming.CutGreenWire, 
                SecondStageDisarming = Disarming.CutControlWire, ThirdStageDisarming = Disarming.CutYellowWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.Ball, FirstStageDisarming = Disarming.CutRedWire, 
                SecondStageDisarming = Disarming.CutGreenWire,ThirdStageDisarming = Disarming.CutBlueWire},
            new BombDecisionTree { Successfull = false, bomb=BombTypes.Demolition, FirstStageDisarming = Disarming.CutControlWire, 
                SecondStageDisarming = Disarming.CutBlueWire, ThirdStageDisarming = Disarming.CutGreenWire},

            };
        }

    }
    public class DecisionTree : IDataManipulation
    { 
        private BombTypes _typeBomb;
        private readonly Task _learnTreeTask;
        private LearningModel _model;

        public DecisionTree()
        {
            _learnTreeTask = GenericTree();
        }
        async Task GenericTree()
        {
            var data = BombDecisionTree.GetData();
            var d = Descriptor.Create<BombDecisionTree>();
            var g = new DecisionTreeGenerator(d);
            g.SetHint(false);
            _model = Learner.Learn(data, 0.80, 1000, g);
        }

        public async Task<Tuple<Disarming, Disarming, Disarming>> GetDisarmingProcedure(int beepsLevel)
        {
            await _learnTreeTask;
            var result = new Disarming[3];
            switch (beepsLevel)
            {
                case 1:
                    _typeBomb = BombTypes.Ball;
                    break;
                case 2:
                    _typeBomb = BombTypes.Demolition;
                    break;
                case 3:
                    _typeBomb = BombTypes.DemolitionExplosive;
                    break;
                case 4:
                    _typeBomb = BombTypes.Explosive;
                    break;
                case 5:
                    _typeBomb = BombTypes.Mine;
                    break;
            }

            var _result = _model.Model.Predict(new BombDecisionTree
            {
                bomb = _typeBomb
            });

            return Tuple.Create(result[0], result[1], result[2]);
        }

        private BombDecisionTree DisarmingDecisionTree(LearningModel model, string idBomb, Disarming step1, Disarming step2, Disarming step3)
        {

            switch (int.Parse(idBomb))
            {
                case 1:
                    _typeBomb = BombTypes.Ball;
                    break;
                case 2:
                    _typeBomb = BombTypes.Demolition;
                    break;
                case 3:
                    _typeBomb = BombTypes.DemolitionExplosive;
                    break;
                case 4:
                    _typeBomb = BombTypes.Explosive;
                    break;
                case 5:
                    _typeBomb = BombTypes.Mine;
                    break;
            }

            var result = model.Model.Predict(new BombDecisionTree
            {
                bomb = _typeBomb,
                FirstStageDisarming = step1,
                SecondStageDisarming = step2,
                ThirdStageDisarming = step3
            });

            return result;     
        }
    }
}

//private readonly DecisionTree _modelTree = new DecisionTree();

//{
//    var resultModelTree = _modelTree.GenericTree();
//    var bombFound = _bombTypeses.SingleOrDefault(b => b.BeepsLevel == int.Parse(bomb.BombId));
//    var bombDisarmingProcedureDecisionTree = DecisionTree.DisarmingDecisionTree(resultModelTree, bomb.BombId ,bombFound.FirstStageDisarming,
//        bombFound.SecondStageDisarming, bombFound.ThirdStageDisarming);
//    bomb.BombId = resultModelTree.Equals(bombDisarmingProcedureDecisionTree) ? "^" : "&";
//}