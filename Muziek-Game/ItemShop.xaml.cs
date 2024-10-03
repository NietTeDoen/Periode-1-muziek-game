using System.Windows;
using System.Windows.Controls;

namespace Muziek_Game
{
    public partial class ItemShop : UserControl
    {
        public ItemShop()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Ga terug naar het hoofdmenu
            var window = Window.GetWindow(this) as MainWindow;
            if (window != null)
            {
                window.MainContent.Content = new MainMenu();
            }
        }
    }
}
