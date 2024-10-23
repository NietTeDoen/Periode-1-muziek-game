using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Muziek_Game
{
    public class CharacterManager
    {
        public Rectangle character { get; private set; }
        public Rectangle weaponHitbox { get; private set; }
        public Rectangle weapon {  get; private set; }
        private DispatcherTimer animationTimer;
        private string[] animationFrames;
        private int currentFrame = 0;


        /// <summary>
        /// Maak Character aan en voeg toe aan het canvas
        /// </summary>
        /// <param name="gameCanvas"></param>
        /// <param name="characterPositie"></param>
        /// <returns></returns>
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
                //Maak de hitbox van het wapen aan
                weaponHitbox = new Rectangle
                {
                    Width = 50,
                    Height = 50,
                    //Fill = System.Windows.Media.Brushes.Blue
                };
                //Maak het vierkant van het wapen aan
                weapon = new Rectangle
                {
                    Width= 100,
                    Height = 150,
                };
                //Stop het wapen plaatje in het wapen vierkant
                ImageBrush weaponBrush = new ImageBrush();
                weaponBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Weapon.png", UriKind.Absolute));
                weapon.Fill = weaponBrush;

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
                Canvas.SetLeft(weaponHitbox, characterPositie[0] + 250);
                Canvas.SetTop(weaponHitbox, characterPositie[1] + 50);
                Canvas.SetLeft(weapon, characterPositie[0] + 200);
                Canvas.SetTop(weapon, characterPositie[1] + 75);

                // Voeg het karakter toe aan het canvas
                gameCanvas.Children.Add(character);
                gameCanvas.Children.Add(weaponHitbox);
                gameCanvas.Children.Add(weapon);
                weapon.RenderTransform = new RotateTransform(-20, 25, 130);

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

        /// <summary>
        /// Laden van Animaties en Definen van frames
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Start de Animatie
        /// </summary>
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
        public void AttackKeyDown()
        {
            weapon.RenderTransform = new RotateTransform(0, 25, 130);

        }
        public void AttackKeyUp()
        {
            weapon.RenderTransform = new RotateTransform(-20, 25, 130);
        }

        /// <summary>
        /// Beweeg het karakter naar boven
        /// </summary>
        public void MoveTop()
        {
            Canvas.SetTop(character, 300);
            Canvas.SetTop(weaponHitbox, 350);
            Canvas.SetTop(weapon, 375);
        }
        /// <summary>
        /// Beweeg het karakter naar beneden
        /// </summary>
        public void MoveBottom()
        {
            Canvas.SetTop(character, 400);
            Canvas.SetTop(weaponHitbox, 450);
            Canvas.SetTop(weapon, 475);
        }
    }
}
