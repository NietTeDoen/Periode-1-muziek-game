using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Muziek_Game
{
    public partial class MainWindow : Window
    {
        private List<Block> blocks;
        private Canvas gameCanvas;
        private PortalManager portalManager;
        private CharacterManager characterManager;
        private int[] portalPositie = { 1000, 200 };
        private int[] characterPositie = { 100, 200 };
        private Stopwatch stopwatch; // Voor delta timing
        private long previousTime;
        private List<Level> levels;

        public MainWindow()
        {
            InitializeComponent();

            stopwatch = new Stopwatch();
            stopwatch.Start(); // Start de stopwatch

            CompositionTarget.Rendering += GameLoop; // Gebruik Rendering voor de game loop

            levels = new List<Level>
            {
                // Voeg niveaus toe met verschillende moeilijkheidsgraden en blokken
                // De array van blokken bepaalt welke rijen blokken spawnen
                // 0 = geen blokken, 1 = bovenste rij, 2 = onderste rij, 3 = beide rijen

                new Level(120, 1, 500, new int[] { 1, 0, 1, 2, 3, 1, 0, 0, 2, 1, 3, 1 }), // Niveau 1
                new Level(130, 2, 400, new int[] { 0, 1, 1, 0, 1 }), // Niveau 2
                new Level(140, 3, 300, new int[] { 1, 1, 0, 2, 2 }), // Niveau 3
                // Voeg meer niveaus toe zoals nodig
            };

            gameCanvas = GameCanvas; // Verbind de gameCanvas van XAML
            portalManager = new PortalManager(); // Initialiseer de portal manager
            characterManager = new CharacterManager(); //Initialiseer de character manager

            StartGame(); // Start het spel
        }

        public void LoadLevel(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= levels.Count) return;

            Level currentLevel = levels[levelIndex];
            blocks = new List<Block>(); // Reset de blokkenlijst voor het nieuwe niveau

            int upperRowY = 150; // Y-positie voor de bovenste rij
            int lowerRowY = 350; // Y-positie voor de onderste rij, met meer afstand

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
            portalManager.InitializePortal(gameCanvas, portalPositie);
            characterManager.InitializeCharacter(gameCanvas, characterPositie);
        }

        public void StartGame()
        {
            LoadLevel(0); // Laad het eerste niveau
        }

        private void GameLoop(object sender, EventArgs e)
        {
            // Bereken de delta time sinds de laatste frame
            long currentTime = stopwatch.ElapsedMilliseconds;
            double deltaTime = (currentTime - previousTime) / 1000.0; // Omzetten naar seconden
            previousTime = currentTime;

            UpdateBlocks(deltaTime); // Update de blokken
        }

        public void SpawnBlock(int startY, int startX, bool firstRow)
        {
            Block block = new Block(gameCanvas, startY, startX, firstRow, 100); // Snelheid = 100
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
    }
}
