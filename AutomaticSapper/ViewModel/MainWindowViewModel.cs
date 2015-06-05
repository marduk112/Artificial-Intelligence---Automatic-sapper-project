using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using AutomaticSapper.Annotations;
using AutomaticSapper.Infos;
using DataManipulation.DataManipulation;
using DataManipulation.Interfaces;
using KnowledgeRepresentation;
using KnowledgeRepresentation.Fabrics;
using KnowledgeRepresentation.Interfaces;
using Microsoft.Practices.Prism.Commands;
using MovementAlgorithms.DepthFirstAlgorithm;
using Timer = System.Timers.Timer;

namespace AutomaticSapper.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Bomb> Bombs { get; private set; }
        public ObservableCollection<DisarmingInfo> DisarmingInfos { get; private set; } 
        public SynchronizationContext ViewContext { get; set; }
        public DelegateCommand<string> NeuralNetworkButton { get; set; }
        public DelegateCommand<string> DecisionTreeButton { get; set; }
        public DisarmingInfo SelectedDisarmingInfo { get; set; }

        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }
        public MainWindowViewModel()
        {
            Bombs = new ObservableCollection<Bomb>();
            DisarmingInfos = new ObservableCollection<DisarmingInfo>();
            NeuralNetworkButton = new DelegateCommand<string>(ChooseDataManipulationMethod, _ => !_runmethod.Equals(NeuralNetwork));
            DecisionTreeButton = new DelegateCommand<string>(ChooseDataManipulationMethod, _ => !_runmethod.Equals(DecisionTree));
            Width = 750;
            Height = 550;
            CanvasLeft = -1;
            CanvasTop = 49;
            var aTimer = new Timer(5);
            aTimer.Elapsed += aTimer_Elapsed;
            aTimer.Enabled = true;
            _destinationPoint = new Point(0, 50);
            CreateBoard();
        }

        public int CanvasTop
        {
            get { return _canvasTop; }
            set
            {
                _canvasTop = value;
                OnPropertyChanged();
            }
        }

        public int CanvasLeft
        {
            get { return _canvasLeft; }
            set
            {
                _canvasLeft = value;
                OnPropertyChanged();
            }
        }

        void aTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (_monitor)
            {
                if (ViewContext != null)
                {
                    ViewContext.Post(async _ =>
                    {
                        foreach (var bomb in Bombs.Where(bomb => Math.Abs(CanvasLeft - bomb.BombCanvasLeft) <= 10
                                                                 && Math.Abs(CanvasTop - bomb.BombCanvasTop) <= 10
                                                                 && bomb.BombId != "" && bomb.BombId != "^" && bomb.BombId != "&"))
                        {
                            var result = await _runmethod.GetDisarmingProcedure(int.Parse(bomb.BombId));
                            var bombFound = _bombTypeses.Single(b => b.BeepsLevel == int.Parse(bomb.BombId));
                            var bombDisarmingProcedure = Tuple.Create(bombFound.FirstStageDisarming,
                                bombFound.SecondStageDisarming, bombFound.ThirdStageDisarming);
                            bomb.BombId = result.Equals(bombDisarmingProcedure) ? "^" : "&";
                            var info = new DisarmingInfo
                            {
                                BombType = (BombTypes) (bombFound.BeepsLevel - 1),
                                DisarmedStatus = bomb.BombId,
                                DataManipulationAlgorithm = _disarmedMethod,
                            };
                            DisarmingInfos.Add(info);
                        }

                        CanvasLeft = _x;
                        CanvasTop = _y;
                        if (_x != _destinationPoint.X || _y != _destinationPoint.Y)
                        {
                            if (_x != _destinationPoint.X)
                            {
                                if (_destinationPoint.X > _x)
                                    _x++;
                                else
                                    _x--;
                            }

                            if (_y != _destinationPoint.Y)
                                if (_destinationPoint.Y > _y)
                                    _y++;
                                else
                                    _y--;
                        }
                        if (CanvasLeft == _destinationPoint.X && CanvasTop == _destinationPoint.Y)
                        {
                            _lastX = _destinationPoint.X;
                            //Console.WriteLine("xxxxxxx");
                            if (_treePositions.Count != 0)
                            {
                                _destinationPoint.X = _treePositions[0].x;
                                _destinationPoint.Y = _treePositions[0].y;
                                //Console.WriteLine("new point x: " + treePositions[0].x + " y: " + treePositions[0].y);
                                _treePositions.RemoveAt(0);
                                //Console.WriteLine(destinationPoint.X + " " + destinationPoint.Y);
                            }
                            else
                            {
                                if (_started)
                                {
                                    if (_direction == "right")
                                        _destinationPoint.X += 50;
                                    else
                                        _destinationPoint.X -= 50;
                                }
                                else
                                {
                                    _started = true;
                                }
                                searchBomb(_destinationPoint.X, _destinationPoint.Y);
                            }
                        }
                    }, null);
                }
            }
        }

        private void searchBomb(double x, double y)
        {
            var b = new DepthFirstAlgorithm();
            //Console.WriteLine("actual x: " + CanvasLeft);
            var root = b.BuildVertGraph(x, y, _direction);
            b.Traverse(root);
            _treePositions = b.getPath();
            if (_treePositions[0].x == 650 && _direction == "right")
            {

                _treePositions.Add(new Vert(700, _treePositions[0].y + 50));
                _treePositions.Add(new Vert(700, _treePositions[0].y + 100));
                _direction = "left";
                _started = false;
            }
            if (_treePositions[0].x == 50 && _direction == "left")
            {
                _treePositions.Add(new Vert(0, _treePositions[0].y + 50));
                _treePositions.Add(new Vert(0, _treePositions[0].y + 100));
                _direction = "right";
                _started = false;
            }
            //Console.WriteLine(treePositions[0]);

        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChooseDataManipulationMethod(string method)
        {
            lock (_monitor)
            {
                switch (method)
                {
                    case "0":
                        _runmethod = NeuralNetwork;
                        _disarmedMethod = "neural network";
                        break;
                    case "1":
                        _runmethod = DecisionTree;
                        _disarmedMethod = "decision tree";
                        break;
                }
                NeuralNetworkButton.RaiseCanExecuteChanged();
                DecisionTreeButton.RaiseCanExecuteChanged();
            }
        }
        private void CreateBoard()
        {
            var rnd = new Random();
            for (var i = 0; i < 20; i++)
            {
                for (var j = 0; j < 20; j++)
                {
                    var rand = rnd.Next(25) + 1;
                    string text;
                    switch (rand)
                    {
                        case 1:
                            text = "1";
                            break;
                        case 2:
                            text = "2";
                            break;
                        case 3:
                            text = "3";
                            break;
                        case 4:
                            text = "4";
                            break;
                        case 5:
                            text = "5";
                            break;
                        default:
                            text = "";
                            break;
                    }
                    var x = rnd.Next(Width - 80);
                    var y = rnd.Next(Height - 50);
                    Bombs.Add(new Bomb
                    {
                        BombId = text,
                        BombCanvasLeft = x,
                        BombCanvasTop = y,
                    });
                }
            }
        }

        private readonly object _monitor = new object();
        private IDataManipulation _runmethod = NeuralNetwork;
        private static readonly NeuralNetwork NeuralNetwork = new NeuralNetwork();
        private static readonly  DecisionTree DecisionTree = new DecisionTree();
        private static int _x = 10, _y = 8, _canvasLeft, _canvasTop;
        private int _width, _height;
        private bool _isIncrement = true;
        private List<TranslateTransform> _bombsPositions = new List<TranslateTransform>();
        private readonly List<IBomb> _bombTypeses = Enum.GetValues(typeof(BombTypes)).Cast<BombTypes>().Select(BombFabric.CreateBomb).Where(tempObject => tempObject != null).ToList();
        private string _disarmedMethod = "neural network";

        //For DepthFirstAlgorithm
        private Point _destinationPoint;
        private List<Vert> _treePositions = new List<Vert>();
        private bool _started = false;
        private double _lastX;
        private string _direction = "right";
    }

    public class Bomb : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string BombId
        {
            get { return _bombId; }
            set
            {
                _bombId = value;
                OnPropertyChanged();
            }
        }
        public int BombCanvasTop
        {
            get { return _canvasTop; }
            set
            {
                _canvasTop = value;
                OnPropertyChanged();
            }
        }

        public int BombCanvasLeft
        {
            get { return _canvasLeft; }
            set
            {
                _canvasLeft = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
       
        private string _bombId;
        private int _canvasLeft, _canvasTop;
    }
}
