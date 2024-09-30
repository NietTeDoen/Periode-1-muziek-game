using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Muziek_Game
{
    public class CharacterManager
    {
        private Rectangle character;

        public int InitializeCharacter(Canvas gameCanvas, int[] characterPositie)
        {
            try
            {
                // Maak het karakterobject
                character = new Rectangle
                {
                    Width = 250,
                    Height = 300,
                };

                // Stel de afbeelding in voor het karakter
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Character.png", UriKind.Absolute));
                character.Fill = imageBrush;

                // Positioneer het karakter op het canvas
                Canvas.SetLeft(character, characterPositie[0]);
                Canvas.SetTop(character, characterPositie[1]);

                // Voeg het karakter toe aan het canvas
                gameCanvas.Children.Add(character);

                return 1;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return 0;
            }
        }
    }
}
