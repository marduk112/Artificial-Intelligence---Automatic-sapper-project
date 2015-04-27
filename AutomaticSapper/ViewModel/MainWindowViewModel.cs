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
using DataManipulation.DataManipulation;
using DataManipulation.Interfaces;
using KnowledgeRepresentation;
using KnowledgeRepresentation.Fabrics;
using KnowledgeRepresentation.Interfaces;
using Timer = System.Timers.Timer;

namespace AutomaticSapper.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Bomb> Bombs { get; private set; }
        public SynchronizationContext ViewContext { get; set; }

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
            //SapperTransform = new TranslateTransform{X = 0, Y = 0};
            Width = 750;
            Height = 550;
            CanvasLeft = 10;
            CanvasTop = 10;
            var aTimer = new Timer(30);
            aTimer.Elapsed += aTimer_Elapsed;
            aTimer.Enabled = true;
            CreateBoard();
        }
        /*public TranslateTransform SapperTransform
        {
            get { return _sapperTransform; }
            set
            {
                _sapperTransform = value;
                OnPropertyChanged();
            }
        }*/

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
            if (ViewContext != null)
            {
                ViewContext.Post(_ =>
                {
                    foreach (var bomb in Bombs)
                    {
                        if (Math.Abs(CanvasLeft - bomb.BombCanvasLeft) <= 0 
                            && Math.Abs(CanvasTop - bomb.BombCanvasTop) <= 13
                            && bomb.BombId != "" && bomb.BombId != "^" && bomb.BombId != "&")
                        {
                            var result = _model.GetDisarmingProcedure(int.Parse(bomb.BombId));
                            var bombFound = _bombTypeses.SingleOrDefault(b => b.BeepsLevel == int.Parse(bomb.BombId));
                            var bombDisarmingProcedure = Tuple.Create(bombFound.FirstStageDisarming,
                                bombFound.SecondStageDisarming, bombFound.ThirdStageDisarming);
                            bomb.BombId = result.Equals(bombDisarmingProcedure) ? "^" : "&";
                        }
                    }
                   
                    CanvasLeft = _x;
                    CanvasTop = _y;

                    if (_x >= Width)
                    {
                        _y += 26;
                        _isIncrement = false;
                    }
                    if (_x <= -10)
                    {
                        _y += 26;
                        _isIncrement = true;
                    }

                    if (_isIncrement)
                        _x++;
                    else
                        _x--;
                }, null);
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CreateBoard()
        {
            var rnd = new Random();
            string text;
            for (var i = 0; i < 20; i++)
            {
                for (var j = 0; j < 20; j++)
                {
                    var rand = rnd.Next(25) + 1;
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

        private static int _x = 10, _y = 8, _canvasLeft, _canvasTop;
        private TranslateTransform _sapperTransform;
        private int _width, _height;
        private bool _isIncrement = true;
        private List<TranslateTransform> _bombsPositions = new List<TranslateTransform>();
        private readonly List<IBomb> _bombTypeses = Enum.GetValues(typeof(BombTypes)).Cast<BombTypes>().Select(BombFabric.CreateBomb).Where(tempObject => tempObject != null).ToList();
        private readonly IDataManipulation _model = new NeuralNetwork();
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
