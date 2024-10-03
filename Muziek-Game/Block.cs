using System.Windows.Controls;
using System.Windows.Shapes;

internal class Block
{
    public Rectangle BlockObj { get; private set; }
    private double Speed;
    private Canvas GameCanvas;

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

    public void MoveLeft(double deltaTime)
    {
        double newX = Canvas.GetLeft(BlockObj) - Speed * deltaTime; // Verplaats de blokken naar links
        Canvas.SetLeft(BlockObj, newX);
    }
}
