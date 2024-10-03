using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Muziek_Game
{
    public partial class ProfilePage : UserControl
    {
        public ProfilePage()
        {
            InitializeComponent();
            LoadCosmetics();
        }

        private void LoadCosmetics()
        {
            // Voorbeeld cosmetica items
            CosmeticItemsControl.Items.Add(new CosmeticItem { Name = "Deadpool", ImageSource = "path_to_image_1.png" });
            CosmeticItemsControl.Items.Add(new CosmeticItem { Name = "Spider-Man", ImageSource = "path_to_image_2.png" });
            CosmeticItemsControl.Items.Add(new CosmeticItem { Name = "Iron Man", ImageSource = "path_to_image_3.png" });
            // Voeg meer items toe als nodig
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Terug naar het hoofdmenu of de vorige pagina
            var window = Window.GetWindow(this) as MainWindow;
            if (window != null)
            {
                window.MainContent.Content = new MainMenu(); // Vervang met het juiste UserControl
            }
        }

        private void Item_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Logica om de cosmetic item te equipen/unequipen
            if (sender is Border border)
            {
                // Toggle equip status (bijvoorbeeld door kleur of border te veranderen)
                border.BorderBrush = (border.BorderBrush == Brushes.Gray) ? Brushes.Blue : Brushes.Gray; // Toggle borderkleur
            }
        }
    }

    public class CosmeticItem
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
    }
}
