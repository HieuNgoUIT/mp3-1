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
        private int vShuffle = 0; // bat che do shuffle
        private Random r = new Random();
        private int biencu; //bien bai hat cu
        public Playlist()
        {
            InitializeComponent();
            sdDuration.Visibility = Visibility.Hidden; // an thanh thoi gian bai hat
            btplay.Visibility = Visibility.Hidden; //an nut play
            shuffleon.Visibility = Visibility.Hidden;//an nut shuffle on
          
        }
        // event moi khi 1 bai hat duoc mo
        private void MyMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            TotalTime = mediaPlayer.NaturalDuration.TimeSpan;
            //hien thanh thoi gian
            sdDuration.Visibility = Visibility.Visible;
            //thanh thoi gian = voi thoi gian cua bai hat
            sdDuration.Maximum = TotalTime.TotalSeconds;
            sdDuration.Value = 0;
           // bat dau bo dem thoi gian
           timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
            //the hien thoi gian bai hat
            txblDuration.Text = new TimeSpan(0, (int)(TotalTime.TotalSeconds / 60), (int)(TotalTime.TotalSeconds % 60)).ToString(@"mm\:ss");           
        }


        // tang thoi gian bai hat len +1
        private void Timer_Tick(object sender, EventArgs e)
        {
                       
            songposition = mediaPlayer.Position.TotalSeconds;
            songposition += 1;           
            sdDuration.Value = songposition;
           
        }

        //re chuot toi thoi gian mong muon, tha chuot ra
        private void timeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {       
            //cap nhat  tg vi tri bai hat tai cho tha chuot  
            songposition = sdDuration.Value;
            //cap nhat lai vi tri de mediaplayer phat'
            mediaPlayer.Position = new TimeSpan(0, 0, (int)songposition);                  
        }
        // thanh thoi gian changed thi text thoi gian cung thay doi
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
            if(vShuffle == 0)
            {
                lbSong.SelectedIndex += 1;
                Uri uri = new Uri(paths[lbSong.SelectedIndex]);
                mediaPlayer.Source = uri;
            }
            else
            {             
                biencu = lbSong.SelectedIndex;
                lbSong.SelectedIndex = r.Next(0, lbSong.Items.Count);
                if (lbSong.SelectedIndex == biencu)
                {
                    lbSong.SelectedIndex = r.Next(0, lbSong.Items.Count);
                }
                Uri uri = new Uri(paths[lbSong.SelectedIndex]);
                mediaPlayer.Source = uri;
            }
                   
        }
        private void previous(object sender, RoutedEventArgs e)
        {
            if (vShuffle == 0)
            {
                lbSong.SelectedIndex -= 1;
                Uri uri = new Uri(paths[lbSong.SelectedIndex]);
                mediaPlayer.Source = uri;
            }
            else
            {
                biencu = lbSong.SelectedIndex;
                lbSong.SelectedIndex = r.Next(0, lbSong.Items.Count);
                if (lbSong.SelectedIndex == biencu)
                {
                    lbSong.SelectedIndex = r.Next(0, lbSong.Items.Count);
                }
                Uri uri = new Uri(paths[lbSong.SelectedIndex]);
                mediaPlayer.Source = uri;
            }

        }
        private void end(object sender, RoutedEventArgs e)
        {
            if (vShuffle == 0)
            {

                lbSong.SelectedIndex += 1;
                Uri uri = new Uri(paths[lbSong.SelectedIndex]);
                mediaPlayer.Source = uri;
            }
            else
            {               
                biencu = lbSong.SelectedIndex;
                lbSong.SelectedIndex = r.Next(0, lbSong.Items.Count);               
                if (lbSong.SelectedIndex == biencu)
                {
                    lbSong.SelectedIndex = r.Next(0, lbSong.Items.Count);
                }                            
                Uri uri = new Uri(paths[lbSong.SelectedIndex]);
                mediaPlayer.Source = uri;
            }
           
        }
        private void shuffleturnon(object sender, RoutedEventArgs e)
        {
            vShuffle = 1;       
            shuffleoff.Visibility = Visibility.Hidden;
            shuffleon.Visibility = Visibility.Visible;        
        }
        private void shuffleturnoff(object sender, RoutedEventArgs e)
        {
            vShuffle = 0;
            shuffleoff.Visibility = Visibility.Visible;
            shuffleon.Visibility = Visibility.Hidden;
        }
    }
}
