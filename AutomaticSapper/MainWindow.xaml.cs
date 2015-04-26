using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KnowledgeRepresentation.Bombs;


namespace AutomaticSapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Button playerX;
        public MainWindow()
        {
            List<List<String>> lsts = new List<List<String>>();

            BallBomb d = new BallBomb();
            Random rnd = new Random();
            String text;
            playerX = bu;
            
            Timer aTimer = new Timer(100);
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Enabled = true;

            for (int i = 0; i < 20; i++)
            {
                lsts.Add(new List<String>());

                for (int j = 0; j < 20; j++)
                {
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

                    lsts[i].Add(text);  
                }

                
            }

            InitializeComponent();

            lst.ItemsSource = lsts;
            
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            seT();

        }

        private static void seT()
        {
            //MainWindow myWindow = Application.Current.MainWindow as MainWindow;
            //Button myButton = myWindow.bu;

            //double actualTop = Canvas.GetTop(myButton);
            //Canvas.SetTop(bu, 100);
        }

        private void bu_Click(object sender, RoutedEventArgs e)
        {

        }
    
    }
}
