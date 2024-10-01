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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Profiel knop geklikt!");
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

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Laad de game
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new GameControl(); // Vervang het menu door de game
            }
        }

        private void ShopButton_Click(object sender, RoutedEventArgs e)
        {
            // Laad de item shop
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new ItemShop(); // Vervang het menu door de winkel
            }
        }
    }
}
