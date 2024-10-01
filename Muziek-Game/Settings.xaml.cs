﻿using System.Windows;
using System.Windows.Controls;

namespace Muziek_Game
{
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Ga terug naar het hoofdmenu
            var window = Window.GetWindow(this) as MainWindow; // Verander hier naar MainWindow
            if (window != null)
            {
                window.MainContent.Content = new MainMenu(); // Zorg ervoor dat je een MainMenu UserControl hebt
            }
            else
            {
                MessageBox.Show("Hoofdvenster niet gevonden.");
            }
        }

    }
}