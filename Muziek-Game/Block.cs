using System.Windows.Controls;
using System.Windows.Shapes;

internal class Block
{
    public Rectangle BlockObj { get; private set; }
    private double Speed;
    private Canvas GameCanvas;


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
}
