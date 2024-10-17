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

        }

        private void Level2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Level3_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
