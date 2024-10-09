using Muziek_Game.Properties;
using System;
using System.Windows;
using System.Windows.Media;

namespace Muziek_Game
{
    public partial class Menu : Window
    {
        private MediaPlayer _mediaPlayer;
        public Menu() 
        {
            MainContent.Content = new MainMenu(); // Start met het hoofdmenu
            _mediaPlayer = new MediaPlayer();
            InitializeComponent();
        }

        private void PlayMusic_Click(object sender, RoutedEventArgs e)
        {
            Uri musicUri = new Uri("pack://application:,,,/Assets/muziek/ABBA.mp3");
            _mediaPlayer.Open(musicUri);
            _mediaPlayer.Play();
        }

        public void SwitchToSettings()
        {
            MainContent.Content = new Settings(); // Laad de instellingen
        }
    }
}
