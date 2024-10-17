using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.CodeDom;

internal class Block
{
    public Rectangle BlockObj { get; private set; }
    private double Speed;
    private Canvas GameCanvas;
    public int hit = 0;


    /// <summary>
    /// Constructor voor het blok
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="starty"></param>
    /// <param name="startx"></param>
    /// <param name="firstrow"></param>
    /// <param name="speed"></param>
    public Block(Canvas canvas, int starty, int startx, bool firstrow, double bpm, int sprite)
    {
        int width = 50;
        int height = 50;

        GameCanvas = canvas;
        Speed = bpm;

        BlockObj = new Rectangle()
        {
            Width = width,
            Height = height,
            Fill = System.Windows.Media.Brushes.Red
        };

        //Selecteer een willekeurig nummer tussen 1 en 3
        Random random = new Random();
        int randomNumber = random.Next(1, 4); // Voeg het nummer toe aan de randomNumber Int

        //Voeg afbeeldingen toe aan de blokken en selecteer de afbeelding afhankelijk van randomNumber.
        ImageBrush imageBrush = new ImageBrush();
        switch (sprite)
        {
            case 0:
                imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Note1.png", UriKind.Absolute));
                break;
            case 1:
                imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Note2.png", UriKind.Absolute));
                break;
            case 2:
                imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Note3.png", UriKind.Absolute));
                break;
            default:
                imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Note1.png", UriKind.Absolute));
                break;
        }
        BlockObj.Fill = imageBrush;

        Canvas.SetLeft(BlockObj, startx);
        Canvas.SetTop(BlockObj, firstrow ? starty : starty - 50);

        GameCanvas.Children.Add(BlockObj);
    }

    /// <summary>
    /// Functie om blokken te verplaatsen
    /// </summary>
    /// <param name="deltaTime"></param>
    // Definieer de BPM en de afstand per beat
    private double bpm; //bpm
    private double distancePerBeat = 100; // Afstand die het blok per beat beweegt

    public void MoveLeft(double deltaTime)
    {
        bpm = Speed;
        // Bereken de tijd per beat in seconden
        double secondsPerBeat = 60.0 / bpm;

        // Bereken de snelheid in pixels per seconde
        double speed = distancePerBeat / secondsPerBeat; // pixels per seconde

        // Verplaats de blokken naar links
        double newX = Canvas.GetLeft(BlockObj) - speed * deltaTime;
        Canvas.SetLeft(BlockObj, newX);
    }
    public void Delete()
    {
        GameCanvas.Children.Remove(BlockObj);
    }
    public void IsHit()
    {
        hit = 1;
    }
}
