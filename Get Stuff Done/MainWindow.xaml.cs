using System;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

namespace Get_Stuff_Done
{ 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
        }

        /*
         * This Starts the progress Bar
         */
        private void StartWorkFlow_Click(object sender, RoutedEventArgs e)
        {
            float timeLength  = float.Parse(workLength.Text);               // Get Time Duration
            float breakLength = float.Parse(BreakLength.Text);              // Get Break Duration

            timeBar.Value = 0;                                              // Progress Bar Initilization

            int m = (int)timeLength % 60;                                   // Parse into Minutes
            int s = m * 60;                                                 // Parse into Seconds

            Task.Run(() =>
            {
                for (int i = 0; i < s; i += 1)
                {
                    Thread.Sleep(1000);                                     // Sleep Thread
                    this.Dispatcher.Invoke(() =>                            // Using Dispatcher to update IU
                    {
                        timeBar.Value = i;                                  // Increase Data
                    });
                }
            });
        }
    }
}
