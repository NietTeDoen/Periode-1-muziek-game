using Muziek_Game.Properties;
using System;
using System.Windows;
using System.Windows.Media;

namespace Muziek_Game
{
    public partial class Menu : Window
    {
        public Menu() 
        {
            MainContent.Content = new MainMenu(); // Start met het hoofdmenu
            InitializeComponent();
        }

        public void SwitchToSettings()
        {
            MainContent.Content = new Settings(); // Laad de instellingen
        }
    }
}
