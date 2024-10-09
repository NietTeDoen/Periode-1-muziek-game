using System.Windows;
using System.Windows.Controls;

namespace Muziek_Game
{
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
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
