using System.Windows.Media;
using System;

public class AudioManager
{
    private static MediaPlayer _mediaPlayer;

    // Zorg ervoor dat er slechts één MediaPlayer is
    public static MediaPlayer GetMediaPlayer()
    {
        _mediaPlayer = new MediaPlayer();

        // Gebruik een reguliere bestands-URI
        string musicPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "muziek", "ABBA.mp3");
        Uri musicUri = new Uri(musicPath);

        _mediaPlayer.Volume = 0.5; // Maximaal volume
        _mediaPlayer.Open(musicUri);
        _mediaPlayer.Play();

        return _mediaPlayer;
    }

    public int VolumeChange(double volume)
    {
        _mediaPlayer.Volume = volume;
        return 1;
    }
}
