using System.Windows;

namespace Muziek_Game
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new MainMenu(); // Start met het hoofdmenu
        }

        public void StartGame()
        {
            MainContent.Content = new GameControl(); // Vervang het menu door de game
        }
    }
}
