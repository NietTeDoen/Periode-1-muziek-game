using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Muziek_Game
{
    public partial class GameControl : UserControl
    {
        private List<Block> blocks; // Lijst om alle blokken op te slaan
        private PortalManager portalManager; // Beheerder voor portalen
        private CharacterManager characterManager; // Beheerder voor het karakter
        private int[] portalPositie = { 1000, 200 }; // Startpositie voor het portaal
        private int[] characterPositie = { 100, 200 }; // Startpositie voor het karakter
        private Stopwatch stopwatch; // Voor delta timing
        private long previousTime; // Tijd van de vorige frame
        private List<Level> levels; // Lijst van niveaus
        private bool isPaused = false; // Houdt bij of het spel gepauzeerd is

        public GameControl()
        {
            InitializeComponent();

            // Start de stopwatch voor timing
            stopwatch = new Stopwatch();
            stopwatch.Start();

            CompositionTarget.Rendering += GameLoop; // Registreer de game loop

            // Initialiseer de niveaus
            levels = new List<Level>
            {
                new Level(120, 1, 500, new int[] { 1, 0, 1, 2, 3, 1, 0, 0, 2, 1, 3, 1 }), // Niveau 1
                new Level(130, 2, 400, new int[] { 0, 1, 1, 0, 1 }), // Niveau 2
                new Level(140, 3, 300, new int[] { 1, 1, 0, 2, 2 }), // Niveau 3
            };

            portalManager = new PortalManager(); // Initialiseer de portal manager
            characterManager = new CharacterManager(); // Initialiseer de character manager

            StartGame(); // Start het spel
        }

        public void LoadLevel(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= levels.Count) return;

            Level currentLevel = levels[levelIndex];
            blocks = new List<Block>(); // Reset de blokkenlijst voor het nieuwe niveau

            int upperRowY = 150; // Y-positie voor de bovenste rij
            int lowerRowY = 350; // Y-positie voor de onderste rij

            // Spawn blokken op basis van de array van het huidige niveau
            for (int i = 0; i < currentLevel.BlockRows.Length; i++)
            {
                int startX = 800 + i * 100; // Verschuif elk blok naar rechts

                switch (currentLevel.BlockRows[i])
                {
                    case 0:
                        break; // Geen blokken
                    case 1:
                        SpawnBlock(upperRowY, startX, true); // Bovenste rij
                        break;
                    case 2:
                        SpawnBlock(lowerRowY, startX, false); // Onderste rij
                        break;
                    case 3:
                        SpawnBlock(upperRowY, startX, true); // Bovenste rij
                        SpawnBlock(lowerRowY, startX, false); // Onderste rij
                        break;
                }
            }

            // Initialiseer het portaal en het karakter
            portalManager.InitializePortal(GameCanvas, portalPositie);
            characterManager.InitializeCharacter(GameCanvas, characterPositie);
        }

        public void StartGame()
        {
            LoadLevel(0); // Laad het eerste niveau
        }

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
        }

        public void SpawnBlock(int startY, int startX, bool firstRow)
        {
            Block block = new Block(GameCanvas, startY, startX, firstRow, 100); // Snelheid = 100
            blocks.Add(block);
        }

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

        // Pauze functie
        public void PauseGame()
        {
            isPaused = true; // Set de pauzestatus
        }

        // Hervat het spel
        public void ResumeGame()
        {
            isPaused = false; // Set de pauzestatus terug naar false
            previousTime = stopwatch.ElapsedMilliseconds; // Reset de tijd om te voorkomen dat de blokken teleporteren
        }

        // Pauze-knop
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            PauseGame();
            PauseMenu.Visibility = Visibility.Visible; // Toon het pauze menu
        }

        // Hervat-knop
        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            ResumeGame();
            PauseMenu.Visibility = Visibility.Collapsed; // Verberg het pauze menu
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this) as MainWindow; // Zorg ervoor dat je een referentie hebt naar MainWindow
            if (window != null)
            {
                window.MainContent.Content = new MainMenu(); // MainMenu moet een UserControl zijn
            }
        }
    }
}
