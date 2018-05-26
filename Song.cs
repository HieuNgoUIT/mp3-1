using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAppMP3
{
    public class Song
    {
        private string songName;
        private string singerName;
        private string songURL;
        private int sTT;
        private string lyric;
        private string downloadURL;
        private string photoURL;
        private string savePath;
        private double duration;
        private double position;

        public string SongName
        {
            get
            {
                return songName;
            }

            set
            {
                songName = value;
            }
        }

        public string SingerName
        {
            get
            {
                return singerName;
            }

            set
            {
                singerName = value;
            }
        }

        public string SongURL
        {
            get
            {
                return songURL;
            }

            set
            {
                songURL = value;
            }
        }

        public int STT
        {
            get
            {
                return sTT;
            }

            set
            {
                sTT = value;
            }
        }

        public string Lyric
        {
            get
            {
                return lyric;
            }

            set
            {
                lyric = value;
            }
        }

        public string DownloadURL
        {
            get
            {
                return downloadURL;
            }

            set
            {
                downloadURL = value;
            }
        }

        public string PhotoURL
        {
            get
            {
                return photoURL;
            }

            set
            {
                photoURL = value;
            }
        }

        public string SavePath
        {
            get
            {
                return savePath;
            }

            set
            {
                savePath = value;
            }
        }

        public double Duration
        {
            get
            {
                return duration;
            }

            set
            {
                duration = value;
            }
        }

        public double Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }
    }
}
