using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using xNet;

namespace MusicAppMP3
{
    /// <summary>
    /// Interaction logic for Ranking.xaml
    /// </summary>
    public partial class Ranking : Page, INotifyPropertyChanged
    {
        private bool isCheckVN;
        private bool isCheckEU;
        private bool isCheckKO;
        private ObservableCollection<Song> listVN;
        private ObservableCollection<Song> listEU;
        private ObservableCollection<Song> listKO;
        private Song currentSong;
        public bool IsCheckVN
        {
            get
            {
                return isCheckVN;
            }

            set
            {
                isCheckVN = value;
                lsbTopSongs.ItemsSource = ListVN;
                isCheckEU = false;
                isCheckKO = false;
                OnPropertyChanged("IsCheckVN");
                OnPropertyChanged("IsCheckEU");
                OnPropertyChanged("IsCheckKO");
            }
        }

        public bool IsCheckEU
        {
            get
            {
                return isCheckEU;
            }

            set
            {
                isCheckEU = value;
                lsbTopSongs.ItemsSource = ListEU;
                isCheckVN = false;
                isCheckKO = false;
                OnPropertyChanged("IsCheckVN");
                OnPropertyChanged("IsCheckEU");
                OnPropertyChanged("IsCheckKO");
            }
        }

        public bool IsCheckKO
        {
            get
            {
                return isCheckKO;
            }

            set
            {
                isCheckKO = value;
                lsbTopSongs.ItemsSource = ListKO;
                isCheckEU = false;
                isCheckVN = false;
                OnPropertyChanged("IsCheckVN");
                OnPropertyChanged("IsCheckEU");
                OnPropertyChanged("IsCheckKO");
            }
        }

        public ObservableCollection<Song> ListVN
        {
            get
            {
                return listVN;
            }

            set
            {
                listVN = value;
            }
        }

        public ObservableCollection<Song> ListEU
        {
            get
            {
                return listEU;
            }

            set
            {
                listEU = value;
            }
        }

        public ObservableCollection<Song> ListKO
        {
            get
            {
                return listKO;
            }

            set
            {
                listKO = value;
            }
        }

        public Song CurrentSong
        {
            get
            {
                return currentSong;
            }

            set
            {
                currentSong = value;
            }
        }
        public Ranking()
        {
            InitializeComponent();
            ucSongInfo.BackToMain += UcSongInfo_BackToMain;

            this.DataContext = this;

            ListVN = new ObservableCollection<Song>();
            ListEU = new ObservableCollection<Song>();
            ListKO = new ObservableCollection<Song>();
            CrawlBXH();
            IsCheckVN = true;
        }
        void CrawlBXH()
        {
            HttpRequest http = new HttpRequest();
            //lay bxh vn
            string htmlBXH = http.Get(@"http://keeng.vn/bang-xep-hang/song/viet-nam.html").ToString();
            string bxhPattern = @"<div id=""show_audio_play""(.*?)</ul>";
            var listBXH = Regex.Matches(htmlBXH, bxhPattern, RegexOptions.Singleline);            
            AddSongToListSong(ListVN, listBXH[0].ToString());

            HttpRequest http1 = new HttpRequest();
            //lay bxh aumy
            string htmlBXH1 = http1.Get(@"http://keeng.vn/bang-xep-hang/song/au-my.html").ToString();           
            var listBXH1 = Regex.Matches(htmlBXH1, bxhPattern, RegexOptions.Singleline);
            AddSongToListSong(ListEU, listBXH1[0].ToString());

            HttpRequest http2 = new HttpRequest();
            string htmlBXH2 = http2.Get(@"http://keeng.vn/bang-xep-hang/song/chau-a.html").ToString();          
            var listBXH2 = Regex.Matches(htmlBXH2, bxhPattern, RegexOptions.Singleline);
            AddSongToListSong(ListKO, listBXH2[0].ToString());




        }
        void AddSongToListSong(ObservableCollection<Song> listSong, string html)
        {
            //lay tung bai hat va add vao listbxh
            var listSongHTML = Regex.Matches(html.ToString(), @"<div class=""ka-content""(.*?)</li>", RegexOptions.Singleline);
            //tra ve 20 bai hat, bat dau to bai dau tien
            for (int i = 0; i < listSongHTML.Count; i++)
            {

                var song = Regex.Matches(listSongHTML[i].ToString(), @"<a\s\S*\stitle=""(.*?)""", RegexOptions.Singleline);
                string songString = song[0].ToString();
                int indexSong = songString.IndexOf("title=\"");
                string songName = songString.Substring(indexSong, songString.Length - indexSong - 1).Replace("title=\"", "");

                var art = Regex.Matches(listSongHTML[i].ToString(), @"<a name=\""bxh_singer_name\"">(.*?)<", RegexOptions.Singleline);
                string singerString = art[0].ToString();
                int indexSinger = singerString.IndexOf(">");
                string singerName = singerString.Substring(indexSinger, singerString.Length - indexSinger - 1).Replace(">", "");

                var downloadURL = Regex.Matches(listSongHTML[i].ToString(), @"data-src=""(.*?)""", RegexOptions.Singleline);
                string downloadURLt = downloadURL[0].ToString().Replace("amp;", "").Replace("\"", "").Replace("<input type=hidden value=", "").Replace("data-src=", "");

                string savePath = AppDomain.CurrentDomain.BaseDirectory + "Song\\" + songName + ".mp3";

                var image=Regex.Matches(listSongHTML[i].ToString(), @"http(.*?)""", RegexOptions.Singleline);
                string imaget = image[1].ToString().Replace("\"", "");
                //add no vao bxh 
                listSong.Add(new Song() { SingerName = singerName, SongName = songName,PhotoURL=imaget, /*SongURL = URL*//*,*/ STT = i + 1, DownloadURL = downloadURLt, SavePath = savePath });
            }
        }
        private void UcSongInfo_BackToMain(object sender, EventArgs e)
        {
            gridTop10.Visibility = Visibility.Visible;
            ucSongInfo.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Song song = (sender as Button).DataContext as Song;
            CurrentSong = song;
            gridTop10.Visibility = Visibility.Hidden;
            ucSongInfo.Visibility = Visibility.Visible;
            ucSongInfo.SongInfo = song;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }

        void ChangeToNextWSong(ObservableCollection<Song> listSong, int position, int addCount)
        {
            int index = listSong.IndexOf(CurrentSong);
            if (index == position)
            {
                return;
            }
            else
            {
                CurrentSong = listSong[index + addCount];
                ucSongInfo.SongInfo = CurrentSong;
            }
        }

        private void ucSongInfo_PrevioursClicked(object sender, EventArgs e)
        {
            if (IsCheckVN)
            {
                ChangeToNextWSong(ListVN, 0, -1);
            }
            else if (IsCheckEU)
            {
                ChangeToNextWSong(ListEU, 0, -1);
            }
            else
            {
                ChangeToNextWSong(ListKO, 0, -1);
            }
        }

        private void ucSongInfo_NextClicked(object sender, EventArgs e)
        {
            if (IsCheckVN)
            {
                ChangeToNextWSong(ListVN, 9, 1);
            }
            else if (IsCheckEU)
            {
                ChangeToNextWSong(ListEU, 9, 1);
            }
            else
            {
                ChangeToNextWSong(ListKO, 9, 1);
            }
        }

    }
}
