using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Muziek_Game
{
    public partial class ItemShop : UserControl
    {
        public ObservableCollection<Item> Items { get; set; }

        public ItemShop()
        {
            InitializeComponent();
            LoadItems();
            DataContext = this;
        }

        private void LoadItems()
        {
            Items = new ObservableCollection<Item>
            {
                new Item { Name = "Outfit 1", Price = "1500 V-Bucks", Image = "path_to_image" },
                new Item { Name = "Pickaxe 1", Price = "800 V-Bucks", Image = "path_to_image" },
                new Item { Name = "Glider 1", Price = "1200 V-Bucks", Image = "path_to_image" }
            };

            ItemList.ItemsSource = Items;
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public ICommand BuyCommand { get; set; }

        public Item()
        {
            BuyCommand = new RelayCommand(BuyItem);
        }

        private void BuyItem(object parameter)
        {
            // Voeg hier de kooplogica toe
            MessageBox.Show($"You bought {Name} for {Price}!");
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;

        public RelayCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
