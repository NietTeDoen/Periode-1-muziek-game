using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Muziek_Game
{
    public partial class Settings : UserControl
    {
        private AudioManager audioManager = new AudioManager();
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
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs <double> e)
        {
            var window = Window.GetWindow(this) as MainWindow;
            if (window != null)
            {
                audioManager.VolumeChange(e.NewValue / 100);
            }
        } 

        private void Graphics_Checked(object sender, RoutedEventArgs e)
        {
            // Delay execution using Dispatcher to ensure the UI is fully initialized
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (sender == HighGraphics)
                {
                    MediumGraphics.IsChecked = false;
                    LowGraphics.IsChecked = false;
                }
                else if (sender == MediumGraphics)
                {
                    HighGraphics.IsChecked = false;
                    LowGraphics.IsChecked = false;
                }
                else if (sender == LowGraphics)
                {
                    HighGraphics.IsChecked = false;
                    MediumGraphics.IsChecked = false;
                }
            }), System.Windows.Threading.DispatcherPriority.Background); //zorgt ervoor dat het pas wordt uitgevoerd als de UI volledig is geïnitialiseerd
        }


    }
}
