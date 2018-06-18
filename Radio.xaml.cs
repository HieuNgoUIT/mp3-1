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

namespace MusicAppMP3
{
    /// <summary>
    /// Interaction logic for Radio.xaml
    /// </summary>
    public partial class Radio : Page
    {
        private string[] a={ "http://www.powerhitz.com/powerhitz.asx",
                               "http://www.hot108.com/hot108jamz.asx",
                                "http://www.powerhitz.com/wm/bumpincs.asx"};
                
        MediaPlayer mda = new MediaPlayer();
        Random r = new Random();
        int index;
        public Radio()
        {
            InitializeComponent();
        }
        private void finding(object sender, RoutedEventArgs e)
        {           
            int random = r.Next(0,a.Length);
            index = random;
            Uri url = new Uri(a[index]);
            mda.Open(url);
            mda.Play();
        }
        private void nextrad(object sender, RoutedEventArgs e)
        {
            if (index < a.Length)
            {
                Uri url = new Uri(a[index + 1]);
                mda.Open(url);
                mda.Play();
            }
        }
        private void prerad(object sender, RoutedEventArgs e)
        {
            if (index != 0)
            {           
            Uri url = new Uri(a[index - 1]);
            mda.Open(url);
            mda.Play();
            }
        }
        private void stoping(object sender, RoutedEventArgs e)
        {
            mda.Stop();
        }
    }
}
