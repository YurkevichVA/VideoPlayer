using Microsoft.Win32;
using System;
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
using System.Windows.Threading;

namespace VideoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDragging = false;
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (myMediaElement.Source != null && myMediaElement.NaturalDuration.HasTimeSpan && !isDragging)
            {
                timeLineSlider.Minimum = 0;
                timeLineSlider.Maximum = myMediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                timeLineSlider.Value = myMediaElement.Position.TotalSeconds;
            }
        }
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.LoadedBehavior = MediaState.Play;
        }
        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.LoadedBehavior = MediaState.Pause;
        }
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.LoadedBehavior = MediaState.Stop;
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                myMediaElement.Source = new Uri(openFileDialog.FileName);
        }
        private void myMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            timeLineSlider.Maximum = myMediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void myMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            myMediaElement.Stop();
        }

        private void timeLineSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            isDragging = true;
        }

        private void timeLineSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            isDragging = false;
            myMediaElement.Position = TimeSpan.FromSeconds(timeLineSlider.Value);
        }
    }
}
