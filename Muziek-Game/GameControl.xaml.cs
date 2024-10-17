﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Muziek_Game
{
    public partial class GameControl : UserControl
    {
        private List<Block> blocks; // Lijst om alle blokken op te slaan
        private PortalManager portalManager; // Beheerder voor portalen
        private CharacterManager characterManager; // Beheerder voor het karakter
        private int[] portalPositie = { 1000, 300 }; // Startpositie voor het portaal
        private int[] characterPositie = { 100, 300 }; // Startpositie voor het karakter
        private Stopwatch stopwatch; // Voor delta timing
        private long previousTime; // Tijd van de vorige frame
        private List<Level> levels; // Lijst van niveaus
        private bool isPaused = false; // Houdt bij of het spel gepauzeerd is
        private int HitCount = 0;
        private int score = 0;
        public Label ScoreLabel;
        private static MediaPlayer _mediaPlayer;
        public GameControl(int level)
        {
            InitializeComponent();

            //Maak een MediaPlayer aan voor de level
            if (_mediaPlayer == null)
            {
                _mediaPlayer = new MediaPlayer();
            }

            // Gebruik een reguliere bestands-URI en vul het level nummer in als pad
            string musicPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "muziek", level + ".mp3");

            Uri musicUri = new Uri(musicPath);
            _mediaPlayer.Volume = 0.5; // Maximaal volume
            _mediaPlayer.Open(musicUri);
            _mediaPlayer.Play();


            // Start de stopwatch voor timing
            stopwatch = new Stopwatch();
            stopwatch.Start();

            CompositionTarget.Rendering += GameLoop; // Registreer de game loop

            // Initialiseer de niveaus
            // 0 is geen blokken
            // 1 is een blok in de bovenste rij
            // 2 is een blok in de onderste rij
            // 3 is een blok in zowel de bovenste als onderste rij
            // het eerste cijfer bepaald de snelheid van het level
            levels = new List<Level>
            {
                new Level(120, 1, 500, new int[]
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 1,
                    0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 1, 0, 2, 0, 1, 0, 3, 0, 2, 0, 3, 0, 1, 0, 2, 0, 1,
                    0, 3, 0, 2, 0, 1, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 1, 0, 1, 0, 2,
                    0, 3, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 1, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 3, 0, 1,
                    0, 2, 0, 1, 0, 2, 0, 1, 0, 3, 0, 2, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 2, 0, 1, 0
                }), // Niveau 1 - Running up that hill
                new Level(117, 2, 400, new int[]
                {
                    1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0,
                    2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0,
                    1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0,
                    3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0,
                    1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0,
                    2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0,
                    1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0,
                    3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0,
                    1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0,
                    2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0,
                    1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0,
                    3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0,
                    1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0,
                    2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0,
                    1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 1, 0,
                    3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0
                }), // Niveau 2 - Billie Jean
                new Level(166, 3, 300, new int[]
                {
                    0, 0, 0, 0, 0, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 1, 0, 3, 0, 2, 0, 3, 0, 1, 0, 2, 0, 3, 0, 1, 0, 1, 0,
                    2, 0, 1, 0, 1, 0, 3, 0, 2, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 3, 0, 1, 0,
                    2, 0, 1, 0, 2, 0, 3, 0, 1, 0, 3, 0, 2, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 1, 0, 1, 0, 3, 0, 2, 0, 3, 0,
                    2, 0, 1, 0, 1, 0, 2, 0, 3, 0, 1, 0, 3, 0, 1, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 2, 0,
                    1, 0, 3, 0, 1, 0, 1, 0, 2, 0, 3, 0, 1, 0, 1, 0, 3, 0, 2, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 3, 0, 1, 0,
                    2, 0, 3, 0, 1, 0, 2, 0, 1, 0, 2, 0, 3, 0, 1, 0, 2, 0, 3, 0, 1, 0, 1, 0, 2, 0, 3, 0, 3, 0, 2, 0, 1, 0,
                    2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 2, 0, 1, 0, 3, 0, 1, 0, 1, 0, 2, 0, 3, 0, 1, 0, 3, 0, 2, 0, 1, 0, 2, 0,
                    1, 0, 3, 0, 2, 0, 1, 0, 2, 0, 1, 0, 3, 0, 1, 0, 2, 0, 3, 0, 1, 0, 3, 0, 1, 0, 2, 0, 3, 0, 1, 0, 3, 0,
                    2, 0, 1, 0, 2, 0, 1, 0, 3, 0, 2, 0, 1, 0, 2, 0, 1, 0, 3, 0, 2, 0
                }), // Niveau 3 - Godzilla
            };

            portalManager = new PortalManager(); // Initialiseer de portal manager
            characterManager = new CharacterManager(); // Initialiseer de character manager

            StartGame(level - 1); // Start het spel. Int is de level die gekozen word.
    }

        //}
        /// <summary>
        /// Zorgt er voor dat de ProcessInput de windows meekrijgt als variabele zodat het buiten de gamecontrol bruikbaar is.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ProcessInput_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += ProcessInput;
        }

        /// <summary>
        /// Zorgt ervoor dat het character omhoog en omlaag kan bewegen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessInput(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                characterManager.MoveTop();
            }
            else if (e.Key == Key.Down)
            {
                characterManager.MoveBottom();
            }
            else if (e.Key == Key.Escape)
            {
                if (isPaused)
                {
                    ResumeGame();
                }else{
                    PauseGame();
                }
            }
        }

        /// <summary>
        /// Spawned alle blokken die passen bij het level
        /// </summary>
        /// <param name="levelIndex"></param>
        public void LoadLevel(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= levels.Count) return;

            Level currentLevel = levels[levelIndex];
            blocks = new List<Block>(); // Reset de blokkenlijst voor het nieuwe niveau

            int upperRowY = 350; // Y-positie voor de bovenste rij
            int lowerRowY = 500; // Y-positie voor de onderste rij

            // Spawn blokken op basis van de array van het huidige niveau
            for (int i = 0; i < currentLevel.BlockRows.Length; i++)
            {
                int startX = 800 + i * 100; // Verschuif elk blok naar rechts

                switch (currentLevel.BlockRows[i])
                {
                    case 0:
                        break; // Geen blokken
                    case 1:
                        SpawnBlock(upperRowY, startX, true, levelIndex, 0); // Bovenste rij
                        break;
                    case 2:
                        SpawnBlock(lowerRowY, startX, false, levelIndex, 1); // Onderste rij
                        break;
                    case 3:
                        SpawnBlock(upperRowY, startX, true, levelIndex, 2); // Bovenste rij
                        SpawnBlock(lowerRowY, startX, false, levelIndex, 2); // Onderste rij
                        break;
                }
            }

            // Initialiseer het portaal en het karakter
            portalManager.InitializePortal(GameCanvas, portalPositie);
            characterManager.InitializeCharacter(GameCanvas, characterPositie);
        }
        
        /// <summary>
        /// Laad het level in
        /// </summary>
        /// <param name="level"></param>
        public void StartGame(int level)
        {
            LoadLevel(level); // Laad het eerste niveau
        }

        /// <summary>
        /// De GameLoop zorgt voor de timing van het spel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameLoop(object sender, EventArgs e)
        {
            if (isPaused)
            {
                return; // Stop de game loop als het spel gepauzeerd is
            }

            // Bereken de delta time sinds de laatste frame
            long currentTime = stopwatch.ElapsedMilliseconds;
            double deltaTime = (currentTime - previousTime) / 1000.0; // Omzetten naar seconden
            previousTime = currentTime;

            UpdateBlocks(deltaTime); // Update de blokken

            HitDetection(); // Controleer of er een blok geraakt is
            MissDetection(); // Controleer of er een blok gemist is
            GameCanvas.Focus(); // Reset de focus om besturing te houden
        }

        public void SpawnBlock(int startY, int startX, bool firstRow, int levelIndex, int sprite)
        {
            Level currentLevel = levels[levelIndex];
            Block block = new Block(GameCanvas, startY, startX, firstRow, currentLevel.Tempo, sprite); // Snelheid = 100
            blocks.Add(block);
        }

        /// <summary>
        /// Update de locatie van de blokken
        /// </summary>
        /// <param name="deltaTime"></param>
        public void UpdateBlocks(double deltaTime)
        {
            for (int i = blocks.Count - 1; i >= 0; i--) // Achterwaarts itereren om veilig te verwijderen
            {
                var block = blocks[i];
                block.MoveLeft(deltaTime); // Verplaats blok
                if (Canvas.GetLeft(block.BlockObj) < -50) // Verwijder blokken die buiten het canvas zijn
                {
                    blocks.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Checkt of weaponHitbox overlapt met BlockObj en maakt het block oranje als test
        /// </summary>
        public void HitDetection()
        {
            Rect hitboxWeapon = new Rect(Canvas.GetLeft(characterManager.weaponHitbox), Canvas.GetTop(characterManager.weaponHitbox), 50, 50); // Maak een Rect aan op de plek van de Rectangle zodat het te vergelijken is
            foreach (var block in blocks)
            {
                Rect hitboxBlock = new Rect(Canvas.GetLeft(block.BlockObj), Canvas.GetTop(block.BlockObj), block.BlockObj.ActualHeight , block.BlockObj.ActualWidth); //Maak een Rect aan op de plek van de Rectangle zodat het te vergelijken is
                if (hitboxWeapon.IntersectsWith(hitboxBlock)) //Checkt bij elk blok of het kruist met de hitbox van het wapen
                {
                    if (block.hit == 0)
                    {
                        block.BlockObj.Fill = System.Windows.Media.Brushes.Green;
                        //block.Delete();
                        block.IsHit();
                        HitDetected(true);
                    }
                }
            }
        }
        /// <summary>
        /// Checkt of BlockObj overlapt met een onzichtbare hitbox achter het karakter om een miss te herkennen.
        /// </summary>
        public void MissDetection()
        {
            Rect hitboxMiss = new Rect(200, 0, 10, 5000); //Maak een onzichtbare Rect achter het karakter
            foreach (var block in blocks)
            {
                Rect hitboxBlock = new Rect(Canvas.GetLeft(block.BlockObj), Canvas.GetTop(block.BlockObj), block.BlockObj.ActualHeight, block.BlockObj.ActualWidth); //Maak een Rect aan op de plek van de Rectangle zodat het te vergelijken is
                if (hitboxMiss.IntersectsWith(hitboxBlock)) //Checkt bij elk blok of het kruist met de hitbox van het "miss" punt
                {
                    if (block.hit == 0)
                    {
                        block.BlockObj.Fill = System.Windows.Media.Brushes.Red;
                        MissDetected(true);
                    }
                }
            }
        }
        public void HitDetected(bool hit)
        {   if (hit == true)
            {
                HitCount++;
                score = Score(HitCount);
                DisplayScore();
            }
        }
        public void MissDetected(bool miss)
        {
            if (miss == true)
            {
                // Er is een miss gedetecteerd. Voer deze code uit.
            }
        }

        /// <summary>
        /// Berekent score
        /// </summary>
        /// <param name="Hitcount"></param>
        /// <returns></returns>
        public int Score(int Hitcount)
        {
            int score = Hitcount * 1; 
            Console.WriteLine("score: " + score); 
            return score;
        }

        private void DisplayScore()
        {
            scoreLabel.Content = "Score: " + score.ToString();

            if (score >= 1000)
            {
                scoreLabel.Foreground = new SolidColorBrush(Colors.Green); // Groen als de score 100 of hoger is
            }
            else if (score >= 100)
            {
                scoreLabel.Foreground = new SolidColorBrush(Colors.Orange); // Oranje bij een score tussen 50 en 100
            }
            else
            {
                scoreLabel.Foreground = new SolidColorBrush(Colors.Red); // Rood als de score lager dan 50 is
            }
        }
        
        // Pauze functie
        public void PauseGame()
        {
            isPaused = true; // Set de pauzestatus
            PauseMenu.Visibility = Visibility.Visible; // Toon het pauze menu
        }

        // Hervat het spel
        public void ResumeGame()
        {
            isPaused = false; // Set de pauzestatus terug naar false
            previousTime = stopwatch.ElapsedMilliseconds; // Reset de tijd om te voorkomen dat de blokken teleporteren
            PauseMenu.Visibility = Visibility.Collapsed; // Verberg het pauze menu
        }

        // Pauze-knop
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            PauseGame();
        }

        // Hervat-knop
        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            ResumeGame();
        }


        private void LevelButton_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this) as MainWindow; // Zorg ervoor dat je een referentie hebt naar MainWindow
            if (window != null)
            {
                window.MainContent.Content = new LevelSelector(); // MainMenu moet een UserControl zijn
            }
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this) as MainWindow; // Zorg ervoor dat je een referentie hebt naar MainWindow
            if (window != null)
            {
                window.MainContent.Content = new MainMenu(); // MainMenu moet een UserControl zijn
            }
            _mediaPlayer.Stop();
        }
    }
}
