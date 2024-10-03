using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Muziek_Game
{
    public class PortalManager
    {
        private Rectangle portal;

        public int InitializePortal(Canvas gameCanvas, int[] portalPositie)
        {
            try
            {
                // Maak het portaalobject
                portal = new Rectangle
                {
                    Width = 250,
                    Height = 300,
                    Stroke = Brushes.Black,
                    StrokeThickness = 4
                };

                // Stel de afbeelding in voor het portaal
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/NetherPortal.png", UriKind.Absolute));
                portal.Fill = imageBrush;

                // Positioneer het portaal op het canvas
                Canvas.SetLeft(portal, portalPositie[0]);
                Canvas.SetTop(portal, portalPositie[1]);

                // Voeg het portaal toe aan het canvas
                gameCanvas.Children.Add(portal);

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
