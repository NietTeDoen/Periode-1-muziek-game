using Muziek_Game.Properties;
using System.Windows;

namespace Muziek_Game
{
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
            MainContent.Content = new MainMenu(); // Start met het hoofdmenu
        }

        public void SwitchToSettings()
        {
            MainContent.Content = new Settings(); // Laad de instellingen
        }
    }
}
