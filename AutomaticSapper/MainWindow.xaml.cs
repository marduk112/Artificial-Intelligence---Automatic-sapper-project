using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using AutomaticSapper.ViewModel;


namespace AutomaticSapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // The ViewModel object that needs to marshal some actions is
            // attached as the DataContext by the time of the loaded event.
            var vmTest = (DataContext as MainWindowViewModel);
            if (null != vmTest)
            {
                // Set the ViewModel's reference SynchronizationContext to
                // the View's current context.
                vmTest.ViewContext = Dispatcher.Invoke
                    (() => SynchronizationContext.Current);
            }
        }
    }
}
