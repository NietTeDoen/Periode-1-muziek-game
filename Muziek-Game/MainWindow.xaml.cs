using System.Windows;
//using System.Windows.Controls;
using System.Windows.Input;
//using System.Windows.Shapes;

namespace Muziek_Game
{
    public partial class MainWindow : Window
    {
        //private CharacterManager characterManager = new CharacterManager();

        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new MainMenu(); // Start met het hoofdmenu
            //characterManager = new CharacterManager();
        }


        //public void StartGame()
        //{
        //    MainContent.Content = new GameControl(); // Vervang het menu door de game
        //    characterManager = new CharacterManager();
        //}
        //private void ProcessInput(object sender, KeyEventArgs e, Canvas canvas )
        //{
        //    characterManager.CharacterSend(sender,e, canvas);
        //    //if (e.Key == Key.Up)
        //    //{
        //    //    characterManager.MoveTop();
        //    //}
        //    //else if (e.Key == Key.Down)
        //    //{
        //    //    characterManager.MoveBottom();
        //    //}
        //}
        //private void ProcessInput_Loaded(object sender, KeyEventArgs e, Canvas canvas )
        //{
        //    characterManager.CharacterSend(sender, e, canvas);
        //}
    }
}
