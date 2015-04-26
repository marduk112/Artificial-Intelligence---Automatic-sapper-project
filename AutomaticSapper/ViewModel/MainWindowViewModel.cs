using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;
using System.Windows.Media;
using AutomaticSapper.Annotations;
using Timer = System.Timers.Timer;

namespace AutomaticSapper.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<List<String>> Bombs { get; private set; }
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
            Bombs = new ObservableCollection<List<String>>();
            SapperTransform = new TranslateTransform();
            Width = 525;
            Height = 350;
            var aTimer = new Timer(10);
            aTimer.Elapsed += aTimer_Elapsed;
            aTimer.Enabled = true;
            CreateBoard();
        }

        void aTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (ViewContext != null)
            {
                ViewContext.Post(_ =>
                {
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
                    SapperTransform = new TranslateTransform(_x, _y);
                    if (_isIncrement)
                        _x++;
                    else
                        _x--;
                }, null);
            }
        }

        public TranslateTransform SapperTransform
        {
            get { return _sapperTransform; }
            set
            {
                _sapperTransform = value;
                OnPropertyChanged();
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
            var tempList = new List<String>();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Bombs.Add(new List<string>());
                    int rand = rnd.Next(25);
                    switch (rand)
                    {
                        case 0:
                            text = "0";
                            break;
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
                        default:
                            text = "";
                            break;
                    }
                    Bombs[i].Add(text);
                }
            }
        }

        private static int _x = 10, _y = 8;
        private TranslateTransform _sapperTransform;
        private int _width, _height;
        private bool _isIncrement = true;
    }
}
