using System.Windows;
using System.Windows.Controls;

namespace Muziek_Game
{
    public partial class LevelSelector : UserControl
    {
        public LevelSelector()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this) as MainWindow;
            if (window != null)
            {
                window.MainContent.Content = new MainMenu();
            }
        }

        private void Level1_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new GameControl(1); // Vervang het menu door de game

            }
        }

        private void Level2_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new GameControl(2); // Vervang het menu door de game

            }
        }

        private void Level3_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new GameControl(3); // Vervang het menu door de game

            }
        }
    }
}
