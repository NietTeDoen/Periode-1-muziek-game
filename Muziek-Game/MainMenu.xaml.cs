using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Muziek_Game
{
    public partial class MainMenu : UserControl
    { 
        public MediaPlayer _mediaPlayer = new MediaPlayer();
        private AudioManager _audioManager = new AudioManager();

        public MainMenu()
        {
            InitializeComponent();
            InitializeMediaPlayer();
            _mediaPlayer = AudioManager.GetMediaPlayer(); 
        }

        /// <summary>
        /// Initialiseer de MediaPlayer
        /// </summary>
        private void InitializeMediaPlayer()
        {
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            _mediaPlayer.MediaFailed += MediaPlayer_MediaFailed;
        }

        /// <summary>
        /// Speelt muziek af
        /// </summary>
        public void PlayMusic()
        {
            _mediaPlayer = new MediaPlayer();

            // Gebruik een reguliere bestands-URI
            string musicPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "muziek", "ABBA.mp3");
            Uri musicUri = new Uri(musicPath);

            _mediaPlayer.Volume = 0.5; // Maximaal volume
            _mediaPlayer.Open(musicUri);
            _mediaPlayer.Play();
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            // Stop de muziek nadat het afspelen is voltooid
            _mediaPlayer.Stop();
        }

        private void MediaPlayer_MediaFailed(object sender, ExceptionEventArgs e)
        {
            MessageBox.Show($"Error: {e.ErrorException.Message}");
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigeren naar de profielpagina
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new ProfilePage(); // Zorg ervoor dat je een ProfilePage UserControl hebt
            }
        }

        private void ModsButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigeren naar de mods pagina
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new ModsMenu(); // Zorg ervoor dat je een ModsMenu UserControl hebt
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigeer naar het instellingenmenu
            var window = Window.GetWindow(this) as MainWindow;
            if (window != null)
            {
                window.MainContent.Content = new Settings(); // Zorg ervoor dat je een Settings UserControl hebt
            }
        }

        private void ShopButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigeren naar de winkelpagina
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new ItemShop(); // Zorg ervoor dat je een ShopMenu UserControl hebt
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Laad de game
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new GameControl(); // Vervang het menu door de game
                _audioManager.PauseMedia();
                
            }
        }

        private void HighScore_Click(object sender, RoutedEventArgs e)
        {
            // Navigeren naar de HighScore pagina
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new HighScore(); // Zorg ervoor dat je een HighScore UserControl hebt
            }
        }

    }
}
