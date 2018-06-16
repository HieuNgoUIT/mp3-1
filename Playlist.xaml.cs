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

namespace MusicAppMP3
{
    /// <summary>
    /// Interaction logic for Playlist.xaml
    /// </summary>
    public partial class Playlist : Page
    {
       // private MediaPlayer mediaPlayer = new MediaPlayer();
        string[] files, paths;
        DispatcherTimer timer;
        private TimeSpan TotalTime;
        private double songposition;
        public Playlist()
        {
            InitializeComponent();
            sdDuration.Visibility = Visibility.Hidden;
            btplay.Visibility = Visibility.Hidden;
          
        }
        private void MyMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            TotalTime = mediaPlayer.NaturalDuration.TimeSpan;
            sdDuration.Visibility = Visibility.Visible;
            sdDuration.Maximum = TotalTime.TotalSeconds;
            sdDuration.Value = 0;
           // Create a timer that will update the counters and the time slider
           timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
            txblDuration.Text = new TimeSpan(0, (int)(TotalTime.TotalSeconds / 60), (int)(TotalTime.TotalSeconds % 60)).ToString(@"mm\:ss");
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            
            
            songposition = mediaPlayer.Position.TotalSeconds;
            songposition += 1;           
            sdDuration.Value = songposition;
           
        }
        private void timeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            songposition = sdDuration.Value;
            mediaPlayer.Position = new TimeSpan(0, 0, (int)songposition);
         

           
        }
        private void sdDuration_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {          
            txblPosition.Text =Convert.ToString(mediaPlayer.Position.Minutes )+":"+ Convert.ToString(mediaPlayer.Position.Seconds);                       
        }
        private void openMusic(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {               
                files = openFileDialog.SafeFileNames;
                paths = openFileDialog.FileNames;
                for (int i = 0; i < files.Length; i++)
                {
                    lbSong.Items.Add(files[i]);
                }

            }
        }
        private void PlayMusic(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
            btpause.Visibility = Visibility.Visible;
            btplay.Visibility = Visibility.Hidden;
        }
        private void PauseMusic(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
            btpause.Visibility = Visibility.Hidden;
            btplay.Visibility = Visibility.Visible;
        }

        private void lbSong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Uri uri = new Uri(paths[lbSong.SelectedIndex]);
            mediaPlayer.Source=uri;
            mediaPlayer.Play();
        }

       
        private void next(object sender, RoutedEventArgs e)
        {
            lbSong.SelectedIndex += 1;
            Uri uri = new Uri(paths[lbSong.SelectedIndex]);
            mediaPlayer.Source = uri;
          //  mediaPlayer.Play();
        }
        private void previous(object sender, RoutedEventArgs e)
        {
            lbSong.SelectedIndex -= 1;
            Uri uri = new Uri(paths[lbSong.SelectedIndex]);
            mediaPlayer.Source = uri;
           // mediaPlayer.Play();
        }
        private void end(object sender, RoutedEventArgs e)
        {
            lbSong.SelectedIndex += 1;
            Uri uri = new Uri(paths[lbSong.SelectedIndex]);
            mediaPlayer.Source = uri;
            //  mediaPlayer.Play();
        }
    }
}
