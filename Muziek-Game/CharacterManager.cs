using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Muziek_Game
{
    public class CharacterManager
    {
        private Rectangle character;
        private DispatcherTimer animationTimer;
        private string[] animationFrames;
        private int currentFrame = 0;

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

                // Laad animatieframes vanuit de "Character Animation" map
                string characterFolderPath = "pack://application:,,,/Assets/Character%20Animation/";
                animationFrames = LoadAnimationFrames(characterFolderPath);

                // Stel de eerste afbeelding in
                if (animationFrames.Length > 0)
                {
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri(animationFrames[0], UriKind.Absolute));
                    character.Fill = imageBrush;
                }

                // Positioneer het karakter op het canvas
                Canvas.SetLeft(character, characterPositie[0]);
                Canvas.SetTop(character, characterPositie[1]);

                // Voeg het karakter toe aan het canvas
                gameCanvas.Children.Add(character);

                // Start de animatie met een timer
                StartAnimation();

                return 1;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return 0;
            }
        }

        private string[] LoadAnimationFrames(string folderPath)
        {
            try
            {
                // Handmatig de juiste URIs bouwen voor elke afbeelding
                string[] frames = {
                    folderPath + "color1.png",
                    folderPath + "color2.png",
                    folderPath + "color3.png",
                    folderPath + "color4.png",
                    folderPath + "color5.png"
                };
                return frames;
            }
            catch (Exception error)
            {
                Console.WriteLine("Fout bij het laden van animatieframes: " + error.Message);
                return new string[0];
            }
        }

        private void StartAnimation()
        {
            animationTimer = new DispatcherTimer();
            animationTimer.Interval = TimeSpan.FromMilliseconds(100); // 100 ms per frame
            animationTimer.Tick += UpdateAnimationFrame;
            animationTimer.Start();
        }

        private void UpdateAnimationFrame(object sender, EventArgs e)
        {
            if (animationFrames.Length == 0)
                return;

            // Update het frame van de animatie
            currentFrame = (currentFrame + 1) % animationFrames.Length;
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri(animationFrames[currentFrame], UriKind.Absolute));
            character.Fill = imageBrush;
        }
        public void MoveTop()
        {
            //double newY = Canvas.GetLeft(BlockObj) - Speed * deltaTime; // Verplaats de blokken naar links
            Canvas.SetTop(character, 300);
        }
        public void MoveBottom()
        {
            //double newX = Canvas.GetLeft(BlockObj) - Speed * deltaTime; // Verplaats de blokken naar links
            Canvas.SetTop(character, 400);
        }
    }
}
