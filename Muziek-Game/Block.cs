using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;

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
    public Block(Canvas canvas, int starty, int startx, bool firstrow, double speed)
    {
        int width = 50;
        int height = 50;

        GameCanvas = canvas;
        Speed = speed;

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
        imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Note"+ randomNumber +".png", UriKind.Absolute));
        BlockObj.Fill = imageBrush;

        Canvas.SetLeft(BlockObj, startx);
        Canvas.SetTop(BlockObj, firstrow ? starty : starty - 50);

        GameCanvas.Children.Add(BlockObj);
    }

    /// <summary>
    /// Functie om blokken te verplaatsen
    /// </summary>
    /// <param name="deltaTime"></param>
    public void MoveLeft(double deltaTime)
    {
            double newX = Canvas.GetLeft(BlockObj) - Speed * deltaTime; // Verplaats de blokken naar links
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
