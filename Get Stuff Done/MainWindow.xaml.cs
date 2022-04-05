using System;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace Get_Stuff_Done
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));

            Console.WriteLine("Hello World");
        }

        /*
         * This Starts the progress Bar
         */
        private void StartWorkFlow_Click(object sender, RoutedEventArgs e)
        {
            float timeLength = float.Parse(workLength.Text);                // Get Time Duration
            float breakLength = float.Parse(BreakLength.Text);              // Get Break Duration

            timeBar.Value = 0;                                              // Progress Bar Initilization

            int m = (int)timeLength % 60;                                   // Parse into Minutes
            int s = m * 60;                                                 // Parse into Seconds

            int b_m = (int)breakLength % 60;                                // Parse Break Length
            int s_m = b_m * 60;

            tabs.SelectedIndex = 1;                                         // Switch Tab View

            currentProgress.Content = "Current Progress - " + s;            // Updated Current Progress to Match the time

            Task.Run(() =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    timeBar.Maximum = s;                                    // Set the Maximum Length of the Progress Bar

                    timeBar.Foreground =
                            new SolidColorBrush(Colors.Green);
                });

                for (int i = 0; i <= s; i += 1)
                {
                    Thread.Sleep(1000);                                     // Sleep Thread
                    this.Dispatcher.Invoke(() =>                            // Using Dispatcher to update IU
                    {
                        timeBar.Value = i;                                  // Increase Data

                        if (timeBar.Maximum == i)
                        {
                            timeBar.Value = 0;                              // Reset the Progress Bar to signify Completed Time
                            
                            timeBar.Maximum = b_m;                          // Change the Maximum Length of the Progress                

                            timeBar.Foreground = 
                            new SolidColorBrush(Colors.Blue);               // Changing ForeGround to Signify new Time Length 

                            for (int j = 0; j <= b_m; j += 1)               // For the Break Length Session 
                            {
                                Thread.Sleep(1000);
                                timeBar.Value = j;

                                if (timeBar.Maximum == j)                    // End of the Program
                                {
                                    currentProgress.Content 
                                    = "Finished Promodoro";

                                    Thread.Sleep(10000);                    // Awaiting the User

                                    tabs.SelectedIndex = 0;                 // Send Back to the Options Page
                                }
                            }
                        }
                    });
                }
            });
        }
    }
}