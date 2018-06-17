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
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Page
    {
        private Song currentSong;
        private Song ss;
        private Song ss1;
        private Song ss2;
        private Song ss3;
        private Song ss4;
        private string songlink;
        private string songlink1;
        private string songlink2;
        private string songlink3;
        private string songlink4;
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

        public Song Ss
        {
            get
            {
                return ss;
            }

            set
            {
                ss = value;
            }
        }

        public Song Ss1
        {
            get
            {
                return ss1;
            }

            set
            {
                ss1 = value;
            }
        }

        public Song Ss2
        {
            get
            {
                return ss2;
            }

            set
            {
                ss2 = value;
            }
        }

        public Song Ss3
        {
            get
            {
                return ss3;
            }

            set
            {
                ss3 = value;
            }
        }

        public Song Ss4
        {
            get
            {
                return ss4;
            }

            set
            {
                ss4 = value;
            }
        }
        public Search()
        {
            InitializeComponent();
            ucSongInfo.BackToMain += UcSongInfo_BackToMain;
        }
        void CrawLKQ()
        {
            HttpRequest html = new HttpRequest();
            //nhap vao input bai hat can search
            string searchname = tbSearch.Text.Replace(" ", "+");
            string searchname1 = (@"https://nhac.vn/search?q=" + searchname);
            // tra ve html cua trang web search
            string htmlink = html.Get(searchname1).ToString();

            //loc ket qua tu html sang bai hat kiem duoc
            var sResult = Regex.Matches(htmlink, @"<li class=""song-list-new-item(.*?)</li>", RegexOptions.Singleline);
            if (sResult.Count == 0)
            {
                MessageBox.Show("ko ton tai");
            }
            else
            {
                if (sResult.Count == 5)
                {
                    //lay bai hat dau tien tu sresult
                    string song = sResult[0].ToString();
                    songlink = Addsong(bt0, song);
                    Ss = crawlasong(songlink, 0);

                    string song1 = sResult[1].ToString();
                    songlink1 = Addsong(bt1, song1);
                    Ss1 = crawlasong(songlink1, 1);

                    string song2 = sResult[2].ToString();
                    songlink2 = Addsong(bt2, song2);
                    Ss2 = crawlasong(songlink2, 2);

                    string song3 = sResult[3].ToString();
                    songlink3 = Addsong(bt3, song3);
                    Ss3 = crawlasong(songlink3, 3);


                    string song4 = sResult[4].ToString();
                    songlink4 = Addsong(bt4, song4);
                    Ss4 = crawlasong(songlink4, 4);
                }
            }
        }

        Song crawlasong(string slink, int i)
        {
            Song any = new Song();
            string specific = slink;
            HttpRequest spehtm = new HttpRequest();
            string htmlink = spehtm.Get(specific).ToString();
            var Result = Regex.Matches(htmlink, @"sources:(.*?):""(.*?)""", RegexOptions.Singleline);
            // link dowload
            string resultt = Result[0].ToString();
            int indexurl = resultt.IndexOf("file");
            string songurl = resultt.Substring(indexurl, resultt.Length - indexurl - 1).Replace(@"\/", @"/").Replace("file\":\"", "").Replace(" ", "");
            any.DownloadURL = songurl;
            // hinh anh
            var ketquahinhanh= Regex.Matches(htmlink, @"image: '(.*?)'", RegexOptions.Singleline);
            string pturl = ketquahinhanh[0].ToString().Replace("image: '","").Replace("'","");
            any.PhotoURL = pturl;
            //lyric 
            var ketqualyric= Regex.Matches(htmlink, @"dsc-body"">(.*?)<div", RegexOptions.Singleline);
            string lyricurl=ketqualyric[0].ToString().Replace("<br />", "").Replace("dsc-body\">", "").Replace("<div","");
            any.Lyric = lyricurl;
            any.SingerName = "";
            any.STT = 0;
            return any;
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {

            CrawLKQ();
        }

        //lay chi tiet cu the tung bai hat nhu cai si, ten ,....
        private string Addsong(Button a, string httt)
        {
            //ten bai hat
            var songandsinger = Regex.Matches(httt.ToString(), @"<a title=""(.*?)""", RegexOptions.Singleline);
            string songString = songandsinger[0].ToString();
            int indexSong = songString.IndexOf("title=\"");
            string songName = songString.Substring(indexSong, songString.Length - indexSong - 1).Replace("title=\"", "");

            //ten ca si
            string singerString = songandsinger[1].ToString();
            int indexSinger = singerString.IndexOf("title=\"");
            string singerName = singerString.Substring(indexSinger, singerString.Length - indexSinger - 1).Replace("title=\"", "");

            //tra ve ket qua vao nut button
            a.Content = songName + "-" + singerName;
            // link download bai hat
            var url = Regex.Matches(httt.ToString(), @"<a href=""(.*?)""", RegexOptions.Singleline);
            string urlstring = url[0].ToString();
            int indexurl = urlstring.IndexOf("href=\"");
            string songurl = urlstring.Substring(indexurl, urlstring.Length - indexurl - 1).Replace("href=\"", "");         
            return songurl;

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

        // dua den ham songinfo de download va play
        private void bh0click(object sender, RoutedEventArgs e)
        {
            Ss.SongName = bt0.Content.ToString();
            // c: asus:debug\song
            Ss.SavePath = AppDomain.CurrentDomain.BaseDirectory + "Song\\" + Ss.SongName + ".mp3";
            currentSong = Ss;
            aSong.Visibility = Visibility.Hidden;
            ucSongInfo.Visibility = Visibility.Visible;
            ucSongInfo.SongInfo = Ss;

        }
        private void bh1click(object sender, RoutedEventArgs e)
        {
            Ss1.SongName = bt1.Content.ToString();
            Ss1.SavePath = AppDomain.CurrentDomain.BaseDirectory + "Song\\" + Ss1.SongName + ".mp3";
            currentSong = Ss1;
            ucSongInfo.Visibility = Visibility.Visible;
            aSong.Visibility = Visibility.Hidden;
            ucSongInfo.SongInfo = Ss1;
        }
        private void bh2click(object sender, RoutedEventArgs e)
        {
            Ss2.SavePath = AppDomain.CurrentDomain.BaseDirectory + "Song\\" + Ss2.SongName + ".mp3";
            Ss2.SongName = bt2.Content.ToString();
            currentSong = Ss2;
            ucSongInfo.Visibility = Visibility.Visible;
            aSong.Visibility = Visibility.Hidden;
            ucSongInfo.SongInfo = Ss2;
        }
        private void bh3click(object sender, RoutedEventArgs e)
        {
            Ss3.SavePath = AppDomain.CurrentDomain.BaseDirectory + "Song\\" + Ss3.SongName + ".mp3";
            Ss3.SongName = bt3.Content.ToString();
            currentSong = Ss3;
            ucSongInfo.Visibility = Visibility.Visible;
            aSong.Visibility = Visibility.Hidden;
            ucSongInfo.SongInfo = Ss3;
        }
        private void bh4click(object sender, RoutedEventArgs e)
        {
            Ss4.SavePath = AppDomain.CurrentDomain.BaseDirectory + "Song\\" + Ss4.SongName + ".mp3";
            Ss4.SongName = bt4.Content.ToString();
            currentSong = Ss4;
            ucSongInfo.Visibility = Visibility.Visible;
            aSong.Visibility = Visibility.Hidden;
            ucSongInfo.SongInfo = Ss4;
        }
        private void UcSongInfo_BackToMain(object sender, EventArgs e)
        {
            aSong.Visibility = Visibility.Visible;
            ucSongInfo.Visibility = Visibility.Hidden;
        }

    
    }
}
