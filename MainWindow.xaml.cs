
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
using xNet;

namespace MusicAppMP3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       
        public MainWindow()
        {
            InitializeComponent();
            
        }

       
        private void btsearch(object sender, RoutedEventArgs e)
        {
            Main.Content = new Search();

        }
        private void btRanking(object sender, RoutedEventArgs e)
        {
            Main.Content = new Ranking();
        }
       private void openPlaylist(object sender, RoutedEventArgs e)
        {
            Main.Content = new Playlist();
        }
       
        private void exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void hide(object sender, RoutedEventArgs e)
        {
           WindowState= WindowState.Minimized;

        }
    }
}
