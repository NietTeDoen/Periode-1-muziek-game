using Muziek_Game;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Muziek_Game
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<ModItem> Mods { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // Mods-lijst verplaatsen naar het MainWindow zodat de status behouden blijft
            Mods = new ObservableCollection<ModItem>
            {
                new ModItem { Name = "Easy", IsActive = false },
                new ModItem { Name = "No Fail", IsActive = false },
                new ModItem { Name = "Hard", IsActive = false },
                new ModItem { Name = "Hidden", IsActive = false }
            };

            MainContent.Content = new MainMenu(); // Start met het hoofdmenu
        }
    }
}
