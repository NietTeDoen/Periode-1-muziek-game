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

            // Haal de Mods-lijst uit het MainWindow
            var window = Application.Current.MainWindow as MainWindow;
            if (window != null)
            {
                Mods = window.Mods; // Gebruik de Mods-lijst van MainWindow
                ModsList.ItemsSource = Mods; // Verbind de lijst met de UI
            }
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
