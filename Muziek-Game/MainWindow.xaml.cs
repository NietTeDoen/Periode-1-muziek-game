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
    }
}
