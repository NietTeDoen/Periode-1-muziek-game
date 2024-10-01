using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Muziek_Game
{
    public partial class ModsMenu : UserControl
    {
        public ObservableCollection<ModItem> Mods { get; set; }

        public ModsMenu()
        {
            InitializeComponent();
            // Initialiseer de Mods-lijst met IsActive standaard op false
            Mods = new ObservableCollection<ModItem>
            {
                new ModItem { Name = "Easy", IsActive = false },   // Standaard op uit
                new ModItem { Name = "No Fail", IsActive = false }, // Standaard op uit
                new ModItem { Name = "Hard", IsActive = false },    // Standaard op uit
                new ModItem { Name = "Hidden", IsActive = false }   // Standaard op uit
            };
            ModsList.ItemsSource = Mods;
        }

        private void ToggleMod_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ModItem modItem)
            {
                modItem.IsActive = !modItem.IsActive; // Toggle de status
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this) as MainWindow;
            if (window != null)
            {
                window.MainContent.Content = new MainMenu(); // Navigeer terug naar het hoofdmenu
            }
        }
    }

    public class ModItem : INotifyPropertyChanged
    {
        private bool isActive;

        public string Name { get; set; }

        public bool IsActive
        {
            get => isActive;
            set
            {
                if (isActive != value)
                {
                    isActive = value;
                    OnPropertyChanged(nameof(IsActive));
                    OnPropertyChanged(nameof(StatusText));
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }
        }

        // Tekst voor de knop
        public string StatusText => IsActive ? "Aan" : "Uit";
        // Kleur afhankelijk van status
        public Brush BackgroundColor => IsActive ? Brushes.Green : Brushes.Red;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
