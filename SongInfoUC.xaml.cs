using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for SongInfoUC.xaml
    /// </summary>
    public partial class SongInfoUC : UserControl, INotifyPropertyChanged
    {
        private Song songInfo;
        public Song SongInfo {
            get { return songInfo; }
            set
            {
                songInfo = value;
                DownloadSong(SongInfo);
                this.DataContext = SongInfo;
                OnPropertyChanged("SongInfo");               
            }
        }
        private double speedRatio;

        public bool IsPlaying { get { return isPlaying; }
            set
            {
                isPlaying = value;
                if (isPlaying)
                {                    
                    mdAudio.Play();
                    timer.Start();
                    btnPlay.Content = "Pause";
                }
                else
                {
                    mdAudio.Pause();
                    timer.Stop();
                    btnPlay.Content = "Play";
                }
            }
        }

        public double SpeedRatio
        {
            get { return speedRatio; }
            set
            {
                speedRatio = value;
            }
        }

        private bool isPlaying;

        DispatcherTimer timer;
        public SongInfoUC()
        {
            InitializeComponent();
            this.DataContext = SongInfo;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            SpeedRatio = 1;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SongInfo.Position+= SpeedRatio;
            sdDuration.Value = SongInfo.Position;
        }

        private event EventHandler backToMain;
        public event EventHandler BackToMain
        {
            add { backToMain += value; }
            remove { backToMain -= value; }
        }

        void DownloadSong(Song songInfo)
        {
            string songName = songInfo.SavePath;
            if (!File.Exists(songName))            
            {
                WebClient wb = new WebClient();
                // truyen vao url download, ve ten bai hat
                wb.DownloadFile(SongInfo.DownloadURL, songName);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (backToMain != null)
                backToMain(this, new EventArgs());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }



        private void mdAudio_MediaOpened(object sender, RoutedEventArgs e)
        {
            IsPlaying = true;
            SongInfo.Duration = mdAudio.NaturalDuration.TimeSpan.TotalSeconds;
            txblDuration.Text = new TimeSpan(0, (int)(SongInfo.Duration / 60), (int)(SongInfo.Duration % 60)).ToString(@"mm\:ss");
            sdDuration.Maximum = SongInfo.Duration;
            SongInfo.Position = 0;
            //timer.Start();    
        }

        bool isDraging = false;
        private void sdDuration_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {            
            if (isDraging)
            {
                SongInfo.Position = sdDuration.Value;
                mdAudio.Position = new TimeSpan(0, 0, (int)SongInfo.Position);
            }
            txblPosition.Text = new TimeSpan(0, (int)(SongInfo.Position / 60), (int)(SongInfo.Position % 60)).ToString(@"mm\:ss");
        }

        private void sdDuration_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDraging = true;
        }

        private void sdDuration_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDraging = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            IsPlaying = !IsPlaying;            
        }

        private event EventHandler previoursClicked;
        public event EventHandler PrevioursClicked
        {
            add { previoursClicked += value; }
            remove { previoursClicked -= value; }
        }

        private event EventHandler nextClicked;
        public event EventHandler NextClicked
        {
            add { nextClicked += value; }
            remove { nextClicked -= value; }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (previoursClicked != null)
                previoursClicked(this, new EventArgs());
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (nextClicked != null)
                nextClicked(this, new EventArgs());
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggle = sender as ToggleButton;
            if (toggle.IsChecked == true)
            {
                SpeedRatio = 2; 
            }
            else
            {
                SpeedRatio = 1;       
            }
            mdAudio.SpeedRatio = SpeedRatio;
            toggle.Content = string.Format("{0}.0",SpeedRatio);
        }
    }
}
